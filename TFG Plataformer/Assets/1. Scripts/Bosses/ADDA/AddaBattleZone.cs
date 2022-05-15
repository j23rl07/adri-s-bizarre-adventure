using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddaBattleZone : MonoBehaviour
{
    [Header("Logic")]
    public bool isGolem1Destroyed;
    public bool isGolem2Destroyed;
    public GameObject teleport;

    [Header("HealthBars")]
    public GameObject healthBars;

    [Header("Audio")]
    public AudioSource audio;
    public AudioSource bossAudio;

    [Header("Bosses References")]
    public GameObject meleeGolem;
    public GameObject rangeGolem;

    [Header("GolemBossHealth")]
    public GolemBossHealth meleeBossHealth;
    public GolemBossHealth rangeBossHealth;

    void Start()
    {
        bossAudio.loop = true;
        audio.loop = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            healthBars.SetActive(true);
            if (audio.isPlaying)
            {
                audio.Stop();
                bossAudio.Play();
            }

        }
    }

    void Update() {
        if(isGolem1Destroyed && isGolem2Destroyed)
        {
            teleport.active = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (bossAudio.isPlaying)
            {
                bossAudio.Stop();
                audio.Play();
            }

            if (meleeGolem.active == true)
            {
                meleeBossHealth.setMaxHealth();
            }
            else
            {
                meleeGolem.active = true;
                meleeBossHealth.setMaxHealth();
                isGolem1Destroyed = false;
            }
            if(rangeGolem.active == true)
            {
                rangeBossHealth.setMaxHealth();
            }
            else
            {
                rangeGolem.active = true;
                rangeBossHealth.setMaxHealth();
                isGolem2Destroyed = false;
            }

            if (teleport.active == true)
            {
                teleport.SetActive(false);
            }
            healthBars.SetActive(false);
            

        }
    }

}
