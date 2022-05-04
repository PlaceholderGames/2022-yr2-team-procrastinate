using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeController : MonoBehaviour
{
    GameObject player = null;
    GameObject home = null;
    SpriteRenderer homeSprite = null;
    string homeClosedTexture = null;
    string homeOpenTexture = null;
    [SerializeField] float distanceToPlayer = 0;
    Level2End endLevelCollider;

    [SerializeField] bool doorUnlocked;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Jeremy");
        home = this.gameObject;
        homeSprite = home.GetComponent<SpriteRenderer>();
        homeClosedTexture = "Textures/house1";
        homeOpenTexture = "Textures/house1_doorless";

        endLevelCollider = this.gameObject.transform.GetChild(2).GetComponent<Level2End>();

        doorUnlocked = false;
    }

    // Update is called once per frame
    void Update()
    {
        //This is in case doorUnlocked gets set manually
        if (doorUnlocked == true)
        {
            unlockDoor();
        }

        distanceToPlayer = Vector3.Distance(player.transform.position, home.transform.position);

        if (distanceToPlayer <= 2.75 && doorUnlocked)
        {
            home.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(homeOpenTexture);
        }
        else
        {
            home.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(homeClosedTexture);
        }
    }

    public void unlockDoor()
    {
        this.gameObject.transform.GetChild(1).gameObject.GetComponent<BoxCollider2D>().enabled = false;
        doorUnlocked = true;
        endLevelCollider.doorUnlocked = true;
    }

}
