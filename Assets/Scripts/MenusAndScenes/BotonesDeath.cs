using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonesDeath : MonoBehaviour
{
    public void ReiniciarPartida()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void RegresarAMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
