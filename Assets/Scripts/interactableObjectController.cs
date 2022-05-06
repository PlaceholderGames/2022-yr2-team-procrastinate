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
    [SerializeField] bool foodRecharging;
    [SerializeField] int foodItem;
    SpriteRenderer spriteRenderer;

    Canvas popUpCanvas;
    SpriteRenderer foodItemRenderer;
    Sprite burgerSprite;
    Sprite hotDogSprite;
    Sprite pizzaSliceSprite;
    List<Sprite> foodSpriteList = new List<Sprite>();

    CharacterController characterController;


    public enum objectType
    {
        FOODBIN
    }

    public enum foodType
    {
        EMPTY,
        BURGER,
        HOTDOG,
        PIZZASLICE
    }

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

        this.

        foodItemRenderer = GameObject.Find(this.gameObject.name).transform.GetChild(1).GetComponent<SpriteRenderer>();
        foodItem = generateFoodItem();
        foodRecharging = false;
        //Food sprites used
        burgerSprite = Resources.Load("Textures/foodBurger", typeof(Sprite)) as Sprite;
        hotDogSprite = Resources.Load("Textures/foodHotDog", typeof(Sprite)) as Sprite;
        pizzaSliceSprite = Resources.Load("Textures/foodPizzaSlice", typeof(Sprite)) as Sprite;
        foodSpriteList.Add(pizzaSliceSprite);
        foodSpriteList.Add(hotDogSprite);
        foodSpriteList.Add(burgerSprite);


        if (foodItem > (int)foodType.PIZZASLICE)
        {
            foodItem = 0;
            foodItemRenderer.sprite = null;
        }
        else if (foodItem > 0)
        {
            foodItemRenderer.sprite = foodSpriteList[foodItem-1];
        }



        popUpCanvas = this.gameObject.transform.GetChild(0).GetComponent<Canvas>();


        characterController = GameObject.Find("Jeremy").GetComponent<CharacterController>();
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
        if(popUpCanvas == null)
        {
            popUpCanvas = this.gameObject.transform.GetChild(0).GetComponent<Canvas>();
        }
        if(characterController == null)
        {
            characterController = GameObject.Find("Jeremy").GetComponent<CharacterController>();
        }

        //If foodItem is EMPTY then the player pressing E will start the recharge.
        //An empty bin will NOT recharge unless prompted by the player (unless set to empty after eating something from it)
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Player eats the food, which heals them
            eatFood();
            //Setting the current food item to empty so that the player can't spam eat the food to heal to full.
            //using ENUMs to make it easier to read.
            foodItem = (int)foodType.EMPTY;
            foodItemRenderer.sprite = null;
            foodRecharging = true;
            StartCoroutine(rechargeFood());
        }
    }

    //Resets the food item after 30 seconds.
    IEnumerator rechargeFood()
    {
        yield return new WaitForSeconds(30);
        foodItem = generateFoodItem();
        print("FoodItem: " + foodItem);
        if (foodItem > (int)foodType.PIZZASLICE)
        {
            foodItem = 0;
            foodItemRenderer.sprite = null;
        }
        else if (foodItem > 0)
        {
            foodItemRenderer.sprite = foodSpriteList[foodItem-1];
        }
        foodRecharging = false;
    }

    int generateFoodItem()
    {
        return Random.Range(0, 7);
    }

    //Heals the player
    void eatFood()
    {
        switch(foodItem)
        {
            case (int)foodType.EMPTY:
                print("Bin's empty!");
                break;
            case (int)foodType.BURGER:
                characterController.healPlayer(30);
                break;
            case (int)foodType.HOTDOG:
                characterController.healPlayer(20);
                break;
            case (int)foodType.PIZZASLICE:
                characterController.healPlayer(10);
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Open the bin
        if (collision.gameObject.tag == "Player" && open == false)
        {
            open = true;
            popUpCanvas.enabled = true;
            spriteRenderer.sprite = spriteList[Random.Range(0, 2)];
            foodItemRenderer.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //close the bin
        spriteRenderer.sprite = startingSprite;
        open = false;
        popUpCanvas.enabled = false;
        foodItemRenderer.enabled = false;
    }
}
