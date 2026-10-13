using UnityEngine;
using UnityEngine.SceneManagement; // ต้องมีบรรทัดนี้เพื่อใช้คำสั่งเปลี่ยนด่าน

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("ตั้งค่าหน้าต่าง UI")]
    public GameObject winPanel;
    public GameObject losePanel;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    // เรียกให้หน้าต่างผ่านด่านเด้งขึ้นมา
    public void ShowWinUI()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0f; // หยุดเวลาในเกม (สายพานและไอเทมจะหยุดวิ่ง)
    }

    // เรียกให้หน้าต่างแพ้เด้งขึ้นมา
    public void ShowLoseUI()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    // ผูกกับปุ่ม Restart เพื่อเล่นด่านเดิม
    public void RestartLevel()
    {
        Time.timeScale = 1f; // คืนค่าเวลาให้กลับมาเดินปกติ
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // โหลด Scene ปัจจุบันใหม่
    }

    // ผูกกับปุ่ม Continue หรือ Menu เพื่อไปหน้าอื่น
    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // ต้องพิมพ์ชื่อ Scene ให้ตรงกับหน้าเมนูของคุณเป๊ะๆ
    }
}