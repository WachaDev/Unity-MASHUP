using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float currentHealth;
    [SerializeField] protected float maxHealth;

    private void Update() => Die();

    public void TakeDamage(float damage)
    {
        if (currentHealth >= 0f)
            currentHealth -= damage;
    }

    public void Die()
    {
        if (currentHealth <= 0f)
            Destroy(gameObject);
    }
}