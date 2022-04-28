using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{
    [SerializeField] int enemiesToSpawn;
    [SerializeField] int enemiesSpawned;

    [SerializeField] bool playerDead;

    Canvas deathScreenCanvas;
    GameObject buttons;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        enemiesToSpawn = Random.Range(3, 11);
        playerDead = false;
        deathScreenCanvas = GameObject.Find("DeathScreenCanvas").GetComponent<Canvas>();
        buttons = GameObject.Find("Buttons");
    }

    // Update is called once per frame
    void Update()
    {
        
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
    public void respawnPlayer()
    {
        Time.timeScale = 1;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        
    }
}
