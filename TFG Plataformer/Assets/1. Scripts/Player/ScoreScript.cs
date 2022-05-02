using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public Text MyScoreText;
    [HideInInspector] public int ScoreNum;
    public int goldValue = 10;
    public int silverValue = 1;


    // Start is called before the first frame update
    void Start()
    {
        ScoreNum = 0;
        MyScoreText.text = "" + ScoreNum;
    }

    private void OnTriggerEnter2D(Collider2D Coin)
    {
        if (Coin.tag == "MyCoin")
        {
            addMoney(goldValue);
            Destroy(Coin.gameObject);
        }
        if (Coin.tag == "SilverCoin")
        {
            addMoney(silverValue);
            Destroy(Coin.gameObject);
        }
    }

    public void addMoney(int ammount)
    {
        ScoreNum += ammount;
        MyScoreText.text = "" + ScoreNum;
    }
}
