using UnityEngine;

public class FlexibleCameraFollow : MonoBehaviour
{
    public Transform target;  // Takip edilecek karakter
    public Vector3 offset;  // Kameranýn karaktere olan mesafesi
    public float smoothSpeed = 0.125f;  // Kameranýn hareket hýzýný yumuþatma
    private bool followX = true;
    private bool followY = true;
    private bool followZ = true;

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;

        // Eksenlere göre konumu ayarla
        if (!followX) desiredPosition.x = transform.position.x;
        if (!followY) desiredPosition.y = transform.position.y;
        if (!followZ) desiredPosition.z = transform.position.z;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
