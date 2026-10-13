using UnityEngine;
using TMPro;
using System.Collections.Generic; // เพิ่มบรรทัดนี้เพื่อให้ใช้ List (รายการเก็บของ) ได้

public class ItemBox : MonoBehaviour
{
    [Header("ตั้งค่าประเภทที่รับได้")]
    public string[] acceptedItems;

    [Header("ตั้งค่าจำนวน")]
    public int maxCount = 5;
    public int currentCount = 0;

    [Header("UI แสดงผล")]
    public TMP_Text countText;

    // ** เพิ่มตัวแปร List สำหรับเก็บไอเทมที่ถูกใส่ลงมาในตะกร้านี้ **
    private List<GameObject> storedItems = new List<GameObject>();

    void Start()
    {
        UpdateUI();
    }

    public bool TryStoreItem(GameObject itemObj)
    {
        // 1. ดักจับ "ระเบิด" (เช็กจากชื่อที่มีคำว่า Bomb)
        if (itemObj.name.Contains("Bomb") || itemObj.CompareTag("Bomb"))
        {
            Debug.Log("💥 บึ้ม! ระเบิดลงตะกร้านี้ ของที่เคยเก็บไว้จะเด้งกลับไปที่สายพาน!");

            // วนลูปเอาไอเทมทั้งหมดที่เคยเก็บไว้ในตะกร้านี้ กลับมาบนสายพาน
            foreach (GameObject storedObj in storedItems)
            {
                if (storedObj != null)
                {
                    storedObj.SetActive(true); // เปิดให้มองเห็นอีกครั้ง

                    // สั่งให้วาร์ปกลับไปจุดเริ่มต้น
                    ConveyorItem cItem = storedObj.GetComponent<ConveyorItem>();
                    if (cItem != null)
                    {
                        cItem.ResetToStart();
                    }
                }
            }

            // ล้างประวัติการเก็บในตะกร้า และรีเซ็ตตัวเลขเป็น 0
            storedItems.Clear();
            currentCount = 0;
            UpdateUI();

            // ทำลายลูกระเบิดทิ้ง
            Destroy(itemObj);

            // เช็กสถานะเกมเผื่อไว้ (เช่น เกมอาจจะยกเลิกการชนะเพราะของหาย)
            if (PuzzleManager.Instance != null)
            {
                PuzzleManager.Instance.CheckPuzzleCompletion();
            }

            return true; // รับระเบิดเข้าระบบเพื่อเคลียร์ตะกร้า
        }

        // 2. เช็กไอเทมปกติว่าตรงกับตะกร้าไหม
        bool isCorrectItem = false;
        foreach (string acceptedName in acceptedItems)
        {
            if (itemObj.name.Contains(acceptedName) || itemObj.tag == acceptedName)
            {
                isCorrectItem = true;
                break;
            }
        }

        if (isCorrectItem)
        {
            if (currentCount < maxCount)
            {
                currentCount++;
                UpdateUI();

                // ** สำคัญ: เปลี่ยนจาก Destroy ทิ้ง เป็นซ่อนไว้เฉยๆ เพื่อให้เรียกคืนได้ตอนระเบิดลง **
                itemObj.SetActive(false);
                storedItems.Add(itemObj); // จดจำไว้ว่ามีชิ้นนี้อยู่ในตะกร้า

                if (PuzzleManager.Instance != null)
                {
                    PuzzleManager.Instance.CheckPuzzleCompletion();
                }
                return true;
            }
        }

        return false;
    }

    public void UpdateUI()
    {
        if (countText != null) countText.text = currentCount + "/" + maxCount;
    }

    public bool IsComplete()
    {
        return currentCount >= maxCount;
    }
}