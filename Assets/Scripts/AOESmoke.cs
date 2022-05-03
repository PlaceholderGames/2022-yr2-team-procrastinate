using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AOESmoke : MonoBehaviour
{
    Canvas SmokeCanvas;
    Image layer1;
    Image layer2;
    Image layer3;
    float degrees;

    [SerializeField] public float radius;

    [SerializeField] bool canDamage;

    CharacterController characterController;


    // Start is called before the first frame update
    void Start()
    {
        SmokeCanvas = this.transform.GetChild(0).GetComponent<Canvas>();
        layer1 = SmokeCanvas.transform.GetChild(0).GetComponent<Image>();
        layer2 = SmokeCanvas.transform.GetChild(1).GetComponent<Image>();
        layer3 = SmokeCanvas.transform.GetChild(2).GetComponent<Image>();

        characterController = GameObject.Find("Jeremy").GetComponent<CharacterController>();

        canDamage = true;

        degrees = 0f;
        radius = 4.0f;

        this.transform.localScale = new Vector3(radius / 100f, radius / 100f, 0f);

        StartCoroutine(fadeSmoke());
    }

    // Update is called once per frame
    void Update()
    {
        degrees += 0.025f;
        layer1.transform.rotation = Quaternion.Euler(Vector3.forward * degrees);
        layer2.transform.rotation = Quaternion.Euler(Vector3.forward * -degrees);
        layer3.transform.rotation = Quaternion.Euler(Vector3.forward * degrees);
    }

    IEnumerator rechargeDamage()
    {
        yield return new WaitForSeconds(1.0f);
        canDamage = true;
    }
    //Destroys the smoke after 30 seconds
    IEnumerator fadeSmoke()
    {
        yield return new WaitForSeconds(30.0f);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && canDamage)
        {
            characterController.damagePlayer(10.0f);
            canDamage = false;
            StartCoroutine(rechargeDamage());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && canDamage)
        {
            characterController.damagePlayer(10.0f);
            canDamage = false;
            StartCoroutine(rechargeDamage());
        }
    }
}
