using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    [Tooltip("The key to pause the game and open the pause menu")]
    public KeyCode pauseKey;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        GameManager.Instance.EnterUIMode();

        GameManager.Instance.PauseGame();
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        GameManager.Instance.EnterGameplayMode();

        Time.timeScale = 1;
        GameManager.Instance.ResumeGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            if (GameManager.Instance.gameIsPaused)
            {

                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
}
