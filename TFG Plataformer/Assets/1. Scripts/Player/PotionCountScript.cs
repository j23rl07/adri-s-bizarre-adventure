using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionCountScript : MonoBehaviour
{
    public Text potionCountText;
    private int potionNum;
    public Player player;

    public HealthBar healthBar;

    void Start()
    {
        potionNum = 0;
        potionCountText.text = "" + potionNum;  
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) )
        {
            sumPotion();
        }
    }

    public void sumPotion()
    {
        potionNum += 1;
        potionCountText.text = "" + potionNum;
    }

}
