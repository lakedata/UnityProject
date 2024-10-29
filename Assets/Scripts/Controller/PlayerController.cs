using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip carrotSE;
    public AudioClip bombSE;
    AudioSource aud;
    GameObject director;

    public bl_Joystick js;//���̽�ƽ ������Ʈ�� ������ ����
    public float speed; //���̽�ƽ�� ���� ������ ������Ʈ �ӵ�

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
        //��ƽ�� �����ִ� ������ ����
        Vector3 dir = new Vector3(js.Horizontal, js.Vertical, 0);
        //Vector�� ������ ���������� ũ�⸦ 1�� ���δ�. ���̰� ����ȭ ���� ������ 0���� ����.
        dir.Normalize();
        //������Ʈ�� ��ġ�� dir�������� �̵���Ų��.
        transform.position += dir * speed * Time.deltaTime;

        // �÷��̾��� ���⿡ ���� �������� �����Ͽ� �¿� ����
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

    // �ӵ��� �����ϴ� �Լ�
    public void SetSpeed(float newSpeed)
    {
        this.speed = newSpeed;
    }
}
