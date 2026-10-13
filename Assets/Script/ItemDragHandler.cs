using UnityEngine;

public class ItemDragHandler : MonoBehaviour
{
    private Vector3 offset;
    private float zCoord;
    private ConveyorItem conveyorScript;
    private bool isDragging = false;

    // 1. เพิ่มตัวแปรสำหรับจำตำแหน่งเดิมก่อนถูกลาก
    private Vector3 originalPosition;

    void Start()
    {
        conveyorScript = GetComponent<ConveyorItem>();
    }

    void OnMouseDown()
    {
        zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        offset = gameObject.transform.position - GetMouseAsWorldPoint();

        // 2. จำตำแหน่งของไอเทม ณ วินาทีที่คลิกเมาส์หยิบ
        originalPosition = transform.position;

        isDragging = true;

        if (conveyorScript != null)
        {
            conveyorScript.OnPickUp(); // หยุดวิ่งบนสายพานขณะถือ
        }
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.position = GetMouseAsWorldPoint() + offset;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        bool stored = false;
        if (Physics.Raycast(ray, out hit))
        {
            ItemBox box = hit.collider.GetComponent<ItemBox>();
            if (box != null)
            {
                // ถ้าตะกร้าไม่รับไอเทมนี้ ฟังก์ชัน TryStoreItem จะส่งค่ากลับมาเป็น false
                stored = box.TryStoreItem(gameObject);
            }
        }

        // 3. ถ้าวางผิดตะกร้า หรือวางพลาด (stored == false)
        if (!stored)
        {
            // สั่งให้เด้งกลับไปตำแหน่งเดิมก่อนถูกลากทันที
            transform.position = originalPosition;

            if (conveyorScript != null)
            {
                conveyorScript.OnDrop(); // กลับมาวิ่งบนสายพานต่อจากจุดเดิม
            }
        }
    }
}