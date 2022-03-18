using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public Text cointText;
    private int coinNum;

    // Start is called before the first frame update
    void Start()
    {
        coinNum = 0;
        cointText.text = "" + coinNum;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            sumCoin();
        }
    }

    public void sumCoin()
    {
        coinNum += 1;
        cointText.text = "" + coinNum;
    }
   
}
