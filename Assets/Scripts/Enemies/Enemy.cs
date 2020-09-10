using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected float currentHealth;
    protected float maxHealth;

    public void TakeDamage(float damage)
    {
        if (currentHealth >= 0)
            this.currentHealth -= damage;
    }

    public void Die()
    {
        if (currentHealth < 0)
            Destroy(gameObject);
    }
}