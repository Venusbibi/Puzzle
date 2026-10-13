using UnityEngine;

public class ConveyorItem : MonoBehaviour
{
    public float itemSpeedMultiplier = 1f;
    private bool isPickedUp = false;

    // ตัวแปรจำตำแหน่งเริ่มต้น
    private Vector3 startPosition;

    void Start()
    {
        // บันทึกตำแหน่งของไอเทมชิ้นนี้ตอนเริ่มฉาก
        startPosition = transform.position;
    }

    void Update()
    {
        if (!isPickedUp && ConveyorManager.Instance != null)
        {
            transform.Translate(Vector3.right * (ConveyorManager.Instance.currentSpeed * itemSpeedMultiplier) * Time.deltaTime);
        }
    }

    public void OnPickUp()
    {
        isPickedUp = true;
    }

    public void OnDrop()
    {
        isPickedUp = false;
    }

    // ฟังก์ชันวาร์ปกลับจุดเริ่มต้นเมื่อเกิดระเบิด
    public void ResetToStart()
    {
        transform.position = startPosition;
        isPickedUp = false; // ป้องกันบั๊กกรณีผู้เล่นกำลังถืออยู่ตอนระเบิดพอดี
    }
}