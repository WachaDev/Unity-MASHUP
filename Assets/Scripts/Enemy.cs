using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        protected float currentHealth;
        protected float maxHealth;

        public void TakeDamage(float damage)
        {
            if(currentHealth >= 0)
                this.currentHealth -= damage;
        }

        public void Die()
        {
            if(currentHealth < 0)
                Destroy(gameObject);
        }
    }

    public class Gangstar : Enemy 
    {
        private Gangstar()
        {
            this.maxHealth = 10.0f;
            this.currentHealth = this.maxHealth;
        }
    }
}