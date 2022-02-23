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

    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        playerRigidBody.MovePosition(playerRigidBody.position + (movementSpeed * movement) * Time.deltaTime);
        //Moving using Rigidbody2D.MovePosition so that propper collision detection works. 
        //old method of transform.position doesn't work well with collision detection


        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitute", movement.magnitude);


}
