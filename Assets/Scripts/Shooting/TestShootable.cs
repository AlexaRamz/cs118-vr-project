using UnityEngine;
using UnityEngine.Rendering;

public class TestShootable : MonoBehaviour, IShootable
{
    public AudioClip hitSound;
    private AudioSource audioSource;

    private float lastHitTime = -Mathf.Infinity;
    private float soundCooldown = 0.3f;

    private Renderer rend;
    private Material materialInstance;
    private Color initialAlbedoColor;

    private float currentTransparency = 1f;
    [SerializeField] private float hitFadeStep = 0.1f;
    [SerializeField] private float fadeOutDuration = 2f;
    [SerializeField] private float fadeInSpeed = 1f;

    private bool isHitRecently = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rend = GetComponent<Renderer>();

        materialInstance = rend.material;
        initialAlbedoColor = materialInstance.color;

        SetMaterialToTransparentMode();
        ApplyTransparency(currentTransparency);
    }

    public void OnHit()
    {
        float currentTime = Time.time;

        if (currentTime - lastHitTime >= soundCooldown)
        {
            Debug.Log("The object was shot");

            if (hitSound && audioSource)
            {
                audioSource.PlayOneShot(hitSound);
            }
            lastHitTime = currentTime;
        }

        isHitRecently = true;

        currentTransparency -= hitFadeStep;
        currentTransparency = Mathf.Max(0f, currentTransparency);
        ApplyTransparency(currentTransparency);
    }

    void Update()
    {
        if (Time.time - lastHitTime > soundCooldown)
        {
            isHitRecently = false;
        }

        if (!isHitRecently && currentTransparency < 1f)
        {
            currentTransparency += fadeInSpeed * Time.deltaTime;
            currentTransparency = Mathf.Min(1f, currentTransparency);

            ApplyTransparency(currentTransparency);
        }
    }

    private void ApplyTransparency(float alphaValue)
    {
        Color newColor = initialAlbedoColor;
        newColor.a = alphaValue;
        materialInstance.color = newColor;
    }

    private void SetMaterialToTransparentMode()
    {
        materialInstance.SetFloat("_Surface", 1f);

        materialInstance.SetInt("_Blend", (int)BlendMode.SrcAlpha);
        materialInstance.SetInt("_SrcBlend", (int)BlendMode.SrcAlpha);
        materialInstance.SetInt("_DstBlend", (int)BlendMode.OneMinusSrcAlpha);

        materialInstance.SetInt("_ZWrite", 0);
        materialInstance.DisableKeyword("_ALPHATEST_ON");
        materialInstance.EnableKeyword("_ALPHABLEND_ON");
        materialInstance.DisableKeyword("_ALPHAPREMULTIPLY_ON");

        materialInstance.renderQueue = (int)RenderQueue.Transparent;
    }

    void OnDestroy()
    {
        if (materialInstance != null)
        {
            Destroy(materialInstance);
        }
    }
}