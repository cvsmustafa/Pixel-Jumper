using UnityEngine;

public class EnemyHealth2 : MonoBehaviour
{
    Animator E_animator;
    EnemySM enemySM;
    public Transform player;
    private Rigidbody2D rb2d;
    private Collider2D mainCollider;
    private BoxCollider2D deadBodyCollider;

    public int health = 50;  // D��man�n can�
    private bool isDead = false;  // �ld� m� bayra��

    private void Start()
    {
        enemySM = GetComponent<EnemySM>();
        rb2d = GetComponent<Rigidbody2D>();
        E_animator = GetComponent<Animator>();
        mainCollider = GetComponent<Collider2D>();

        deadBodyCollider = gameObject.AddComponent<BoxCollider2D>();
        deadBodyCollider.size = new Vector2(0.1f, 0.1f);
        deadBodyCollider.isTrigger = true; // Ceset collider'�n� tetikleyici 
        deadBodyCollider.enabled = false; // Ba�lang��ta devre d��� b�rak
    }

    private void Update()
    {
        if (isDead)
        {
            rb2d.velocity = Vector2.zero;  // �ld���nde d��man� durdur
            return;  // �ld���nde g�ncellemeyi durdur
        }

    }

    #region Health
    public void TakeDamage(int damage)
    {
        if (isDead)
        {
            return;  // �ld�yse hasar almaz
        }

        health -= damage;
        enemySM.isAttacking = false;  // Sald�r� kesildi�inde bu bayra�� s�f�rla
#if UNITY_EDITOR
        Debug.Log("Hasar vurdu, kalan can: " + health);
#endif

        if (E_animator != null)
        {
            E_animator.SetTrigger("Hurt");
        }

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead)
        {
            return;  // Zaten �l� ise tekrar �ld�rmeyi deneme
        }

        isDead = true;  // �ld� bayra��n� ayarla

        if (E_animator != null)
        {
            E_animator.SetBool("isDead", true);
#if UNITY_EDITOR
            Debug.Log("D��man �ld� animasyonu ba�lat�ld�");
#endif
        }

        HandleDeath();
    }

    private void HandleDeath()
    {
        // Rigidbody2D bile�enini kinematik yap
        rb2d.isKinematic = true;

        // Mevcut collider'� devre d��� b�rak
        mainCollider.enabled = false;

        // Ceset collider'�n� etkinle�tir
        deadBodyCollider.enabled = true;

#if UNITY_EDITOR
        Debug.Log("D��man �ld�");
#endif
    }
    public bool IsDead()
    {
        return isDead;
    }
    #endregion
}