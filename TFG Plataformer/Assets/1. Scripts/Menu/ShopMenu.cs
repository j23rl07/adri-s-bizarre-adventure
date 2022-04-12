using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] private GameObject hintText;
    [SerializeField] private GameObject shopCanvas;
    
    private bool shopAccess = false;


    private void Start()
    {
        hintText.SetActive(false);
        shopCanvas.SetActive(false);
    }

    void Update()
    {
        if (!PauseMenu.isPauseMenuOn)
        {
            if(shopAccess & Input.GetKeyDown(KeyCode.E))
            {
                Shop();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {

            hintText.gameObject.SetActive(true);
            shopAccess = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            hintText.gameObject.SetActive(false);
            shopAccess = false;
        }
    }

    public void Shop()
    {
        if (!shopCanvas.activeSelf)
        {
            Time.timeScale = 0;
            shopCanvas.SetActive(true);
            PauseMenu.isGamePaused = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if(shopCanvas.activeSelf)
        {
            Time.timeScale = 1;
            shopCanvas.SetActive(false);
            PauseMenu.isGamePaused = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }
}
