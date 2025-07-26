using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MeneController : MonoBehaviour
{
    public string sceneNameToLoad;
    public RectTransform targetToTween;

    public float animationDuration = 1.0f;

    public void IniciarPartida()
    {
        targetToTween.DOSizeDelta(new Vector2(Screen.width, Screen.height),0.5f).SetEase(Ease.OutExpo).OnComplete(() =>
        {
            SceneManager.LoadScene("SampleScene");
        });
    }

    public void CerrarJuego()
    {

    }
}
