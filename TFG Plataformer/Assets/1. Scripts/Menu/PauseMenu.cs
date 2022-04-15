using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

using TMPro;

public class PauseMenu : MonoBehaviour
{
    public Toggle toggle;

    public Animator settingsAnimatorA;
    public GameObject pauseMenu;
    [HideInInspector] public static bool isGamePaused;
    [HideInInspector] public static bool isPauseMenuOn = false;

    private bool wasPaused = false;

    void Start()
    {
        if (Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }
    }


    // Start is called before the first frame update
    void Awake()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        isGamePaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        Pause();
    }

    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenu.activeSelf)
        {
            if(isGamePaused)
            {
                wasPaused = true;
            }

            isPauseMenuOn = true;
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            isGamePaused = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf)
        {
            if (!wasPaused)
            {
                isGamePaused = false;
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            isPauseMenuOn = false;
            pauseMenu.SetActive(false);
            wasPaused = false;
        }
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void PauseOff()
    {
        if (isGamePaused)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            isGamePaused = false;
        }
    }
    public void ShowASettings()
    {
        settingsAnimatorA.SetBool("ShowSettings", true);
    }

    public void HideASettings()
    {
        settingsAnimatorA.SetBool("ShowSettings", false);
    }

    public void ActivarPantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }

    public bool IsGamePaused()
    {
        return isGamePaused;
    }
}
