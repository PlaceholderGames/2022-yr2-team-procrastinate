using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovment : MonoBehaviour
{

    public Animator animator;

    [SerializeField] bool collidingWithStaticObject = false;
    [SerializeField] public Rigidbody2D playerRigidBody = null;
    [SerializeField] float movementSpeed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GameObject.Find("Jeremy").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerRigidBody == null)
        {
            playerRigidBody = GameObject.Find("Jeremy").GetComponent<Rigidbody2D>();
        }
        print("Rigid body: " + playerRigidBody);
        Vector2 movement = new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        playerRigidBody.MovePosition(playerRigidBody.position + (movementSpeed * movement) * Time.fixedDeltaTime);

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitute", movement.magnitude);
    }

}
