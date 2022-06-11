using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FisicaBossZone : MonoBehaviour
{
    [SerializeField] private Tilemap doorTM;
    [SerializeField] Transform initialPosition;

    [Header("SliderHealth")]
    [SerializeField] private GameObject canvas;
    [SerializeField] private FisicaBoss bossScript;

    private bool waitingReward;
    [SerializeField] private GameObject door;
    [SerializeField] private Transform doorSpawn;
    [SerializeField] private GameObject loot;

    private Quaternion rotation;

    void Start()
    {
        waitingReward = true;
        doorTM.gameObject.SetActive(false);
        foreach(Transform child in canvas.transform)
            child.gameObject.SetActive(false);
    }
    void Update()
    {
        if(bossScript == null && waitingReward)
        {
            waitingReward = false;
            canvas.transform.GetChild(1).gameObject.SetActive(true);
            canvas.transform.GetChild(0).gameObject.SetActive(false);
            door.transform.position = doorSpawn.position;
            loot.transform.position = initialPosition.position;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bossScript.active = true;
            doorTM.gameObject.SetActive(true);
            canvas.transform.GetChild(0).gameObject.SetActive(true);
            rotation = bossScript.transform.rotation;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bossScript.active = false;
            doorTM.gameObject.SetActive(false);
            canvas.transform.GetChild(0).gameObject.SetActive(false);
            bossScript.gameObject.transform.position = initialPosition.position;
            bossScript.currentHealth = bossScript.maxHealth;
            bossScript.transform.rotation = rotation;
            bossScript.healthBar.SetHealth(bossScript.maxHealth);
        }
    }
}
