using UnityEngine;

public class ConveyorItem : MonoBehaviour
{
    private bool isHeld = false; // เช็กว่าผู้เล่นกำลังหยิบอยู่ไหม

    void Update()
    {
        if (isHeld) return;

        // วิ่งจากซ้ายไปขวา โดยใช้ความเร็วจากตัวจัดการสายพาน
        float speed = ConveyorManager.Instance != null ? ConveyorManager.Instance.currentSpeed : 2f;
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    public void OnPickUp()
    {
        isHeld = true;
    }

    public void OnDrop()
    {
        isHeld = false;
    }
}