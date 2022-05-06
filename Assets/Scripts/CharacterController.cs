using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    [SerializeField] static GameControllerLevel2 GameControllerLevel2;
    [SerializeField] static GameControllerLevel1 GameControllerLevel1;

    [SerializeField] static BasicMovment movementController;

    //High precision used for calculating health and other bits
    [SerializeField] decimal healthM;
    [SerializeField] decimal maxHealthM;
    [SerializeField] decimal healingSpeedM;
    [SerializeField] bool unkillable;

    //Low precision versions used for displaying in the inspector without using editor voodoo.
    [SerializeField] float healthF;
    [SerializeField] float maxHealthF;
    [SerializeField] float healingSpeedF;
    Slider healthBarSlider;

    [SerializeField] bool healed;

    //Audio
    [SerializeField] public AudioSource audioSource;
    [SerializeField] AudioClip fireGun = null;
    [SerializeField] AudioClip healFood = null;



    //Shooting
    [SerializeField] int aimDirection;
    [SerializeField] bool readyToFire;
    [SerializeField] int projectileSpeed;
    [SerializeField] Rigidbody2D bulletPrefab;
    [SerializeField] float damage;
    [SerializeField] float rateOfFire;
    [SerializeField] bool canFire;


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


    //Movement
    [SerializeField] float potHeadDebuffSpeed;
    
    [SerializeField] float movementSpeed;//Default 2.0f

    //Status Effects
    StatusEffectBarController statusEffectBarController;
    //PotHead
    GameObject potHeadStatusEffectObject;
    [SerializeField] float potHeadDebuffTime;
    [SerializeField] float currentPotHeadDebuffTime;
    bool potHeadDebuffIconAdded;
    bool potHeadDebuffIconFlashing;

    //SmackHead
    GameObject smackHeadStatusEffectObject;
    [SerializeField] float smackHeadDebuffTime;
    [SerializeField] float currentSmackHeadDebuffTime;
    bool smackHeadDebuffIconAdded;
    bool smackHeadDebuffIconFlashing;
    [SerializeField] bool poisonCanDamage;
    [SerializeField] decimal poisonDamage;

    //energy drink
    GameObject energyDrinkStatusEffectObject;
    [SerializeField] float energyDrinkBuffTime;
    [SerializeField] float currentEnergyDrinkBuffTime;
    bool energyDrinkBuffIconAdded;
    bool energyDrinkBuffIconFlashing;
    float energyDrinkBuffedMovementSpeed;

    //Levels
    [SerializeField] public int level1Iteration;
    [SerializeField] public int level2Iteration;


    //Renderers
    //public LineRenderer targetLineRenderer;

    public Vector2 pointerDirection;
    GameObject objectivePointer;

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
        GameControllerLevel1 = GameObject.Find("GameControllerLevel1").GetComponent<GameControllerLevel1>();
        movementController = this.gameObject.GetComponent<BasicMovment>();
        statusEffectBarController = GameObject.Find("StatusEffects").GetComponent<StatusEffectBarController>();

        audioSource = this.gameObject.GetComponent<AudioSource>();

        healFood = Resources.Load("Audio/healing4", typeof(AudioClip)) as AudioClip;
        fireGun = Resources.Load("Audio/GunShot", typeof(AudioClip)) as AudioClip;

        objectivePointer = this.gameObject.transform.GetChild(3).gameObject;

        //targetLineRenderer = this.gameObject.GetComponent<LineRenderer>();

        level1Iteration = 0;
        level2Iteration = 0;

        healthM = 90.0M;
        maxHealthM = 100.0M;
        healingSpeedM = 0.1M;
        damage = 20.0f;
        rateOfFire = 60.0f;
        healed = false;
        aimDirection = 5;
        readyToFire = true;
        projectileSpeed = 30;
        canFire = false;

        potHeadDebuffSpeed = 0.75f;
        potHeadDebuffTime = 10.0f;
        currentPotHeadDebuffTime = 0.0f;
        movementSpeed = 2.0f;

        smackHeadDebuffTime = 20.0f;
        poisonCanDamage = false;
        poisonDamage = 1.0M;
        smackHeadDebuffIconFlashing = false;


        healthBarSlider = GameObject.Find("HealthBarSlider").GetComponent<Slider>();

        enemyDied = addToBloodMoney;

        minimapCameraObject = GameObject.Find("MiniMapCamera").GetComponent<Camera>();
        minimapCanvasObject = minimapCameraObject.transform.GetChild(0).GetComponent<Canvas>();

        gamePaused = false;
        miniMapOpen = false;

        potHeadDebuffIconAdded = false;
        potHeadDebuffIconFlashing = false;


        //energyDrinkStatusEffectObject;
        energyDrinkBuffTime = 20.0f;
        currentEnergyDrinkBuffTime = 0.0f;
        energyDrinkBuffIconAdded = false;
        energyDrinkBuffIconFlashing = false;
        energyDrinkBuffedMovementSpeed = 3.0f;

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

        if (GameControllerLevel2 == null && SceneManager.GetActiveScene().name == "Level2")
        {
            Debug.LogWarning("GameControllerLevel2 not found!");
            GameControllerLevel2 = GameObject.Find("GameController").GetComponent<GameControllerLevel2>();
        }
        if (GameControllerLevel1 == null && SceneManager.GetActiveScene().name == "Level1")
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Debug.LogError("Scenes Open: " + SceneManager.GetSceneAt(i).name);
            }
            Debug.LogWarning("GameControllerLevel1 not found!");
            GameControllerLevel1 = GameObject.Find("GameControllerLevel1").GetComponent<GameControllerLevel1>();
        }
        
        
        if (SceneManager.GetActiveScene().name == "Level1" || SceneManager.GetActiveScene().name == "CrackOfDawn" && GameControllerLevel1.levelCompleted == true)
        {
            objectivePointer.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            objectivePointer.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (SceneManager.GetActiveScene().name == "Level1" || SceneManager.GetActiveScene().name == "CrackOfDawn")
        {
            pointerDirection = this.transform.position - GameObject.Find("FrontDoor").transform.position;

            //This code makes a 2D object rotate to face another
            Vector3 targ = GameObject.Find("FrontDoor").transform.position;
            targ.z = 0f;

            Vector3 objectPos = this.transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;

            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            objectivePointer.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        }
        else if (SceneManager.GetActiveScene().name == "Level2")
        {
            //This code makes a 2D object rotate to face another
            Vector3 targ = GameObject.Find("Home").transform.position;
            targ.z = 0f;

            Vector3 objectPos = this.transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;

            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            objectivePointer.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        if (SceneManager.GetActiveScene().name == "Level2")
        {
            GameControllerLevel2 = GameObject.Find("GameController").GetComponent<GameControllerLevel2>();
            canFire = true;
        }
        //The player is dead
        if (healthM <= 0.0M && !unkillable)
        {
            GameControllerLevel2.playerDied();
        }
        if (healthM != maxHealthM && !healed)
        {
            healthM += healingSpeedM;
            healed = true;
            StartCoroutine(healCoolDown());
            //print("Health: " + healthM + "/100");
        }

        //Pot Head debuff stuff
        if (currentPotHeadDebuffTime > 0.0f)
        {
            currentPotHeadDebuffTime -= Time.deltaTime;
            if (!potHeadDebuffIconAdded)
            {
                potHeadStatusEffectObject = statusEffectBarController.addStatusEffect("Textures/iconPotHeadSmokeDebuff");
                potHeadDebuffIconAdded = true;
            }
        }
        else
        {
            movementController.speedDebuffed = false;

            if (potHeadDebuffIconAdded)
            {
                potHeadDebuffIconAdded = false;
                statusEffectBarController.removeStatusEffect(potHeadStatusEffectObject);
            }
            
        }
        if (currentPotHeadDebuffTime < potHeadDebuffTime * 0.25f && !potHeadDebuffIconFlashing && currentPotHeadDebuffTime > 0)
        {
            print("Flashing pot head effect");
            potHeadDebuffIconFlashing = true;
            statusEffectBarController.flashEffect(potHeadStatusEffectObject);
        }

        //Smack Head debuff stuff
        if (currentSmackHeadDebuffTime > 0.0f)
        {
            if (poisonCanDamage)
            {
                healthM -= poisonDamage;
                poisonCanDamage = false;
                StartCoroutine(resetPoisonDamage());
            }
            
            currentSmackHeadDebuffTime -= Time.deltaTime;
            if (!smackHeadDebuffIconAdded)
            {
                smackHeadStatusEffectObject = statusEffectBarController.addStatusEffect("Textures/iconSmackHeadPoisonDebuff");
                smackHeadDebuffIconAdded = true;
            }
        }
        else
        {
            if (smackHeadDebuffIconAdded)
            {
                smackHeadDebuffIconAdded = false;
                statusEffectBarController.removeStatusEffect(smackHeadStatusEffectObject);
            }

        }
        if (currentSmackHeadDebuffTime < smackHeadDebuffTime * 0.25f && !smackHeadDebuffIconFlashing && currentSmackHeadDebuffTime > 0)
        {
            print("Flashing pot head effect");
            smackHeadDebuffIconFlashing = true;
            statusEffectBarController.flashEffect(smackHeadStatusEffectObject);
        }

        //energyDrinkStatusEffectObject
        //Energy debuff stuff
        if (currentEnergyDrinkBuffTime > 0.0f)
        {

            currentEnergyDrinkBuffTime -= Time.deltaTime;
            if (!energyDrinkBuffIconAdded)
            {
                energyDrinkStatusEffectObject = statusEffectBarController.addStatusEffect("Textures/iconEnergyDrinkBuff");
                energyDrinkBuffIconAdded = true;
            }
        }
        else
        {
            if (energyDrinkBuffIconAdded)
            {
                movementController.setMovementSpeed(movementSpeed);
                energyDrinkBuffIconAdded = false;
                statusEffectBarController.removeStatusEffect(energyDrinkStatusEffectObject);
            }
        }
        if (currentEnergyDrinkBuffTime < energyDrinkBuffTime * 0.25f && !energyDrinkBuffIconFlashing && currentEnergyDrinkBuffTime > 0)
        {
            print("Flashing pot head effect");
            energyDrinkBuffIconFlashing = true;
            statusEffectBarController.flashEffect(energyDrinkStatusEffectObject);
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
            if (readyToFire && canFire)
            {
                print("Shooting!");
                fire();
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (readyToFire && canFire)
            {
                print("Shooting!");
                fire();
            };
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


    void addToBloodMoney(AIController.enemyType type)
    {
        switch(type)
        {
            case AIController.enemyType.Alcoholic:
                money += 30;
                break;
            case AIController.enemyType.CokeHead:
                money += 30;
                break;
            case AIController.enemyType.CrackHead:
                money += 20;
                break;
            case AIController.enemyType.PotHead:
                money += 40;
                break;
            case AIController.enemyType.SmackHead:
                money += 60;
                break;
        }
    }

    public void addToPaycheque(float income)
    {
        paycheque += income;
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
            audioSource.volume = 1.0f;
            audioSource.clip = fireGun;
            audioSource.PlayOneShot(fireGun);
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


    IEnumerator resetPoisonDamage()
    {
        yield return new WaitForSeconds(1);
        poisonCanDamage = true;
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
        audioSource.clip = healFood;
        audioSource.volume = 0.5f;
        audioSource.PlayOneShot(healFood);
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


    //debuff the players speed when they walk into the pot heads smoke
    public void potHeadDebuff()
    {
        currentPotHeadDebuffTime = potHeadDebuffTime;
        movementController.setMovementSpeed(potHeadDebuffSpeed);
        movementController.speedDebuffed = true;
        potHeadDebuffIconFlashing = false;
    }

    //called when level 1 is completed so that the progress can be tracked
    public void level1Complete()
    {
        level1Iteration++;
        payPlayer();
    }

    //called when level 2 is completed so that the progress can be tracked
    public void level2Complete()
    {
        level2Iteration++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "AIBullet")
        {
            damagePlayer(collision.gameObject.GetComponent<bulletController>().getDamage());
            print("Health: " + healthM + "/100");
        }
        else if (collision.gameObject.tag == "AIPoisonBullet")
        {
            
            if (currentSmackHeadDebuffTime <= 0)
            {
                poisonCanDamage = true;
            }
            currentSmackHeadDebuffTime = smackHeadDebuffTime;
            damagePlayer(collision.gameObject.GetComponent<bulletController>().getDamage());
            print("Health: " + healthM + "/100");
        }
    }

    public void drinkEnergy()
    {
        currentEnergyDrinkBuffTime = energyDrinkBuffTime;
        movementController.setMovementSpeed(energyDrinkBuffedMovementSpeed);
    }
}
