using UnityEngine;

public class TrashBin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // เช็กว่าเป็นวัตถุดิบหรือไม่ (ดูจาก Tag ว่าเป็น "Item")
        if (other.CompareTag("Item") || other.GetComponent<ConveyorItem>() != null)
        {
            // สั่งเร่งความเร็วสายพาน
            if (ConveyorManager.Instance != null)
            {
                ConveyorManager.Instance.IncreaseConveyorSpeed();
            }

            // ทำลายวัตถุดิบชิ้นนั้นทิ้ง
            Destroy(other.gameObject);
            Debug.Log("วัตถุดิบตกถังขยะ! สายพานเร่งความเร็วขึ้น");
        }
    }
}