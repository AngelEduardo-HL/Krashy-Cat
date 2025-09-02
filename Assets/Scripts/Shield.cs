using UnityEngine;

public class Shield : MonoBehaviour
{
    private Animator animator;
    bool isFlying;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Flying", true);
        isFlying = true;
    }

    void Update()
    {
        if (!isFlying)
        {
            animator.SetBool("Flying", false);
        }
    }


    // Activamos el escudo / pájaro para que siga al jugador
    public void ActiveShield()
    {
        isFlying = false;
    }


}
