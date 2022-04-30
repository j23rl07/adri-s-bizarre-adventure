using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HojaDeEstudioScript : MonoBehaviour
{
    public Trinket trinket;
    public GameObject player;
    private bool active = false;
    public TrinketState TrinketState { get; set; }

    public void Start()
    {
        player = GameObject.Find("Player FIXED");
    }

    public HojaDeEstudioScript()
    {
        TrinketState = new TrinketState();
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
        player.GetComponent<Player>().maxMana += 25;
        Debug.Log(player.GetComponent<Player>().maxMana);
        active = true;
    }

    void Unequip()
    {
        player.GetComponent<Player>().maxMana -= 25;
        Debug.Log(player.GetComponent<Player>().maxMana);
        active = false;
    }
}
