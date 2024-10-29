using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using static LevelDirector;

public class GameDirector : MonoBehaviour
{
    GameObject timerText;
    GameObject pointText;
    float time = 60.0f;
    int point = 0;
    GameObject generator;
    GameObject rule;

    bool isItemSelectionActive = false;  // ������ ���� ������ ���θ� ��Ÿ���� �÷���
    bool isWindowActive = false;  // â�� Ȱ��ȭ�Ǿ����� ����

    GameObject window;
    GameObject skillAxe;
    GameObject skillShoes;
    GameObject skillBook;
    GameObject axe;
    GameObject axe1;
    GameObject axe2;
    GameObject axe3;

    public GameObject success;
    public GameObject fail;

    public void GetCarrot()
    {
        this.point += 100;
    }

    public void GetBomb()
    {
        this.axe.SetActive(false);
        this.axe1.SetActive(false);
        this.axe2.SetActive(false);
        this.axe3.SetActive(false);

        this.point /= 2;
    }


    // Start is called before the first frame update
    void Start()
    {
        this.timerText = GameObject.Find("Time");
        this.pointText = GameObject.Find("Point");
        this.generator = GameObject.Find("ItemGenerator");
        this.window = GameObject.Find("Window");  // â �̸����� ����
        this.skillAxe = GameObject.Find("skillAxe");
        this.skillShoes = GameObject.Find("skillShoes");
        this.skillBook = GameObject.Find("skillBook");

        this.axe = GameObject.Find("Axe");
        this.axe1 = GameObject.Find("Axe (1)");
        this.axe2 = GameObject.Find("Axe (2)");
        this.axe3 = GameObject.Find("Axe (3)");
        this.success = GameObject.Find("Success");
        this.fail = GameObject.Find("Fail");

        this.window.SetActive(false);  // ���� �� â ��Ȱ��ȭ
        this.skillAxe.SetActive(false);
        this.skillShoes.SetActive(false);
        this.skillBook.SetActive(false);

        this.axe.SetActive(false);
        this.axe1.SetActive(false);
        this.axe2.SetActive(false);
        this.axe3.SetActive(false);

        this.success.SetActive(false);
        this.fail.SetActive(false);


        this.rule = GameObject.Find("rule");
        this.rule.SetActive(true); // ���� ���� �� ��Ģ �̹����� Ȱ��ȭ
        StartCoroutine(StartGameAfterRule()); // ��Ģ �̹����� ������ �� ���� �ð� �ڿ� ���� ������ ����

        // �����͸� Json���� ��ȯ�Ͽ� ����
        string jsonData = PlayerPrefs.GetString("GameSceneData");
        // Json �����͸� ��ü�� ��ȯ
        GameSceneData data = JsonUtility.FromJson<GameSceneData>(jsonData);
        // ���� ���Ϳ� ������ ����
        ApplySettingsInGameScene(data);
    }

    void ApplySettingsInGameScene(GameSceneData data)
    {
        // GameScene1���� ItemGenerator�� ã�� ������ �����մϴ�.
        ItemGenerator itemGenerator = FindObjectOfType<ItemGenerator>();

        if (itemGenerator != null)
        {
            itemGenerator.SetParameter(data.Span, data.Speed, data.Ratio);
            Debug.Log("Setting game parameters: span=" + data.Span + ", speed=" + data.Speed + ", ratio=" + data.Ratio);
        }
    }

    IEnumerator StartGameAfterRule()
    {
        yield return new WaitForSeconds(3.0f); // ��Ģ �̹����� ������ �ð� (�� ����)
        this.rule.SetActive(false);        // ��Ģ �̹����� ��Ȱ��ȭ�ϰ� ������ ����
        ShowWindow();  // â�� Ȱ��ȭ
    }

    // â�� Ȱ��ȭ�ϰ� �������� ���̰� �ϴ� �Լ�
    void ShowWindow()
    {
        isWindowActive = true;
        this.window.SetActive(true);
        this.skillAxe.SetActive(true);
        this.skillShoes.SetActive(true);
        this.skillBook.SetActive(true);

        // ������ ���� �߿� ������ ����
        isItemSelectionActive = true;
        Time.timeScale = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        if (isItemSelectionActive)
        {
            // â�� Ȱ��ȭ�Ǿ� ���� ���� ����
            // ���⿡ ������ ���ÿ� ���� ������ �߰��� �� �ֽ��ϴ�.
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                // 1 Ű�� ������ �Ź� ������ ����
                SelectItem("axe");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                // 2 Ű�� ������ ���� ������ ����
                SelectItem("shoes");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                // 3 Ű�� ������ å ������ ����
                SelectItem("book");
            }
        }

        if (!rule.activeSelf && !isWindowActive)
        {
            //���� ����
            this.time -= Time.deltaTime;

            if (this.time < 0)
            {
                this.time = 0;
                this.generator.GetComponent<ItemGenerator>().SetParameter(10000.0f, 0, 0);
                SceneManager.LoadScene("LevelScene");
            }

            this.timerText.GetComponent<TextMeshProUGUI>().text = this.time.ToString("F1");
            this.pointText.GetComponent<TextMeshProUGUI>().text = this.point.ToString() + " point";

            if (this.time == 0 && this.point >= 1000)
            {
                success.SetActive(true);
            }
            else if (this.time == 0 && this.point < 1000)
            {
                fail.SetActive(true);
            }
        }
    }
  

    // ������ ���ÿ� ���� ������ ó���ϴ� �Լ�
    void SelectItem(string itemName)
    {
        switch (itemName)
        {
            case "axe":
                this.axe.SetActive(true);
                this.axe1.SetActive(true);
                this.axe2.SetActive(true);
                this.axe3.SetActive(true);
                break;
            case "shoes":
                // �Ź� ������ ���� �� �÷��̾� �ӵ��� ������ ����
                SetPlayerSpeed(8.0f);  
                break;
            case "book":
                // å ������ ���� ���� +100
                this.point += 100;
                break;
        }

        // ������ ���� �� â�� ��Ȱ��ȭ
        this.window.SetActive(false);
        isWindowActive = false;

        ResumeGame();
    }

    // ���� �Ķ���� ���� �Լ�
    public void SetGameParameters(float span, float speed, int radio)
    {
        ItemGenerator itemGenerator = this.generator.GetComponent<ItemGenerator>();
        if (itemGenerator != null)
        {
            itemGenerator.SetParameter(span, speed, radio);
        }
    }


    // �÷��̾� �ӵ��� �����ϴ� �Լ�
    public void SetPlayerSpeed(float speed)
    {
        // ���⿡�� �÷��̾��� �ӵ��� �����ϴ� ������ ����
        // ���� ���, �÷��̾��� �ӵ� ������ �ٲٰų� �÷��̾� ��ũ��Ʈ�� �˷��ִ� ���� ������� ����
        GameObject player = GameObject.Find("player");  // �÷��̾� ������Ʈ�� ã��
        if (player != null)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            playerController.SetSpeed(speed);
        }
    }
    // ���� �Ͻ�����
    void PauseGame()
    {
        Time.timeScale = 0f;
    }

    // ���� �ٽ� ����
    void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}