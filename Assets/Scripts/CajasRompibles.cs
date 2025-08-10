using UnityEngine;

public class CajasRompibles : MonoBehaviour
{
    public int health = 1;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
