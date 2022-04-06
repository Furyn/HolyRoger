using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    public GameObject winPanel = null;
    public GameObject losePanel = null;
    public AudioSource source = null;
    public float pitch = 1f;

    public int NextLevelScene = 0;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void Win()
    {
        source.pitch = pitch;
        AudioManager.Instance.PlaySound(source, "WIN");
        winPanel.SetActive(true);
    }

    public void Lose()
    {
        source.pitch = pitch;
        AudioManager.Instance.PlaySound(source, "LOSE");
        losePanel.SetActive(true);
    }

    public void NextLevel()
    {
        AudioManager.Instance.PlaySound(source, "BUTTON_PRESSED");
        SceneManager.LoadScene(NextLevelScene);
    }

    public void Retry()
    {
        AudioManager.Instance.PlaySound(source, "BUTTON_PRESSED");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        AudioManager.Instance.PlaySound(source, "BUTTON_PRESSED");
        SceneManager.LoadScene(0);
    }

}
