using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovment : MonoBehaviour
{

    public Animator animator;

    public Animator animator;

    // Update is called once per frame
    void Update()
    {

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"), 0.0f);

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitute", movement.magnitude);

        transform.position = transform.position + movement * Time.deltaTime;
        



        /*
        //flips the characters sprite so they face the direction they're walking
        if (movement.x < 0)
        {
            GameObject.Find("Jeremy").GetComponent<SpriteRenderer>().flipX = true;
            GameObject.Find("Jeremy").GetComponent<BoxCollider2D>().offset = new Vector2(0.039f, -0.1402018f);
        }
        else if (movement.x > 0)
        {
            GameObject.Find("Jeremy").GetComponent<SpriteRenderer>().flipX = false;
            GameObject.Find("Jeremy").GetComponent<BoxCollider2D>().offset = new Vector2(-0.02473149f, -0.1402018f);
        }

        */
    }
    
    //private void OnTriggerEnter2D(Collider2D collider)
    //{
    //    print("Colliding with object!");
    //    if (collider.transform.parent.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Static)
    //    {
    //        print("Colliding with wall!");
    //        collidingWithStaticObject = true;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collider)
    //{
    //    print("Not Colliding with object!");
    //    if (collider.transform.parent.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Static)
    //    {
    //        print("Not Colliding with wall!");
    //        collidingWithStaticObject = false;
    //    }
    //}
}
