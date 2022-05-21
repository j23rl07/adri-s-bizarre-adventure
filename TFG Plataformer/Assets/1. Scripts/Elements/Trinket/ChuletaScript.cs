using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChuletaScript : MonoBehaviour
{
    public Trinket trinket;
    public GameObject player;
    public TrinketState TrinketState { get; set; }
    private bool esperando = false;

    public ChuletaScript()
    {
        TrinketState = new TrinketState();
    }

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (transform.GetChild(0).gameObject.activeSelf & !esperando & player.GetComponent<Player>().currentMana < Player.maxMana)
        {
            StartCoroutine(Equip());
        }
    }
    private IEnumerator Equip()
    {
        esperando = true;
        yield return new WaitForSeconds(2);
        esperando = false;
        int mana = Mathf.FloorToInt(Player.maxMana * 0.05f);
        int currentMana = player.GetComponent<Player>().currentMana;
        int manaMax = Player.maxMana;
        if (mana + currentMana > manaMax)
        {
            mana = manaMax - currentMana;
        }
        player.GetComponent<Player>().currentMana += mana;
        player.GetComponent<Player>().manaBar.SetMana(player.GetComponent<Player>().currentMana);
        yield return player.GetComponent<Player>().regenTick;
    }
}
