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
    }

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
