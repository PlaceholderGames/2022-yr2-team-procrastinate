using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    CharacterController characterController;
    GameObject pauseMenuGameObject;
    Canvas pauseMenuCanvas;
    Button resumeButton;
    Button exitButton;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenuGameObject = this.gameObject;
        pauseMenuCanvas = pauseMenuGameObject.GetComponent<Canvas>();

        characterController = GameObject.Find("Jeremy").GetComponent<CharacterController>();

        //Finds the button component by searching through the children of the PauseMenu canvas
        resumeButton = pauseMenuGameObject.transform.GetChild(1).transform.GetChild(1).GetComponent<Button>();
        exitButton = pauseMenuGameObject.transform.GetChild(1).transform.GetChild(2).GetComponent<Button>();


    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //P is temporary since Escape ends the editor play mode.
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0;
            pauseMenuCanvas.enabled = true;
            characterController.gamePaused = true;

            if (characterController.miniMapOpen)
            {
                characterController.toggleMinimap();
            }
        }
    }

    public void resumeGame()
    {
        Time.timeScale = 1;
        pauseMenuCanvas.enabled = false;
        characterController.gamePaused = false;

        if (characterController.miniMapOpen)
        {
            characterController.toggleMinimap();
        }
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
