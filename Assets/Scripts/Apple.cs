using UnityEngine;

public class Apple : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // Eðer çarpýþan nesne oyuncuysa
        {
            CollectApples player = other.GetComponent<CollectApples>();  // Oyuncu scriptine eriþ
            if (player != null)
            {
                player.CollectApple();  // Elma toplama fonksiyonunu çaðýr
                Destroy(gameObject);  // Elmayý yok et
            }
        }
    }
}
