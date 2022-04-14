using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HojaDeEstudioScript : MonoBehaviour
{
    public Trinket trinket;
    public Player player;
    public TrinketState TrinketState { get; set; }

    public HojaDeEstudioScript()
    {
        TrinketState = new TrinketState();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (TrinketState.isEquipped == false)
            {
                TrinketState.isEquipped = true;
                Equip();
            }
            else
            {
                TrinketState.isEquipped = false;
                Unequip();
            }
        }
    }

    void Equip()
    {
        player.maxMana += 25;
        Debug.Log(player.maxMana);
    }

    void Unequip()
    {
        player.maxMana -= 25;
        Debug.Log(player.maxMana);
    }
}
