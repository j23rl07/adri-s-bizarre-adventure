using UnityEngine;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour
{
    [SerializeField] private Slider musica;
    [SerializeField] private Slider sonido;
    // Start is called before the first frame update
    void Start()
    {
        LoadSettings();
    }

    private void LoadSettings()
    {
        musica.value = PlayerPrefs.GetFloat("MV");
        sonido.value = PlayerPrefs.GetFloat("EV");
    }

    public void SetMusicVolume()
    {
        PlayerPrefs.SetFloat("MV", musica.value);
    }

    public void SetEffectVolume()
    {
        PlayerPrefs.SetFloat("EV", sonido.value);
    }
}
