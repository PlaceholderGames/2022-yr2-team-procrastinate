using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    CharacterController characterController;
    //gets damage from the character controller script on the player
    float damage;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GameObject.Find("Jeremy").GetComponent<CharacterController>();
        if(this.tag == "bullet")
        {
            damage = characterController.getPlayerDamage();
        }
        else if (this.tag == "AIBullet" || this.tag == "AIPoisonBullet")
        {
            damage = 10.0f;
        }


        StartCoroutine(destroyBullet());
    }

    // Update is called once per frame
    void Update()
    {
        //Physics2D.IgnoreCollision(GameObject.Find("Jeremy").GetComponent<Collider2D>(), this.GetComponent<Collider2D>(), true);
    }

    IEnumerator destroyBullet()
    {
        yield return new WaitForSeconds(10);
        print("Destroying " + this);
        Destroy(this.gameObject);
    }

    //Used to check what the bullet has hit
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "AIBullet")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), this.GetComponent<Collider2D>(), true);
        }

        if (collision.gameObject.tag == "enemy")
        {
            print("Enemy hit!");
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Wall")
        {
            print("Destroying bullet!");
            Destroy(this.gameObject);
        }
        
        if (collision.gameObject.tag == "Player")
        {
            print("Hit the player!");
            Destroy(this.gameObject);
        }
    }

    public float getDamage()
    {
        return damage;
    }
}
