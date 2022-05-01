using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckyCoinScript : MonoBehaviour
{
    public Trinket trinket;
    public GameObject player;
    private bool active = false;
    public TrinketState TrinketState { get; set; }

    public LuckyCoinScript()
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
        player.GetComponent<ScoreScript>().goldValue += 5;
        player.GetComponent<ScoreScript>().silverValue += 2;
        active = true;
    }

    void Unequip()
    {
        player.GetComponent<ScoreScript>().goldValue -= 5;
        player.GetComponent<ScoreScript>().silverValue -= 2;
        active = false;
    }
}
