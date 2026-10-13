using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;



    [SerializeField] private AudioSource sfxObject;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void PlaySFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(sfxObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClip;         
        audioSource.volume = volume;
        audioSource.Play(); //play sound
        float clipLength = audioSource.clip.length; //get the length of the audio clip
        Destroy(audioSource.gameObject, clipLength); //Destroy audio clip after it's finished playing
    }


    public AudioSource PlayLoopingSFX(AudioClip clip, Transform spawnTransform, float volume)
    {
        if (clip == null) return null;
        AudioSource loopedSFX = Instantiate(sfxObject, spawnTransform.position, Quaternion.identity);
        loopedSFX.transform.SetParent(spawnTransform);

        loopedSFX.clip = clip;
        loopedSFX.volume = volume;
        loopedSFX.loop = true;
        

        loopedSFX.Play();
        return loopedSFX;
    }

    public void StopLoopingSFX(AudioSource loopedSFX)
    {
        if (loopedSFX != null)
        {
            Destroy(loopedSFX.gameObject);
            loopedSFX = null;
        }
    }
}
