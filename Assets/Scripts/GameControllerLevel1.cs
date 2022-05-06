using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerLevel1 : MonoBehaviour
{
    CharacterController characterController;
    TaskListController taskListController;
    FrontDoorController frontDoorController;
    [SerializeField] int tasksCompleted;
    [SerializeField] int totalTasks;
    [SerializeField] public bool levelCompleted;
    GameObject ObjectivePointer;

    [SerializeField] public bool SceneIsLoaded;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GameObject.Find("Jeremy").GetComponent<CharacterController>();
        taskListController = GameObject.Find("TaskList").GetComponent<TaskListController>();
        frontDoorController = GameObject.Find("FrontDoor").GetComponent<FrontDoorController>();

        tasksCompleted = 0;
        totalTasks = -1;
        //totalTasks = GameObject.Find("TaskList").transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).childCount;

        GameObject.Find("Jif_Bozos").transform.GetChild(0).GetComponent<Canvas>().worldCamera = GameObject.Find("Jeremy").transform.GetChild(0).GetComponent<Camera>();
        levelCompleted = false;
        ObjectivePointer = GameObject.Find("ObjectivePointer");

        SceneManager.sceneLoaded += sceneIsLoadedFunc;
    }

    // Update is called once per frame
    void Update()
    {
        totalTasks = GameObject.Find("TaskList").transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).childCount;
        if (tasksCompleted == totalTasks)
        {
            Debug.LogWarning("Level Completed, tasks completed: " + tasksCompleted + " total tasks: " + totalTasks);
            characterController.level1Complete();
            frontDoorController.unlockDoor();
            levelCompleted = true;
            ObjectivePointer.GetComponent<SpriteRenderer>().enabled = true;
            //characterController.payPlayer();
        }
        
    }

    public void taskComplete()
    {
        tasksCompleted++;
    }

    void sceneIsLoadedFunc(Scene scene, LoadSceneMode mode)
    {
        Debug.LogWarning("Scene " + scene.name + " loaded!");
        if (scene.name == "Level1")
        {
            Debug.LogWarning("Entered if?");
            SceneIsLoaded = true;
            print("SceneIsLoaded: " + SceneIsLoaded);
        }
    }
}
