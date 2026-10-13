using UnityEngine;
using System.Collections;

public class ConveyorItem : MonoBehaviour
{
    public float itemSpeedMultiplier = 1f;
    private bool isPickedUp = false;
    private bool isExploding = false; // ป้องกันการวิ่งขณะกระเด็นกลับ

    private Vector3 startPosition;
    private Rigidbody rb;
    private Collider col;

    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    void Update()
    {
        // วิ่งตามปกติถ้าไม่ได้ถูกหยิบและไม่ได้อยู่ในสถานะระเบิด
        if (!isPickedUp && !isExploding && ConveyorManager.Instance != null)
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

    // ฟังก์ชันสั่งให้โมเดลกระเด็นโค้งกลับไปจุดเริ่มต้น
    public void ExplodeBackToStart(Vector3 explosionSource)
    {
        StartCoroutine(ExplodeSequence(explosionSource));
    }

    private IEnumerator ExplodeSequence(Vector3 explosionSource)
    {
        isExploding = true;
        isPickedUp = false;

        // 1. เปิดใช้ฟิสิกส์ Rigidbody แบบจริง (ปิด Kinematic ชั่วคราวเพื่อให้มีแรงผลัก)
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;

            // คำนวณแรงระเบิดให้ปลิวกระเด็นออกไปตามทิศทางตรงข้ามของจุดระเบิด
            Vector3 pushDirection = (transform.position - explosionSource).normalized;
            pushDirection.y = 1.5f; // ผลักให้ลอยขึ้นข้างบน
            pushDirection.z = 0f;   // ล็อคแกนความลึก

            rb.AddForce(pushDirection * 8f, ForceMode.Impulse); // ใส่แรงกระเด็น
            rb.AddTorque(new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), 100), ForceMode.Impulse); // หมุนเคว้ง
        }

        // รอให้กระเด็นโชว์ 0.8 วินาที
        yield return new WaitForSeconds(0.8f);

        // 2. เคลื่อนที่ลอยโค้งกลับไปที่สายพาน
        float duration = 1f; // ความเร็วในการบินกลับ (วินาที)
        float elapsed = 0f;
        Vector3 currentPos = transform.position;

        if (rb != null)
        {
            rb.isKinematic = true; // ล็อคฟิสิกส์เพื่อคุมตำแหน่งเอง
            rb.useGravity = false;
        }

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // บินกลับไปพร้อมทำวิถีโค้ง (Parabolic Arc) ขึ้นไปในอากาศ
            Vector3 midPoint = Vector3.Lerp(currentPos, startPosition, t);
            float height = Mathf.Sin(t * Mathf.PI) * 5f; // ยิ่งคูณเยอะ ยิ่งกระโดดโค้งสูง
            transform.position = new Vector3(midPoint.x, midPoint.y + height, midPoint.z);

            // หมุนกลับคืนค่าปกติ
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, t);
            yield return null;
        }

        // 3. ถึงเป้าหมาย กลับสู่สภาพปกติบนสายพาน
        transform.position = startPosition;
        transform.rotation = Quaternion.identity;
        isExploding = false;
    }
}