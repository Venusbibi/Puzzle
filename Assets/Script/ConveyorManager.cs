using UnityEngine;

public class ConveyorManager : MonoBehaviour
{
    public static ConveyorManager Instance;

    [Header("ความเร็วปัจจุบันของสายพาน")]
    public float currentSpeed = 2f;
    public float speedIncrement = 0.5f; // ความเร็วที่จะเพิ่มขึ้นเมื่อของตกถังขยะ
    public float maxSpeed = 8f;        // ความเร็วสูงสุด

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // ฟังก์ชันสั่งเพิ่มความเร็วสายพานเมื่อทิ้งขยะ
    public void IncreaseConveyorSpeed()
    {
        currentSpeed += speedIncrement;
        if (currentSpeed > maxSpeed)
        {
            currentSpeed = maxSpeed;
        }
        Debug.Log("สายพานเร็วขึ้น! ความเร็วปัจจุบัน: " + currentSpeed);
    }
}