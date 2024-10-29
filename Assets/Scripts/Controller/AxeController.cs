using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{

    GameObject director;

    public float rotationSpeed = 1e-50f; // ������ ȸ�� �ӵ�
    public float radius = 1e-9f; // ���� ������
    private float angle = 0.0f;

    void Start()
    {
        this.director = GameObject.Find("GameDirector");
    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
            // �÷��̾��� ���� ��ġ�� ����
            Vector3 playerPosition = transform.parent.position;

            // �� ������� ������ �̵�
            float x = playerPosition.x + Mathf.Cos(angle) * radius;
            float y = playerPosition.y + Mathf.Sin(angle) * radius;

            // ���� ��ġ ������Ʈ
            transform.position = new Vector3(x, y, 0);

            // ������ �÷��̾� ������ ȸ��
            float playerRotation = transform.parent.rotation.eulerAngles.z;
            transform.rotation = Quaternion.Euler(0, 0, playerRotation);

            // ������ ȸ��
            transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);

            // ���� ������Ʈ
            angle += Time.deltaTime * rotationSpeed;
            if (angle > 360.0f)
            {
                angle -= 360.0f;
            }
        }
    }
}
