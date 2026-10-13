using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    [Header("กล่องวัตถุดิบทั้งหมดที่ต้องเก็บให้ครบ")]
    public ItemBox[] itemBoxes; // ลากกล่องวัตถุดิบทั้ง 3 กล่องมาใส่ที่นี่

    [Header("วัตถุดิบเป้าหมาย / กุญแจที่จะเด้งออกมา")]
    public GameObject keyReward; // วาง UI หรือ GameObject รูปกุญแจ/ดวงจันทร์
    public Transform keySpawnPoint; // ตำแหน่งที่จะให้กุญแจปรากฏ

    private bool isPuzzleSolved = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // ซ่อนกุญแจไว้ก่อนในตอนเริ่มเกม
        if (keyReward != null)
        {
            keyReward.SetActive(false);
        }
    }

    // ฟังก์ชันเช็กว่าสะสมครบทุกกล่องหรือยัง
    public void CheckPuzzleCompletion()
    {
        if (isPuzzleSolved) return;

        foreach (ItemBox box in itemBoxes)
        {
            // ถ้ามีกล่องใดกล่องหนึ่งยังไม่ครบ ให้ข้ามไปก่อน
            if (!box.IsComplete())
            {
                return;
            }
        }

        // ถ้าผ่านเงื่อนไขทุกกล่องแล้ว = ไขปริศนาสำเร็จ!
        SolvePuzzle();
    }

    void SolvePuzzle()
    {
        isPuzzleSolved = true;
        Debug.Log("สะสมวัตถุดิบครบแล้ว!");

        if (keyReward != null)
        {
            // ย้ายตำแหน่งกุญแจไปที่จุด Spawn แล้วเปิดการแสดงผล
            if (keySpawnPoint != null)
            {
                keyReward.transform.position = keySpawnPoint.position;
            }
            keyReward.SetActive(true);

            // (Optional) เล่น Effect เด้งขยายขนาดสั้นๆ
            StartCoroutine(AnimateKeyPop(keyReward));
        }
    }

    // คอรูทีนทำให้กุญแจเด้งออกมาชัดเจน (Pop Animation)
    System.Collections.IEnumerator AnimateKeyPop(GameObject key)
    {
        Vector3 originalScale = key.transform.localScale;
        key.transform.localScale = Vector3.zero;

        float timer = 0f;
        float duration = 0.5f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;
            // ใช้ AnimationCurve หรือ Lerp แบบเด้ง overshoot เล็กน้อย
            key.transform.localScale = Vector3.Lerp(Vector3.zero, originalScale * 1.2f, progress);
            yield return null;
        }

        key.transform.localScale = originalScale;
    }
}