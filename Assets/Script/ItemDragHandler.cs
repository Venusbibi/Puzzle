using UnityEngine;

public class ItemDragHandler : MonoBehaviour
{
    private Vector3 offset;
    private float zCoord;
    private ConveyorItem conveyorScript;
    private bool isDragging = false;

    void Start()
    {
        conveyorScript = GetComponent<ConveyorItem>();
    }

    void OnMouseDown()
    {
        zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        offset = gameObject.transform.position - GetMouseAsWorldPoint();
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

        // ยิง Raycast เช็กว่าตอนปล่อยเมาส์ วางโดนกล่อง/ตะกร้าอันไหนอยู่
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        bool stored = false;
        if (Physics.Raycast(ray, out hit))
        {
            ItemBox box = hit.collider.GetComponent<ItemBox>();
            if (box != null)
            {
                // ลองส่งไอเทมเข้าไปเก็บในกล่อง
                stored = box.TryStoreItem(gameObject);
            }
        }

        // ถ้าไม่ได้วางลงในกล่องที่ถูกต้อง ให้ปล่อยให้มันไหลต่อหรือจัดการตามต้องการ
        if (!stored)
        {
            if (conveyorScript != null)
            {
                conveyorScript.OnDrop(); // ให้วิ่งบนสายพานต่อถ้าลงไม่ถูกช่อง
            }
        }
    }
}