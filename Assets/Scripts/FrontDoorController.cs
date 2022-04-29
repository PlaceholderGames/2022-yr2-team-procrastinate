using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FrontDoorController : MonoBehaviour
{
    [SerializeField] bool doorUnlocked;
    [SerializeField] string level2;

    // Start is called before the first frame update
    void Start()
    {
        doorUnlocked = false;
        level2 = "Level2";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Unlocks the door and allows the player to travel to the next level
    public void unlockDoor()
    {
        doorUnlocked = true;
    }

    void loadLevel2()
    {
        SceneManager.LoadScene(level2, LoadSceneMode.Single);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (doorUnlocked)
        {
            loadLevel2();
            //Moves Jeremy to the front of the store when the next level loads
            GameObject.Find("Jeremy").transform.position = new Vector3(-26.5f, 0.5f, 0f);
        }
    }
}
