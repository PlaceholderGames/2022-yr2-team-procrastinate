using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerLevel1 : MonoBehaviour
{
    CharacterController characterController;
    TaskListController taskListController;
    FrontDoorController frontDoorController;
    [SerializeField] int tasksCompleted;
    [SerializeField] int totalTasks;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GameObject.Find("Jeremy").GetComponent<CharacterController>();
        taskListController = GameObject.Find("TaskList").GetComponent<TaskListController>();
        frontDoorController = GameObject.Find("FrontDoor").GetComponent<FrontDoorController>();

        tasksCompleted = 0;
        totalTasks = GameObject.Find("TaskList").transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).childCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (totalTasks == 0 && tasksCompleted == 0)
        {
            totalTasks = GameObject.Find("TaskList").transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).childCount;
        }
        if (tasksCompleted == totalTasks)
        {
            frontDoorController.unlockDoor();
            characterController.payPlayer();
        }
    }

    public void taskComplete()
    {
        tasksCompleted++;
    }
}
