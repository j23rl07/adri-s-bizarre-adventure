using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrabajoExtraScript : MonoBehaviour
{
    public Trinket trinket;
    public GameObject player;
    private bool active = false;
    public TrinketState TrinketState { get; set; }

    public TrabajoExtraScript()
    {
        TrinketState = new TrinketState();
    }

    public void Start()
    {
        player = GameObject.Find("Player FIXED");
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
        player.GetComponent<Player>().selfDamage -= 10;
        Debug.Log(player.GetComponent<Player>().selfDamage);
        active = true;
    }

    void Unequip()
    {
        player.GetComponent<Player>().selfDamage += 10;
        Debug.Log(player.GetComponent<Player>().selfDamage);
        active = false;
    }
}
