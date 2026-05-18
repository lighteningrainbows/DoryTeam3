using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;

    public string titleSceneName = "Title";

    bool isPaused;

    void Start()
    {
        SetPause(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPause(!isPaused);
        }

        if (!isPaused) return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(titleSceneName);
        }
    }

    void SetPause(bool pause)
    {
        isPaused = pause;

        pausePanel.SetActive(isPaused);

        Time.timeScale = isPaused ? 0f : 1f;
    }

    public bool IsPaused()
    {
        return isPaused;
    }
}