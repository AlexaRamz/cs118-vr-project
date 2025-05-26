using UnityEngine;

public class TriggerSound3D : MonoBehaviour
{
    public AudioClip soundClip;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Player")) 
        //{
            audioSource.PlayOneShot(soundClip);
        //}
    }
}
