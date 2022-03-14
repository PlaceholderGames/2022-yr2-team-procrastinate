using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObjectLayerScript : MonoBehaviour
{
    GameObject Player = null;
    [SerializeField] int orderInLayerModifier = 0;
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
            this.GetComponent<SpriteRenderer>().sortingOrder = (43 - orderInLayerModifier);
            if(this.transform.childCount > 0)
            {
                this.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = (44 - orderInLayerModifier);
            }
        }
        else if (this.transform.position.y < Player.transform.position.y)
        {
            this.GetComponent<SpriteRenderer>().sortingOrder = (56 - orderInLayerModifier);
            if (this.transform.childCount > 0)
            {
                this.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = (57 - orderInLayerModifier);
            }
        }
    }
}
