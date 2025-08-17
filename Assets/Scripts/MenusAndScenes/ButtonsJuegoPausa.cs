using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsJuegoPausa : MonoBehaviour
{
    public Pausa pausaScript; 
    public void ReiniciarPartida()
    {
        pausaScript.TogglePause(false);
        SceneManager.LoadScene("SampleScene");
    }

    public void RegresarAMenu()
    {
        pausaScript.TogglePause(false);
        SceneManager.LoadScene("Main Menu");
    }
}
