using UnityEngine;
using Unity.Cinemachine;
using System.Collections;
public class ScreenShake : MonoBehaviour
{
    [Header("Screen Shake Settings")]
    public float shakeTime = 0.5f;
    public float shakeAmplitude = 0.5f;
    public float shakeFrequency = 2f;

    public CinemachineBasicMultiChannelPerlin perlin;

    private void Start()
    {
        //Asegurarse que no haya shake al iniciar el juego
        perlin.AmplitudeGain = 0;
        perlin.FrequencyGain = 0;
    }

    public void ShakeCamera(float duration, float shakeAmplitude, float shakeFrequency)
    {
        perlin.AmplitudeGain = shakeAmplitude; // Establece la amplitud del sacudón
        perlin.FrequencyGain = shakeFrequency; // Establece la frecuencia del sacudón
        StartCoroutine(StopShakeRoutine(shakeTime)); // Inicia la corrutina para detener el sacudón después de un tiempo
    }

    IEnumerator StopShakeRoutine(float shakeTime)
    {
        yield return new WaitForSeconds(shakeTime);
        perlin.AmplitudeGain = 0;
        perlin.FrequencyGain = 0;
    }
}
