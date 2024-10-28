using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class UIAudioSource : MonoBehaviour
{
    AudioSource audioSource;

    public static UIAudioSource Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }

        Instance = this;
    }


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClip(AudioResource resource)
    {
        if (audioSource != null)
        {
            audioSource.resource = resource;
            audioSource.Play();
        }
    }
}
