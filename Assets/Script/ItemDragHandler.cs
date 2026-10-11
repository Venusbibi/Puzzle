using UnityEngine;

public class ItemDragHandler : MonoBehaviour
{
    private Vector3 offset;
    private float zCoord;
    private ConveyorItem conveyorScript;

    void Start()
    {
        conveyorScript = GetComponent<ConveyorItem>();
    }

    void OnMouseDown()
    {
        // คำนวณตำแหน่งเมาส์กับวัตถุดิบ
        zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        offset = gameObject.transform.position - GetMouseAsWorldPoint();

        if (conveyorScript != null)
        {
            conveyorScript.OnPickUp(); // แจ้งว่ากำลังถูกหยิบ (หยุดเลื่อนตามสายพาน)
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
        // ลากวัตถุดิบตามตำแหน่งเมาส์
        transform.position = GetMouseAsWorldPoint() + offset;
    }

    void OnMouseUp()
    {
        if (conveyorScript != null)
        {
            conveyorScript.OnDrop(); // ปล่อยมือ (ให้กลับมาเลื่อนต่อ หรือเช็กการลงกล่อง)
        }
    }
}