using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThermoScript : MonoBehaviour
{
    public Trinket trinket;
    public GameObject player;
    private bool active = false;
    public TrinketState TrinketState { get; set; }

    public ThermoScript()
    {
        TrinketState = new TrinketState();
    }
    public void Start()
    {
        player = GameObject.Find("Player FIXED");
    }
    void Update()
    {
        if (transform.GetChild(0).gameObject.activeSelf & active == false)
        {
            Equip();
        }
        else if (!transform.GetChild(0).gameObject.activeSelf & active == true)
        {
            Unequip();
        }
    }

    void Equip()
    {
        player.GetComponent<Player>().maxHealth += 25;
        Debug.Log(player.GetComponent<Player>().maxHealth);
        active = true;
    }

    void Unequip()
    {
        player.GetComponent<Player>().maxHealth -= 25;
        Debug.Log(player.GetComponent<Player>().maxHealth);
        active = false;
    }
}
