using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.InputSystem;

public class WaterGun : MonoBehaviour
{
    [SerializeField] private ParticleSystem waterStreamEffect;
    private XRGrabInteractable grabInteractable;
    [SerializeField] private InputActionProperty activateAction;
    public bool useXRInput = false;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(OnSelectEntered);
        grabInteractable.selectExited.AddListener(OnSelectExited);

        if (activateAction.action != null)
        {
            activateAction.action.Enable();
        }
    }


    void Update()
    {
        if (!useXRInput)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartShooting();
            }
            if (Input.GetMouseButtonUp(0))
            {
                StopShooting();
            }
        }
    }

    void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnSelectEntered);
            grabInteractable.selectExited.RemoveListener(OnSelectExited);
        }

        if (activateAction.action != null)
        {
            activateAction.action.Disable();
        }
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        activateAction.action.performed += OnActivatePerformed;
        activateAction.action.canceled += OnActivateCanceled;
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        StopShooting();

        activateAction.action.performed -= OnActivatePerformed;
        activateAction.action.canceled -= OnActivateCanceled;
    }

    private void OnActivatePerformed(InputAction.CallbackContext context)
    {
        if (useXRInput) StartShooting();
    }

    private void OnActivateCanceled(InputAction.CallbackContext context)
    {
        if (useXRInput) StopShooting();
    }

    private void StartShooting()
    {
        waterStreamEffect.Play();
    }

    private void StopShooting()
    {
        waterStreamEffect.Stop();
    }

    public void OnWaterCollision(GameObject hitObject)
    {
        IShootable shootable = hitObject.GetComponent<IShootable>();
        if (shootable != null)
        {
            shootable.OnHit();
            if (ScoreManager.Instance == null)
            {
                Debug.LogError("Score manager does not exist!");
            }
            else
            {
                // TODO: Increment score each time a new object is hit
                ScoreManager.Instance.IncrementScore();
            }
        }
    }
}
