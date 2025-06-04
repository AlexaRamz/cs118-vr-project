//using UnityEngine;

//public class TestShootable : MonoBehaviour, IShootable
//{
//    public AudioClip hitSound;
//    private AudioSource audioSource;

//    private float lastHitTime = -Mathf.Infinity;
//    private float soundCooldown = 0.3f;  

//    void Start()
//    {
//        audioSource = GetComponent<AudioSource>();
//    }

//    public void OnHit()
//    {
//        float currentTime = Time.time;


//        if (currentTime - lastHitTime >= soundCooldown)
//        {
//            Debug.Log("The object was shot");


//            if (hitSound && audioSource)
//            {
//                audioSource.PlayOneShot(hitSound);
//            }

//            lastHitTime = currentTime;
//        }
//    }
//}

using UnityEngine;

public class TestShootable : MonoBehaviour, IShootable
{
    public AudioClip hitSound;
    private AudioSource audioSource;

    private float lastHitTime = -Mathf.Infinity;
    private float soundCooldown = 0.3f;

    private Renderer rend;
    private Material mat;
    private Color originalColor;
    private float currentAlpha = 1f;
    private float fadeStep = 0.1f;
    private float fadeDuration = 2f;
    private float fadeCooldown = 0.2f; // 투명화나 복원 한 단계당 시간 (2초에 10단계)
    private float nextFadeTime = 0f;
    private bool isBeingShot = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rend = GetComponent<Renderer>();
        mat = rend.material;
        originalColor = mat.color;

        SetMaterialToTransparent();
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

        isBeingShot = true;
    }

    void Update()
    {
        float currentTime = Time.time;

        if (currentTime >= nextFadeTime)
        {
            nextFadeTime = currentTime + fadeCooldown;

            if (isBeingShot && currentAlpha > 0.1f)
            {
                // 투명해짐
                currentAlpha -= fadeStep;
                currentAlpha = Mathf.Max(0.1f, currentAlpha);
                ApplyAlpha(currentAlpha);
            }
            else if (!isBeingShot && currentAlpha < 1f)
            {
                // 복원
                currentAlpha += fadeStep;
                currentAlpha = Mathf.Min(1f, currentAlpha);
                ApplyAlpha(currentAlpha);
            }
        }

        // 일정 시간 지나면 쏘고 있지 않다고 판단
        if (Time.time - lastHitTime > 0.3f)
        {
            isBeingShot = false;
        }
    }

    private void ApplyAlpha(float alpha)
    {
        Color c = originalColor;
        c.a = alpha;
        mat.color = c;
    }

    private void SetMaterialToTransparent()
    {
        // Standard Shader 기준
        mat.SetFloat("_Mode", 3);
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = 3000;
    }
}

