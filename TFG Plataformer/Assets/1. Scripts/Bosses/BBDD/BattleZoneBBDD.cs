using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleZoneBBDD : MonoBehaviour
{
    [Header("Boss Logic")]
    public bool isWolfDestroyed;
    public bool isOwlDestroyed;
    public bool isHeroDestroyed;
    public bool isMainDestroyed;
    public GameObject teleportOut;

    [Header("Audio")]
    public AudioSource audio;
    public AudioSource bossAudio;

    [Header("HealthBars")]
    public GameObject healthBars;

    [Header("Bosses References")]
    public GameObject wolfStatue;
    public GameObject owlStatue;
    public GameObject heroStatue;
    public GameObject core;

    [Header("StatueHealthReferences")]
    public StatueHealth wolfHealth;
    public StatueHealth owlHealth;
    public StatueHealth heroHealth;
    public StatueHealth coreHealth;

    [Header("Text")]
    public GameObject text;


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
            teleportOut.SetActive(false);
            if (audio.isPlaying)
            {
                audio.Stop();
                bossAudio.Play();
            }

        }
    }

    void Update()
    {
        if(isWolfDestroyed && isOwlDestroyed && isHeroDestroyed && isMainDestroyed)
        {
            teleportOut.active = true;
            text.active = true;
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

            if(wolfStatue.active == true)
            {
                wolfHealth.setMaxHealth();
            }
            else
            {
                wolfStatue.active = true;
                wolfHealth.setMaxHealth();
                isWolfDestroyed = false;
            }

            if (owlStatue.active == true)
            {
                owlHealth.setMaxHealth();
            }
            else
            {
                owlStatue.active = true;
                owlHealth.setMaxHealth();
                isOwlDestroyed = false;
            }

            if (heroStatue.active == true)
            {
                heroHealth.setMaxHealth();
            }
            else
            {
                heroStatue.active = true;
                heroHealth.setMaxHealth();
                isHeroDestroyed = false;
            }

            if (core.active == true)
            {
                coreHealth.setMaxHealth();
            }
            else
            {
                core.active = true;
                coreHealth.setMaxHealth();
                isMainDestroyed = false;
            }

            teleportOut.SetActive(false);
            text.active = false;
            healthBars.SetActive(false);
            
        }
    }
}
