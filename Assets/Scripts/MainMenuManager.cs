using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public GameObject level_panel = null;
    public GameObject main_panel = null;
    public AudioSource source = null;

    public void PlayButton()
    {
        AudioManager.Instance.PlaySound(source, "BUTTON_PRESSED");
        SceneManager.LoadScene(1);
    }

    public void OpenLevelPanel()
    {
        AudioManager.Instance.PlaySound(source, "BUTTON_PRESSED");
        level_panel.SetActive(true);
        main_panel.SetActive(false);
    }

    public void CloseLevelPanel()
    {
        AudioManager.Instance.PlaySound(source, "BUTTON_PRESSED");
        level_panel.SetActive(false);
        main_panel.SetActive(true);
    }

    public void OpenLevel(int LevelInt)
    {
        SceneManager.LoadScene(LevelInt);
    }
}
