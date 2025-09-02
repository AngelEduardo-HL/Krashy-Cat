using UnityEngine;

public class Shield : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Flying", true);
    }

    void Update()
    {
        
    }


    // Activamos el escudo / pájaro para que siga al jugador
    public void ActiveShield()
    {
        animator.SetBool("Flying", false);
    }


}
