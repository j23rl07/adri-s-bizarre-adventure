using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckyCoinScript : MonoBehaviour
{
    public Trinket trinket;
    public ScoreScript sc;
    public TrinketState TrinketState { get; set; }

    public LuckyCoinScript()
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
        Debug.Log("Equipado");
        sc.goldValue += 5;
        sc.silverValue += 2;
    }

    void Unequip()
    {
        Debug.Log("Desequipado");
        sc.goldValue -= 5;
        sc.silverValue -= 2;
    }
}
