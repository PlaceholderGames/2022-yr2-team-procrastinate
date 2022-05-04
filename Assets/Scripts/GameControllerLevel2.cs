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

    Canvas deathScreenCanvas;
    GameObject buttons;

    [SerializeField] int tasksComplete;
    [SerializeField] int totalTasks;

    //How many times this level has been played
    //the game loops between level 1 and 2 with increasing difficulty so this is what will track it
    [SerializeField] int levelIteration;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        enemiesToSpawn = Random.Range(3, 11);
        playerDead = false;
        deathScreenCanvas = GameObject.Find("DeathScreenCanvas").GetComponent<Canvas>();
        buttons = GameObject.Find("Buttons");
        characterController = GameObject.Find("Jeremy").GetComponent<CharacterController>();

        CharacterController.enemyDied += enemyDied;

        tasksComplete = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (tasksComplete == totalTasks)
        {
            characterController.level2Complete();
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
        SceneManager.LoadScene(currentScene.name);
        
    }


    public void taskComplete()
    {
        tasksComplete++;
    }

    void spawnEnemies()
    {

    }
}
