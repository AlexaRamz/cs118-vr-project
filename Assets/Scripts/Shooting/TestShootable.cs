using UnityEngine;

public class TestShootable : MonoBehaviour, IShootable
{
    public GameObject hitEffectPrefab;
    public AudioClip hitSound;
    private AudioSource audioSource;

    private float lastHitTime = -Mathf.Infinity;
    private float soundCooldown = 0.3f;  

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnHit()
    {
        float currentTime = Time.time;

 
        if (currentTime - lastHitTime >= soundCooldown)
        {
            Debug.Log("The object was shot");

            if (hitEffectPrefab)
            {
                Instantiate(hitEffectPrefab, transform.position + Vector3.up * 0.1f, Quaternion.identity);
            }

            if (hitSound && audioSource)
            {
                audioSource.PlayOneShot(hitSound);
            }

            lastHitTime = currentTime;
        }
    }
}
