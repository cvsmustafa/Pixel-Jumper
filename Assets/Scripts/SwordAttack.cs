using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    CollectApples C_damage; //hasar

    private void Start()
    {
        C_damage = GetComponentInParent<CollectApples>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Düþman hasar alma fonksiyonunu çaðýrýn
            other.GetComponent<EnemyHealth2>().TakeDamage(C_damage.baseDamage);
        }
    }
}
