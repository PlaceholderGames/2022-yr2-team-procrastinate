using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TaskListController : MonoBehaviour
{
    GameObject TaskListObject = null;
    GameObject TaskListContentObject = null;
    List<GameObject> TaskList = new List<GameObject>();

    [SerializeField] bool forceEmptyList;
    [SerializeField] bool level1ListItemsCleared;
    // Start is called before the first frame update
    void Start()
    {
        TaskListObject = GameObject.Find("TaskList");
        TaskListContentObject = TaskListObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject;
        level1ListItemsCleared = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (TaskListObject == null)
        {
            TaskListObject = GameObject.Find("TaskList"); TaskListObject = GameObject.Find("TaskList");
        }

        if (forceEmptyList)
        {
            clearList();
        }

        if (SceneManager.GetActiveScene().name == "Level2" && !level1ListItemsCleared)
        {
            print("Level 1 list items cleared!");
            clearList();
            level1ListItemsCleared = true;
            for (int i = 0; i < GameObject.Find("Tasks").transform.childCount; i++)
            {
                GameObject.Find("Tasks").transform.GetChild(i).GetComponent<killTaskController>().setupTask();
            }
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
            print("TaskList Count = 0");
            prefabToSpawn.transform.parent = TaskListObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0);
            prefabToSpawn.transform.localScale = new Vector3(1, 0.5f, 0.5f);
            
            if (SceneManager.GetActiveScene().name == "Level2")
            {
                prefabToSpawn.transform.localPosition = prefabToSpawn.transform.parent.localPosition + new Vector3(125, -25, 0);
            }
            else
            {
                prefabToSpawn.transform.localPosition = prefabToSpawn.transform.parent.localPosition + new Vector3(0, -25, 0);
            }
            prefabToSpawn.name = "Task" + TaskID;
            prefabToSpawn.transform.GetChild(1).GetComponent<TMP_Text>().text = name;
            prefabToSpawn.transform.GetChild(2).GetComponent<TMP_Text>().text = description;
        }
        else if (TaskList.Count() >= 0)
        {
            TaskID = TaskList.Count();
            TaskList.Add(prefabToSpawn);
            print("TaskListObject:" + TaskListObject);
            print("TaskList Count = \"> 0\"");
            prefabToSpawn.transform.parent = TaskListObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0);
            prefabToSpawn.transform.localScale = new Vector3(1, 0.5f, 0.5f);
            if (SceneManager.GetActiveScene().name == "Level2")
            {
                if (TaskID == 1)
                {
                    prefabToSpawn.transform.localPosition = prefabToSpawn.transform.parent.localPosition + new Vector3(125, (-70), 0);
                }
                else
                {
                    prefabToSpawn.transform.localPosition = prefabToSpawn.transform.parent.localPosition + new Vector3(125, (-39f + (-39f * (TaskID))), 0);
                }
            }
            else
            {
                if (TaskID == 1)
                {
                    prefabToSpawn.transform.localPosition = prefabToSpawn.transform.parent.localPosition + new Vector3(0, (-70), 0);
                }
                else
                {
                    prefabToSpawn.transform.localPosition = prefabToSpawn.transform.parent.localPosition + new Vector3(0, (-39f + (-39f * (TaskID))), 0);
                }
            }
            
            
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


    public void clearList()
    {
        for (int i = 0; i < TaskList.Count(); i++)
        {
            Destroy(TaskListContentObject.transform.GetChild(i).gameObject);
            
        }
        TaskList.Clear();
    }

}
