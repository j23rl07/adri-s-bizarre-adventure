using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionCountScript : MonoBehaviour
{
    [Header("Other")]
    public Text potionCountText;
    private int potionNum;
    public Text manaPotionCountText;
    private int manaPotionNum;
    public Player player;
    public HealthBar healthBar;

    void Start()
    {
        potionNum = 0;
        potionCountText.text = "" + potionNum;

        manaPotionNum = 0;
        manaPotionCountText.text = "" + manaPotionNum;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && potionNum > 0)
        {
            usePotion();
        }

        if (Input.GetKeyDown(KeyCode.W) && manaPotionNum > 0)
        {
            useManaPotion();
        }
    }

    private void OnTriggerEnter2D(Collider2D Potion)
    {
        if (Potion.tag == "HealthPotion")
        {
            potionNum += 1;
            Destroy(Potion.gameObject);
            potionCountText.text = "" + potionNum;
        }
        if (Potion.tag == "ManaPotion")
        {
            manaPotionNum += 1;
            Destroy(Potion.gameObject);
            manaPotionCountText.text = "" + manaPotionNum;
        }
    }

    private void usePotion()
    {
        potionNum -= 1;
        potionCountText.text = "" + potionNum;
        player.heal(System.Convert.ToInt32(player.maxHealth * 0.35));
    }

    private void useManaPotion()
    {
        manaPotionNum -= 1;
        manaPotionCountText.text = "" + manaPotionNum;
        player.healMana(System.Convert.ToInt32(player.maxMana * 0.5));
    }

}
