using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    [Header("Time Parameters")]
    public bool runningTimer;
    public int currentTime;
    public int maxTime;

    [Header("Position Options")]
    public Transform restartPosition;
    public Transform playerPosition;
    public Coroutine timer = null;

    [Header("References")]
    public Player player;
    public Text timerText;
    public GameObject text;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") { 
            if(runningTimer == false) 
            {
                currentTime = maxTime;
                timer = StartCoroutine(Timer(currentTime));
                text.SetActive(true);
            }
            else
            {
                currentTime = maxTime;
                runningTimer = false;
                StopCoroutine(timer);
                runningTimer = true;
                timer = StartCoroutine(Timer(currentTime));
                text.SetActive(true);
            }
                
        }
    }

    void Update()
    {
        if(runningTimer == false & timer != null)
        {
            StopCoroutine(timer);
            text.SetActive(false);
        }
    }

    private IEnumerator Timer(int seconds)
    {
        runningTimer = true;
        int currentTime = seconds;
        while(currentTime > 0)
        {
            yield return new WaitForSeconds(1);
            currentTime--;
            timerText.text = "" + currentTime;
            Debug.Log(currentTime);
        }
        runningTimer = false;
        playerPosition.position = restartPosition.position;
    }
}
