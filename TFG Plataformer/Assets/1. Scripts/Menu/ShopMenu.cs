using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] private GameObject hintText;
    [SerializeField] private GameObject shopCanvas;
    [SerializeField] private GameObject floatingTextPrefab;
    private Inventory inventoryScript;
    [Header("Goods")]
    [SerializeField] private int precioCafe;
    [SerializeField] private int precioChuleta;
    [SerializeField] private int precioFragInsp;
    [SerializeField] private int stockFragInsp;
    [SerializeField] private int precioFragVit;
    [SerializeField] private int stockFragVit;
    [SerializeField] private GameObject trabajoExtra;
    [SerializeField] private int precioTrabajoExtra;
    private int stockTrabajoExtra;
    [SerializeField] private GameObject MonedaSuerte;
    [SerializeField] private int precioMonedaSuerte;
    private int stockMonedaSuerte;
    [SerializeField] private GameObject ChuletaT;
    [SerializeField] private int precioChuletaT;
    private int stockChuletaT;

    private bool shopAccess = false;
    private GameObject player;
    private ScoreScript scoreScript;
    private PotionCountScript potionCountScript;
    private HealingItemsScript healingItemsScript;

    private void Start()
    {
        hintText.SetActive(false);
        shopCanvas.SetActive(false);

        inventoryScript = Inventory.instance.GetComponent<Inventory>();
        stockTrabajoExtra = getTrinketStock(trabajoExtra.name);
        stockMonedaSuerte = getTrinketStock(MonedaSuerte.name);
        stockChuletaT = getTrinketStock(ChuletaT.name);
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
            potionCountScript = player.GetComponent<PotionCountScript>();
            healingItemsScript = player.GetComponent<HealingItemsScript>();
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
        if (!shopCanvas.activeSelf & !PauseMenu.otherMenuOn)
        {
            Time.timeScale = 0;
            shopCanvas.SetActive(true);
            PauseMenu.isGamePaused = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            PauseMenu.otherMenuOn = true;

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
            PauseMenu.otherMenuOn = false;
        }
    }

    private void SetPrices()
    {
        //Dinero del jugador
        shopCanvas.transform.Find("Money/Text").gameObject.GetComponent<TextMeshProUGUI>().text = ScoreScript.ScoreNum.ToString();
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
        //TrabajoExtra
        shopCanvas.transform.Find("TrabajoExtra/Text").gameObject.GetComponent<TextMeshProUGUI>().text = precioTrabajoExtra.ToString();       
        shopCanvas.transform.Find("TrabajoExtra/Stock/Text").gameObject.GetComponent<TextMeshProUGUI>().text = stockTrabajoExtra.ToString();
        //MonedaSuerte
        shopCanvas.transform.Find("MonedaSuerte/Text").gameObject.GetComponent<TextMeshProUGUI>().text = precioMonedaSuerte.ToString();        
        shopCanvas.transform.Find("MonedaSuerte/Stock/Text").gameObject.GetComponent<TextMeshProUGUI>().text = stockMonedaSuerte.ToString();
        //ChuletaT
        shopCanvas.transform.Find("ChuletaT/Text").gameObject.GetComponent<TextMeshProUGUI>().text = precioChuletaT.ToString();
        shopCanvas.transform.Find("ChuletaT/Stock/Text").gameObject.GetComponent<TextMeshProUGUI>().text = stockChuletaT.ToString();
    }

    public void Buy(string obj)
    {
        int initialMoney = ScoreScript.ScoreNum;

        switch (obj)
        {
            case "Cafe":
                if(initialMoney < precioCafe)
                {
                    Vector3 position = shopCanvas.transform.position + new Vector3(0, -392, 0);
                    string errorMsg = "Dinero insuficiente";
                    CreateErrorMsg(errorMsg, position);
                }
                else if (PotionCountScript.potionNum >= 5)
                {
                    Vector3 position = shopCanvas.transform.position + new Vector3(0, -392, 0);
                    string errorMsg = "Capacidad máxima alcanzada";
                    CreateErrorMsg(errorMsg, position);
                }
                else
                {
                    scoreScript.addMoney(-precioCafe);
                    shopCanvas.transform.Find("Money/Text").gameObject.GetComponent<TextMeshProUGUI>().text = ScoreScript.ScoreNum.ToString();
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
                else if (PotionCountScript.manaPotionNum >= 5)
                {
                    Vector3 position = shopCanvas.transform.position + new Vector3(0, -392, 0);
                    string errorMsg = "Capacidad máxima alcanzada";
                    CreateErrorMsg(errorMsg, position);
                }
                else
                {
                    scoreScript.addMoney(-precioChuleta);
                    shopCanvas.transform.Find("Money/Text").gameObject.GetComponent<TextMeshProUGUI>().text = ScoreScript.ScoreNum.ToString();
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
                    shopCanvas.transform.Find("Money/Text").gameObject.GetComponent<TextMeshProUGUI>().text = ScoreScript.ScoreNum.ToString();
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
                    shopCanvas.transform.Find("Money/Text").gameObject.GetComponent<TextMeshProUGUI>().text = ScoreScript.ScoreNum.ToString();
                    SetPrices();
                }
                break;

            case "TrabajoExtra":
                if (initialMoney < precioTrabajoExtra)
                {
                    Vector3 position = shopCanvas.transform.position + new Vector3(0, -392, 0);
                    string errorMsg = "Dinero insuficiente";
                    CreateErrorMsg(errorMsg, position);
                }
                else if (stockTrabajoExtra <= 0)
                {
                    Vector3 position = shopCanvas.transform.position + new Vector3(0, -392, 0);
                    string errorMsg = "Capacidad máxima alcanzada";
                    CreateErrorMsg(errorMsg, position);
                }
                else
                {
                    inventoryScript.CheckTSlotsAvailability(trabajoExtra, trabajoExtra.name, 1);
                    scoreScript.addMoney(-precioTrabajoExtra);
                    shopCanvas.transform.Find("Money/Text").gameObject.GetComponent<TextMeshProUGUI>().text = ScoreScript.ScoreNum.ToString();
                    stockTrabajoExtra -= 1;
                    SetPrices();
                }
                break;

            case "MonedaSuerte":
                if (initialMoney < precioMonedaSuerte)
                {
                    Vector3 position = shopCanvas.transform.position + new Vector3(0, -392, 0);
                    string errorMsg = "Dinero insuficiente";
                    CreateErrorMsg(errorMsg, position);
                }
                else if (stockMonedaSuerte <= 0)
                {
                    Vector3 position = shopCanvas.transform.position + new Vector3(0, -392, 0);
                    string errorMsg = "Capacidad máxima alcanzada";
                    CreateErrorMsg(errorMsg, position);
                }
                else
                {
                    inventoryScript.CheckTSlotsAvailability(MonedaSuerte, MonedaSuerte.name, 1);
                    scoreScript.addMoney(-precioTrabajoExtra);
                    shopCanvas.transform.Find("Money/Text").gameObject.GetComponent<TextMeshProUGUI>().text = ScoreScript.ScoreNum.ToString();
                    stockMonedaSuerte -= 1;
                    SetPrices();
                }
                break;

            case "ChuletaT":
                if (initialMoney < precioChuletaT)
                {
                    Vector3 position = shopCanvas.transform.position + new Vector3(0, -392, 0);
                    string errorMsg = "Dinero insuficiente";
                    CreateErrorMsg(errorMsg, position);
                }
                else if (stockChuletaT <= 0)
                {
                    Vector3 position = shopCanvas.transform.position + new Vector3(0, -392, 0);
                    string errorMsg = "Capacidad máxima alcanzada";
                    CreateErrorMsg(errorMsg, position);
                }
                else
                {
                    inventoryScript.CheckTSlotsAvailability(ChuletaT, ChuletaT.name, 1);
                    scoreScript.addMoney(-precioTrabajoExtra);
                    shopCanvas.transform.Find("Money/Text").gameObject.GetComponent<TextMeshProUGUI>().text = ScoreScript.ScoreNum.ToString();
                    stockChuletaT -= 1;
                    SetPrices();
                }
                break;
        }
    }

    private void CreateErrorMsg(string msg, Vector3 position)
    {
        Instantiate(floatingTextPrefab, position, Quaternion.identity, shopCanvas.transform).GetComponent<FloatingText>().text = msg;
    }

    private int getTrinketStock(string trinket)
    {
        int result = 1;

        foreach (GameObject slot in inventoryScript.Tslots)
        {
            if(slot.transform.childCount > 0)
                if (slot.transform.GetChild(0).name.Equals(trinket))
                    result = 0;
        }

        return result;
    }
}
