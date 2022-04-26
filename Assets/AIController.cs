using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] Vector2 startingPosition;
    [SerializeField] Vector2 targetPosition;//Target position to walk towards
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
    CharacterController playerControllerScript;


    [SerializeField] float AIHealth;
    [SerializeField] float AIDamage;
    [SerializeField] bool canAttack;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = new Vector2(this.transform.position.x, this.transform.position.y);
        targetPosition = GetRandomPosition();
        movementSpeed = 0.02f;
        canMove = true;
        movesBeforeSleep = Random.Range(1, 10);
        isSleeping = false;
        sleepTime = Random.Range(1, 4);
        playerPosition = GameObject.Find("Jeremy").transform.position;
        playerIsTarget = false;

        AIHealth = 100.0f;
        AIDamage = 10.0f;
        playerControllerScript = GameObject.Find("Jeremy").GetComponent<CharacterController>();
        canAttack = true;
    }

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
                movementSpeed = 2.0f;
                playerIsTarget = false;
                targetPosition = GetRandomPosition();
                moves++;
            }
            else if (Vector2.Distance(this.transform.position, playerPosition) > 3.3f)
            {
                movementSpeed = 2.0f;
                playerIsTarget = false;
                StartCoroutine(goToTargetPosition());
            }
            else if(Vector2.Distance(this.transform.position, playerPosition) < 3.3f)
            {
                movementSpeed = 2.0f;
                playerIsTarget = true;
                targetPosition = new Vector2(playerPosition.x, playerPosition.y);
                StartCoroutine(goToTargetPosition());
            }
        }
        currentPosition = this.transform.position;
        distanceToPlayer = Vector2.Distance(this.transform.position, playerPosition);
    }




    IEnumerator goToTargetPosition()
    {
        canMove = false;
        Vector2 direction = new Vector2(targetPosition.x, targetPosition.y) - new Vector2(this.transform.position.x, this.transform.position.y);
        float speedMultiplier = 3.3f - distanceToPlayer;
        float movementSpeedlocal;
        movementSpeedlocal = movementSpeed;
        
        this.GetComponent<Rigidbody2D>().MovePosition(new Vector2(this.transform.position.x, this.transform.position.y) + direction.normalized * movementSpeed * Time.deltaTime);
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
    public static Vector2 GetRandomDirection()
    {
        return new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }

    //Gets a random position to walk to
    Vector2 GetRandomPosition()
    {
        return startingPosition + GetRandomDirection() * Random.Range(1f, 3f);
    }

    //If the AI hits an object it will find a new target to head towards
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && canAttack == true)
        {
            print("Attacking Player!");
            canAttack = false;
            playerControllerScript.damagePlayer(AIDamage);
            StartCoroutine(rechargeAttack());
        }
        else if (collision.gameObject.tag != "bullet" && collision.gameObject.tag != "Player")
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

    IEnumerator rechargeAttack()
    {
        yield return new WaitForSeconds(1);
        canAttack = true;
    }

    //sometimes the AI's new target, after hitting something, is still towards the object
    //so to prevent it getting stuck there this function checks if it stays in the collider for too long then gives it a new target
    private void OnCollisionStay2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Player" && canAttack == true)
        {
            print("Attacking Player!");
            canAttack = false;
            playerControllerScript.damagePlayer(AIDamage);
            StartCoroutine(rechargeAttack());
        }
        else if (collision.gameObject.tag == "Player" && canAttack == false)
        {
            print("Following Player");
        }
        else
        {
            targetPosition = GetRandomPosition();
        }
    }
}
