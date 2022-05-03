using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    //Locations
    [SerializeField] Vector2 startingPosition;
    [SerializeField] Vector2 targetPosition;//Target position to walk towards
    [SerializeField] Vector3 currentPosition;//Target position to walk towards
    [SerializeField] Vector2 direction;

    //Movement
    [SerializeField] int movesBeforeSleep;
    [SerializeField] bool isSleeping;
    [SerializeField] int sleepTime;
    [SerializeField] int moves;
    [SerializeField] float movementSpeed;
    [SerializeField] bool canMove;

    //Player data
    [SerializeField] Vector3 playerPosition;
    [SerializeField] bool playerIsTarget;
    [SerializeField] float distanceToPlayer;
    CharacterController playerControllerScript;

    //Stats
    [SerializeField] float AIHealth;
    [SerializeField] float AIMaxHealth;
    [SerializeField] float AIDamage;
    [SerializeField] bool canAttack;
    [SerializeField] float attackCooldown;
    [SerializeField] float rangedAttackCooldown;

    //Projectile
    [SerializeField] bool readyToFire;
    [SerializeField] int projectileSpeed;
    [SerializeField] Rigidbody2D bulletPrefab;
    [SerializeField] float damage;
    [SerializeField] float radius;
    CircleCollider2D circleCollider;

    //Smoke
    [SerializeField] GameObject AOESmokePrefab;
    [SerializeField] bool readyToSmoke;
    [SerializeField] float smokeAttackCooldown;



    public Animator animator;
    [SerializeField] bool attackAnimationFinished;

    //This line below, and the enum "enemyType" below it are what creates the drop down in the inspector
    public enemyType AIType = new enemyType();
    public enum enemyType
    {
        Alcoholic,
        CrackHead,
        CokeHead,
        SmackHead,
        PotHead
    }

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = new Vector2(this.transform.position.x, this.transform.position.y);
        targetPosition = GetRandomPosition();
        
        canMove = true;
        movesBeforeSleep = Random.Range(1, 10);
        isSleeping = false;
        sleepTime = Random.Range(1, 4);
        playerPosition = GameObject.Find("Jeremy").transform.position;
        playerIsTarget = false;

        attackAnimationFinished = true;

        rangedAttackCooldown = 2.0f;
        readyToFire = true;
        projectileSpeed = 450;//450
        damage = 10.0f;

        //For Sphere Collider 2D
        radius = 2.0f;

        //Smoke
        readyToSmoke = true;
        smokeAttackCooldown = 15.0f;

        switch (AIType)
        {
            //Tank
            case enemyType.Alcoholic:
                AIMaxHealth = 200.0f;
                AIHealth = 200.0f;
                AIDamage = 30.0f;
                movementSpeed = 1.5f;
                attackCooldown = 2.5f;
                break;
            //Basic
            case enemyType.CrackHead:
                AIMaxHealth = 100.0f;
                AIHealth = 100.0f;
                AIDamage = 10.0f;
                movementSpeed = 2.0f;
                attackCooldown = 1.5f;
                break;
            //Speed
            case enemyType.CokeHead:
                AIMaxHealth = 75.0f;
                AIHealth = 75.0f;
                AIDamage = 10.0f;
                movementSpeed = 6.0f;
                attackCooldown = 1f;
                break;
            //Shooting
            case enemyType.SmackHead:
                AIMaxHealth = 100.0f;
                AIHealth = 100.0f;
                AIDamage = 10.0f;
                movementSpeed = 1.5f;
                attackCooldown = 2f;
                rangedAttackCooldown = 3f;
                break;
            //Status Effect
            case enemyType.PotHead:
                AIMaxHealth = 100.0f;
                AIHealth = 100.0f;
                AIDamage = 10.0f;
                movementSpeed = 2.0f;
                attackCooldown = 2f;
                circleCollider = this.gameObject.AddComponent<CircleCollider2D>();
                circleCollider.radius = radius;
                circleCollider.isTrigger = true;
                break;
        }

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
                playerIsTarget = false;
                targetPosition = GetRandomPosition();
                moves++;
            }
            else if (Vector2.Distance(this.transform.position, playerPosition) > 3.3f)
            {
                playerIsTarget = false;
                StartCoroutine(goToTargetPosition());
            }
            else if (Vector2.Distance(this.transform.position, playerPosition) < 3.3f)
            {
                playerIsTarget = true;
                targetPosition = new Vector2(playerPosition.x, playerPosition.y);
                StartCoroutine(goToTargetPosition());
            }
        }
        currentPosition = this.transform.position;
        distanceToPlayer = Vector2.Distance(this.transform.position, playerPosition);

        if (playerIsTarget && readyToFire && AIType == enemyType.SmackHead)
        {
            rangedAttack();
        }
        if (playerIsTarget && readyToSmoke && AIType == enemyType.PotHead)
        {
            smokeAttack();
        }

    }

    IEnumerator goToTargetPosition()
    {
        if (attackAnimationFinished)
        {
            animator.SetBool("Attacking", false);
        }
        
        canMove = false;
        direction = new Vector2(targetPosition.x, targetPosition.y) - new Vector2(this.transform.position.x, this.transform.position.y);
        float speedMultiplier = 3.3f - distanceToPlayer;
        float movementSpeedlocal;
        movementSpeedlocal = movementSpeed;

        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Magnitute", direction.magnitude);
        

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


    void rangedAttack()
    {
        animator.SetBool("Attacking", true);
        attackAnimationFinished = false;
        Vector2 direction = new Vector2(targetPosition.x, targetPosition.y) - new Vector2(this.transform.position.x, this.transform.position.y);

        Rigidbody2D bullet = Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation) as Rigidbody2D;
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), this.GetComponent<Collider2D>(), true);

        bullet.GetComponent<Rigidbody2D>().AddForce(direction * projectileSpeed);


        //This code makes a 2D object rotate to face another
        Vector3 targ = playerPosition;
        targ.z = 0f;

        Vector3 objectPos = bullet.transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));


        readyToFire = false;
        StartCoroutine(stopAttackAnimation());
        StartCoroutine(rechargeRangedAttack());
    }

    void smokeAttack()
    {
        animator.SetBool("Attacking", true);
        attackAnimationFinished = false;
        GameObject AOESmoke = Instantiate(AOESmokePrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation) as GameObject;
        AOESmoke.GetComponent<AOESmoke>().radius = radius;

        readyToSmoke = false;

        StartCoroutine(stopAttackAnimation());
        StartCoroutine(rechargeSmokeAttack());
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
            animator.SetBool("Attacking", true);
            attackAnimationFinished = false;
            canAttack = false;
            playerControllerScript.damagePlayer(AIDamage);
            StartCoroutine(stopAttackAnimation());
            StartCoroutine(rechargeAttack());
        }
        else if (collision.gameObject.tag == "Player" && canAttack != true)
        {
            //print("Waiting to attack player!");
        }
        else if (collision.gameObject.tag != "bullet" && collision.gameObject.tag != "Player")
        {
            //print("Finding new target!");
            targetPosition = GetRandomPosition();
        }
        else if (collision.gameObject.tag == "bullet")
        {
            AIHealth -= collision.gameObject.GetComponent<bulletController>().getDamage();
            print("Health: " + AIHealth + "/" + AIMaxHealth);
            if (AIHealth <= 0.0f)
            {
                CharacterController.enemyDied(AIType);
                Destroy(this.gameObject);
            }
        }
    }

    IEnumerator stopAttackAnimation()
    {
        yield return new WaitForSeconds(0.517f);
        attackAnimationFinished = true;
        animator.SetBool("Attacking", false);
    }

    IEnumerator rechargeAttack()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
    IEnumerator rechargeRangedAttack()
    {
        yield return new WaitForSeconds(rangedAttackCooldown);
        readyToFire = true;
    }

    IEnumerator rechargeSmokeAttack()
    {
        yield return new WaitForSeconds(smokeAttackCooldown);
        readyToSmoke = true;
    }

    //sometimes the AI's new target, after hitting something, is still towards the object
    //so to prevent it getting stuck there this function checks if it stays in the collider for too long then gives it a new target
    private void OnCollisionStay2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Player" && canAttack == true)
        {
            print("Attacking Player Continued!");
            animator.SetBool("Attacking", true);
            attackAnimationFinished = false;
            canAttack = false;
            playerControllerScript.damagePlayer(AIDamage);
            StartCoroutine(rechargeAttack());
            StartCoroutine(stopAttackAnimation());
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
