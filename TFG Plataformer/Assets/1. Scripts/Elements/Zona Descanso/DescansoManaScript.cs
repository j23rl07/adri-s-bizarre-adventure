using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescansoManaScript : MonoBehaviour
{

    private GameObject availableText;
    private GameObject emptyText;
    private GameObject currentText;
    private bool canBeUsed = true;
    private bool inRange = false;
    private Player playerScript;
    [SerializeField] private int uses = 1;
    
    void Start()
    {
        availableText = transform.GetChild(0).GetChild(0).gameObject;
        availableText.SetActive(false);
        emptyText = transform.GetChild(0).GetChild(1).gameObject;
        emptyText.SetActive(false);
        currentText = availableText;
    }
    void Update()
    {
        if (!PauseMenu.isPauseMenuOn)
        {
            if (inRange & canBeUsed & playerScript.currentMana < playerScript.maxMana  & Input.GetKeyDown(KeyCode.E))
            {
                playerScript.healMana(playerScript.maxMana);
                uses -= 1;
                if (uses <= 0)
                    ToggleUse();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            currentText.SetActive(true);
            inRange = true;
            playerScript = collision.gameObject.GetComponent<Player>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            currentText.SetActive(false);
            inRange = false;
        }
    }

    private void ToggleUse()
    {
        canBeUsed = !canBeUsed;
        if (inRange)
            currentText.SetActive(false);
        if (canBeUsed)
        {
            currentText = availableText;
        }
        else
        {
            currentText = emptyText;
        }
        if (inRange)
            currentText.SetActive(true);
    }
}
