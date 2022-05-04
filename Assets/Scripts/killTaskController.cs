using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class killTaskController : MonoBehaviour
{
    [SerializeField] GameObject TaskObject = null;
    [SerializeField] TMP_Text TotalEnemiesKilledText;
    [SerializeField] int totalEnemiesKilled;
    [SerializeField] bool completed;
    [SerializeField] GameObject TaskTextPrefab;
    TaskListController TaskListScript = null;
    GameControllerLevel2 GameController = null;

    [SerializeField] string taskName;
    [SerializeField] string taskDescription;
    [SerializeField] int taskProgress;
    [SerializeField] int taskTarget;
    [SerializeField] int TaskID = 0;

    [SerializeField] public AudioSource audioSource;

    GameObject TaskList = null;

    public AIController.enemyType AIType = new AIController.enemyType();

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (completed == true)
        {
            print("Completed!");
            TaskObject.transform.GetChild(1).GetComponent<TMP_Text>().color = new Color(0, 255, 0);
            TaskObject.transform.GetChild(2).GetComponent<TMP_Text>().color = new Color(0, 255, 0);
            TaskObject.transform.GetChild(4).GetComponent<Image>().color = new Color(0, 255, 0);
            GameController.taskComplete();
        }
    }

    public void setupTask()
    {
        GameController = GameObject.Find("GameController").GetComponent<GameControllerLevel2>();

        CharacterController.enemyDied += enemyDied;

        //taskName = "Deliver Toys";
        //taskDescription = "Deliver crates of toys to the delivery zone!";
        taskProgress = 0;
        completed = false;
        totalEnemiesKilled = 0;
        TaskList = GameObject.Find("TaskList");
        print("TaskList: " + TaskList);
        TaskListScript = TaskList.GetComponent("TaskListController") as TaskListController;
        print("TaskListScript: " + TaskListScript);
        TaskID = TaskListScript.addItemToList(TaskTextPrefab, taskName, taskDescription);
        print("TaskID: " + TaskID);

        //Adds an audio source then loads the audio clip
        audioSource = this.gameObject.GetComponent<AudioSource>();


        TaskObject = GameObject.Find("Task" + TaskID);
        TotalEnemiesKilledText = GameObject.Find("Task" + TaskID).transform.GetChild(3).GetComponent<TMP_Text>();
    }

    public void enemyDied(AIController.enemyType type)
    {
        if (type == AIType)
        {
            taskProgress++;
            totalEnemiesKilled++;
            TotalEnemiesKilledText.text = "Progress: " + (totalEnemiesKilled + "/" + taskTarget);
        }
        if (totalEnemiesKilled >= taskTarget)
        {
            completed = true;
        }
        
    }

}
