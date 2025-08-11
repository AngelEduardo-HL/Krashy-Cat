using UnityEngine;
using System.Collections;

public class FreezeFrame : MonoBehaviour
{
    [Header("Freeze Frame Settings")]
    public float freezeDuration;
    public float freezeValueTime;
    public Coroutine freezeCoroutine;

    public void FreezeTime()
    {
        if (freezeCoroutine != null)
        {
            StopCoroutine(freezeCoroutine); // Detiene cualquier congelación anterior
        }

        freezeCoroutine = StartCoroutine(FreezeFrameCoroutine()); // Inicia una nueva congelación
    }

    IEnumerator FreezeFrameCoroutine()
    {
        Time.timeScale = freezeValueTime; // Congela el tiempo
        yield return new WaitForSecondsRealtime(freezeDuration); // Espera el tiempo congelado
        Time.timeScale = 1; // Restaura el tiempo
    }
}
