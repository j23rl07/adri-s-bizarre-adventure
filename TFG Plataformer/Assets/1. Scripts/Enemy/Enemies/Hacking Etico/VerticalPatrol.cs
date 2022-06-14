using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform downPoint;
    [SerializeField] private Transform upPoint;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool moveLeft;

    [Header("Idle")]
    [SerializeField] private float waitPeriod;
    private float waitTimer;

    [Header("Enemy Animations")]
    [SerializeField] private Animator animator;

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    /*Se comprueba la posición actual del enemigo con la de los puntos de referencia para determinar si seguir caminando
     en la misma dirección o cambiar*/
    private void Update()
    {
        if (moveLeft)
        {
            if (enemy.position.y >= downPoint.position.y)
                MoveInDirection(-1);
            else
                DirectionChange();
        }
        else
        {
            if (enemy.position.y <= upPoint.position.y)
                MoveInDirection(1);
            else
                DirectionChange();
        }
    }

    /*Determina el tiempo que el enemigo se queda esperando en base a las variables de espera correspondientes.
     Una vez acabado ese periodo, continúa el movimiento*/
    private void DirectionChange()
    {
        waitTimer += Time.deltaTime;

        if (waitTimer > waitPeriod)
            moveLeft = !moveLeft;
    }

    private void MoveInDirection(int _direction)
    {
        waitTimer = 0;

        //Orienta al enemigo en una dirección determinada
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction,
            initScale.y, initScale.z);

        //Ele enemigo caminará en dicha dirección
        enemy.position = new Vector3(enemy.position.x,
            enemy.position.y + Time.deltaTime * _direction * speed, enemy.position.z);
    }
}
