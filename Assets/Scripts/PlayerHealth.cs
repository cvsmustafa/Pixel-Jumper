using UnityEngine;
//Bu kod playerin can�n� tutuyor ve �lmesine yar�yor 
public class PlayerHealth : MonoBehaviour
{
    Animator P_animator;
    public int maxHealth = 150;
    private int currentHealth;
    private bool isDead = false;  // Oyuncunun �l�p �lmedi�ini kontrol eden bayrak

    private void Start()
    {
        P_animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;  // E�er oyuncu �ld�yse hasar almay� engelle

        currentHealth -= damage;
        Debug.Log("Oyuncu hasar ald�: " + damage + ", Kalan can: " + currentHealth);
        P_animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;  // E�er oyuncu zaten �ld�yse ikinci kez �lmesini engelle

        isDead = true;  // Oyuncunun �ld���n� belirle
        P_animator.SetTrigger("Death");
        Debug.Log("playerHealt : Oyuncu �ld�!");
    }

    public bool IsDead()
    {
        return isDead;
    }
}
