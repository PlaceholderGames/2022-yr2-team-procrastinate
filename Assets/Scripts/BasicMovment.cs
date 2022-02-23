using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovment : MonoBehaviour
{

    public Animator animator;

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

        Vector2 movement = new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        playerRigidBody.MovePosition(playerRigidBody.position + (movementSpeed * movement) * Time.deltaTime);

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
