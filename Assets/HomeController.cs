using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeController : MonoBehaviour
{
    GameObject player = null;
    GameObject home = null;
    SpriteRenderer homeSprite = null;
    string homeClosedTexture = null;
    string homeOpenTexture = null;
    [SerializeField] float distanceToPlayer = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Jeremy");
        home = this.gameObject;
        homeSprite = home.GetComponent<SpriteRenderer>();
        homeClosedTexture = "Textures/house1";
        homeOpenTexture = "Textures/house1_doorless";
    }

    // Update is called once per frame
    void Update()
    {

        print("ClosedSprite: " + homeClosedTexture);
        distanceToPlayer = Vector3.Distance(player.transform.position, home.transform.position);

        if (distanceToPlayer <= 10.1)
        {
            home.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(homeOpenTexture);
            print("Home Sprite = Open");
        }
        else
        {
            home.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(homeClosedTexture);
            //homeSprite.sprite = homeClosedTexture;
            print("Home Sprite = Closed");
        }
    }
}
