using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;

public class ComputerBoss : MonoBehaviour
{
    public Tilemap bossDoor;
    public Tilemap exit;
    public ckeckPlayerInside checkPlayer;
    private bool isInit;
    [Serializable]
    public class Wave
    {
        public List<GameObject> enemies;
        public int spawnTime;
    }
    public List<Wave> waves = new List<Wave>();

    public int currentTime;
    private int previousTime;


    void Start()
    {
        isInit = true;
        bossDoor.gameObject.SetActive(false);
        exit.gameObject.SetActive(true);
        currentTime = -1;
        previousTime = -1;
        foreach (Wave wave in waves) 
        { 
            foreach(GameObject enemy in wave.enemies)
            {
                DisableComponents(enemy);
                foreach (Transform child in enemy.transform)  child.gameObject.SetActive(false);
                enemy.SetActive(false);
            }
        }
    }
    void Update()
    {
        
        if (!checkPlayer.isInside && !isInit)
        {
            Init();
        }
        else if (currentTime > 0 && currentTime != previousTime)
        {
            bossDoor.gameObject.SetActive(true);
            previousTime = currentTime;
            DeployWaves();
        }
    }
    public void Init()
    {
        isInit = true;
        GetComponent<ComputerScript>().isOff = false;
        GetComponent<ComputerScript>().runningTimer = false;
        bossDoor.gameObject.SetActive(false);
        exit.gameObject.SetActive(true);
        currentTime = -1;
        previousTime = -1;
        foreach (Wave wave in waves)
        {
            foreach (GameObject enemy in wave.enemies)
            {
                DisableComponents(enemy);
                enemy.SetActive(false);
                foreach (Transform child in enemy.transform) child.gameObject.SetActive(false);
            }
        }

    }

    void DisableComponents(GameObject obj)
    {
        foreach (MonoBehaviour component in obj.GetComponents<MonoBehaviour>())
        {
            component.enabled = false;
        }
        if(obj.GetComponent<Collider2D>() != null) obj.GetComponent<Collider2D>().enabled = false;
    }
    void EnableComponents(GameObject obj)
    {
        foreach (MonoBehaviour component in obj.GetComponents<MonoBehaviour>())
        {
            component.enabled = true;
        }
        if (obj.GetComponent<Collider2D>() != null) obj.GetComponent<Collider2D>().enabled = true;
    }
    void DeployWaves()
    {
        isInit = false;
        foreach(Wave wave in waves)
        {
            if (wave.spawnTime == currentTime)
            {
                foreach (GameObject enemy in wave.enemies)
                {
                    StartCoroutine(ActivateEnemy(enemy));
                }
                break;
            }
        }
    }
    IEnumerator ActivateEnemy(GameObject enemy)
    {
        enemy.SetActive(true);
        float opacity = 0;
        enemy.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,opacity);
        while (opacity < 1)
        {
            opacity += .015f;
            enemy.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, opacity);
            yield return new WaitForFixedUpdate();
        }
        foreach (Transform child in enemy.transform)  child.gameObject.SetActive(true);
        EnableComponents(enemy);
        }
}
