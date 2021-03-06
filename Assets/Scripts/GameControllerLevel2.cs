using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControllerLevel2 : MonoBehaviour
{
    [SerializeField] int enemiesToSpawn;
    [SerializeField] int enemiesSpawned;

    [SerializeField] bool playerDead;

    CharacterController characterController;
    HomeController homeController;

    Canvas deathScreenCanvas;
    GameObject buttons;

    [SerializeField] int tasksCompleted;
    [SerializeField] int totalTasks;

    //How many times this level has been played
    //the game loops between level 1 and 2 with increasing difficulty so this is what will track it
    [SerializeField] int levelIteration;

    GameObject ObjectivePointer;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        enemiesToSpawn = Random.Range(3, 11);
        playerDead = false;
        deathScreenCanvas = GameObject.Find("DeathScreenCanvas").GetComponent<Canvas>();
        buttons = GameObject.Find("Buttons");
        characterController = GameObject.Find("Jeremy").GetComponent<CharacterController>();
        homeController = GameObject.Find("Home").GetComponent<HomeController>();

        ObjectivePointer = GameObject.Find("ObjectivePointer");

        CharacterController.enemyDied += enemyDied;

        tasksCompleted = 0;
        characterController.drinkEnergy();
    }

    // Update is called once per frame
    void Update()
    {
        totalTasks = GameObject.Find("TaskList").transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).childCount;
        if (totalTasks == 0)
        {

        }

        if (ObjectivePointer == null)
        {
            ObjectivePointer = GameObject.Find("ObjectivePointer");
        }
        

        if (tasksCompleted == totalTasks)
        {
            Debug.LogWarning("Level2 Complete! " + tasksCompleted + "/" + totalTasks);
            characterController.level2Complete();
            homeController.unlockDoor();
            ObjectivePointer.GetComponent<SpriteRenderer>().enabled = true;
        }


    }

    public void playerDied()
    {

        playerDead = true;
        //Pauses the game in the background
        Time.timeScale = 0;
        deathScreenCanvas.enabled = true;
        //Should enable all child buttons
        for (int i = 0; i < buttons.transform.childCount; i++)
        {
            buttons.transform.GetChild(i).GetComponent<Button>().enabled = true;
        }

        //Disables the minimap
        GameObject.Find("MiniMapCamera").GetComponent<Camera>().enabled = false;
        GameObject.Find("MiniMapCamera").transform.GetChild(0).GetComponent<Canvas>().enabled = false;
    }
    //This is what the rehab button activates.
    //Reloads the scene

    public void enemyDied(AIController.enemyType type)
    {
    }

    public void respawnPlayer()
    {
        characterController.respawnPlayer(new Vector3(-26.5f, 0.5f, 0f));
        Time.timeScale = 1;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene("Level2");
        
    }


    public void taskComplete()
    {
        tasksCompleted++;
    }

    void spawnEnemies()
    {

    }
}
