using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovment : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"), 0.0f);
        transform.position = transform.position + movement * Time.deltaTime;
        
        //flips the characters sprite so they face the direction they're walking
        if (movement.x < 0)
        {
            GameObject.Find("Jeremy").GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (movement.x > 0)
        {
            GameObject.Find("Jeremy").GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
