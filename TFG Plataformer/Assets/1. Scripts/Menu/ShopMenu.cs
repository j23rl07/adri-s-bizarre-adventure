using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] private GameObject hintText;
    [SerializeField] private GameObject shopCanvas;
    [SerializeField] private GameObject floatingTextPrefab;
    [Header("Goods")]
    [SerializeField] private int precioCafe;
    [SerializeField] private int precioChuleta;
    [SerializeField] private int precioFragInsp;
    [SerializeField] private int stockFragInsp;
    [SerializeField] private int precioFragVit;
    [SerializeField] private int stockFragVit;
    
    private bool shopAccess = false;
    private GameObject player;
    private ScoreScript scoreScript;
    private PotionCountScript potionCountScript;
    private HealingItemsScript healingItemsScript;


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
            player = collision.gameObject;
            scoreScript = player.GetComponent<ScoreScript>();
            potionCountScript = player.GetComponent<PotionCountScript>();
            healingItemsScript = player.GetComponent<HealingItemsScript>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
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
        //FragVit
        shopCanvas.transform.Find("FragmentoVit/Text").gameObject.GetComponent<TextMeshProUGUI>().text = precioFragVit.ToString();
        shopCanvas.transform.Find("FragmentoVit/Stock/Text").gameObject.GetComponent<TextMeshProUGUI>().text = (stockFragVit - healingItemsScript.shopEnergyF).ToString();
        //FragInsp
        shopCanvas.transform.Find("FragmentoInsp/Text").gameObject.GetComponent<TextMeshProUGUI>().text = precioFragInsp.ToString();
        shopCanvas.transform.Find("FragmentoInsp/Stock/Text").gameObject.GetComponent<TextMeshProUGUI>().text = (stockFragInsp - healingItemsScript.shopManaF).ToString();
    }

    public void Buy(string obj)
    {
        int initialMoney = scoreScript.ScoreNum;

        switch (obj)
        {
            case "Cafe":
                if(initialMoney < precioCafe)
                {
                    Vector3 position = shopCanvas.transform.position + new Vector3(0, -392, 0);
                    string errorMsg = "Dinero insuficiente";
                    CreateErrorMsg(errorMsg, position);
                }
                else if (potionCountScript.potionNum >= 5)
                {
                    Vector3 position = shopCanvas.transform.position + new Vector3(0, -392, 0);
                    string errorMsg = "Capacidad máxima alcanzada";
                    CreateErrorMsg(errorMsg, position);
                }
                else
                {
                    scoreScript.addMoney(-precioCafe);
                    shopCanvas.transform.Find("Money/Text").gameObject.GetComponent<TextMeshProUGUI>().text = scoreScript.ScoreNum.ToString();
                    player.GetComponent<PotionCountScript>().addPotion();
                }
                break;

            case "Chuleta":
                if (initialMoney < precioChuleta)
                {
                    Vector3 position = shopCanvas.transform.position + new Vector3(0, -392, 0);
                    string errorMsg = "Dinero insuficiente";
                    CreateErrorMsg(errorMsg, position);
                }
                else if (potionCountScript.manaPotionNum >= 5)
                {
                    Vector3 position = shopCanvas.transform.position + new Vector3(0, -392, 0);
                    string errorMsg = "Capacidad máxima alcanzada";
                    CreateErrorMsg(errorMsg, position);
                }
                else
                {
                    scoreScript.addMoney(-precioChuleta);
                    shopCanvas.transform.Find("Money/Text").gameObject.GetComponent<TextMeshProUGUI>().text = scoreScript.ScoreNum.ToString();
                    player.GetComponent<PotionCountScript>().addManaPotion();
                }
                break;

            case "FragVit":
                if (initialMoney < precioFragVit)
                {
                    Vector3 position = shopCanvas.transform.position + new Vector3(0, -392, 0);
                    string errorMsg = "Dinero insuficiente";
                    CreateErrorMsg(errorMsg, position);
                }
                else if (stockFragVit - healingItemsScript.shopEnergyF <= 0)
                {
                    Vector3 position = shopCanvas.transform.position + new Vector3(0, -392, 0);
                    string errorMsg = "Capacidad máxima alcanzada";
                    CreateErrorMsg(errorMsg, position);
                }
                else
                {
                    healingItemsScript.shopEnergyF += 1;
                    healingItemsScript.GetEnergyFragment();
                    scoreScript.addMoney(-precioFragVit);
                    shopCanvas.transform.Find("Money/Text").gameObject.GetComponent<TextMeshProUGUI>().text = scoreScript.ScoreNum.ToString();
                    SetPrices();
                }
                break;

            case "FragInsp":
                if (initialMoney < precioFragInsp)
                {
                    Vector3 position = shopCanvas.transform.position + new Vector3(0, -392, 0);
                    string errorMsg = "Dinero insuficiente";
                    CreateErrorMsg(errorMsg, position);
                }
                else if (stockFragInsp - healingItemsScript.shopManaF <= 0)
                {
                    Vector3 position = shopCanvas.transform.position + new Vector3(0, -392, 0);
                    string errorMsg = "Capacidad máxima alcanzada";
                    CreateErrorMsg(errorMsg, position);
                }
                else
                {
                    healingItemsScript.shopManaF += 1;
                    healingItemsScript.GetManaFragment();
                    scoreScript.addMoney(-precioFragInsp);
                    shopCanvas.transform.Find("Money/Text").gameObject.GetComponent<TextMeshProUGUI>().text = scoreScript.ScoreNum.ToString();
                    SetPrices();
                }
                break;
        }
    }

    private void CreateErrorMsg(string msg, Vector3 position)
    {
        Instantiate(floatingTextPrefab, position, Quaternion.identity, shopCanvas.transform).GetComponent<FloatingText>().text = msg;
    }
}
