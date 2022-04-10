using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public Text MyScoreText;
    private int ScoreNum;
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
            ScoreNum += 10;
            Destroy(Coin.gameObject);
            MyScoreText.text = "" + goldValue;
        }
        if (Coin.tag == "SilverCoin")
        {
            ScoreNum += 1;
            Destroy(Coin.gameObject);
            MyScoreText.text = "" + silverValue;
        }
    }
}