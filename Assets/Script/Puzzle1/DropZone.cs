using UnityEngine;

public class DropZone : MonoBehaviour
{
    public string targetItemTag = "Sugar"; // กำหนดประเภทของที่ต้องใส่กล่องนี้

    void OnTriggerEnter2D(Collider2D other) // ใช้ OnTriggerEnter ถ้าเป็น 3D
    {
        if (other.CompareTag(targetItemTag))
        {
            // ถูกต้อง: แก้จาก Debug.Append เป็น Debug.Log
            Debug.Log("คัดแยกสำเร็จ!");
            Destroy(other.gameObject);
        }
        else
        {
            // ผิดกล่อง
            Debug.Log("ผิดกล่อง!");
        }
    }
}