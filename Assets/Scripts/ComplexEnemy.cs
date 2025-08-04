using UnityEngine;

public class ComplexEnemy : MonoBehaviour
{
    [Header("Patrullaje")]
    public float speed = 3f;
    public float changeDirectionTime = 3f;
    public float waitTime = 1.5f;

    [Header("Detección del jugador")]
    public Transform player;
    public float detectionRange = 8f; //Rango de deteccion

    [Header("Timer de patrullaje")]
    private float timer;
    private int currentDirection = 0;
    private Vector3[] directions;

    private bool isChasing = false;
    private bool isWaiting = false;
    private float waitTimer = 0f;

    void Start()
    {
        timer = changeDirectionTime;

        directions = new Vector3[]  //Direcciones a las que va a ir el enemigo creando un patron con forma de rombo :)  <>
        {
            Vector3.forward + Vector3.right,
            Vector3.back + Vector3.right,
            Vector3.back + Vector3.left,
            Vector3.forward + Vector3.left
        };
    }

    void Update()
    {
        //Checa si el jugador esta en su rango para perseguirlo
        if (player != null && IsPlayerInRange())
        {
            isChasing = true;
            isWaiting = false;
        }
        else if (isChasing && Vector3.Distance(transform.position, player.position) > detectionRange * 1.5f)
        {
            isChasing = false;
            timer = changeDirectionTime; // Reinicia timer al volver a patrullar
        }

        if (isWaiting) //Se espera un poco 
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0f)
            {
                isWaiting = false;
                NextDirection();
                timer = changeDirectionTime;
            }
            return;
        }

        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol() //El enemigo hace su patrullaje
    {
        Vector3 moveDir = directions[currentDirection].normalized;
        transform.Translate(moveDir * speed * Time.deltaTime);

        //Timer para que no patrulle infinitamente si no colisiona
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            StartWaiting();
        }
    }

    void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
        RotateTowards(direction);
    }

    void StartWaiting()
    {
        isWaiting = true;
        waitTimer = waitTime;
    }

    void NextDirection()//Va hacia el siguiente destino
    {
        currentDirection = (currentDirection + 1) % directions.Length;
        RotateTowards(directions[currentDirection]);
    }

    void RotateTowards(Vector3 direction) //Rota, sirve para las animaciones
    {
        if (direction != Vector3.zero)
        {
            Quaternion lookRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 5f);
        }
    }

    bool IsPlayerInRange() //Persigue al jugador
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < detectionRange)
        {
            // Detecta si hay obstáculos usando Raycast
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            Ray ray = new Ray(transform.position + Vector3.up * 0.5f, dirToPlayer);
            if (Physics.Raycast(ray, out RaycastHit hit, detectionRange))
            {
                return hit.transform == player;
            }
        }
        return false;
    }

    void OnCollisionEnter(Collision collision) //Checa si colisiono para cambiar la direccion
    {
        if (!isChasing && collision.gameObject.CompareTag("Wall"))
        {
            NextDirection();
            timer = changeDirectionTime;
        }
    }

    void OnDrawGizmosSelected()//Se ve el gizmos, sirve para cuando se coloquen los enemigos y se haga el test con el jugador
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
