using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Maze : MonoBehaviour
{
    [Header("GameObjects references")]
    public GameObject lights;

    [Header("Books")]
    public GameObject book1;
    public GameObject book2;
    public GameObject book3;
    public GameObject book4;

    [Header("Check Books")]
    public bool gotBook1;
    public bool gotBook2;
    public bool gotBook3;
    public bool gotBook4;
    public int bookNum;
    public Text bookText;

    [Header("Audio")]
    public AudioSource backgroundAudio;
    public AudioSource mazeAudio;

    [Header("Puerta")]
    public GameObject puerta;

    [Header("Alert")]
    public GameObject alert;

    void Start()
    {
        mazeAudio.loop = true;
        backgroundAudio.loop = true;
    }

    void Update()
    {
        if(gotBook1 && gotBook2 && gotBook3 && gotBook4)
        {
            puerta.active = false;
        }
        else
        {
            puerta.active = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            lights.SetActive(true);
            StartCoroutine(ShowText());
            if (backgroundAudio.isPlaying)
            {
                backgroundAudio.Stop();
                mazeAudio.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            
            if (mazeAudio.isPlaying)
            {
                mazeAudio.Stop();
                backgroundAudio.Play();
            }

            if (gotBook1)
            {
                gotBook1 = false;
                book1.active = true;
            }
            if (gotBook2)
            {
                gotBook2 = false;
                book2.active = true;
            }
            if (gotBook3)
            {
                gotBook3 = false;
                book3.active = true;
            }
            if (gotBook4)
            {
                gotBook4 = false;
                book4.active = true;
            }

            bookNum = 0;
            bookText.text = "" + bookNum + "/4";

            lights.SetActive(false);
        }
    }

    public IEnumerator ShowText()
    {
        alert.active = true;
        yield return new WaitForSeconds(5f);
        alert.active = false;
    }
}
