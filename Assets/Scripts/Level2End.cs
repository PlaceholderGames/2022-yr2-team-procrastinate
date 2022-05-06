using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2End : MonoBehaviour
{
    [SerializeField] public bool doorUnlocked;

    GameObject objectivePointer;

    CharacterController characterController;
    TaskListController taskListController;
    // Start is called before the first frame update
    void Start()
    {
        doorUnlocked = false;
        characterController = GameObject.Find("Jeremy").GetComponent<CharacterController>();

        objectivePointer = GameObject.Find("ObjectivePointer");
        taskListController = GameObject.Find("TaskList").GetComponent<TaskListController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (characterController == null)
        {
            characterController = GameObject.Find("Jeremy").GetComponent<CharacterController>();
        }
        if (objectivePointer == null)
        {
            objectivePointer = GameObject.Find("ObjectivePointer");
        }
        if (taskListController)
        {
            taskListController = GameObject.Find("TaskList").GetComponent<TaskListController>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && doorUnlocked)
        {
            objectivePointer.GetComponent<SpriteRenderer>().enabled = false;
            //taskListController.clearList();
            SceneManager.LoadScene("Level1", LoadSceneMode.Single);
            GameObject.Find("Jeremy").transform.position = new Vector3(-13.8400002f, 0.689999998f, 0);
        }
    }
}
