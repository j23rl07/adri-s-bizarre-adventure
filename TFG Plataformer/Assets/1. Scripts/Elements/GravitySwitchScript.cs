using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwitchScript : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D playerRB;
    private PlayerMovement playerMovementScript;
    private BasicCameraController cameraControllerScript;

    [SerializeField] private GameObject hintText;

    private bool hasAccess = false;

    private void Start()
    {
        if (hintText != null)
            hintText.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        playerRB = player.GetComponent<Rigidbody2D>();
        playerMovementScript = player.GetComponent<PlayerMovement>();
        cameraControllerScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BasicCameraController>();
    }
    private void Update()
    {
        if(hasAccess && Input.GetKeyDown(KeyCode.E))
        {
            FlipGravity();
            player.transform.Rotate(180f, 0f, 0f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hasAccess = true;
            if(hintText != null)
                hintText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hasAccess = false;
            if (hintText != null)
                hintText.gameObject.SetActive(false);
        }
    }

    private void FlipGravity()
    {
        playerRB.gravityScale = -1*playerMovementScript.gravity;
        playerMovementScript.gravity *= -1;
        cameraControllerScript.offset.y = - cameraControllerScript.offset.y;
        //Cambia la dirección en la que se comprueba la colisión con el suelo
        playerMovementScript.groundCheckDirection *= -1;
    }
}
