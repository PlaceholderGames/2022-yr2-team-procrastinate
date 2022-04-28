using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactableObjectController : MonoBehaviour
{
    [SerializeField] Sprite startingSprite;
    [SerializeField] Sprite openSprite;
    [SerializeField] Sprite openWithLidSprite;
    [SerializeField] List<Sprite> spriteList = new List<Sprite>();
    [SerializeField] bool open;
    SpriteRenderer spriteRenderer;

    Canvas popUpCanvas;



    enum objectType
    {
        FOODBIN
    };

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        startingSprite = spriteRenderer.sprite;
        openSprite = Resources.Load("Textures/binOpenedNoLid", typeof(Sprite)) as Sprite;
        openWithLidSprite = Resources.Load("Textures/binOpenedWithLid", typeof(Sprite)) as Sprite;
        spriteList.Add(openSprite);
        spriteList.Add(openWithLidSprite);
        open = false;

        popUpCanvas = this.gameObject.transform.GetChild(0).GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        //Making sure the sprites are loaded
        if (openSprite == null)
        {
            openSprite = Resources.Load("Textures/binOpenedNoLid", typeof(Sprite)) as Sprite;
        }
        if (openWithLidSprite == null)
        {
            openWithLidSprite = Resources.Load("Textures/binOpenedWithLid", typeof(Sprite)) as Sprite;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && open == false)
        {
            open = true;
            popUpCanvas.enabled = true;
            spriteRenderer.sprite = spriteList[Random.Range(0, 2)];
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //close the bin
        spriteRenderer.sprite = startingSprite;
        open = false;
        popUpCanvas.enabled = false;
    }
}
