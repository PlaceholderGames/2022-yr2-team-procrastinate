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
    [SerializeField] public bool level1ListItemsCleared;
    [SerializeField] public bool level2ListItemsCleared;
    [SerializeField] public bool level1Reset;
    [SerializeField] public bool level2Reset;
    [SerializeField] int TaskID = 0;
    // Start is called before the first frame update
    void Start()
    {
        TaskListObject = GameObject.Find("TaskList");
        TaskListContentObject = TaskListObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject;
        level1ListItemsCleared = false;
        level2ListItemsCleared = false;
        level1Reset = false;
        level2Reset = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (level1Reset || level2Reset)
        {
            TaskID = 0;
        }

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
            
            //while (TaskListContentObject.transform.childCount != 0)
            //{
            //    clearList();
            //}
            if (TaskListContentObject.transform.childCount == 0)
            {
                level1ListItemsCleared = true;
                for (int i = 0; i < GameObject.Find("TasksLevel2").transform.childCount; i++)
                {
                    GameObject.Find("TasksLevel2").transform.GetChild(i).GetComponent<killTaskController>().setupTask();
                }
            }
            
        }

        if (SceneManager.GetActiveScene().name == "Level2" && level2Reset && !level2ListItemsCleared)
        {
            print("Level 2 list items cleared!");
            clearList();
            
            if (TaskListContentObject.transform.childCount == 0)
            {
                level2ListItemsCleared = true;
                for (int i = 0; i < GameObject.Find("TasksLevel2").transform.childCount; i++)
                {
                    GameObject.Find("TasksLevel2").transform.GetChild(i).GetComponent<killTaskController>().setupTask();
                }
                level2Reset = false;
            }
            
            
        }

        if (SceneManager.GetActiveScene().name == "Level1" && !level2ListItemsCleared)
        {
            print("Level 1 list items cleared!");
            clearList();
            level2ListItemsCleared = true;

            GameObject.Find("TasksLevel1").transform.GetChild(0).GetComponent<TaskController>().setupTask();
            GameObject.Find("TasksLevel1").transform.GetChild(1).GetComponent<TestTask2>().setupTask();
        }
    }

    public int addItemToList(GameObject prefab, string name, string description)
    {

        GameObject prefabToSpawn = Instantiate(prefab);
        while (TaskListObject == null)
        {
            TaskListObject = GameObject.Find("TaskList"); TaskListObject = GameObject.Find("TaskList");
        }
        if (TaskListContentObject.transform.childCount == 0)
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
            else if (SceneManager.GetActiveScene().name == "CrackOfDawn")
            {
                prefabToSpawn.transform.localPosition = prefabToSpawn.transform.parent.localPosition + new Vector3(0, -25, 0);
            }
            else
            {
                prefabToSpawn.transform.localPosition = prefabToSpawn.transform.parent.localPosition + new Vector3(125, -25, 0);
            }
            prefabToSpawn.name = "Task" + TaskID;
            prefabToSpawn.transform.GetChild(1).GetComponent<TMP_Text>().text = name;
            prefabToSpawn.transform.GetChild(2).GetComponent<TMP_Text>().text = description;
        }
        else if (TaskListContentObject.transform.childCount > 0)
        {
            TaskID = TaskListContentObject.transform.childCount;
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
            else if (SceneManager.GetActiveScene().name == "Level1")
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

        print("New Tasks ID: " + TaskID);
        return TaskID;
    }


    public void clearList()
    {
        for (int i = 0; i < TaskList.Count(); i++)
        {
            Destroy(TaskListContentObject.transform.GetChild(i).gameObject);
            
        }
        TaskList.Clear();
        Debug.LogWarning("List Cleared!");
    }

}
