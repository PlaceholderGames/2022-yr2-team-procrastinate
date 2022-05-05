using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2End : MonoBehaviour
{
    [SerializeField] public bool doorUnlocked;

    CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        doorUnlocked = false;
        characterController = GameObject.Find("Jeremy").GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && doorUnlocked)
        {
            SceneManager.LoadScene("Level1", LoadSceneMode.Single);
        }
    }
}
