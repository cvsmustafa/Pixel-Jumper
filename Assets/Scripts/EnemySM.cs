using UnityEngine;

public class EnemySM : MonoBehaviour
{
    public enum State
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Dead
    }
    public State currentState = State.Idle;

    public float detectionRange = 5.0f;
    public float attackRange = 1.0f;
    public float moveSpeed = 2.0f;
    public int attackDamage = 10;
    public LayerMask playerLayer;

    private Transform player;
    private Rigidbody2D rb2d;
    private PlayerHealth playerHealth;
    private EnemyHealth2 enemyHealth;
    private Animator E_animator;
    private bool playerDetected = false;
    public bool isAttacking = false;

    private int m_facingDirection = 1; // Baþlangýçta saða bakýyor

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb2d = GetComponent<Rigidbody2D>();
        enemyHealth = GetComponent<EnemyHealth2>();
        playerHealth = player.GetComponent<PlayerHealth>();
        E_animator = GetComponent<Animator>();

        TransitionToState(State.Idle);
    }

    private void Update()
    {
        if (enemyHealth.IsDead())
        {
            TransitionToState(State.Dead);
        }
        else
        {
            switch (currentState)
            {
                case State.Idle:
                    IdleUpdate();
                    break;
                case State.Patrol:
                    PatrolUpdate();
                    break;
                case State.Chase:
                    ChaseUpdate();
                    break;
                case State.Attack:
                    AttackUpdate();
                    break;
                case State.Dead:
                    DeadUpdate();
                    break;
            }
        }
    }

    private void TransitionToState(State newState)
    {
        currentState = newState;

        switch (newState)
        {
            case State.Idle:
                E_animator.SetBool("isRunning", false);
                rb2d.velocity = Vector2.zero;
                break;
            case State.Patrol:
                // Patrolling logic if needed
                break;
            case State.Chase:
                E_animator.SetBool("isRunning", true);
                break;
            case State.Attack:
                E_animator.SetBool("isRunning", false);
                rb2d.velocity = Vector2.zero;
                if (!isAttacking)
                {
                    isAttacking = true;
                    E_animator.SetTrigger("isAttacking");
                }
                break;
            case State.Dead:
                E_animator.SetBool("isDead", true);
                rb2d.velocity = Vector2.zero;
                break;
        }
    }

    private void IdleUpdate()
    {
        DetectPlayer();
        if (playerDetected)
        {
            TransitionToState(State.Chase);
        }
    }

    private void PatrolUpdate()
    {
        // Patrol logic
        DetectPlayer();
        if (playerDetected)
        {
            TransitionToState(State.Chase);
        }
    }

    private void ChaseUpdate()
    {
        if (IsInAttackRange())
        {
            TransitionToState(State.Attack);
        }
        else if (playerDetected)
        {
            MoveTowardsPlayer();
        }
        else
        {
            TransitionToState(State.Idle);
        }
    }

    private void AttackUpdate()
    {
        if (!IsInAttackRange())
        {
            TransitionToState(State.Chase);
        }
    }

    private void DeadUpdate()
    {
        // Any death logic if needed
    }

    private void DetectPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange && IsFacingPlayer())
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position, detectionRange, playerLayer);

            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                playerDetected = true;
                TransitionToState(State.Chase);
            }
        }
        else
        {
            playerDetected = false;
        }
    }

    private bool IsFacingPlayer()
    {
        Vector3 toPlayer = (player.position - transform.position).normalized;
        float dotProduct = Vector3.Dot(transform.right, toPlayer);

        return dotProduct > 0;
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
            transform.eulerAngles = new Vector3(0, 0, 0); // Sað tarafa bakýyor
        }
        else if (direction.x < 0)
        {
            // Sol tarafa doðru hareket ediyor
            m_facingDirection = -1;
            transform.eulerAngles = new Vector3(0, 180, 0); // Sol tarafa bakýyor
        }
    }

    private bool IsInAttackRange()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        return distanceToPlayer < attackRange;
    }

    public void DealDamage()
    {
        if (playerHealth != null && IsInAttackRange())
        {
            playerHealth.TakeDamage(attackDamage);
        }
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}