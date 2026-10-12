using UnityEngine;

public class ConveyorItem : MonoBehaviour
{
    public float itemSpeedMultiplier = 2f; // ตัวคูณความเร็วเฉพาะชิ้นนี้ (ปรับค่านี้ให้เยอะขึ้นได้ครับ)
    private bool isHeld = false;

    void Update()
    {
        if (isHeld) return;

        float baseSpeed = ConveyorManager.Instance != null ? ConveyorManager.Instance.currentSpeed : 2f;

        // คูณความเร็วเพิ่มเข้าไป
        float finalSpeed = baseSpeed * itemSpeedMultiplier;
        transform.Translate(Vector3.right * finalSpeed * Time.deltaTime);
    }

    public void OnPickUp() { isHeld = true; }
    public void OnDrop() { isHeld = false; }
}