using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleZoneBBDD : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audio;
    public AudioSource bossAudio;


    void Start()
    {
        bossAudio.loop = true;
        audio.loop = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (audio.isPlaying)
            {
                audio.Stop();
                bossAudio.Play();
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (bossAudio.isPlaying)
            {
                bossAudio.Stop();
                audio.Play();
            }
        }
    }
}
