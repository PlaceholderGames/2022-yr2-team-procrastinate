using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskController : MonoBehaviour
{
    [SerializeField] GameObject TaskObject = null;
    [SerializeField] TMP_Text TotalItemsDeliveredText;
    [SerializeField] int totalItemsDelivered;
    [SerializeField] bool completed;
    [SerializeField] GameObject TestTaskTextPrefab;
    TaskListController TaskListScript = null;
    GameControllerLevel1 GameController = null;
    [SerializeField] string taskName;
    [SerializeField] string taskDescription;
    [SerializeField] float taskProgress;
    [SerializeField] int TaskID = 0;
    [SerializeField] AudioClip wrongDeliveryZone = null;
    [SerializeField] AudioClip correctDeliveryZone = null;

    CharacterController characterController;

    [SerializeField] public AudioSource audioSource;

    GameObject TaskList = null;
    // Start is called before the first frame update
    void Start()
    {
        GameController = GameObject.Find("GameControllerLevel1").GetComponent<GameControllerLevel1>();
        characterController = GameObject.Find("Jeremy").GetComponent<CharacterController>();

        taskName = "Deliver Toys";
        taskDescription = "Deliver crates of toys to the delivery zone!";
        taskProgress = 0.0f;
        completed = false;
        totalItemsDelivered = 0;
        TaskList = GameObject.Find("TaskList");
        print("TaskList: " + TaskList);
        TaskListScript = TaskList.GetComponent("TaskListController") as TaskListController;
        print("TaskListScript: " + TaskListScript);
        TaskID = TaskListScript.addItemToList(TestTaskTextPrefab, taskName, taskDescription);
        print("TaskID: " + TaskID);

        //Adds an audio source then loads the audio clip
        audioSource = this.gameObject.GetComponent<AudioSource>();

        wrongDeliveryZone = Resources.Load("Audio/WrongDeliveryZone") as AudioClip;
        correctDeliveryZone = Resources.Load("Audio/528730__alexhanj__ping") as AudioClip;


        //GameObject TestTaskTextObject = Instantiate(TestTaskTextPrefab);
        //should set the parent of the new prefab to the Content part of the scroll view for the tasklist
        //TestTaskTextObject.transform.parent = TaskList.transform.GetChild(0);
        //TestTaskTextObject.transform.parent = TestTaskTextObject.transform.parent.GetChild(0);
        //TestTaskTextObject.transform.parent = TestTaskTextObject.transform.parent.GetChild(0);
        //TestTaskTextObject.transform.localScale = new Vector3(1, 1, 1);
        //TestTaskTextObject.transform.localPosition = TestTaskTextObject.transform.parent.localPosition + new Vector3(6, 0, 0);
        //TestTaskTextObject.name = "TestTask-TaskList";

        TaskObject = GameObject.Find("Task" + TaskID);
        TotalItemsDeliveredText = GameObject.Find("Task" + TaskID).transform.GetChild(3).GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (wrongDeliveryZone == null)
        {
            print("WrongDeliveryZone is null");
            wrongDeliveryZone = Resources.Load("Audio/WrongDeliveryZone") as AudioClip;
            //wrongDeliveryZone = Resources.Load("F:/Unity/Projects/2022-yr2-team-procrastinate/Assets/Audio/WrongDeliveryZone.wav") as AudioClip;
        }

        if (completed == true)
        {
            print("Completed!");
            TaskObject.transform.GetChild(1).GetComponent<TMP_Text>().color = new Color(0, 255, 0);
            TaskObject.transform.GetChild(2).GetComponent<TMP_Text>().color = new Color(0, 255, 0);
            TaskObject.transform.GetChild(4).GetComponent<Image>().color = new Color(0, 255, 0);
            GameController.taskComplete();
            characterController.addToPaycheque(totalItemsDelivered * 15);
            Debug.LogWarning("Adding to Paycheque!");
            //print("Adding to Paycheque!");
            this.GetComponent<TaskController>().enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        print(collider + " with tag{" + collider.tag + "} Entered the box!");
        if (completed != true && collider.tag == "SuppliesToys")
        {
            //destroys the box that collided with the task area
            print("Succesfully delivered " + collider.name);
            totalItemsDelivered++;
            //This task has 5 progress points so it's (totalItemsDelivered * 100) / 5
            TotalItemsDeliveredText.text = "Progress: " + ((totalItemsDelivered * 100) / 5) + "%";
            Destroy(collider.gameObject);
            audioSource.clip = (AudioClip)correctDeliveryZone;
            audioSource.Play();
            print("Playing audio");
            if (totalItemsDelivered == 5)
            {
                completed = true;
            }
        }
        else if (collider.tag != "SuppliesToys" && collider.tag != "Player")
        {
            //audioSource.clip = Resources.Load("Assets/Audio/WrongDeliveryZone.wav") as AudioClip;
            audioSource.clip = (AudioClip)wrongDeliveryZone;
            audioSource.Play();
        }

        
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        Debug.LogWarning(collider + " with tag{" + collider.tag + "} Exited the box!");
    }

}
