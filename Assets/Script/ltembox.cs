using UnityEngine;
using TMPro;

public class ItemBox : MonoBehaviour
{
    [Header("ตั้งค่าประเภทและจำนวน")]
    public string requiredItemTag; // เช่น "Sugar"
    public int maxCount = 5;
    public int currentCount = 0;

    [Header("UI แสดงผล")]
    public TMP_Text countText;

    void Start()
    {
        UpdateUI();
    }

    // ฟังก์ชันรับไอเทมเมื่อถูกลากมาปล่อยใส่
    public bool TryStoreItem(GameObject itemObj)
    {
        // เช็กว่า Tag ตรงกับตะกร้านี้ไหม
        if (itemObj.CompareTag(requiredItemTag))
        {
            if (currentCount < maxCount)
            {
                currentCount++;
                UpdateUI();

                // ทำลายวัตถุดิบชิ้นนั้นทิ้งทันที
                Destroy(itemObj);

                // เช็กว่าเกมเคลียร์ครบทุกกล่องหรือยัง
                if (PuzzleManager.Instance != null)
                {
                    PuzzleManager.Instance.CheckPuzzleCompletion();
                }

                Debug.Log("เก็บลงตะกร้าสำเร็จ! จำนวน: " + currentCount + "/" + maxCount);
                return true; // บอกว่าเก็บสำเร็จ
            }
        }

        Debug.Log("วางไม่ถูกตะกร้า หรือจำนวนเต็มแล้ว!");
        return false; // เก็บไม่สำเร็จ
    }

    public void UpdateUI()
    {
        if (countText != null)
        {
            countText.text = currentCount + "/" + maxCount;
        }
    }

    public bool IsComplete()
    {
        return currentCount >= maxCount;
    }
}