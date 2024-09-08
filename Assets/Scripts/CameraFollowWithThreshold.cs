using UnityEngine;

public class CameraFollowWithThreshold : MonoBehaviour
{
    public Transform target;  // Takip edilecek karakter
    public Vector3 offset;  // Kameran�n karaktere olan mesafesi
    public float smoothSpeed = 0.125f;  // Kameran�n hareket h�z�n� yumu�atma
    public Vector3 followThreshold = new Vector3(1f, 1f, 0f);  // Takip s�n�rlar� (X, Y, Z i�in ayr� ayr�)

    private void LateUpdate()
    {
        // Kameran�n mevcut pozisyonunu hedefin offset'i ile kar��la�t�rarak, s�n�rlar� kontrol et
        Vector3 targetPosition = target.position + offset;
        Vector3 difference = targetPosition - transform.position;

        // Yaln�zca s�n�rlar�n d���na ��k�ld���nda kameray� hareket ettir
        Vector3 newPosition = transform.position;

        if (Mathf.Abs(difference.x) > followThreshold.x)
        {
            newPosition.x = targetPosition.x - followThreshold.x * Mathf.Sign(difference.x);
        }
        if (Mathf.Abs(difference.y) > followThreshold.y)
        {
            newPosition.y = targetPosition.y - followThreshold.y * Mathf.Sign(difference.y);
        }
        if (Mathf.Abs(difference.z) > followThreshold.z)
        {
            newPosition.z = targetPosition.z - followThreshold.z * Mathf.Sign(difference.z);
        }

        // Kameray� yumu�ak bir �ekilde yeni pozisyona ta��
        transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed);
    }

    private void OnDrawGizmos()
    {
        if (target != null)
        {
            Gizmos.color = Color.black;

            // Takip s�n�rlar� i�in merkez nokta
            Vector3 center = target.position + offset;

            // Takip s�n�rlar�n�n boyutlar�n� belirleyin
            Vector3 size = new Vector3(followThreshold.x * 2, followThreshold.y * 2, followThreshold.z * 2);

            // S�n�rlar� g�stermek i�in bir kutu �iz
            Gizmos.DrawWireCube(center, size);
        }
    }
}
