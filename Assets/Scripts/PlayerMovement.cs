using UnityEngine;
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public float rotateSpeed = 200f;

    private CharacterController controller;
    private Animator animator;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        //Movimiento del jugador
        float turnInput = Input.GetAxis("Horizontal");
        transform.Rotate(0f, turnInput * rotateSpeed * Time.deltaTime, 0f);

        float forwardInput = Input.GetAxis("Vertical");
        Vector3 move = transform.forward * forwardInput * speed;

        bool isWalking = Mathf.Abs(forwardInput) > 0.1f;

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (controller.isGrounded && velocity.y < 0f)
            velocity.y = -1f;
        velocity.y += gravity * Time.deltaTime;
        move.y = velocity.y;

        controller.Move(move * Time.deltaTime);


        //Animaciones
        bool running = controller.isGrounded && Mathf.Abs(forwardInput) > 0.1f;
        bool jumping = !controller.isGrounded;
        animator.SetBool("Running", running);
        animator.SetBool("Jumping", jumping);
        //Idle ocurre cuando Run es igual a falso y el de saltar tambien pa no hacer desmadre
    }
}
