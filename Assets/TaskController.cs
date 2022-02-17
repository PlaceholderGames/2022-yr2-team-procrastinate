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
    [SerializeField] string taskName;
    [SerializeField] string taskDescription;
    [SerializeField] float taskProgress;
    [SerializeField] int TaskID = 0;

    GameObject TaskList = null;
    // Start is called before the first frame update
    void Start()
    {
        taskName = "Deliver Toys";
        taskDescription = "Deliver crates of toys to the delivery zone!";
        taskProgress = 0.0f;
        completed = false;
        totalItemsDelivered = 0;
        TaskList = GameObject.Find("TaskList");
        TaskListScript = TaskList.GetComponent("TaskListController") as TaskListController;
        TaskID = TaskListScript.addItemToList(TestTaskTextPrefab, taskName, taskDescription);
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
            if (totalItemsDelivered == 5)
            {
                completed = true;
            }
        }

        if (completed == true)
        {
            print("Completed!");
            TaskObject.transform.GetChild(1).GetComponent<TMP_Text>().color = new Color(0, 255, 0);
            TaskObject.transform.GetChild(2).GetComponent<TMP_Text>().color = new Color(0, 255, 0);
            TaskObject.transform.GetChild(4).GetComponent<Image>().color = new Color(0, 255, 0);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        print(collider + " with tag{" + collider.tag + "} Exited the box!");
    }
}
