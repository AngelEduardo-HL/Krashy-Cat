using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour
{
    [Header("Vidas")]
    public int MaxLives = 3;
    public int CurrentLives;

    private bool isInvincible  = false;

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

        if (CurrentLives <= 0)
        {
            CurrentLives = 0;
            SceneManager.LoadScene("DeathScene"); //Cuando las vidas son 0 se carga la Escena de muerte
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
