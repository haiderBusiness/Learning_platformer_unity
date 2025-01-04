using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance {get; private set; }
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();

        // Keep this object even when changing scenes
        if(instance == null) {
            instance = this; 
            DontDestroyOnLoad(gameObject);
        } 
        // Destroy duplicate game objects 
        else if (instance != null && instance != this) {
            Destroy(gameObject);
        }
    }


/*     public void PlaySound(AudioClip _soure) {
        audioSource.PlayOneShot(_soure);
    } */

    public void PlaySound(AudioClip clip, float startTime = 0f)

    {
        audioSource.clip = clip;
        audioSource.time = startTime;
        audioSource.PlayOneShot(audioSource.clip);

/*         audioSource.clip = clip;          // Assign the clip to the AudioSource
        audioSource.time = startTime;     // Set the starting time
        audioSource.Play();               // Play the clip from the specified time */

    }

}
