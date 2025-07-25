using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;

    public float rotateSpeed = 200f;
    private CharacterController controller;
    private Vector3 velocity;
    private Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        //Movimiento
        Vector3 inputDir = Vector3.zero;

        float turnInput = Input.GetAxis("Horizontal");
        transform.Rotate(0f, turnInput * rotateSpeed * Time.deltaTime, 0f);

        float forwardInput = Input.GetAxis("Vertical");
        Vector3 move = transform.forward * forwardInput * speed;

        bool isWalking = Mathf.Abs(forwardInput) > 0.1f;

        if (Input.GetKey(KeyCode.Space) && controller.isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight - 2 * gravity);

        if (inputDir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(inputDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);
        }

        if (controller.isGrounded && velocity.y < 0f)
            velocity.y = -1f;
        velocity.y += gravity * Time.deltaTime;

        controller.Move((move + velocity) * Time.deltaTime);

        //Animaciones
        bool running = controller.isGrounded && Mathf.Abs(forwardInput) > 0.1f;
        bool jumping = !controller.isGrounded;
        animator.SetBool("Running", running);
        animator.SetBool("Jumping", jumping);
        //Idle se activa cuando el Running y Jumping son falsos pa no hacer tanto desmadreS
    }
}