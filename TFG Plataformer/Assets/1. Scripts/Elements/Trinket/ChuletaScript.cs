using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChuletaScript : MonoBehaviour
{
    public Trinket trinket;
    public GameObject player;
    public TrinketState TrinketState { get; set; }
    private bool active = false;

    public ChuletaScript()
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
            equip();
        }
        else if(!transform.GetChild(0).gameObject.activeSelf & active == true)
        {
            unequip();
        }
    }

    public void equip()
    {
        StartCoroutine(Equip());
        active = true;
    }

    public void unequip()
    {
        StopAllCoroutines();
        active = false;
    }

    private IEnumerator Equip()
    {
        Debug.Log("Recuperando");
        yield return new WaitForSeconds(2);
        while (player.GetComponent<Player>().currentMana < player.GetComponent<Player>().maxMana)
        {
            player.GetComponent<Player>().currentMana += player.GetComponent<Player>().maxMana / 100;
            player.GetComponent<Player>().manaBar.SetMana(player.GetComponent<Player>().currentMana);
            yield return player.GetComponent<Player>().regenTick;
        }
    }
}
