using UnityEngine;

public class ConveyorManager : MonoBehaviour
{
    public static ConveyorManager Instance;

    [Header("ตั้งค่าความเร็วสายพาน")]
    public float currentSpeed = 2f;
    public float speedIncreaseAmount = 1f;
    public float maxSpeed = 8f;

    // เพิ่มตัวแปรเก็บความเร็วเริ่มต้นเพื่อใช้ตอนโดนระเบิด
    private float defaultSpeed;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        defaultSpeed = currentSpeed;
    }

    public void SpeedUpConveyor()
    {
        currentSpeed += speedIncreaseAmount;
        if (currentSpeed > maxSpeed) currentSpeed = maxSpeed;
    }

    // ฟังก์ชันสั่งให้ไอเทมทุกชิ้นกลับไปจุดเริ่มต้น และรีเซ็ตความเร็วสายพาน
    public void ResetAllItems()
    {
        // ค้นหาไอเทมทุกชิ้นบนสายพาน (ผลไม้, น้ำตาล, ผงเมจิก ฯลฯ)
        ConveyorItem[] allItems = FindObjectsOfType<ConveyorItem>();

        foreach (ConveyorItem item in allItems)
        {
            item.ResetToStart(); // สั่งให้กลับที่เดิม
        }

        currentSpeed = defaultSpeed; // คืนค่าความเร็วสายพานกลับเป็นปกติ
        Debug.Log("🔄 รีเซ็ตไอเทมทั้งหมดกลับจุดเริ่มต้น!");
    }
}