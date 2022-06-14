using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleZone : MonoBehaviour
{
    [Header("SliderHealth")]
    public GameObject bossHealthSlider;
    public BossHealth bossHealth;

    [Header("Columna Health")]
    public ColumnaLogic columnaHealth1;
    public ColumnaLogic columnaHealth2;
    public ColumnaLogic columnaHealth3;
    public ColumnaLogic columnaHealth4;

    [Header("Columna reference")]
    public GameObject columna1;
    public GameObject columna2;
    public GameObject columna3;
    public GameObject columna4;

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
            bossHealthSlider.SetActive(true);
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
            bossHealth.setMaxHealth();
            if (columna1.activeSelf)
            {
                Debug.Log("activo");
                columnaHealth1.setMaxHealth();
            }
            if (columna2.activeSelf)
            {
                columnaHealth2.setMaxHealth();
            }
            if (columna3.activeSelf)
            {
                columnaHealth3.setMaxHealth();
            }
            if (columna4.activeSelf)
            {
                columnaHealth4.setMaxHealth();
            }
            if (bossAudio.isPlaying)
            {
                bossAudio.Stop();
                audio.Play();
            }
            bossHealthSlider.SetActive(false);
        }
    }
}
