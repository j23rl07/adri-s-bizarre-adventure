using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsUse : MonoBehaviour
{
    public ItemType itemType;
    public float damageIncrease;

    public void UseBotton()
    {
        if (itemType == ItemType.TRINKET)
        {
            //Se le suman stats al personaje
            //PlayerCombat.i
        }
    }
}
