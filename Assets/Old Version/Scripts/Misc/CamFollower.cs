using UnityEngine;

public class CamFollower : MonoBehaviour
{
    public Transform target; // Takip edilecek obje (Player gibi)
    public float smoothSpeed = 0.125f; // Kamera hareketinin yumuşaklığı
    public Vector3 offset; // Kamera ile takip edilen obje arasındaki mesafe

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset; // Kameranın gitmesi gereken pozisyon
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // Yumuşak hareket için Lerp kullanılıyor
            transform.position = smoothedPosition; // Kameranın pozisyonunu ayarla
        }
    }
}
