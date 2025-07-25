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

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 inputDir = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) inputDir += transform.forward;
        if (Input.GetKey(KeyCode.S)) inputDir -= transform.forward;
        if (Input.GetKey(KeyCode.A)) inputDir -= transform.right;
        if (Input.GetKey(KeyCode.D)) inputDir += transform.right;
        if(Input.GetKey(KeyCode.Space) && controller.isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight-2 * gravity);

        if (inputDir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot =  Quaternion.LookRotation(inputDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);
        }


        Vector3 move = inputDir.normalized * speed;

        if (controller.isGrounded && velocity.y < 0f)
            velocity.y = -1f;
        velocity.y += gravity * Time.deltaTime;

        controller.Move((move + velocity) * Time.deltaTime);
    }
}
