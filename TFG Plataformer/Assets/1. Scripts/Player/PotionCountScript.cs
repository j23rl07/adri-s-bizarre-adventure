using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionCountScript : MonoBehaviour
{
    [Header("Other")]
    public Text potionCountText;
    public static int potionNum;
    public Text manaPotionCountText;
    public static int manaPotionNum;
    public Player player;
    public HealthBar healthBar;

    void Start()
    {
        potionCountText.text = "" + potionNum;
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
            addPotion();
            Destroy(Potion.gameObject);
        }
        if (Potion.tag == "ManaPotion")
        {
            addManaPotion();
            Destroy(Potion.gameObject);
        }
    }

    private void usePotion()
    {
        subPotion();
        player.heal(System.Convert.ToInt32(Player.maxHealth * 0.35));
    }

    private void useManaPotion()
    {
        subManaPotion();
        player.healMana(System.Convert.ToInt32(Player.maxMana * 0.5));
    }

    public void addPotion()
    {
        potionNum += 1;
        potionCountText.text = "" + potionNum;
    }

    public void subPotion()
    {
        potionNum -= 1;
        potionCountText.text = "" + potionNum;
    }

    public void addManaPotion()
    {
        manaPotionNum += 1;
        manaPotionCountText.text = "" + manaPotionNum;
    }

    public void subManaPotion()
    {
        manaPotionNum -= 1;
        manaPotionCountText.text = "" + manaPotionNum;
    }
}
