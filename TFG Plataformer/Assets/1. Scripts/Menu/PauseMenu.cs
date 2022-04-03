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
    private bool isPaused;

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
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        Pause();
    }

    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            isPaused = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            isPaused = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void PauseOff()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            isPaused = false;
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
        return isPaused;
    }
}
