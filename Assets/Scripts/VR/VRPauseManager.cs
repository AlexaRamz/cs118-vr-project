using UnityEngine;
using UnityEngine.InputSystem;

public class VRPauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    public InputActionProperty pauseButton;
    public Transform head;
    public float spawnDistance = 2;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        pauseMenu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;

        GameManager.Instance.EnterUIMode();
        GameManager.Instance.PauseGame();
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);

        GameManager.Instance.EnterGameplayMode();
        GameManager.Instance.ResumeGame();
    }

    private void Update()
    {
        if (pauseButton.action.WasPressedThisFrame())
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
        if (GameManager.Instance.gameIsPaused)
        {
            pauseMenu.transform.LookAt(new Vector3(head.position.x, pauseMenu.transform.position.y, head.position.z));
            pauseMenu.transform.forward *= -1;
        }
    }
}
