using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] private GameObject hintText;
    [SerializeField] private GameObject shopCanvas;
    [Header("Prices")]
    [SerializeField] private int precioCafe;
    [SerializeField] private int precioChuleta;
    
    private bool shopAccess = false;
    private GameObject player;
    private ScoreScript scoreScript;


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
        if (collision.gameObject.CompareTag("Player"))
        {
            hintText.gameObject.SetActive(true);
            shopAccess = true;
            player = collision.gameObject;
            scoreScript = player.GetComponent<ScoreScript>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hintText.gameObject.SetActive(false);
            shopAccess = false;
            player = null;
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

            //Visualizar el dinero del jugador
            SetPrices();
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

    private void SetPrices()
    {
        //Dinero del jugador
        shopCanvas.transform.Find("Money/Text").gameObject.GetComponent<TextMeshProUGUI>().text = scoreScript.ScoreNum.ToString();
        //Cafe
        shopCanvas.transform.Find("HealthPotion/Text").gameObject.GetComponent<TextMeshProUGUI>().text = precioCafe.ToString();
        //Chuleta
        shopCanvas.transform.Find("ManaPotion/Text").gameObject.GetComponent<TextMeshProUGUI>().text = precioChuleta.ToString();
    }

    public void Buy(string obj)
    {
        int initialMoney = scoreScript.ScoreNum;

        switch (obj)
        {
            case "Cafe":
                if(initialMoney >= precioCafe)
                {
                    scoreScript.addMoney(-precioCafe);
                    shopCanvas.transform.Find("Money/Text").gameObject.GetComponent<TextMeshProUGUI>().text = scoreScript.ScoreNum.ToString();
                    player.GetComponent<PotionCountScript>().addPotion();
                }
                break;
            case "Chuleta":
                if (initialMoney >= precioChuleta)
                {
                    scoreScript.addMoney(-precioChuleta);
                    shopCanvas.transform.Find("Money/Text").gameObject.GetComponent<TextMeshProUGUI>().text = scoreScript.ScoreNum.ToString();
                    player.GetComponent<PotionCountScript>().addManaPotion();
                }
                break;
        }
    }
}
