using UnityEngine;

public class TrashBin : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        ProcessTrash(other.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        ProcessTrash(other.gameObject);
    }

    public void ProcessTrash(GameObject itemObj)
    {
        if (ConveyorManager.Instance != null)
        {
            ConveyorManager.Instance.SpeedUpConveyor();
        }

        Destroy(itemObj);

        Debug.Log("🗑️ ไอเทมลงถังขยะแล้ว! เร่งความเร็วสายพาน");
    }
}