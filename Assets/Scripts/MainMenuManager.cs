using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public GameObject level_panel = null;
    public GameObject main_panel = null;

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenLevelPanel()
    {
        level_panel.SetActive(true);
        main_panel.SetActive(false);
    }

    public void OpenLevel(int LevelInt)
    {
        SceneManager.LoadScene(LevelInt);
    }
}
