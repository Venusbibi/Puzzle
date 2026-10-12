using UnityEngine;

public class ConveyorManager : MonoBehaviour
{
    // ตัวแปร Instance เพื่อให้สคริปต์อื่นเรียกใช้ได้ง่ายๆ
    public static ConveyorManager Instance;

    [Header("ตั้งค่าความเร็วสายพาน")]
    public float currentSpeed = 2f;      // ความเร็วเริ่มต้น
    public float speedIncreaseAmount = 1f; // จำนวนความเร็วที่เพิ่มขึ้นเมื่อขยะลงถัง
    public float maxSpeed = 8f;          // ขีดจำกัดความเร็วสูงสุด ป้องกันเร็วเกินไป

    void Awake()
    {
        // ตั้งค่า Instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ฟังก์ชันนี้แหละครับที่ Unity แจ้งเตือนว่าหาไม่เจอ
    public void SpeedUpConveyor()
    {
        // เพิ่มความเร็วขึ้น
        currentSpeed += speedIncreaseAmount;

        // เช็กไม่ให้ความเร็วเกินขีดจำกัด
        if (currentSpeed > maxSpeed)
        {
            currentSpeed = maxSpeed;
        }

        Debug.Log("⚠️ ไอเทมตกถังขยะ! สายพานเร่งความเร็วเป็น: " + currentSpeed);
    }
}