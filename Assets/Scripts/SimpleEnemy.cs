using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    public float speed = 3f;
    public float changeDirectionTime = 3f;
    public float waitTime = 1.5f;

    private float timer;
    private Vector3 direction = Vector3.forward;
    private bool isWaiting = false;
    private float waitTimer = 0f;

    private Animator animator;

    void Start()
    {
        timer = changeDirectionTime;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isWaiting)
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Idle", true);
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0f)
            {
                FlipDirection(); //Cuando termina de esperar, se voltea
                timer = changeDirectionTime;
                isWaiting = false;
            }
            return;
        }

        transform.Translate(direction * speed * Time.deltaTime);
        animator.SetBool("Walk", true);
        animator.SetBool("Idle", false);

        // Temporizador
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            StartWaiting(); //Si se acaba el tiempo, se espera antes de cambiar de dirección
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall")) //Cambia la direccion si colisiona
        {
            StartWaiting();
        }
    }

    void StartWaiting() //Espera unos segundos antes de cambiar de direccion
    {

        isWaiting = true;
        waitTimer = waitTime;
    }

    void FlipDirection() //Se voltea el enemigo, sirve para la animacion y movimiento
    {
        Vector3 rot = transform.rotation.eulerAngles;
        rot.y += 180f;
        transform.rotation = Quaternion.Euler(rot);
    }

}
