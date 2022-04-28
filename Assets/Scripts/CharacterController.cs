using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    gameController GameController;

    //High precision used for calculating health and other bits
    [SerializeField] decimal healthM;
    [SerializeField] decimal maxHealthM;
    [SerializeField] decimal healingSpeedM;

    //Low precision versions used for displaying in the inspector without using editor voodoo.
    [SerializeField] float healthF;
    [SerializeField] float maxHealthF;
    [SerializeField] float healingSpeedF;
    Slider healthBarSlider;


    [SerializeField] float damage;
    [SerializeField] float rateOfFire;


    [SerializeField] bool healed;


    [SerializeField] int aimDirection;
    [SerializeField] bool readyToFire;
    [SerializeField] int projectileSpeed;
    [SerializeField] Rigidbody2D bulletPrefab;


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
        damage = 34.0f;
        rateOfFire = 30.0f;
        healed = false;
        aimDirection = 5;
        readyToFire = true;
        projectileSpeed = 300;

        healthBarSlider = GameObject.Find("HealthBarSlider").GetComponent<Slider>();
    }


    //void FixedUpdate()
    //{
        
    //}


    // Update is called once per frame
    void Update()
    {
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
            print("Health: " + healthM);
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
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
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

    void fire()
    {
        if (readyToFire == true)
        {
            readyToFire = false;
            Rigidbody2D bullet = Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation) as Rigidbody2D;
            //bullet.GetComponent<SpriteRenderer>().flipX = false;
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), this.GetComponent<Collider2D>(), true);
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
}
