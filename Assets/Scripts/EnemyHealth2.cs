using UnityEngine;

public class EnemyHealth2 : MonoBehaviour
{
    Animator E_animator;
    EnemySM enemySM;
    public Transform player;
    private Rigidbody2D rb2d;
    private Collider2D mainCollider;
    private BoxCollider2D deadBodyCollider;

    public int health = 50;  // Düþmanýn caný
    private bool isDead = false;  // Öldü mü bayraðý

    private void Start()
    {
        enemySM = GetComponent<EnemySM>();
        rb2d = GetComponent<Rigidbody2D>();
        E_animator = GetComponent<Animator>();
        mainCollider = GetComponent<Collider2D>();

        deadBodyCollider = gameObject.AddComponent<BoxCollider2D>();
        deadBodyCollider.size = new Vector2(0.1f, 0.1f);
        deadBodyCollider.isTrigger = true; // Ceset collider'ýný tetikleyici 
        deadBodyCollider.enabled = false; // Baþlangýçta devre dýþý býrak
    }

    private void Update()
    {
        if (isDead)
        {
            rb2d.velocity = Vector2.zero;  // Öldüðünde düþmaný durdur
            return;  // Öldüðünde güncellemeyi durdur
        }

    }

    #region Health
    public void TakeDamage(int damage)
    {
        if (isDead)
        {
            return;  // Öldüyse hasar almaz
        }

        health -= damage;
        enemySM.isAttacking = false;  // Saldýrý kesildiðinde bu bayraðý sýfýrla
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
            return;  // Zaten ölü ise tekrar öldürmeyi deneme
        }

        isDead = true;  // Öldü bayraðýný ayarla

        if (E_animator != null)
        {
            E_animator.SetBool("isDead", true);
#if UNITY_EDITOR
            Debug.Log("Düþman öldü animasyonu baþlatýldý");
#endif
        }

        HandleDeath();
    }

    private void HandleDeath()
    {
        // Rigidbody2D bileþenini kinematik yap
        rb2d.isKinematic = true;

        // Mevcut collider'ý devre dýþý býrak
        mainCollider.enabled = false;

        // Ceset collider'ýný etkinleþtir
        deadBodyCollider.enabled = true;

#if UNITY_EDITOR
        Debug.Log("Düþman öldü");
#endif
    }
    public bool IsDead()
    {
        return isDead;
    }
    #endregion
}