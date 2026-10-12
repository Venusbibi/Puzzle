using UnityEngine;

public class TrashBin : MonoBehaviour
{
    // ระบบที่ 1: ตรวจจับเมื่อไอเทมไหลมาชนถังขยะเอง (ระบบหลัก)
    void OnTriggerEnter(Collider other)
    {
        ProcessTrash(other.gameObject);
    }

    // ฟังก์ชันทำลายขยะและเร่งความเร็ว (ใช้ร่วมกันทั้งแบบไหลมาเอง และแบบเมาส์ลากมาปล่อย)
    public void ProcessTrash(GameObject itemObj)
    {
        // เช็กว่าเป็นไอเทมสายพาน หรือ Tag ถูกต้องไหม
        ConveyorItem conveyorItem = itemObj.GetComponent<ConveyorItem>();
        if (conveyorItem != null || itemObj.CompareTag("Sugar"))
        {
            // สั่งเร่งความเร็วสายพาน
            if (ConveyorManager.Instance != null)
            {
                ConveyorManager.Instance.SpeedUpConveyor();
            }

            // ทำลายไอเทมทิ้ง
            Destroy(itemObj);
            Debug.Log("🗑️ ไอเทมลงถังขยะแล้ว! สายพานเร็วขึ้น!");
        }
    }
}