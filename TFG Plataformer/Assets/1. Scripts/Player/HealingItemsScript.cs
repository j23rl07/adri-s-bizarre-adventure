using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItemsScript : MonoBehaviour
{
    public HealthBar healthBar;
    public Player player;

    private void OnTriggerEnter2D(Collider2D healingObject)
    {
        if (healingObject.tag == "Aprobado")
        {
            player.maxHealth += 20;
            player.maxMana += 20;
        }
        if (healingObject.tag == "EnergyF")
        {
            player.maxHealth += 10;
        }
        if (healingObject.tag == "ManaF")
        {
            player.maxMana += 10;
        }
        if (healingObject.tag == "Cookie")
        {
            player.heal(System.Convert.ToInt32(player.maxHealth * 0.1));
        }
        if (healingObject.tag == "Essence")
        {
            player.healMana(System.Convert.ToInt32(player.maxMana * 0.1));
        }

        Destroy(healingObject.gameObject);
    }

}
