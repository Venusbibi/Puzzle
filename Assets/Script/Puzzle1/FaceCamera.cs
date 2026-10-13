using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    void LateUpdate()
    {
        // เช็กว่ามีกล้องหลักในฉากหรือไม่
        if (Camera.main != null)
        {
            // บังคับให้ Canvas หันหน้าไปในทิศทางเดียวกับกล้องเสมอ
            transform.forward = Camera.main.transform.forward;
        }
    }
}