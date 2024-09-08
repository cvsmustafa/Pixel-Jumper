using UnityEngine;

public class CameraFollowWithThreshold : MonoBehaviour
{
    public Transform target;  // Takip edilecek karakter
    public Vector3 offset;  // Kameranýn karaktere olan mesafesi
    public float smoothSpeed = 0.125f;  // Kameranýn hareket hýzýný yumuþatma
    public Vector3 followThreshold = new Vector3(1f, 1f, 0f);  // Takip sýnýrlarý (X, Y, Z için ayrý ayrý)

    private void LateUpdate()
    {
        // Kameranýn mevcut pozisyonunu hedefin offset'i ile karþýlaþtýrarak, sýnýrlarý kontrol et
        Vector3 targetPosition = target.position + offset;
        Vector3 difference = targetPosition - transform.position;

        // Yalnýzca sýnýrlarýn dýþýna çýkýldýðýnda kamerayý hareket ettir
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

        // Kamerayý yumuþak bir þekilde yeni pozisyona taþý
        transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed);
    }

    private void OnDrawGizmos()
    {
        if (target != null)
        {
            Gizmos.color = Color.black;

            // Takip sýnýrlarý için merkez nokta
            Vector3 center = target.position + offset;

            // Takip sýnýrlarýnýn boyutlarýný belirleyin
            Vector3 size = new Vector3(followThreshold.x * 2, followThreshold.y * 2, followThreshold.z * 2);

            // Sýnýrlarý göstermek için bir kutu çiz
            Gizmos.DrawWireCube(center, size);
        }
    }
}
