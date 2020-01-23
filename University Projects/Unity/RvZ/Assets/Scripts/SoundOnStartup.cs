using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundOnStartup : MonoBehaviour
{
    [Tooltip("The sound to play when this object is created.")]
    public AudioClip startupSound;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(startupSound);
    }
}
