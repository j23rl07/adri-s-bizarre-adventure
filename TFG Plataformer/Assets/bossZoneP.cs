using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class bossZoneP : MonoBehaviour
{
    [Header("SliderHealth")]
    public GameObject bossHealthSlider;
    public GameObject boss;
    [SerializeField] private BoxCollider2D boxCollider;

    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Vector3Int position;

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
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            boss.GetComponent<Enemy>().currentHealth = boss.GetComponent<Enemy>().maxHealth;
            boss.transform.position = new Vector3(146.149994f, -39.2000008f, 0f);
            boss.GetComponent<Animator>().enabled = false;
            boxCollider.enabled = false;
            abre();
            if (bossAudio.isPlaying)
            {
                bossAudio.Stop();
                audio.Play();
            }
            bossHealthSlider.SetActive(false);
        }
    }

    private void bloque(Vector3Int vector)
    {
        Vector3Int positionaux = new Vector3Int();
        positionaux = vector;

        tilemap.SetTile(positionaux, null);

        positionaux = vector;
        positionaux.Set(vector.x + 1, vector.y, 0);
        tilemap.SetTile(positionaux, null);

        positionaux = vector;
        positionaux.Set(vector.x, vector.y - 1, 0);
        tilemap.SetTile(positionaux, null);

        positionaux = vector;
        positionaux.Set(vector.x + 1, vector.y - 1, 0);
        tilemap.SetTile(positionaux, null);
    }

    private void abre()
    {
        Vector3Int positionaux = new Vector3Int();
        bloque(position);

        positionaux = position;
        positionaux.Set(position.x, position.y - 2, 0);
        bloque(positionaux);

        positionaux = position;
        positionaux.Set(position.x, position.y - 4, 0);
        bloque(positionaux);

        positionaux = position;
        positionaux.Set(position.x, position.y - 6, 0);
        bloque(positionaux);
    }
}
