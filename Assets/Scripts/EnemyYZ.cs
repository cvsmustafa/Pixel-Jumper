using UnityEngine;

public class EnemyYZ : MonoBehaviour
{
    public float detectionRange = 5.0f;  // Alg�lama mesafesi
    public float attackRange = 1.0f;  // Sald�r� mesafesi
    public float moveSpeed = 2.0f;  // Hareket h�z�
    public int attackDamage = 1;  // Sald�r� hasar�
    public LayerMask playerLayer;  // Oyuncu layer'�
    private Transform player;  // Oyuncu transform'u
    private Rigidbody2D rb2d;  // Rigidbody2D bile�eni
    private PlayerHealth playerHealth;  // Oyuncu sa�l���
    private EnemyHealth2 enemyHealth;  // Enemy scriptine referans
    private bool playerDetected = false;  // Oyuncu alg�land� m�

    private int m_facingDirection = 1; // Ba�lang��ta sa�a bak�yor

    #region Animasyon
    Animator E_animator;
    private bool isAttacking; // sald�r� animasyonu i�in
    #endregion

    private void Start()
    {
        // Oyuncu transform'unu bulun (Oyuncunun tag'i "Player" olmal�)
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb2d = GetComponent<Rigidbody2D>();
        enemyHealth = GetComponent<EnemyHealth2>();
        playerHealth = player.GetComponent<PlayerHealth>();
        E_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (enemyHealth.IsDead())
        {
            rb2d.velocity = Vector2.zero;  // �ld���nde d��man� durdur
            return;  // �ld���nde g�ncellemeyi durdur
        }

        DetectPlayer();

        if (playerDetected)
        {
            if (IsInAttackRange())
            {
                AttackPlayer();
            }
            else
            {
                MoveTowardsPlayer();
            }
        }
        else
        {
            E_animator.SetBool("isRunning", false);
        }
    }

    private void DetectPlayer()
    {
        // Oyuncu ile d��man aras�ndaki mesafeyi hesapla
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // E�er oyuncu alg�lama mesafesi i�indeyse
        if (distanceToPlayer < detectionRange)
        {
            playerDetected = true;
            Debug.Log("Oyuncu alg�land�!");
        }
        else
        {
            playerDetected = false;
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb2d.velocity = direction * moveSpeed;
        E_animator.SetBool("isRunning", true);

        // Y�z� hareket y�n�ne g�re d�nd�r
        if (direction.x > 0)
        {
            // Sa� tarafa do�ru hareket ediyor
            m_facingDirection = 1;
        }
        else if (direction.x < 0)
        {
            // Sol tarafa do�ru hareket ediyor
            m_facingDirection = -1;
        }

        // Y�z� hareket y�n�ne g�re d�nd�r
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * m_facingDirection, transform.localScale.y, transform.localScale.z);
    }

    private bool IsInAttackRange()
    {
        // Oyuncu ile d��man aras�ndaki mesafeyi hesapla
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Oyuncu sald�r� mesafesinde mi
        return distanceToPlayer < attackRange;
    }

    private void AttackPlayer()
    {
        rb2d.velocity = Vector2.zero;  // Sald�r� s�ras�nda hareketi durdur
        E_animator.SetBool("isRunning", false);
        if (!isAttacking)
        {
            isAttacking = true;
            E_animator.SetTrigger("isAttacking");  // Sald�r� animasyonunu tetikle
        }
    }

    // Animasyon olay�ndan �a�r�lacak bir y�ntem
    public void DealDamage()
    {
        if (playerHealth != null && IsInAttackRange())
        {
            playerHealth.TakeDamage(attackDamage);
            Debug.Log("Oyuncuya hasar verildi: " + attackDamage);
        }
        isAttacking = false; // Sald�r�n�n tamamland���n� i�aretle
    }

    private void OnDrawGizmosSelected()
    {
        // Alg�lama mesafesini g�rselle�tirmek i�in bir daire �izin
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Sald�r� mesafesini g�rselle�tirmek i�in bir daire �izin
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}