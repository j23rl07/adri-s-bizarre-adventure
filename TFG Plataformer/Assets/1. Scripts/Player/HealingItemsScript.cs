using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItemsScript : MonoBehaviour
{
    public HealthBar healthBar;
    public Player player;

    [HideInInspector] public int shopEnergyF;
    [HideInInspector] public int shopManaF;

    private void Start()
    {
        shopEnergyF = 0;
        shopManaF = 0;
    }

    private void OnTriggerEnter2D(Collider2D healingObject)
    {
        if (healingObject.tag == "Aprobado")
        {
            Player.maxHealth += 20;
            Player.maxMana += 20;
            Destroy(healingObject.gameObject);
        }
        if (healingObject.tag == "EnergyF")
        {
            GetEnergyFragment();
            Destroy(healingObject.gameObject);
        }
        if (healingObject.tag == "ManaF")
        {
            GetManaFragment();
            Destroy(healingObject.gameObject);
        }
        if (healingObject.tag == "Cookie")
        {
            player.heal(System.Convert.ToInt32(Player.maxHealth * 0.1));
            Destroy(healingObject.gameObject);
        }
        if (healingObject.tag == "Essence")
        {
            player.healMana(System.Convert.ToInt32(Player.maxMana * 0.1));
            Destroy(healingObject.gameObject);
        }
    }

    public void GetEnergyFragment()
    {
        Player.maxHealth += 10;
    }

    public void GetManaFragment()
    {
        Player.maxMana += 10;
    }
}
