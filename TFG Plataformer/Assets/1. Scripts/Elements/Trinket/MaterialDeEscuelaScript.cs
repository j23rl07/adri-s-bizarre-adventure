using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialDeEscuelaScript : MonoBehaviour
{
    public Trinket trinket;
    public PlayerCombat pc;
    public TrinketState TrinketState { get; set; }

    public MaterialDeEscuelaScript()
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
        pc.attackDamage += 10;
        Debug.Log(pc.attackDamage);
    }

    void Unequip()
    {
        pc.attackDamage -= 10;
        Debug.Log(pc.attackDamage);
    }
}
