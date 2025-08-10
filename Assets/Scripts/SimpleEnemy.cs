using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    public float speed = 3f;
    public float changeDirectionTime = 3f;
    public float waitTime = 1.5f; // Tiempo que espera antes de cambiar de dirección

    private float timer;
    private Vector3 direction = Vector3.right;
    private bool isWaiting = false;
    private float waitTimer = 0f;

    void Start()
    {
        timer = changeDirectionTime;
    }

    void Update()
    {
        if (isWaiting)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0f)
            {
                isWaiting = false;
                FlipDirection(); //Cuando termina de esperar, se voltea
                timer = changeDirectionTime;
            }
            return;
        }

        transform.Translate(direction * speed * Time.deltaTime);

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

    void FlipDirection() //Se voltea el enemigo, sirve para la animacion
    {
        direction *= -1f;

        // Voltea visualmente el sprite cambiando el localScale.x
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Sign(direction.x) * Mathf.Abs(scale.x); // Asegura que se voltee correctamente
        transform.localScale = scale;
    }
}
