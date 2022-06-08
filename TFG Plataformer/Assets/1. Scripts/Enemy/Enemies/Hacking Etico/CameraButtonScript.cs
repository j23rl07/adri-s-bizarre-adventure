using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraButtonScript : MonoBehaviour
{
    [SerializeField] private Sprite buttonOnSprite;
    [SerializeField] private Sprite buttonOffSprite;
    [SerializeField] private GameObject visionRange;
    [SerializeField] private GameObject hintText;
    private GameObject player;

    private bool hasAccess;
    private bool isOff;


    void Start()
    {
        hasAccess = false;
        isOff = false;
        GetComponent<SpriteRenderer>().sprite = buttonOnSprite;
        player = GameObject.FindGameObjectWithTag("Player");
        if (hintText != null)
            hintText.SetActive(false);
    }
    void Update()
    {
        if (hasAccess && Input.GetKeyDown(KeyCode.E))
        {
            TurnOff();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isOff)
        {
            hasAccess = true;
            if (hintText != null)
                hintText.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isOff)
        {
            hasAccess = false;
            if (hintText != null)
                hintText.gameObject.SetActive(false);
        }
    }

    private void TurnOff()
    {
        if (!isOff)
        {
            isOff = true;
            visionRange.SetActive(false);
            GetComponent<SpriteRenderer>().sprite = buttonOffSprite;
            if (hintText != null)
                hintText.gameObject.SetActive(false);

        }
    }
}
