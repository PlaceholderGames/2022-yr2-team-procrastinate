using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskListController : MonoBehaviour
{
    GameObject TaskListObject = null;
    List<GameObject> TaskList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        TaskListObject = GameObject.Find("TaskList");
    }

    // Update is called once per frame
    void Update()
    {
        if (TaskListObject == null)
        {
            TaskListObject = GameObject.Find("TaskList"); TaskListObject = GameObject.Find("TaskList");
        }
    }

    public int addItemToList(GameObject prefab, string name, string description)
    {
        int TaskID = 0;
        GameObject prefabToSpawn = Instantiate(prefab);
        while (TaskListObject == null)
        {
            TaskListObject = GameObject.Find("TaskList"); TaskListObject = GameObject.Find("TaskList");
        }
        if (TaskList.Count() == 0)
        {
            TaskID = 0;
            TaskList.Add(prefabToSpawn);
            print("TaskListObject:" + TaskListObject);
            prefabToSpawn.transform.parent = TaskListObject.transform.GetChild(0);
            prefabToSpawn.transform.parent = prefabToSpawn.transform.parent.GetChild(0);
            prefabToSpawn.transform.parent = prefabToSpawn.transform.parent.GetChild(0);
            prefabToSpawn.transform.localScale = new Vector3(1, 0.5f, 0.5f);
            prefabToSpawn.transform.localPosition = prefabToSpawn.transform.parent.localPosition + new Vector3(1, -15, 0);
            prefabToSpawn.name = "Task" + TaskID;
            prefabToSpawn.transform.GetChild(1).GetComponent<TMP_Text>().text = name;
            prefabToSpawn.transform.GetChild(2).GetComponent<TMP_Text>().text = description;
        }
        else if (TaskList.Count() >= 0)
        {
            TaskID = TaskList.Count();
            TaskList.Add(prefabToSpawn);
            print("TaskListObject:" + TaskListObject);
            prefabToSpawn.transform.parent = TaskListObject.transform.GetChild(0);
            prefabToSpawn.transform.parent = prefabToSpawn.transform.parent.GetChild(0);
            prefabToSpawn.transform.parent = prefabToSpawn.transform.parent.GetChild(0);
            prefabToSpawn.transform.localScale = new Vector3(1, 0.5f, 0.5f);
            prefabToSpawn.transform.localPosition = prefabToSpawn.transform.parent.localPosition + new Vector3(1, -45, 0);
            prefabToSpawn.name = "Task" + TaskID;
            prefabToSpawn.transform.GetChild(1).GetComponent<TMP_Text>().text = name;
            prefabToSpawn.transform.GetChild(2).GetComponent<TMP_Text>().text = description;
        }
        //else if (TaskList.Count() > 0)
        //{
        //    TaskID = TaskList.Count();
        //    TaskList.Add(prefabToSpawn);
        //    prefabToSpawn.transform.parent = TaskListObject.transform.GetChild(0);
        //    prefabToSpawn.transform.parent = prefabToSpawn.transform.parent.GetChild(0);
        //    prefabToSpawn.transform.parent = prefabToSpawn.transform.parent.GetChild(0);
        //    prefabToSpawn.transform.localScale = new Vector3(1, 1, 1);
        //    prefabToSpawn.transform.localPosition = TaskList[(TaskID - 1)].transform.localPosition + new Vector3(0, -25, 0);
        //    prefabToSpawn.name = "Task" + TaskID;
        //    prefabToSpawn.transform.GetChild(1).GetComponent<TMP_Text>().text = name;
        //    prefabToSpawn.transform.GetChild(2).GetComponent<TMP_Text>().text = description;
        //}

        print(TaskID);
        return TaskID;
    }

}
