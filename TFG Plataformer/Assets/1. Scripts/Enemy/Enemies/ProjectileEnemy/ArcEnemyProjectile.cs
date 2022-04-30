using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcEnemyProjectile : EnemyDamage
{
    [Header("Arc")]
    public Vector3 targetPos;
    public float arcHeight = 1;

    Vector3 startPos;
    Vector3 nextPos;

    [Header("Enemy bullet")]
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float duration;


    public void triggerProjectile()
    {
        startPos = transform.position;
        duration = 0;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        //Calcula la posici�n final para disparar la bala. Se traza la trayectoria del arco.
        float x0 = startPos.x; //Posici�n inicial de la bala
        float x1 = targetPos.x; //Posici�n destino
        float distance = x1 - x0; //Distancia entre ambos puntos
        float nextX = Mathf.MoveTowards(transform.position.x, x1, speed * Time.deltaTime); //Calcula a trav�s de la posici�n donde se encuentra la basa hacia donde se tiene que mover en el eje x con una velocidad dada
        float baseY = Mathf.Lerp(startPos.y, targetPos.y, (nextX - x0) / distance); //Mismo caso que lo anterior para el eye y
        float arc = arcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * distance * distance); //Calculamos la trayectoria del arco
        nextPos = new Vector3(nextX, baseY + arc, transform.position.z); //Se determina el nuevo movimiento en base a los par�metros anteriores

        // Rotaci�n y movimiento del proyectil a la nueva posici�n calculada
        transform.rotation = LookAt2D(nextPos - transform.position);
        transform.position = nextPos;

        // Destruimos el objeto al llegar a la posici�n final
        if (nextPos == targetPos) Arrived();

        if (duration > resetTime)
        {
            gameObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        GameObject[] goldenCoin = GameObject.FindGameObjectsWithTag("MyCoin");
        GameObject[] silverCoin = GameObject.FindGameObjectsWithTag("SilverCoin");
     
        foreach (GameObject obj in goldenCoin)
        {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        foreach (GameObject obj in silverCoin)
        {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.TakeDamage(damage);
            StartCoroutine(player.playerKnockback(0.01f, 50, player.transform.position));
        }
        base.OnTriggerEnter2D(collision); //llamada a parent script 
        gameObject.SetActive(false);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    void Arrived()
    {
        Destroy(gameObject);
    }

    /*Funci�n para que el eje x apunte en la direcci�n otorgada hacia delante. 
         Usado para determinar la rotaci�n en la funci�n anterior*/
    static Quaternion LookAt2D(Vector2 forward)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
    }
}
