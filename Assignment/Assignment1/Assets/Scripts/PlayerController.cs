using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float acceleration = 5f;     // ���ӵ�
    public float deceleration = 3f;     // ���ӵ�
    public float maxSpeed = 20f;     // �ִ� �ӵ�

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

        //���� ���
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
        //�ִ� �ӵ� ����
        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);

        //������ �̵�
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        //�¿� ���� (�ӵ� ��������)
        if(Mathf.Abs(currentSpeed) > 0.1f)
        {
            float turnAmount = turnInput * turnSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, turnAmount);
        }
    }
}
