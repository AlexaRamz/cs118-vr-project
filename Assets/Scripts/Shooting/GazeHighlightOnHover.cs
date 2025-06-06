using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable))]
public class GazeHighlightOnHover : MonoBehaviour
{
    private Material originalMat;
    public Material highlightMat;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalMat = rend.material;

        var interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        interactable.hoverEntered.AddListener(OnHoverEnter);
        interactable.hoverExited.AddListener(OnHoverExit);
    }

    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        if (highlightMat != null)
            rend.material = highlightMat;
    }

    private void OnHoverExit(HoverExitEventArgs args)
    {
        rend.material = originalMat;
    }
}
