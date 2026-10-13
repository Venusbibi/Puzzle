using UnityEngine;
using System.Collections; // ต้องมีเพื่อใช้ระบบหน่วงเวลา

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    [Header("ใส่ตะกร้าทั้งหมดในด่านนี้")]
    public ItemBox[] allBaskets;

    [Header("ใส่โมเดลกุญแจ")]
    public GameObject keyObject;

    private bool isLevelComplete = false;

    void Awake()
    {
        if (Instance == null) Instance = this;

        // ซ่อนกุญแจไว้ก่อนตอนเริ่มเกม
        if (keyObject != null)
        {
            keyObject.SetActive(false);
        }
    }

    public void CheckPuzzleCompletion()
    {
        if (isLevelComplete) return; // ถ้าจบด่านไปแล้วไม่ต้องเช็กซ้ำ

        bool allFull = true;

        // วนลูปเช็กตะกร้าทุกใบที่เราใส่ไว้
        foreach (ItemBox box in allBaskets)
        {
            // ถ้ามีตะกร้าใบไหนที่ยังไม่เต็ม 5/5
            if (!box.IsComplete())
            {
                allFull = false;
                break; // เลิกเช็กใบอื่นต่อ ถือว่ายังไม่ผ่าน
            }
        }

        // ถ้าผ่านการเช็กทั้งหมด (แปลว่า 5/5 ครบทุกใบจริงๆ)
        if (allFull)
        {
            isLevelComplete = true;
            Debug.Log("🎉 เก็บครบ 5 ชิ้น ทั้ง 3 ตะกร้าแล้ว! ปล่อยกุญแจ!");

            // สั่งเริ่มคัตซีนโชว์กุญแจและ UI
            StartCoroutine(WinSequence());
        }
    }

    private IEnumerator WinSequence()
    {
        // 1. โชว์กุญแจให้เด้งขึ้นมาในฉาก
        if (keyObject != null)
        {
            keyObject.SetActive(true);
        }

        // 2. หน่วงเวลาให้ผู้เล่นเห็นกุญแจ (เช่น 2 วินาที)
        yield return new WaitForSeconds(2f);

        // 3. หลังจากนั้นค่อยเรียก UI Mission Complete ขึ้นมา
        if (UIManager.Instance != null)
        {
            UIManager.Instance.ShowWinUI();
        }
    }
}