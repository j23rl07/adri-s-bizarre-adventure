using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [Header("Timers")]
    [SerializeField] private float actDelay;
    [SerializeField] private float actTime;

    [SerializeField] private int damage;

    private Animator animate;
    private SpriteRenderer sr;

    private bool triggeredTrap; //Momenento en el que se activa la trampa
    private bool activeTrap; //Trampa activada. Tiempo en el que causa efecto en el jugador.

    public Player player;
    public BoxCollider2D boxCollider2D;

    private void Awake()
    {
        animate = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (!triggeredTrap) { 
                StartCoroutine(triggerFiretrap());
            }
            if (activeTrap) { 
                player.TakeDamage(damage);
            }
        }
    }

    private IEnumerator triggerFiretrap()
    {
        //Activación de trampa y cambio de color como aviso
        triggeredTrap = true;
        sr.color = Color.red; 

        //Esperar un tiempo. Se activa la trampa junto con su animacion y su color vuelve a por defecto.
        yield return new WaitForSeconds(actDelay);
        sr.color = Color.white; //retomar el color original
        activeTrap = true;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = true;
        animate.SetBool("activated", true);
        
        //Desactivar trampa tras unos segundos
        yield return new WaitForSeconds(actTime);
        activeTrap = false;
        triggeredTrap = false;
        animate.SetBool("activated", false);
    }
}
