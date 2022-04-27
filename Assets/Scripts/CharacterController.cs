using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //High precision used for calculating health and other bits
    [SerializeField] decimal healthM;
    [SerializeField] decimal maxHealthM;
    [SerializeField] decimal healingSpeedM;

    //Low precision versions used for displaying in the inspector without using editor voodoo.
    [SerializeField] float healthF;
    [SerializeField] float maxHealthF;
    [SerializeField] float healingSpeedF;


    [SerializeField] float damage;
    [SerializeField] float rateOfFire;


    [SerializeField] bool healed;


    [SerializeField] int aimDirection;
    [SerializeField] bool readyToFire;
    [SerializeField] int projectileSpeed;
    [SerializeField] Rigidbody2D bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        healthM = 90.0M;
        maxHealthM = 100.0M;
        healingSpeedM = 0.1M;
        damage = 34.0f;
        rateOfFire = 200.0f;
        healed = false;
        aimDirection = 5;
        readyToFire = true;
        projectileSpeed = 300;
    }


    //void FixedUpdate()
    //{
        
    //}


    // Update is called once per frame
    void Update()
    {
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
            aimDirection = 0;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            aimDirection = 1;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            aimDirection = 2;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            aimDirection = 3;
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fire();
        }



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
            Rigidbody2D bullet = Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation) as Rigidbody2D;
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), this.GetComponent<Collider2D>(), true);
            if (aimDirection == 0)
            {
                bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * projectileSpeed);
            }
            else if (aimDirection == 1)
            {
                bullet.GetComponent<Rigidbody2D>().AddForce(transform.right * projectileSpeed);
            }
            else if (aimDirection == 2)
            {
                bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * -projectileSpeed);
            }
            else if (aimDirection == 3)
            {
                bullet.GetComponent<Rigidbody2D>().AddForce(transform.right * -projectileSpeed);
            }

        }
        


        StartCoroutine(weaponCooldown());
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
}
