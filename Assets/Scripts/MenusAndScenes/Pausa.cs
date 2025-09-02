using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Pausa : MonoBehaviour
{
    public KeyCode pausaKey;
    public CanvasGroup canvasGroup;

    private bool gamePaused;
    private const float TWEEN_TIME = 0.3f;
    private Tween pauseTween;
    public SoundManager uiSoundManager;
    public AudioMixerGroup audioMixerGroupMaster, audioMixerGroupMusic, audioMixerGroupSFX;

    void Start()
    {
        gamePaused = false;
        canvasGroup.alpha = 0;
        uiSoundManager.PlaySound("GameMusic");
        //uiSoundManager?.FadeInSound("GameMusic", 2);
    }


    void Update()
    {
        if (Input.GetKeyDown(pausaKey))
        {
            TogglePause(!gamePaused);
            if (gamePaused)
            {
                uiSoundManager?.FadeOutSound("GameMusic", 1);
            }
            else
            {
                uiSoundManager?.FadeInSound("GameMusic", 1);
            }

        }
    }

    public void TogglePause(bool pausado)
    {
        Time.timeScale = pausado ? 0 : 1;

        float canvasAlpha = pausado ? 1 : 0;

        pauseTween?.Kill();
        canvasGroup.interactable = pausado;
            canvasGroup.blocksRaycasts = pausado;

        pauseTween = canvasGroup.DOFade(canvasAlpha, TWEEN_TIME).SetUpdate(true).OnComplete(() => {
            
        });
        gamePaused = pausado;
    }

    public void SetMasterVolume(float volume)
    {
        audioMixerGroupMaster.audioMixer.SetFloat("VolumeMaster", volume);
    }

    public void SetMasterMusic(float volume)
    {
        audioMixerGroupMusic.audioMixer.SetFloat("VolumeMusic", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixerGroupSFX.audioMixer.SetFloat("VolumeSFX", volume);
    }


    public void PressButtonSound()
    {
        uiSoundManager.PlaySound("ButtonClick");
    }
    public void HoverButtonSound()
    {
        uiSoundManager.PlaySound("ButtonHover");
    }

}
