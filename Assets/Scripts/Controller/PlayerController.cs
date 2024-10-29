using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip carrotSE;
    public AudioClip bombSE;
    AudioSource aud;
    GameObject director;

    public bl_Joystick js;//조이스틱 오브젝트를 저장할 변수
    public float speed; //조이스틱에 의해 움직일 오브젝트 속도

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        this.aud = GetComponent<AudioSource>();
        this.director = GameObject.Find("GameDirector");
    }

    // Update is called once per frame
    void Update()
    {
        //스틱이 향해있는 방향을 지정
        Vector3 dir = new Vector3(js.Horizontal, js.Vertical, 0);
        //Vector의 방향은 유지하지만 크기를 1로 줄인다. 길이가 정규화 되지 않을시 0으로 설정.
        dir.Normalize();
        //오브젝트의 위치를 dir방향으로 이동시킨다.
        transform.position += dir * speed * Time.deltaTime;

        // 플레이어의 방향에 따라 스케일을 조절하여 좌우 반전
        if (dir.x < 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        }
        else if (dir.x > 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Carrot")
        {
            this.aud.PlayOneShot(this.carrotSE);
            this.director.GetComponent<GameDirector>().GetCarrot();
        }
        else
        {
            this.aud.PlayOneShot(this.bombSE);
            this.director.GetComponent<GameDirector>().GetBomb();
        }
        Destroy(other.gameObject);
    }

    // 속도를 설정하는 함수
    public void SetSpeed(float newSpeed)
    {
        this.speed = newSpeed;
    }
}
