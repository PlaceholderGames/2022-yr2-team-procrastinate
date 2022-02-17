using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovment : MonoBehaviour
{
    [SerializeField] bool collidingWithStaticObject = false;
    [SerializeField] private Rigidbody2D playerRigidBody = null;
    [SerializeField] float movementSpeed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (collidingWithStaticObject == false)
        {
            //Moving using Rigidbody2D.MovePosition so that propper collision detection works. 
            //old method of transform.position doesn't work well with collision detection
            playerRigidBody.MovePosition(playerRigidBody.position + (movementSpeed * movement) * Time.deltaTime);
        }
        
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
