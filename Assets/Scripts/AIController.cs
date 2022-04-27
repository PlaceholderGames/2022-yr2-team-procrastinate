using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] Vector3 startingPosition;
    [SerializeField] Vector3 targetPosition;//Target position to walk towards
    [SerializeField] Vector3 currentPosition;//Target position to walk towards
    [SerializeField] float movementSpeed;
    [SerializeField] bool canMove;

    [SerializeField] int movesBeforeSleep;
    [SerializeField] bool isSleeping;
    [SerializeField] int sleepTime;
    [SerializeField] int moves;


    [SerializeField] Vector3 playerPosition;
    [SerializeField] bool playerIsTarget;
    [SerializeField] float distanceToPlayer;


    [SerializeField] float AIHealth;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = this.transform.position;
        targetPosition = GetRandomPosition();
        movementSpeed = 0.02f;
        canMove = true;
        movesBeforeSleep = Random.Range(1, 10);
        isSleeping = false;
        sleepTime = Random.Range(1, 4);
        playerPosition = GameObject.Find("Jeremy").transform.position;
        playerIsTarget = false;

        AIHealth = 100.0f;
    }

    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        playerPosition = GameObject.Find("Jeremy").transform.position;
        if (moves >= movesBeforeSleep)
        {
            StartCoroutine(sleepAI());
        }
        if (canMove && !isSleeping)
        {
            if (Vector2.Distance(this.transform.position, targetPosition) < 0.5f)
            {
                movementSpeed = 0.02f;
                playerIsTarget = false;
                targetPosition = GetRandomPosition();
                moves++;
            }
            else if (Vector2.Distance(this.transform.position, playerPosition) > 3.3f)
            {
                movementSpeed = 0.02f;
                playerIsTarget = false;
                StartCoroutine(goToTargetPosition());
            }
            else if(Vector2.Distance(this.transform.position, playerPosition) < 3.3f)
            {
                movementSpeed = 0.1f;
                playerIsTarget = true;
                targetPosition = playerPosition;
                StartCoroutine(goToTargetPosition());
            }
        }
        currentPosition = this.transform.position;
        distanceToPlayer = Vector2.Distance(this.transform.position, playerPosition);


        Vector2 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
  
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitute", movement.magnitude);


    }

        

    IEnumerator goToTargetPosition()
    {
        canMove = false;
        var direction = targetPosition - this.transform.position;
        this.GetComponent<Rigidbody2D>().MovePosition(this.transform.position + direction.normalized * movementSpeed);
        yield return new WaitForSeconds(0.01f);
        canMove = true;
    }

    IEnumerator sleepAI()
    {
        isSleeping = true;
        movesBeforeSleep = Random.Range(1, 10);
        yield return new WaitForSeconds(sleepTime);
        moves = 0;
        isSleeping = false;
        sleepTime = Random.Range(1, 5);
    }

    //Gets a random direction to walk in
    public static Vector3 GetRandomDirection()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }

    //Gets a random position to walk to
    Vector3 GetRandomPosition()
    {
        return startingPosition + GetRandomDirection() * Random.Range(1f, 3f);
    }

    //If the AI hits an object it will find a new target to head towards
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "bullet")
        {
            targetPosition = GetRandomPosition();
        }
        else if (collision.gameObject.tag == "bullet")
        {
            AIHealth -= collision.gameObject.GetComponent<bulletController>().getDamage();
            print("Health: " + AIHealth + "/100");
            if (AIHealth <= 0.0f)
            {
                Destroy(this.gameObject);
            }
        }
    }

    //sometimes the AI's new target, after hitting something, is still towards the object
    //so to prevent it getting stuck there this function checks if it stays in the collider for too long then gives it a new target
    private void OnCollisionStay2D(Collision2D collision)
    {
        targetPosition = GetRandomPosition();
    }
}
