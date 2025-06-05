using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ActivateWhenGrabbedAndClicked : MonoBehaviour
{
    public GameObject targetObject;           
    public AudioClip clickSound;               
    private AudioSource audioSource;
    private XRGrabInteractable grabInteractable;

    private bool hasActivated = false;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (grabInteractable.isSelected)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (targetObject != null && !targetObject.activeSelf)
                {
                    targetObject.SetActive(true);
                    Debug.Log("Object activated while grabbed!");

                    if (clickSound != null)
                    {
                        audioSource.PlayOneShot(clickSound);
                    }

                    Invoke("DeactivateTarget", 6f);
                }
            }
        }
    }

    void DeactivateTarget()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(false);
            Debug.Log("Object deactivated after 5 seconds.");
        }
    }
}
