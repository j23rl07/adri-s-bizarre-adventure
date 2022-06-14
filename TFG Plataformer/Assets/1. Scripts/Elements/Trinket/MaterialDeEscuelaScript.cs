using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialDeEscuelaScript : MonoBehaviour
{
    public Trinket trinket;
    public GameObject player;
    private bool active = false;
    public TrinketState TrinketState { get; set; }

    public MaterialDeEscuelaScript()
    {
        TrinketState = new TrinketState();
    }

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
        player.GetComponent<PlayerCombat>().attackDamage += 10;
        active = true;
    }

    void Unequip()
    {
        player.GetComponent<PlayerCombat>().attackDamage -= 10;
        active = false;
    }
}
