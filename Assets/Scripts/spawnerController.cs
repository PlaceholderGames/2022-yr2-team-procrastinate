using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerController : MonoBehaviour
{
    Vector2 location;
    [SerializeField] bool canSpawn;
    [SerializeField] bool forceSpawn;

    [SerializeField] float respawnTimer;
    [SerializeField] bool timerStarted;
    
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject spawnedEnemy;

    //This line below is what makes the drop down. The enum used is in AIController.cs
    public AIController.enemyType AIType = new AIController.enemyType();


    // Start is called before the first frame update
    void Start()
    {
        canSpawn = true;
        forceSpawn = false;

        respawnTimer = 20.0f;

        switch(AIType)
        {
            case AIController.enemyType.Alcoholic:
                enemyPrefab = Resources.Load("Prefabs/Alcoholic/Alcoholic", typeof(GameObject)) as GameObject;
                break;
            case AIController.enemyType.PotHead:
                enemyPrefab = Resources.Load("Prefabs/PotHead/PotHead", typeof(GameObject)) as GameObject;
                break;
            case AIController.enemyType.SmackHead:
                enemyPrefab = Resources.Load("Prefabs/SmackHead/SmackHead", typeof(GameObject)) as GameObject;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn || forceSpawn)
        {
            canSpawn = false;
            forceSpawn = false;
            spawnedEnemy = Instantiate(enemyPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation) as GameObject;
            spawnedEnemy.transform.parent = GameObject.Find("Enemies").transform;
            Debug.LogWarning("Spawning enemy: " + spawnedEnemy.name + " at: " + spawnedEnemy.transform.position);
        }

        if (spawnedEnemy == null && !timerStarted)
        {
            timerStarted = true;
            print("Timer Started");
            StartCoroutine(spawnCooldown());
        }
    }

    IEnumerator spawnCooldown()
    {
        yield return new WaitForSeconds(respawnTimer);
        print("enemy can spawn!");
        canSpawn = true;
        timerStarted = false;
    }
}
