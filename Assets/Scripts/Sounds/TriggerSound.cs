using System.Collections;
using UnityEngine;

public class TriggerSound : MonoBehaviour
{
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        audioSource.Play(0); // Simply plays the audio clip specified in the Audio Source component
    }

    public void StartFadeOut(float fadeDuration)
    {
        StartCoroutine(FadeOut(fadeDuration));
    }

    IEnumerator FadeOut(float fadeDuration)
    {
        float startVolume = audioSource.volume;

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            Debug.Log("fading");
            audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}
