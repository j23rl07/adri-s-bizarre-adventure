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
    private bool needsHeal;
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
            if (inRange & canBeUsed & needsHeal & Input.GetKeyDown(KeyCode.E))
            {
                playerScript.healMana(Player.maxMana);
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
            if (playerScript.currentMana < Player.maxMana)
            {
                needsHeal = true;
            }
            else
            {
                needsHeal = false;
            }
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
