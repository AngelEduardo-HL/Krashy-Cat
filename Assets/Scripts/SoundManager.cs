using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;

public class SoundManager : MonoBehaviour
{
    public Sound[] soundList;
    private const float MIN_PITCH = 0.85f;
    private const float MAX_PITCH = 1.2f;


    void Start()
    {
        for (int i = 0; i < soundList.Length; i++)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();

            soundList[i].SetUpAudioSource(audioSource);
            soundList[i].audioSource.playOnAwake = false;
        }
    }

    public void PlayRandomPitch(string name)
    {
        Sound sound = FindSound(name);
        sound.audioSource.pitch = Random.Range(MIN_PITCH, MAX_PITCH);
        PlaySound(name);
    }

    public void FadeInSound(string name, float fadeTime)
    {
        Sound sound = FindSound(name);
        sound.audioSource.volume = 0;
        sound.audioSource.Play();
        sound.audioSource.DOFade(sound.volume, fadeTime).SetUpdate(true);
    }

    public void FadeOutSound(string name, float fadeTime)
    {
        Sound sound = FindSound(name);
        sound.audioSource.DOFade(0, fadeTime).OnComplete(sound.audioSource.Stop).SetUpdate(true);
    }

    public void PlaySound(string name)
    {
        Sound sound = FindSound(name);
        sound.audioSource?.Play();
    }

    public void StopSound(string name)
    {
        Sound sound = FindSound(name);
        sound.audioSource?.Stop();
    }

    public void PauseSound(string name)
    {
        Sound sound = FindSound(name);
        sound.audioSource?.Pause();
    }

    private Sound FindSound(string name)
    {
        for (int i = 0; i < soundList.Length; i++)
        {
            if (name == soundList[i].audioName)
            {
                return soundList[i];
            }
        }

        Debug.LogWarning($"<color=yellow>{name} does not exist</color>");
        return null;
    }

}

[System.Serializable]
public class Sound
{
    public string audioName;
    public AudioClip audioClip;
    public float volume;
    public float pitch;
    public bool loop;

    public AudioMixerGroup mixerGroup;

    [HideInInspector]
    public AudioSource audioSource;

    public void SetUpAudioSource(AudioSource audioSource)
    {
        this.audioSource = audioSource;
        this.audioSource.clip = audioClip;
        this.audioSource.volume = volume;
        this.audioSource.pitch = pitch;
        this.audioSource.loop = loop;
        this.audioSource.outputAudioMixerGroup = mixerGroup;
    }
}

