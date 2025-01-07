using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance {get; private set; }
    private AudioSource soundSource;
    private AudioSource musicSource;

    private void Awake()
    {
        instance = this;
        soundSource = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>(); // this is located under sound manger game object

        // Keep this object even when changing scenes
        if(instance == null) {
            instance = this; 
            DontDestroyOnLoad(gameObject);
        } 
        // Destroy duplicate game objects 
        else if (instance != null && instance != this) {
            Destroy(gameObject);
        }

        //Assign initial volumes
        ChangeSoundsVolume(0);
        ChangeMusicVolume(0);
    }


/*     public void PlaySound(AudioClip _soure) {
        soundSource.PlayOneShot(_soure);
    } */

    public void PlaySound(AudioClip clip, float startTime = 0f, bool loop = false)
    {
        //TODO: not all these featrues work... needs to be fixed
        soundSource.loop = loop;
        soundSource.clip = clip;
        soundSource.time = startTime;
        soundSource.PlayOneShot(soundSource.clip);

/*         soundSource.clip = clip;          // Assign the clip to the AudioSource
        soundSource.time = startTime;     // Set the starting time
        soundSource.Play();               // Play the clip from the specified time */

    }


    
    public void ChangeSoundsVolume(float _change)
    {
        
        ChangeSourceVolume(1, "soundsVolume", _change, soundSource);
    }
    public void ChangeMusicVolume(float _change)
    {
        ChangeSourceVolume(0.3f, "musicVolume", _change, musicSource);
    }


    private void ChangeSourceVolume(float baseVolume, string volumeName, float change, AudioSource source)
    {
        //Get initial value of volume and change it
        float currentVolume = PlayerPrefs.GetFloat(volumeName, 1);
        currentVolume += change;

        //Check if we reached the maximum or minimum value
        if (currentVolume > 1)
            currentVolume = 0;
        else if (currentVolume < 0)
            currentVolume = 1;

        //Assign final value
        float finalVolume = currentVolume * baseVolume;
        source.volume = finalVolume;

        //Save final value to player prefs
        PlayerPrefs.SetFloat(volumeName, currentVolume);
    }

}
