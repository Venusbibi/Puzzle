using UnityEngine;

public class ConveyorScroll : MonoBehaviour
{
    [Header("ความเร็วสายพาน (ปรับเป็นค่าบวกหรือลบเพื่อเปลี่ยนทิศ)")]
    public float scrollSpeedX = 0f;
    public float scrollSpeedY = 0.5f;

    private Renderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // คำนวณระยะการเลื่อนของลายพื้นสายพานตามเวลา
        float offsetX = Time.time * scrollSpeedX;
        float offsetY = Time.time * scrollSpeedY;

        // สั่งเลื่อนเฉพาะลาย Texture บนชิ้นส่วนนี้
        meshRenderer.material.mainTextureOffset = new Vector2(offsetX, offsetY);
    }
}