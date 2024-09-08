using UnityEngine;

public class CollectApples : MonoBehaviour
{
    public int collectedApples = 0;  // Toplanan elma say�s�n� takip eden de�i�ken
    public int baseDamage = 10;  // Temel hasar
    public int damageIncreasePercentage = 15;  // Her 3 elma i�in hasar art�� y�zdesi
    public int requiredApples = 3;  // Hasar art��� i�in gereken elma say�s�

    // Elma toplama i�lemi
    public void CollectApple()
    {
        collectedApples++;

        if (collectedApples >= requiredApples)
        {
            IncreaseDamage();
            ResetCollectedApples();
        }
    }

    // Hasar� y�zdesel olarak art�r�r
    private void IncreaseDamage()
    {
        baseDamage = Mathf.CeilToInt(baseDamage * (1 + damageIncreasePercentage / 100f));
        Debug.Log("Hasar artt�! Yeni hasar: " + baseDamage);
    }

    // Toplanan elma sayac�n� s�f�rlar
    private void ResetCollectedApples()
    {
        collectedApples = 0;
    }
}
