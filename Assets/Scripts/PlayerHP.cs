using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class PlayerHP : MonoBehaviour
{
    [Header("Vidas")]
    public int initialLives = 3;
    public int currentLives;
    public bool isInvincible = false;
    public TextMeshProUGUI healthText;

    [Header("Screen Shake")]
    public ScreenShake screenShake;


    [Header("FreezeFrame")]
    public FreezeFrame freezeFrame;

    [Header("Sound")]
    public SoundManager playerSoundManager;
    public SoundManager enemiesSoundManager;

    [Header("Shield")]
    public GameObject shieldPrefab;
    bool isShieldActive;

    void Start()
    {
        currentLives = initialLives;
        healthText.text = ("" + currentLives);
    }

    public void TakeDamage() //Servira aqui mas a delante para actualizar el UI
    {
        if (isShieldActive || isInvincible)
        {
            shieldPrefab.SetActive(false);
            isInvincible = false;
            isShieldActive = false;
            return;
        }
        //if (isInvincible)
        //{
        //    return; //Si el jugador es invencible, no se le quita vida
        //}
        
        currentLives--;
        healthText.text = ("" + currentLives);
        isInvincible  = true; //Para que no lo mate de un vergazo

        Invoke(nameof(ResetInvincibility), 2f); //El jugador sera invencible por 2 segundos

        if (freezeFrame != null) //Si el FreezeFrame esta activo
        {
            freezeFrame.FreezeTime(); //Congela el tiempo por unos segundos
        }

        if (currentLives <= 0)
        {
            currentLives = 0;
            healthText.text = ("" + currentLives);
            Time.timeScale = 1; //Asegura que el tiempo se reanude al morir
            SceneManager.LoadScene("DeathScene"); //Cuando las vidas son 0 se carga la Escena de muerte
        }

        CheckpointManager.Instance?.RespawnPlayer(gameObject); //Respawnea al jugador en el ultimo checkpoint

    }

    public void ActiveInvincible()
    {
        isShieldActive = true;
        shieldPrefab.SetActive(true);
        shieldPrefab.gameObject.GetComponent<Shield>().ActiveShield();
        isInvincible = true;
    }


    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
            playerSoundManager?.PlayRandomPitch("Damage");
            if (hit.gameObject.layer == 6)
            {
                screenShake.ShakeCamera(0.3f, 1.5f, 1.5f);
                enemiesSoundManager?.PlayRandomPitch("HenAttack");

            }
            else if (hit.gameObject.layer == 7)
            {
                screenShake.ShakeCamera(0.5f, 4f, 4f);
                enemiesSoundManager?.PlayRandomPitch("DogAttack");
            }
            else if (hit.gameObject.layer == 8)
            {
                screenShake.ShakeCamera(0.8f, 5f, 5f);
                enemiesSoundManager.PlaySound("Sprinkler");
                Debug.Log("Sprinkler");
            }
        }
        if (hit.gameObject.layer == 11)
        {
            currentLives++;
            playerSoundManager?.PlayRandomPitch("Life");
            hit.gameObject.SetActive(false);
            Debug.Log("HP interact");
        }
        if (hit.gameObject.layer == 12)
        {
            ActiveInvincible();
        }
    }

   

    public int GetCurrentLives() //Para que otros scripts puedan acceder a las vidas actuales del jugador
    {
        return currentLives;
    }

    public void ResetLives()
    {
        currentLives = initialLives;
    }

    private void ResetInvincibility()
    {
        isInvincible  = false; //Resetea la invencibilidad
    }
    public void AddLife(int amount = 1)
    {
        currentLives = Mathf.Clamp(currentLives + amount, 0, initialLives);
    }
}
