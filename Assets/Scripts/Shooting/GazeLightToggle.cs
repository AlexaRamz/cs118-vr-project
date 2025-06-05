using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable))]
public class GazeLightToggle : MonoBehaviour
{
    public GameObject targetObject;

    void Start()
    {
        var interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        interactable.hoverEntered.AddListener(OnGazeEnter);
        interactable.hoverExited.AddListener(OnGazeExit);

        if (targetObject != null)
            targetObject.SetActive(false); 
    }

    private void OnGazeEnter(HoverEnterEventArgs args)
    {
        Debug.Log("Gaze Entered");
        if (targetObject != null)
            targetObject.SetActive(true);
    }

    private void OnGazeExit(HoverExitEventArgs args)
    {
        Debug.Log("Gaze Exited");
        if (targetObject != null)
            targetObject.SetActive(false);
    }
}
