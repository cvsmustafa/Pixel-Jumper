using UnityEngine;
//Bu kod playerin canýný tutuyor ve ölmesine yarýyor 
public class PlayerHealth : MonoBehaviour
{
    Animator P_animator;
    public int maxHealth = 150;
    private int currentHealth;
    private bool isDead = false;  // Oyuncunun ölüp ölmediðini kontrol eden bayrak

    private void Start()
    {
        P_animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;  // Eðer oyuncu öldüyse hasar almayý engelle

        currentHealth -= damage;
        Debug.Log("Oyuncu hasar aldý: " + damage + ", Kalan can: " + currentHealth);
        P_animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;  // Eðer oyuncu zaten öldüyse ikinci kez ölmesini engelle

        isDead = true;  // Oyuncunun öldüðünü belirle
        P_animator.SetTrigger("Death");
        Debug.Log("playerHealt : Oyuncu öldü!");
    }

    public bool IsDead()
    {
        return isDead;
    }
}
