using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ItemBox : MonoBehaviour
{
    [Header("ตั้งค่าประเภทที่รับได้")]
    public string[] acceptedItems;

    [Header("ตั้งค่าจำนวน")]
    public int maxCount = 5;
    public int currentCount = 0;

    [Header("UI แสดงผล")]
    public TMP_Text countText;

    private List<GameObject> storedItems = new List<GameObject>();

    void Start()
    {
        UpdateUI();
    }

    public bool TryStoreItem(GameObject itemObj)
    {
        // 1. ตรวจจับระเบิด (Bomb)
        if (itemObj.name.Contains("Bomb") || itemObj.CompareTag("Bomb"))
        {
            Debug.Log("💥 บึ้ม! ระเบิดตะกร้านี้ โมเดลในกล่องกระเด็นปลิวกลับที่สายพาน!");

            // คลายไอเทมทั้งหมดที่ซ่อนไว้ออกมา แล้วสั่งเด้งกลับทีละชิ้น
            foreach (GameObject storedObj in storedItems)
            {
                if (storedObj != null)
                {
                    storedObj.SetActive(true); // โชว์โมเดลกลับมาในฉาก

                    // เรียกใช้งานฟังก์ชันปลิวกลับตามแรงฟิสิกส์จากจุดตะกร้าชนระเบิด
                    ConveyorItem cItem = storedObj.GetComponent<ConveyorItem>();
                    if (cItem != null)
                    {
                        cItem.ExplodeBackToStart(transform.position);
                    }
                }
            }

            // ล้างประวัติเก็บของ และระเบิดตัวเองทิ้ง
            storedItems.Clear();
            currentCount = 0;
            UpdateUI();

            // ลบลูกระเบิด
            Destroy(itemObj);

            if (PuzzleManager.Instance != null)
            {
                PuzzleManager.Instance.CheckPuzzleCompletion();
            }

            return true;
        }

        // 2. เช็กไอเทมปกติ
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

                itemObj.SetActive(false); // ซ่อนไว้ชั่วคราว
                storedItems.Add(itemObj);

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