using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    CharacterController characterController;
    Canvas mainMenuCanvas;


    //Controls
    Canvas controlsCanvas;

    //Credits
    Canvas creditsCanvas;

    // Start is called before the first frame update
    void Start()
    {
        //Pause the game on start so nothing happens in the background until the game is started
        Time.timeScale = 0;

        mainMenuCanvas = GetComponent<Canvas>();

        controlsCanvas = mainMenuCanvas.transform.GetChild(3).GetComponent<Canvas>();
        creditsCanvas = mainMenuCanvas.transform.GetChild(4).GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Starts the game
    public void startGame()
    {
        Time.timeScale = 1;
        mainMenuCanvas.enabled = false;
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

    //Opens the credits menu
    public void menuCreditsOpen()
    {
        creditsCanvas.enabled = true;
    }

    //Closes the credits menu
    public void menuCreditsBack()
    {
        creditsCanvas.enabled = false;
    }

    //Exits the game
    public void exitGame()
    {
        Application.Quit();
    }
}
