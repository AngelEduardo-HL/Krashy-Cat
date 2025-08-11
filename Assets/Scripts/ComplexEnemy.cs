using UnityEngine;

public class ComplexEnemy : MonoBehaviour
{
    [Header("Patrullaje")]
    public float speed = 3f;
    public float waitTime = 1.5f;
    public Transform[] patrolPoints; 
    private int currentPointIndex = 0;

    [Header("Detección del jugador")]
    public Transform player;
    public float detectionRange = 8f;
    public float stopChaseDistance = 1.5f; 

    private bool isChasing = false;
    private bool isWaiting = false;
    private float waitTimer = 0f;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (patrolPoints.Length > 0)
        {
            transform.position = patrolPoints[0].position;
        }
    }

    void Update()
    {
        if (player != null && IsPlayerInRange())
        {
            isChasing = true;
            isWaiting = false;
        }
        else if (isChasing && Vector3.Distance(transform.position, player.position) > detectionRange * 1.5f)
        {
            isChasing = false;
        }

        if (isWaiting) //Checa si esta esperando
        {
            PlayAnimation(idle: true, walk: false, run: false);

            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0f)
            {
                isWaiting = false;
                GoToNextPoint();
            }
            return;
        }

        if (isChasing) //Checa si persigue
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol() //Se mueve patrullando en cada waypoint
    {
        if (patrolPoints.Length == 0) return;

        Transform targetPoint = patrolPoints[currentPointIndex];
        MoveTowards(targetPoint.position, speed, false); // Rotación instantánea en patrulla

        PlayAnimation(idle: false, walk: true, run: false);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.2f)
        {
            StartWaiting();
        }
    }

    void ChasePlayer() //Persigue al jugador
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > stopChaseDistance)
        {
            MoveTowards(player.position, speed, true);
            PlayAnimation(idle: false, walk: false, run: true);
        }
        else
        {
            PlayAnimation(idle: true, walk: false, run: false);
        }
    }


    void MoveTowards(Vector3 target, float moveSpeed, bool smoothRotation) //Se mueve
    {
        Vector3 direction = (target - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        if (smoothRotation)
            RotateTowardsSmooth(direction); 
        else
            RotateTowardsInstant(direction); 
    }

    void RotateTowardsSmooth(Vector3 direction) //Gira hacia el jugador de manera suave
    {
        if (direction != Vector3.zero)
        {
            Quaternion lookRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 5f);
        }
    }


    void RotateTowardsInstant(Vector3 direction) //Gira hacia los waypoints
    {
        if (direction != Vector3.zero)
        {
            Quaternion lookRot = Quaternion.LookRotation(direction);
            transform.rotation = lookRot; // Gira instantáneamente
        }
    }

    void StartWaiting() //Se espera antes de seguir patrullando
    {
        isWaiting = true;
        waitTimer = waitTime;
    }

    void GoToNextPoint() //Va al siguiente punto
    {
        currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
    }

    bool IsPlayerInRange() //Checa si el player esta dentro del rango
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < detectionRange)
        {
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            Ray ray = new Ray(transform.position + Vector3.up * 0.5f, dirToPlayer);

            if (Physics.Raycast(ray, out RaycastHit hit, detectionRange))
            {
                return hit.transform == player;
            }
        }
        return false;
    }

    void PlayAnimation(bool idle, bool walk, bool run) //Animaciones
    {
        animator.SetBool("Idle", idle);
        animator.SetBool("Walk", walk);
        animator.SetBool("Run", run);
    }

    void OnDrawGizmosSelected() //Gizmo para saber el rango para cuando se acerque el player
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

    }

}
