using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PlayerHP : MonoBehaviour
{
    [Header("Vidas")]
    public int MaxLives = 3;
    public int CurrentLives;
    public bool isInvincible = false;

    [Header("Screen Shake")]
    public ScreenShake screenShake;
    public GameObject Enemy1;
    public GameObject Enemy2;

    [Header("FreezeFrame")]
    public FreezeFrame freezeFrame;


    void Start()
    {
        CurrentLives = MaxLives;

    }

    public void TakeDamage() //Servira aqui mas a delante para actualizar el UI
    {
        if (isInvincible ) return; //Si el jugador es invencible, no se le quita vida
        CurrentLives--;
        isInvincible  = true; //Para que no lo mate de un vergazo

        Invoke(nameof(ResetInvincibility), 2f); //El jugador sera invencible por 2 segundos

        if (freezeFrame != null) //Si el FreezeFrame esta activo
        {
            freezeFrame.FreezeTime(); //Congela el tiempo por unos segundos
        }

        if (CurrentLives <= 0)
        {
            CurrentLives = 0;
            Time.timeScale = 1; //Asegura que el tiempo se reanude al morir
            SceneManager.LoadScene("DeathScene"); //Cuando las vidas son 0 se carga la Escena de muerte
        }
        //Screen Shake segun que enemigo colisiona con el jugador
        if (Enemy1.activeInHierarchy) //Si el enemigo 1 esta activo
        {
            screenShake.ShakeCamera(0.3f, 1.5f, 1.5f); //Sacude la camara por 0.2 segundos con amplitud y frecuencia de 1
        }
        else if (Enemy2.activeInHierarchy) //Si el enemigo 2 esta activo
        {
            screenShake.ShakeCamera(0.5f, 4f, 4f); //Sacude la camara por 0.3 segundos
        }
    }

    //Para colisiones Fisicas Hitbox
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(); //Si el jugador colisiona con un enemigo, se le quita una vida
        }
    }

    //Para colisiones con IsTrigger (No fisicas)
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(); //Si el jugador entra en el trigger de un enemigo, se le quita una vida
        }
    }

    public int GetCurrentLives() //Para que otros scripts puedan acceder a las vidas actuales del jugador
    {
        return CurrentLives;
    }

    public void ResetLives() //Es para mas adelante pa hacer los checkpoints
    {
        CurrentLives = MaxLives;
    }

    private void ResetInvincibility()
    {
        isInvincible  = false; //Resetea la invencibilidad
    }
}
