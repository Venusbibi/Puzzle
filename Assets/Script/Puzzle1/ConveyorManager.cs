using UnityEngine;

public class ConveyorManager : MonoBehaviour
{
    public static ConveyorManager Instance;

    [Header("ตั้งค่าความเร็วสายพาน")]
    public float currentSpeed = 2f;
    public float speedIncreaseAmount = 1f;
    public float maxSpeed = 8f;

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

    // ฟังก์ชันคืนค่าความเร็วสายพานตอนโดนระเบิด
    public void ResetConveyorSpeed()
    {
        currentSpeed = defaultSpeed;
    }
}