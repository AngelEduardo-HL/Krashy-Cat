using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class MeneController : MonoBehaviour
{
    public RectTransform targetToTween;

    public float animationDuration = 1.0f;


    public void IniciarPartida()
    {
        targetToTween.DOScale(Vector3.one,animationDuration).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            SceneManager.LoadScene("SampleScene");
        });
    }

    public void Opciones()
    {
        targetToTween.DOScale(Vector3.one, animationDuration).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            SceneManager.LoadScene("Options");
        });
    }

    public void Regresar()
    {
        targetToTween.DOScale(Vector3.one, animationDuration).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            SceneManager.LoadScene("Main Menu");
        });
    }

    public void CerrarJuego()
    {
        
    }
}
