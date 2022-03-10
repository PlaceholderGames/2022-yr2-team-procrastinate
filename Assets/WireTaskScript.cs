using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireTaskScript : MonoBehaviour
{
    Canvas prompt = null;
    // Start is called before the first frame update
    void Start()
    {
        prompt = GameObject.Find("WireTaskPrompt").GetComponent<Canvas>();
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
