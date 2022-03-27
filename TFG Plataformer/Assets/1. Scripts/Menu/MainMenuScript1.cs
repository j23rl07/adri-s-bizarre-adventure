using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript1 : MonoBehaviour
{

    public Animator settingsAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(){
        SceneManager.LoadScene(1);
    }

    public void ExitGame(){
        Application.Quit();
        print("game close");
    }

    public void GoToMainMenu(){
        SceneManager.LoadScene(0);
    }

    public void ShowSettings(){
        settingsAnimator.SetBool("ShowSettings", true);
    }

    public void HideSettings(){
        settingsAnimator.SetBool("ShowSettings", false);
    }
}
