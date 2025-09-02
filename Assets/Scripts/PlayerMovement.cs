using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;

    [Header("Attack")]
    public float attackRadius = 2f;
    public float attackCooldown = 1f;
    private bool canAttack = true;

    [Header("Rotation")]
    public float rotateSpeed = 200f;
    private CharacterController controller;
    private Vector3 velocity;
    private Animator animator;

    [Header("Sound")]
    public SoundManager playerSoundManager;
    public SoundManager enemiesSoundManager;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        PlayerMove();
        PlayerAttack();
    }

    void PlayerMove()
    {
        //Movimiento
        Vector3 inputDir = Vector3.zero;

        float turnInput = Input.GetAxis("Horizontal");
        transform.Rotate(0f, turnInput * rotateSpeed * Time.deltaTime, 0f);

        float forwardInput = Input.GetAxis("Vertical");
        Vector3 move = transform.forward * forwardInput * speed;

        bool isWalking = Mathf.Abs(forwardInput) > 0.1f;

        if (Input.GetKey(KeyCode.Space) && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            playerSoundManager?.PlayRandomPitch("Jump");
        }


        if (inputDir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(inputDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);
        }

        if (controller.isGrounded && velocity.y < 0f)
            velocity.y = -1f;
        {
            velocity.y += gravity * Time.deltaTime;
        }


        controller.Move((move + velocity) * Time.deltaTime);

        //Animaciones
        bool running = controller.isGrounded && Mathf.Abs(forwardInput) > 0.1f;
        bool jumping = !controller.isGrounded;
        animator.SetBool("Running", running);
        animator.SetBool("Jumping", jumping);
        //Idle se activa cuando el Running y Jumping son falsos pa no hacer tanto desmadre
    }
    void PlayerAttack()
    {
        //Ataque con click izquierdo
        if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack)
        {
            Debug.Log("Attack");
            canAttack = false;
            //animator.SetTrigger("Attack");
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRadius); 
            foreach (var col in hitColliders)
            {
                if (col.CompareTag("Enemy")) //Cualquier Objeto con el tag "Enemy" o "Box" será destruido al ser atacado
                {
                    if(col.gameObject.layer == 6)
                    {
                        enemiesSoundManager?.PlayRandomPitch("HenDamage");//Simple Enemy
                    }
                    else if (col.gameObject.layer == 7)
                    {
                        enemiesSoundManager?.PlayRandomPitch("DogDamage"); //Complex Enemy
                    }
                    else if (col.gameObject.layer == 8)
                    {
                        enemiesSoundManager?.PlayRandomPitch("Boxes"); //Sentinel Enemy
                    }
                    col.gameObject.SetActive(false);
                    //Destroy(col.gameObject);
                    Debug.Log("Jugador Destruyo a: " + col.name);
                }

                if (col.CompareTag("Box"))
                {
                    playerSoundManager?.PlayRandomPitch("Boxes");
                    col.GetComponent<CajasRompibles>().SpawnLoot();
                    // Rebote adicional al romper la caja
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                    col.gameObject.SetActive(false);                  
                    //Destroy(col.gameObject);
                }

                        
            }
            StartCoroutine(ResetAttackCooldown());

        }
    }

    private System.Collections.IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    void OnDrawGizmosSelected()
    {
        //Visualizar el rango de ataque
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }


    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Fish"))
        {
            Destroy(hit.gameObject);
            GetComponent<IngameUIUpdate>().AddFish();
            GetComponent<IngameUIUpdate>().UpdateScore();
            playerSoundManager?.PlayRandomPitch("Fish");
            Debug.Log("Fish Picked");
        }
        if (hit.gameObject.CompareTag("Mask"))
        {
            Destroy(hit.gameObject);
            playerSoundManager?.PlayRandomPitch("Fish"); //Se le va a agregar un sonido diferente
            GetComponent<IngameUIUpdate>().ChangeShieldState();
        }
    }

    public void ResetVerticalVelocity()
    {
        // 'velocity' ya existe en tu script
        // un empuje leve hacia abajo mantiene el contacto con el suelo
        var v = typeof(PlayerMovement)
                .GetField("velocity", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .GetValue(this);
        Vector3 vel = (Vector3)v;
        vel.y = -1f;
        typeof(PlayerMovement)
            .GetField("velocity", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(this, vel);
    }

}