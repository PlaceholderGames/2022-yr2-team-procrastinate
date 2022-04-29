using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerLevel1 : MonoBehaviour
{
    CharacterController characterController;
    TaskListController taskListController;
    [SerializeField] int tasksCompleted;
    [SerializeField] int totalTasks;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GameObject.Find("Jeremy").GetComponent<CharacterController>();
        taskListController = GameObject.Find("TaskList").GetComponent<TaskListController>();

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
            //call a function in the FrontDoor wall of the shop that makes it load the next level when the player touches it!
        }
    }

    public void taskComplete()
    {
        tasksCompleted++;
    }
}
