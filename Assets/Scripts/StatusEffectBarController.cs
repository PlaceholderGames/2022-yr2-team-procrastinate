using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatusEffectBarController : MonoBehaviour
{
    GameObject statusEffectBar;
    [SerializeField] List<GameObject> StatusEffects = new List<GameObject>();
    [SerializeField] List<GameObject> effectsToFlash = new List<GameObject>();
    int removedObjectIndex;

    [SerializeField] float timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = 0;
        statusEffectBar = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //if (SceneManager.GetActiveScene().name == "Level2" && statusEffectBar.transform.childCount != 0)
        //{
        //    print("Local Pos: " + StatusEffects[0].transform.localPosition);
        //}
        timeElapsed += Time.deltaTime;

        if (timeElapsed < 0.25f && effectsToFlash.Count > 0)
        {
            //print("Effects to flash count: " + effectsToFlash.Count);
            for (int i = 0; i < effectsToFlash.Count; i++)
            {
                if (effectsToFlash[i].gameObject != null)
                {
                    effectsToFlash[i].gameObject.GetComponent<Image>().color = Color.red;
                }
                
            }
        }
        else if (effectsToFlash.Count > 0)
        {
            for (int i = 0; i < effectsToFlash.Count; i++)
            {
                if (effectsToFlash[i].gameObject != null)
                {
                    effectsToFlash[i].gameObject.GetComponent<Image>().color = Color.white;
                }
            }
        }
        
        if (timeElapsed > 0.5f)
        {
            timeElapsed = 0.0f;
        }

        
    }


    public GameObject addStatusEffect(string imageName)
    {
        GameObject instantiatedObject = Instantiate(Resources.Load<GameObject>("Prefabs/StatusEffects/StatusEffectPrefab"), new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation) as GameObject;
        instantiatedObject.GetComponent<Image>().sprite = Resources.Load(imageName, typeof(Sprite)) as Sprite;

        StatusEffects.Add(instantiatedObject);

        instantiatedObject.transform.parent = statusEffectBar.transform;

        //Setting the anchor points
        instantiatedObject.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0.5f);
        instantiatedObject.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.5f);
        instantiatedObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        //Setting the local position
        if (StatusEffects.Count == 1)
        {
            instantiatedObject.transform.localPosition = new Vector3(75, 0, 0);
        }
        else if (StatusEffects.Count == 2)
        {
            instantiatedObject.transform.localPosition = new Vector3(-25, 0, 0);
        }else if (StatusEffects.Count == 3)
        {
            instantiatedObject.transform.localPosition = new Vector3(-125, 0, 0);
        }
        
        

        instantiatedObject.name = "StatusEffect" + StatusEffects.IndexOf(instantiatedObject);
        
        return instantiatedObject;
    }

    public void removeStatusEffect(GameObject objectToRemove)
    {
        removedObjectIndex = StatusEffects.IndexOf(objectToRemove);
        StatusEffects.Remove(objectToRemove);
        effectsToFlash.Remove(objectToRemove);

        
        Destroy(statusEffectBar.transform.GetChild(removedObjectIndex).gameObject);
        

        for (int i = 0; i < StatusEffects.Count; i++)
        {
            StatusEffects[i].transform.localPosition = new Vector3(75 - (i * 100), 0, 0);
        }
    }

    public void flashEffect(GameObject objectToFlash)
    {
        effectsToFlash.Add(objectToFlash);
    }
}
