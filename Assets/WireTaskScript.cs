using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WireTaskScript : MonoBehaviour
{
    Canvas prompt = null;
    [SerializeField] GameObject TaskObject = null;
    [SerializeField] TMP_Text TotalItemsCompletedText;
    [SerializeField] int totalItemsCompleted;
    [SerializeField] bool completed;
    [SerializeField] GameObject TestTaskTextPrefab;
    TaskListController TaskListScript = null;
    [SerializeField] string taskName;
    [SerializeField] string taskDescription;
    [SerializeField] float taskProgress;
    [SerializeField] int TaskID = 0;
    //[SerializeField] AudioClip wrongDeliveryZone = null;
    //[SerializeField] AudioClip correctDeliveryZone = null;

    [SerializeField] public AudioSource audioSource;

    GameObject TaskList = null;
    // Start is called before the first frame update
    void Start()
    {
        prompt = GameObject.Find("WireTaskPrompt").GetComponent<Canvas>();

        taskName = "Fix The Wires";
        taskDescription = "Fix the wires in the electrical box to open the door to the store!";
        taskProgress = 0.0f;
        completed = false;
        totalItemsCompleted = 0;
        TaskList = GameObject.Find("TaskList");
        print("TaskList: " + TaskList);
        TaskListScript = TaskList.GetComponent("TaskListController") as TaskListController;
        print("TaskListScript: " + TaskListScript);
        TaskID = TaskListScript.addItemToList(TestTaskTextPrefab, taskName, taskDescription);
        print("TaskID: " + TaskID);

        //Adds an audio source then loads the audio clip
        audioSource = this.gameObject.GetComponent<AudioSource>();

        //wrongDeliveryZone = Resources.Load("Audio/WrongDeliveryZone") as AudioClip;
        //correctDeliveryZone = Resources.Load("Audio/528730__alexhanj__ping") as AudioClip;



        TaskObject = GameObject.Find("Task" + TaskID);
        TotalItemsCompletedText = GameObject.Find("Task" + TaskID).transform.GetChild(3).GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            prompt.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            prompt.enabled = false;
        }
    }
}
