using UnityEngine;

public class Respawn : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector3 respawnPosition = new Vector3 (0, 3, 0);
    public float fallThreshold = -5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < fallThreshold)
        {
            transform.position = respawnPosition;
            GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        }
    }
}
