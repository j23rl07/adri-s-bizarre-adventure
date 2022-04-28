using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrabajoExtraScript : MonoBehaviour
{
    public Trinket trinket;
    public Player player;
    public TrinketState TrinketState { get; set; }

    public TrabajoExtraScript()
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
        player.selfDamage -= 10;
        Debug.Log(player.selfDamage);
    }

    void Unequip()
    {
        player.selfDamage += 10;
        Debug.Log(player.selfDamage);
    }
}
