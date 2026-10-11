using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PadlockController : MonoBehaviour
{
    [Header("การตั้งค่ากุญแจ")]
    public int requiredKeys = 2;             // จำนวนกุญแจที่ต้องใช้ (2 ดอก)
    public string keyPrefsName = "HasKeys";   // ชื่อคีย์ PlayerPrefs

    [Header("ส่วนแสดงผล UI & Animation")]
    public CanvasGroup padlockCanvasGroup;    // ใช้สำหรับทำเอฟเฟกต์จางหาย (Fade Out)
    public Sprite unlockedSprite;             // รูปแม่กุญแจถูกปลดล็อค (ถ้ามี)
    public Button doorButton;                 // ปุ่มประตู

    [Header("เสียง & เอฟเฟกต์การสั่น")]
    public AudioSource audioSource;           // เครื่องเล่นเสียง
    public AudioClip lockLockedSound;         // เสียง "แก๊กๆ" ตอนล็อกอยู่
    public AudioClip unlockSound;             // เสียงปลดล็อก (ถ้ามี)

    private bool isUnlocked = false;
    private Vector3 originalPosition;
    private bool isShaking = false;

    void Start()
    {
        if (padlockCanvasGroup != null)
        {
            originalPosition = padlockCanvasGroup.transform.localPosition;
        }

        // เชื่อมปุ่มประตูให้เรียกฟังก์ชัน OnDoorClicked เมื่อกด
        if (doorButton != null)
        {
            doorButton.onClick.AddListener(OnDoorClicked);
        }

        CheckStatusInitial();
    }

    // ตรวจสอบสถานะเริ่มต้นเมื่อเปิดหน้า
    void CheckStatusInitial()
    {
        int currentKeys = PlayerPrefs.GetInt(keyPrefsName, 0);

        if (currentKeys >= requiredKeys)
        {
            isUnlocked = true;
            if (padlockCanvasGroup != null)
            {
                padlockCanvasGroup.gameObject.SetActive(false); // ปลดล็อกแล้วให้ซ่อนทันที
            }
        }
        else
        {
            isUnlocked = false;
            if (padlockCanvasGroup != null)
            {
                padlockCanvasGroup.gameObject.SetActive(true);
                padlockCanvasGroup.alpha = 1f;
            }
        }
    }

    // ฟังก์ชันทำงานเมื่อผู้เล่นคลิกที่ประตู
    public void OnDoorClicked()
    {
        int currentKeys = PlayerPrefs.GetInt(keyPrefsName, 0);

        if (currentKeys >= requiredKeys && !isUnlocked)
        {
            // ถ้ากุญแจครบแต่ยังไม่ได้เล่น Animation ปลดล็อก
            StartCoroutine(UnlockSequence());
        }
        else if (currentKeys < requiredKeys)
        {
            // ถ้ากุญแจไม่ครบ -> เล่นเสียงแก๊กๆ และสั่นแม่กุญแจ
            if (!isShaking)
            {
                StartCoroutine(ShakeAndSoundSequence());
            }
        }
        else
        {
            // ปลดล็อกเรียบร้อยแล้ว -> เปลี่ยน Scene หรือเปิดประตูเข้าเล่นตามปกติ
            Debug.Log("ประตูเปิดแล้ว! เข้าสู่ด่านถัดไป");
        }
    }

    // Animation สั่นแก๊กๆ + เล่นเสียงล็อก
    IEnumerator ShakeAndSoundSequence()
    {
        isShaking = true;

        // เล่นเสียงแก๊กๆ
        if (audioSource != null && lockLockedSound != null)
        {
            audioSource.PlayOneShot(lockLockedSound);
        }

        // เอฟเฟกต์สั่นแม่กุญแจ (Shake)
        float duration = 0.3f;
        float elapsed = 0f;
        float magnitude = 1.5f; // ความแรงในการสั่น

        while (elapsed < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            if (padlockCanvasGroup != null)
            {
                padlockCanvasGroup.transform.localPosition = originalPosition + new Vector3(offsetX, offsetY, 0);
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        if (padlockCanvasGroup != null)
        {
            padlockCanvasGroup.transform.localPosition = originalPosition;
        }

        isShaking = false;
    }

    // Animation ปลดล็อก + เปลี่ยนรูป + ค่อยๆ จางหายไป
    IEnumerator UnlockSequence()
    {
        isUnlocked = true;

        // เล่นเสียงปลดล็อก
        if (audioSource != null && unlockSound != null)
        {
            audioSource.PlayOneShot(unlockSound);
        }

        // เปลี่ยนเป็นรูปแม่กุญแจที่ถูกปลดล็อกแล้ว (ถ้าใส่ รูปใน Inspector ไว้)
        Image padlockImage = padlockCanvasGroup.GetComponent<Image>();
        if (padlockImage != null && unlockedSprite != null)
        {
            padlockImage.sprite = unlockedSprite;
        }

        yield return new WaitForSeconds(0.3f);

        // ค่อยๆ จางหายไป (Fade Out)
        float fadeDuration = 0.8f;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            if (padlockCanvasGroup != null)
            {
                padlockCanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            }
            yield return null;
        }

        if (padlockCanvasGroup != null)
        {
            padlockCanvasGroup.gameObject.SetActive(false);
        }
    }
}