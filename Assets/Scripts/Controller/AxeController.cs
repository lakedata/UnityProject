using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{

    GameObject director;

    public float rotationSpeed = 1e-50f; // 도끼의 회전 속도
    public float radius = 1e-9f; // 원의 반지름
    private float angle = 0.0f;

    void Start()
    {
        this.director = GameObject.Find("GameDirector");
    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
            // 플레이어의 현재 위치를 얻어옴
            Vector3 playerPosition = transform.parent.position;

            // 원 모양으로 도끼를 이동
            float x = playerPosition.x + Mathf.Cos(angle) * radius;
            float y = playerPosition.y + Mathf.Sin(angle) * radius;

            // 도끼 위치 업데이트
            transform.position = new Vector3(x, y, 0);

            // 도끼를 플레이어 주위로 회전
            float playerRotation = transform.parent.rotation.eulerAngles.z;
            transform.rotation = Quaternion.Euler(0, 0, playerRotation);

            // 도끼를 회전
            transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);

            // 각도 업데이트
            angle += Time.deltaTime * rotationSpeed;
            if (angle > 360.0f)
            {
                angle -= 360.0f;
            }
        }
    }
}
