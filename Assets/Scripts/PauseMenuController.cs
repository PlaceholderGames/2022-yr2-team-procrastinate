using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] GameObject pauseMenuGameObject;
    [SerializeField] Canvas pauseMenuCanvas;
    [SerializeField] Button resumeButton;
    [SerializeField] Button exitButton;
    [SerializeField] TaskListController taskListController;

    //Controls
    Canvas controlsCanvas;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenuGameObject = this.gameObject;
        pauseMenuCanvas = pauseMenuGameObject.GetComponent<Canvas>();

        characterController = GameObject.Find("Jeremy").GetComponent<CharacterController>();

        //Finds the button component by searching through the children of the PauseMenu canvas
        resumeButton = pauseMenuGameObject.transform.GetChild(1).transform.GetChild(1).GetComponent<Button>();
        exitButton = pauseMenuGameObject.transform.GetChild(1).transform.GetChild(2).GetComponent<Button>();

        controlsCanvas = pauseMenuCanvas.transform.GetChild(2).GetComponent<Canvas>();

        taskListController = GameObject.Find("TaskList").GetComponent<TaskListController>();
    }


    // Update is called once per frame
    void Update()
    {
        if (pauseMenuCanvas == null)
        {
            Debug.LogWarning("pauseMenuCanvas not found!");
            pauseMenuCanvas = pauseMenuGameObject.GetComponent<Canvas>();
        }
        //P is temporary since Escape ends the editor play mode.
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Debug.LogWarning("Pause Menu opened!");
            Time.timeScale = 0;
            pauseMenuCanvas.enabled = true;
            characterController.gamePaused = true;

            if (characterController.miniMapOpen)
            {
                characterController.toggleMinimap();
            }
        }
    }

    //Unpauses the game
    public void resumeGame()
    {
        Debug.LogWarning("Resuming game!");
        pauseMenuCanvas.enabled = false;
        characterController.gamePaused = false;
        Time.timeScale = 1;
        

        if (characterController.miniMapOpen)
        {
            characterController.toggleMinimap();
        }
    }

    //Restarts the level
    //Loads Level1 scene to avoid duplicating Jeremy
    public void retryLevel()
    {
        if (SceneManager.GetActiveScene().name == "Level1" || SceneManager.GetActiveScene().name == "CrackOfDawn")
        {
            SceneManager.LoadScene("Level1", LoadSceneMode.Single);
            GameObject.Find("Jeremy").transform.position = new Vector3(-13.8400002f, 0.689999998f, 0);
            //taskListController.clearList();
        }
        else if (SceneManager.GetActiveScene().name == "Level2")
        {
            SceneManager.LoadScene("Level2", LoadSceneMode.Single);
            GameObject.Find("Jeremy").transform.position = new Vector3(-32.4399986f, -0.769999981f, 0);
            //taskListController.clearList();
            taskListController.level2Reset = true;
            taskListController.level2ListItemsCleared = false;
        }
        
    }

    //Opens the controls menu
    public void openControls()
    {
        controlsCanvas.enabled = true;
    }

    //Exits the controls menu
    public void menuControlsBack()
    {
        controlsCanvas.enabled = false;
    }

    //Exits the game
    public void exitGame()
    {
        Application.Quit();
    }
}
