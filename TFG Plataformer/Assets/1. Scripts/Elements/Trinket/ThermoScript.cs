using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThermoScript : MonoBehaviour
{
    public Trinket trinket;
    public Player player;
    public TrinketState TrinketState { get; set; }

    public ThermoScript()
    {
        TrinketState = new TrinketState();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) { 
            if(TrinketState.isEquipped == false)
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
        player.maxHealth += 25;
        Debug.Log(player.maxHealth);
    }

    void Unequip()
    {
        player.maxHealth -= 25;
        Debug.Log(player.maxHealth);
    }
}
