using UnityEngine;

public class Apple : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // E�er �arp��an nesne oyuncuysa
        {
            CollectApples player = other.GetComponent<CollectApples>();  // Oyuncu scriptine eri�
            if (player != null)
            {
                player.CollectApple();  // Elma toplama fonksiyonunu �a��r
                Destroy(gameObject);  // Elmay� yok et
            }
        }
    }
}
