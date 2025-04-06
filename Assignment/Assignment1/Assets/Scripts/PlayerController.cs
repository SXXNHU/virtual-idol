using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float acceleration = 5f;     // 가속도
    public float deceleration = 3f;     // 감속도
    public float maxSpeed = 20f;     // 최대 속도

    public float turnSpeed = 100f;

    public float currentSpeed = 0f;     
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        //엑셀 밟기
        if (forwardInput > 0)
        {
            currentSpeed += acceleration * Time.deltaTime;
        }

        else if (forwardInput < 0)
        {
            currentSpeed -= acceleration * Time.deltaTime;
        }

        else
        {
            if (currentSpeed > 0)
                currentSpeed -= deceleration * Time.deltaTime;
            else if (currentSpeed < 0)
                currentSpeed += acceleration * Time.deltaTime;
        }
        //최대 속도 제한
        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);

        //앞으로 이동
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        //좌우 조향 (속도 있을때만)
        if(Mathf.Abs(currentSpeed) > 0.1f)
        {
            float turnAmount = turnInput * turnSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, turnAmount);
        }
    }
}
