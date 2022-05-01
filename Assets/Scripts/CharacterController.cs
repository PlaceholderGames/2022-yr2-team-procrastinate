using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    [SerializeField] gameController GameController;

    //High precision used for calculating health and other bits
    [SerializeField] decimal healthM;
    [SerializeField] decimal maxHealthM;
    [SerializeField] decimal healingSpeedM;

    //Low precision versions used for displaying in the inspector without using editor voodoo.
    [SerializeField] float healthF;
    [SerializeField] float maxHealthF;
    [SerializeField] float healingSpeedF;
    Slider healthBarSlider;

    [SerializeField] bool healed;

    //Audio
    [SerializeField] public AudioSource audioSource;
    [SerializeField] AudioClip fireGun = null;



    //Shooting
    [SerializeField] int aimDirection;
    [SerializeField] bool readyToFire;
    [SerializeField] int projectileSpeed;
    [SerializeField] Rigidbody2D bulletPrefab;
    [SerializeField] float damage;
    [SerializeField] float rateOfFire;


    //Money Related
    //Paycheck is money paid to the player at the end of each instance of the day levels
    [SerializeField] float paycheque;
    [SerializeField] float money;


    //PauseMenu
    //These scripts will be used to open the pause menu and then pause the game
    //The rest will be done in the pause menu script
    [SerializeField] public bool gamePaused;


    //MiniMap
    [SerializeField] Camera minimapCameraObject = null;
    Canvas minimapCanvasObject = null;
    [SerializeField] public bool miniMapOpen;


    //Enums

    //Delegates
    public delegate void EnemyDied(AIController.enemyType deadEnemyType);
    public static EnemyDied enemyDied;

    enum direction
    {
        UP,
        RIGHT,
        DOWN,
        LEFT
    }

    // Start is called before the first frame update
    void Start()
    {
        GameController = GameObject.Find("GameController").GetComponent<gameController>();

        healthM = 90.0M;
        maxHealthM = 100.0M;
        healingSpeedM = 0.1M;
        damage = 20.0f;
        rateOfFire = 30.0f;
        healed = false;
        aimDirection = 5;
        readyToFire = true;
        projectileSpeed = 30;

        healthBarSlider = GameObject.Find("HealthBarSlider").GetComponent<Slider>();

        enemyDied = addToPaycheque;

        minimapCameraObject = GameObject.Find("MiniMapCamera").GetComponent<Camera>();
        minimapCanvasObject = minimapCameraObject.transform.GetChild(0).GetComponent<Canvas>();

        gamePaused = false;
        miniMapOpen = true;

        //Adds an audio source then loads the audio clip
        //fireGun = Resources.Load("Audio/nameHere") as AudioClip;
        //audioSource = this.gameObject.GetComponent<AudioSource>();
        //audioSource.clip = fireGun;


    }


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    //void FixedUpdate()
    //{

    //}


    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level2")
        {
            GameController = GameObject.Find("GameController").GetComponent<gameController>();
        }
        //The player is dead
        if (healthM <= 0.0M)
        {
            GameController.playerDied();
        }
        if (healthM != maxHealthM && !healed)
        {
            healthM += healingSpeedM;
            healed = true;
            StartCoroutine(healCoolDown());
            //print("Health: " + healthM + "/100");
        }
        //Updates the health variable floats, this is for debugging in the editor
        updateHealthVariables();

        //Aim direction used for shooting
        if (Input.GetKeyDown(KeyCode.W))
        {
            aimDirection = (int)direction.UP;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            aimDirection = (int)direction.RIGHT;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            aimDirection = (int)direction.DOWN;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            aimDirection = (int)direction.LEFT;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            //Using the ternary ?; operator to compress the if else function into one statement/line
            miniMapOpen = miniMapOpen == true ? false : true;
            
            toggleMinimap();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("Shooting!");
            fire();
        }

        healthBarSlider.value = (float)healthM;



        //if (Input.GetKeyUp(KeyCode.W) && aimDirection == 0)
        //{
        //    aimDirection = 5;
        //}
        //if (Input.GetKeyUp(KeyCode.D) && aimDirection == 1)
        //{
        //    aimDirection = 5;
        //}
        //if (Input.GetKeyUp(KeyCode.S) && aimDirection == 2)
        //{
        //    aimDirection = 5;
        //}
        //if (Input.GetKeyUp(KeyCode.A) && aimDirection == 3)
        //{
        //    aimDirection = 5;
        //}
    }

    public void toggleMinimap()
    {
        //Makeing sure the game isn't paused before allowing the minimap to open
        if (minimapCameraObject.enabled)
        {
            minimapCameraObject.enabled = false;
            minimapCanvasObject.enabled = false;
        }
        else if (!gamePaused)
        {
            minimapCameraObject.enabled = true;
            minimapCanvasObject.enabled = true;
        }
    }


    void addToPaycheque(AIController.enemyType type)
    {
        switch(type)
        {
            case AIController.enemyType.Alcoholic:
                paycheque += 30;
                break;
            case AIController.enemyType.CokeHead:
                paycheque += 30;
                break;
            case AIController.enemyType.CrackHead:
                paycheque += 20;
                break;
            case AIController.enemyType.PotHead:
                paycheque += 40;
                break;
            case AIController.enemyType.SmackHead:
                paycheque += 60;
                break;
        }
    }

    public void payPlayer()
    {
        money += paycheque;
        paycheque = 0;
    }

    void fire()
    {
        if (readyToFire == true)
        {
            //audioSource.Play();

            readyToFire = false;

            //Spawning the bullet
            Rigidbody2D bullet = Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation) as Rigidbody2D;
            //Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), this.GetComponent<Collider2D>(), true);
            
            //Adding force to the bullet in a certain direction
            if (aimDirection == (int)direction.UP)
            {
                bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * projectileSpeed);
                bullet.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
            }
            else if (aimDirection == (int)direction.RIGHT)
            {
                bullet.GetComponent<Rigidbody2D>().AddForce(transform.right * projectileSpeed);
            }
            else if (aimDirection == (int)direction.DOWN)
            {
                bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * -projectileSpeed);
                bullet.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, -90f));
            }
            else if (aimDirection == (int)direction.LEFT)
            {
                bullet.GetComponent<Rigidbody2D>().AddForce(transform.right * -projectileSpeed);
                bullet.GetComponent<SpriteRenderer>().flipX = true;
            }


            StartCoroutine(weaponCooldown());
        }
    }


    IEnumerator healCoolDown()
    {
        yield return new WaitForSeconds(1);
        healed = false;
    }

    IEnumerator weaponCooldown()
    {
        yield return new WaitForSeconds(60/rateOfFire);
        readyToFire = true;
    }

    void updateHealthVariables()
    {
        healthF = (float)healthM;
        maxHealthF = (float)maxHealthM;
        healingSpeedF = (float)healingSpeedM;
    }

    public void damagePlayer(float damage)
    {
        healthM -= (decimal)damage;
    }

    public void healPlayer(float healing)
    {
        healthM += (decimal)healing;
        if (healthM > maxHealthM)
        {
            healthM = maxHealthM;
        }
    }

    public float getPlayerDamage()
    {
        return damage;
    }

    //Respawns the player by resetting their health and sending them back to spawn
    public void respawnPlayer(Vector3 location)
    {
        healthM = maxHealthM;
        this.transform.position = location; 
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "AIBullet")
        {
            damagePlayer(collision.gameObject.GetComponent<bulletController>().getDamage());
            print("Health: " + healthM + "/100");
        }
    }
}
