using UnityEngine;

public class SentinelEnemy : MonoBehaviour
{
    public float sprinkleOnTime = 5f;
    public float waitingTime = 3f;
    public ParticleSystem[] waterSystems; 
    public Animator animator;

    private float timer;
    private float waitTimer;
    private bool sprinkleOn;


    void Start()
    {
        sprinkleOn = true;
        timer = sprinkleOnTime;
    }

    void Update()
    {
        if (sprinkleOn) //Cuando los aspersores esten activos
        {
            ActiveSprinkler();
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                timer = 0;
                sprinkleOn = false;
                waitTimer = waitingTime;
            }
        }
        else //Cuando los aspersores esten desactivados
        {
            DesactiveSprinkler();
            waitTimer -= Time.deltaTime;

            if (waitTimer <= 0)
            {
                waitTimer = 0;
                sprinkleOn = true;
                timer = sprinkleOnTime;
            }
        }
    }

    void ActiveSprinkler() //Se activan aspersores
    {
        foreach (ParticleSystem ps in waterSystems)
        {
            if (!ps.isPlaying)
                ps.Play();
        }
        animator.SetBool("Active", true);
        GetComponent<Collider>().enabled = true;
    }

    void DesactiveSprinkler() //Se desactivan aspersores
    {
        foreach (ParticleSystem ps in waterSystems)
        {
            if (ps.isPlaying)
                ps.Stop();
        }
        animator.SetBool("Active", false);
        GetComponent<Collider>().enabled = false;
    }

}
