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
        player = GameObject.FindGameObjectWithTag("Player");
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
        Player.maxMana += 25;
        active = true;
    }

    void Unequip()
    {
        Player.maxMana -= 25;
        active = false;
    }
}
