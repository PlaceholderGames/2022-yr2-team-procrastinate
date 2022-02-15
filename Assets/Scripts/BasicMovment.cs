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
        Vector3 movment = new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"), 0.0f);
        transform.position = transform.position + movment * Time.deltaTime;
    }
}
