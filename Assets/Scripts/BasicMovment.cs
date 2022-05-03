using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicMovment : MonoBehaviour
{

    public Animator animator;

    [SerializeField] bool collidingWithStaticObject = false;
    [SerializeField] public GameObject playerGameObject = null;
    [SerializeField] public Rigidbody2D playerRigidBody = null;
    [SerializeField] public BoxCollider2D playerCollider = null;
    [SerializeField] public CapsuleCollider2D crateDetectorCollider = null;
    [SerializeField] public GameObject connectedObject = null;//this will be what the player pulls
    [SerializeField] float movementSpeed = 2.0f;
    [SerializeField] List<GameObject> NearbyCratesList = new List<GameObject>();

    [SerializeField] public bool speedDebuffed;
    

    // Start is called before the first frame update
    void Start()
    {
        playerGameObject = GameObject.Find("Jeremy");
        playerRigidBody = playerGameObject.GetComponent<Rigidbody2D>();
        playerCollider = playerGameObject.GetComponent<BoxCollider2D>();
        crateDetectorCollider = playerGameObject.GetComponent<CapsuleCollider2D>();

        speedDebuffed = false;
        //connectedObject = playerGameObject.transform.GetChild(1).gameObject;
    }
    

    //This is used here because checking for key presses like below is very unreliable in FixedUpdate()
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (SceneManager.GetActiveScene().name == "Level1" || SceneManager.GetActiveScene().name == "CrackOfDawn")
            {
                print("G");
                if (!connectedObject && NearbyCratesList.Count > 0)
                {
                    grabObject();
                }
                else if (connectedObject != null)
                {
                    releaseObject();
                }
            }
        }
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerGameObject == null)
        {
            playerGameObject = GameObject.Find("Jeremy");
        }
        if (playerRigidBody == null)
        {
            playerRigidBody = playerGameObject.GetComponent<Rigidbody2D>();
        }
        if (playerCollider == null)
        {
            playerCollider = playerGameObject.GetComponent<BoxCollider2D>();
        }
        if (crateDetectorCollider == null)
        {
            crateDetectorCollider = playerGameObject.GetComponent<CapsuleCollider2D>();
        }
        //if (connectedObject == null)
        //{
        //    connectedObject = playerGameObject.transform.GetChild(1).gameObject;
        //}


        Vector2 movement = new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        playerRigidBody.MovePosition(playerRigidBody.position + (movementSpeed * movement) * Time.fixedDeltaTime);

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitute", movement.magnitude);
        





        if (connectedObject)
        {
            movementSpeed = 0.5f;
        }
        else if (!speedDebuffed)
        {
            movementSpeed = 2.0f;
        }
        if (movement.x > 0)
        {
            playerCollider.offset = new Vector2(-0.01578945f, -0.1042103f);
            crateDetectorCollider.offset = new Vector2(0.1017718f, -0.1042103f);
            if (connectedObject)
            {
                connectedObject.transform.localPosition = new Vector3(0.5f, 0.0f, 0.0f);
            }
            
        }
        else if (movement.x < 0)
        {
            playerCollider.offset = new Vector2(0.0155f, -0.1042103f);
            crateDetectorCollider.offset = new Vector2(-0.1017718f, -0.1042103f);
            if (connectedObject)
            {
                connectedObject.transform.localPosition = new Vector3(-0.5f, 0.0f, 0.0f);
            }
        }
        else if (movement.y > 0)
        {
            crateDetectorCollider.offset = new Vector2(0.0f, 0.3f);
            if (connectedObject)
            {
                connectedObject.transform.localPosition = new Vector3(0.0f, 0.4f, 0.0f);
            }
        }
        else if (movement.y < 0)
        {
            crateDetectorCollider.offset = new Vector2(0.0f, -0.3f);
            if (connectedObject)
            {
                connectedObject.transform.localPosition = new Vector3(0.0f, -0.4f, 0.0f);
            }
        }



            
        
    }

    

    private void grabObject()
    {
        //grabs the first item in the list then attaches it to the player
        connectedObject = NearbyCratesList[0];
        connectedObject.transform.parent = playerGameObject.transform;
    }
    private void releaseObject()
    {
        //releases the object attached to the player
        connectedObject.transform.parent = GameObject.Find("Crates").transform;
        connectedObject = null;
    }

    public void setMovementSpeed(float Speed)
    {
        movementSpeed = Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "StaticDeliveryShelf" && collision.tag != "Wall")
        {
            NearbyCratesList.Add(collision.transform.gameObject);
        }
        //if (collision.gameObject.GetComponent<CircleCollider2D>())
        //{
        //    Physics2D.IgnoreCollision(collision.gameObject.GetComponent<CircleCollider2D>(), this.GetComponent<Collider2D>());
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        NearbyCratesList.Remove(collision.transform.gameObject);
    }
}
