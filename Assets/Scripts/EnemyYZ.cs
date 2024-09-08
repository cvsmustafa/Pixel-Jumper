using UnityEngine;

public class EnemyYZ : MonoBehaviour
{
    public float detectionRange = 5.0f;  // Algýlama mesafesi
    public float attackRange = 1.0f;  // Saldýrý mesafesi
    public float moveSpeed = 2.0f;  // Hareket hýzý
    public int attackDamage = 1;  // Saldýrý hasarý
    public LayerMask playerLayer;  // Oyuncu layer'ý
    private Transform player;  // Oyuncu transform'u
    private Rigidbody2D rb2d;  // Rigidbody2D bileþeni
    private PlayerHealth playerHealth;  // Oyuncu saðlýðý
    private EnemyHealth2 enemyHealth;  // Enemy scriptine referans
    private bool playerDetected = false;  // Oyuncu algýlandý mý

    private int m_facingDirection = 1; // Baþlangýçta saða bakýyor

    #region Animasyon
    Animator E_animator;
    private bool isAttacking; // saldýrý animasyonu için
    #endregion

    private void Start()
    {
        // Oyuncu transform'unu bulun (Oyuncunun tag'i "Player" olmalý)
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
            rb2d.velocity = Vector2.zero;  // Öldüðünde düþmaný durdur
            return;  // Öldüðünde güncellemeyi durdur
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
        // Oyuncu ile düþman arasýndaki mesafeyi hesapla
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Eðer oyuncu algýlama mesafesi içindeyse
        if (distanceToPlayer < detectionRange)
        {
            playerDetected = true;
            Debug.Log("Oyuncu algýlandý!");
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

        // Yüzü hareket yönüne göre döndür
        if (direction.x > 0)
        {
            // Sað tarafa doðru hareket ediyor
            m_facingDirection = 1;
        }
        else if (direction.x < 0)
        {
            // Sol tarafa doðru hareket ediyor
            m_facingDirection = -1;
        }

        // Yüzü hareket yönüne göre döndür
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * m_facingDirection, transform.localScale.y, transform.localScale.z);
    }

    private bool IsInAttackRange()
    {
        // Oyuncu ile düþman arasýndaki mesafeyi hesapla
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Oyuncu saldýrý mesafesinde mi
        return distanceToPlayer < attackRange;
    }

    private void AttackPlayer()
    {
        rb2d.velocity = Vector2.zero;  // Saldýrý sýrasýnda hareketi durdur
        E_animator.SetBool("isRunning", false);
        if (!isAttacking)
        {
            isAttacking = true;
            E_animator.SetTrigger("isAttacking");  // Saldýrý animasyonunu tetikle
        }
    }

    // Animasyon olayýndan çaðrýlacak bir yöntem
    public void DealDamage()
    {
        if (playerHealth != null && IsInAttackRange())
        {
            playerHealth.TakeDamage(attackDamage);
            Debug.Log("Oyuncuya hasar verildi: " + attackDamage);
        }
        isAttacking = false; // Saldýrýnýn tamamlandýðýný iþaretle
    }

    private void OnDrawGizmosSelected()
    {
        // Algýlama mesafesini görselleþtirmek için bir daire çizin
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Saldýrý mesafesini görselleþtirmek için bir daire çizin
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}