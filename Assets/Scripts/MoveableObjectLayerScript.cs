using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObjectLayerScript : MonoBehaviour
{
    GameObject Player = null; 
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Jeremy");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y > Player.transform.position.y)
        {
            this.GetComponent<SpriteRenderer>().sortingOrder = 4;
        }
        else if (this.transform.position.y < Player.transform.position.y)
        {
            this.GetComponent<SpriteRenderer>().sortingOrder = 6;
        }
    }
}
