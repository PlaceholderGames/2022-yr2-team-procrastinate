using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundHouseController : MonoBehaviour
{
    [SerializeField] List<Sprite> sprites = new List<Sprite>();
    [SerializeField] bool isStore;



    // Start is called before the first frame update
    void Start()
    { 
        sprites.Add(Resources.Load("Textures/backgroundHouse1", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Textures/backgroundHouse2", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Textures/backgroundHouse3", typeof(Sprite)) as Sprite);
        sprites.Add(Resources.Load("Textures/storeCrackBarrel", typeof(Sprite)) as Sprite);

        //Randommly select what this buildings texture will be
        if (!isStore)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, 3)];
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
