using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Vector3 respawnPosition = new Vector3(-92, 0, 56);

    public float fallThreshold = -5f;

    void Update()
    {
        if (transform.position.y < fallThreshold)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                // 위치와 회전 먼저 초기화
                Debug.Log("[Respawn] teleporting to: " + respawnPosition);
                transform.position = respawnPosition;
                transform.rotation = Quaternion.identity;

                // 속도 초기화
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                // 물리 멈춤 처리
                rb.Sleep();
            }
        }
    }
}
