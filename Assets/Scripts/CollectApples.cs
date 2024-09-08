using UnityEngine;

public class CollectApples : MonoBehaviour
{
    public int collectedApples = 0;  // Toplanan elma sayýsýný takip eden deðiþken
    public int baseDamage = 10;  // Temel hasar
    public int damageIncreasePercentage = 15;  // Her 3 elma için hasar artýþ yüzdesi
    public int requiredApples = 3;  // Hasar artýþý için gereken elma sayýsý

    // Elma toplama iþlemi
    public void CollectApple()
    {
        collectedApples++;

        if (collectedApples >= requiredApples)
        {
            IncreaseDamage();
            ResetCollectedApples();
        }
    }

    // Hasarý yüzdesel olarak artýrýr
    private void IncreaseDamage()
    {
        baseDamage = Mathf.CeilToInt(baseDamage * (1 + damageIncreasePercentage / 100f));
        Debug.Log("Hasar arttý! Yeni hasar: " + baseDamage);
    }

    // Toplanan elma sayacýný sýfýrlar
    private void ResetCollectedApples()
    {
        collectedApples = 0;
    }
}
