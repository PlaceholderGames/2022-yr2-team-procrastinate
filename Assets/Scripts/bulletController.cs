using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    float damage = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(destroyBullet());
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if (collision.gameObject.tag == "bullet")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
        }

        if (collision.gameObject.tag == "enemy")
        {
            print("Enemy hit!");
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }

    public float getDamage()
    {
        return damage;
    }
}