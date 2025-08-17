using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Pausa : MonoBehaviour
{
    public KeyCode pausaKey;
    public CanvasGroup canvasGroup;

    private bool gamePaused;
    private const float TWEEN_TIME = 0.3f;
    private Tween pauseTween;

    void Start()
    {
        gamePaused = false;
        canvasGroup.alpha = 0;
    }


    void Update()
    {
        if (Input.GetKeyDown(pausaKey))
        {
            TogglePause(!gamePaused);
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
}
