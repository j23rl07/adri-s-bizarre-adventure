using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChuletaScript : MonoBehaviour
{
    public Trinket trinket;
    public Player player;
    public TrinketState TrinketState { get; set; }

    public ChuletaScript()
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
                StartCoroutine(Equip());
            }
            else
            {
                TrinketState.isEquipped = false;
                StopAllCoroutines();
            }
        }
    }

    private IEnumerator Equip()
    {
        Debug.Log("Recuperando");
        yield return new WaitForSeconds(2);
        while (player.currentMana < player.maxMana)
        {
            player.currentMana += player.maxMana / 100;
            player.manaBar.SetMana(player.currentMana);
            yield return player.regenTick;
        }
    }
}
