using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAInoScale : MonoBehaviour
{
    public Transform target;
    public float speed = 200f;
    public float nextRefPointDistance = 3f;

    public Transform enemyGraphics;

    Path path;
    int currentRefPoint = 0;
    bool pathIsEnded = false;

    Seeker seeker;
    Rigidbody2D rb;

    void Start()
    {
        //Llama a los componentes a usar
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        //Para generar el camino constantemente
        InvokeRepeating("UpdatePath", 0f, .5f);

    }

    void UpdatePath()
    {
        //Generar el camino de la IA, a través de la posición inicial, posición final y función para calcular el camino
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        //Si no hay errores en el camino generado, se setea el nuevo camino y se resetea el punto de referencia entre caminos
        if (!p.error)
        {
            path = p;
            currentRefPoint = 0;
        }
    }

    void Update()
    {
        //Aseguramos que exista un camino definido
        if (path == null)
            return;

        //Comprobamos que haya puntos de referencia en el camino o por el contrario, se ha llegado al final (no más puntos de referencia)
        if (currentRefPoint >= path.vectorPath.Count)
        {
            pathIsEnded = true;
            return;
        }
        else
        {
            pathIsEnded = false;
        }

        //Aplicamos la fuerza de movimiento al enemigo

        Vector2 dir = ((Vector2)path.vectorPath[currentRefPoint] - rb.position).normalized;
        Vector2 movForce = dir * speed * Time.deltaTime;

        rb.AddForce(movForce);

        float dist = Vector2.Distance(rb.position, path.vectorPath[currentRefPoint]);

        //Actualizamos el punto de referencia del camino
        if (dist < nextRefPointDistance)
        {
            currentRefPoint++;
        }

        if (rb.velocity.x >= 0.01f)
        {
            //Aplicamos la transformación del sprite para orientarlo hacia la derecha
            enemyGraphics.localScale = new Vector3(-1f, 1f, 1f);

            //Comprobamos el caso contrario: hacia la izquierda
        }
        else if (rb.velocity.x <= -0.01f)
        {
            enemyGraphics.localScale = new Vector3(1f, 1f, 1f);
        }

    }
}
