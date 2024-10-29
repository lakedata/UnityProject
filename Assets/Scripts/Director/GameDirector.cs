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

    bool isItemSelectionActive = false;  // 아이템 선택 중인지 여부를 나타내는 플래그
    bool isWindowActive = false;  // 창이 활성화되었는지 여부

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
        this.window = GameObject.Find("Window");  // 창 이름으로 변경
        this.skillAxe = GameObject.Find("skillAxe");
        this.skillShoes = GameObject.Find("skillShoes");
        this.skillBook = GameObject.Find("skillBook");

        this.axe = GameObject.Find("Axe");
        this.axe1 = GameObject.Find("Axe (1)");
        this.axe2 = GameObject.Find("Axe (2)");
        this.axe3 = GameObject.Find("Axe (3)");
        this.success = GameObject.Find("Success");
        this.fail = GameObject.Find("Fail");

        this.window.SetActive(false);  // 시작 시 창 비활성화
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
        this.rule.SetActive(true); // 게임 시작 시 규칙 이미지를 활성화
        StartCoroutine(StartGameAfterRule()); // 규칙 이미지를 보여준 후 일정 시간 뒤에 실제 게임을 시작

        // 데이터를 Json으로 변환하여 저장
        string jsonData = PlayerPrefs.GetString("GameSceneData");
        // Json 데이터를 객체로 변환
        GameSceneData data = JsonUtility.FromJson<GameSceneData>(jsonData);
        // 게임 디렉터에 설정을 적용
        ApplySettingsInGameScene(data);
    }

    void ApplySettingsInGameScene(GameSceneData data)
    {
        // GameScene1에서 ItemGenerator를 찾아 설정을 적용합니다.
        ItemGenerator itemGenerator = FindObjectOfType<ItemGenerator>();

        if (itemGenerator != null)
        {
            itemGenerator.SetParameter(data.Span, data.Speed, data.Ratio);
            Debug.Log("Setting game parameters: span=" + data.Span + ", speed=" + data.Speed + ", ratio=" + data.Ratio);
        }
    }

    IEnumerator StartGameAfterRule()
    {
        yield return new WaitForSeconds(3.0f); // 규칙 이미지를 보여줄 시간 (초 단위)
        this.rule.SetActive(false);        // 규칙 이미지를 비활성화하고 게임을 시작
        ShowWindow();  // 창을 활성화
    }

    // 창을 활성화하고 아이템을 보이게 하는 함수
    void ShowWindow()
    {
        isWindowActive = true;
        this.window.SetActive(true);
        this.skillAxe.SetActive(true);
        this.skillShoes.SetActive(true);
        this.skillBook.SetActive(true);

        // 아이템 선택 중에 게임을 멈춤
        isItemSelectionActive = true;
        Time.timeScale = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        if (isItemSelectionActive)
        {
            // 창이 활성화되어 있을 때의 로직
            // 여기에 아이템 선택에 관한 로직을 추가할 수 있습니다.
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                // 1 키를 누르면 신발 아이템 선택
                SelectItem("axe");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                // 2 키를 누르면 도끼 아이템 선택
                SelectItem("shoes");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                // 3 키를 누르면 책 아이템 선택
                SelectItem("book");
            }
        }

        if (!rule.activeSelf && !isWindowActive)
        {
            //게임 로직
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
  

    // 아이템 선택에 따른 동작을 처리하는 함수
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
                // 신발 아이템 선택 시 플레이어 속도를 빠르게 설정
                SetPlayerSpeed(8.0f);  
                break;
            case "book":
                // 책 아이템 선택 점수 +100
                this.point += 100;
                break;
        }

        // 아이템 선택 후 창을 비활성화
        this.window.SetActive(false);
        isWindowActive = false;

        ResumeGame();
    }

    // 게임 파라미터 설정 함수
    public void SetGameParameters(float span, float speed, int radio)
    {
        ItemGenerator itemGenerator = this.generator.GetComponent<ItemGenerator>();
        if (itemGenerator != null)
        {
            itemGenerator.SetParameter(span, speed, radio);
        }
    }


    // 플레이어 속도를 설정하는 함수
    public void SetPlayerSpeed(float speed)
    {
        // 여기에서 플레이어의 속도를 조절하는 로직을 구현
        // 예를 들어, 플레이어의 속도 변수를 바꾸거나 플레이어 스크립트에 알려주는 등의 방식으로 가능
        GameObject player = GameObject.Find("player");  // 플레이어 오브젝트를 찾음
        if (player != null)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            playerController.SetSpeed(speed);
        }
    }
    // 게임 일시정지
    void PauseGame()
    {
        Time.timeScale = 0f;
    }

    // 게임 다시 시작
    void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}