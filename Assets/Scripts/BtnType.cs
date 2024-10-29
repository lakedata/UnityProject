using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static LevelDirector;
using static MainUI;

public class BtnType : MonoBehaviour
{
    public BTNType currentType;
    GameSceneData data;

    public void OnBtnClick()
    {
        switch (currentType)
        {
            case BTNType.Level1:
                Debug.Log("Level1");
                data = new GameSceneData
                {
                    Level = 1,
                    Span = 2.0f,
                    Speed = 0.5f,
                    Ratio = 2
                };
                break;
            case BTNType.Level2:
                data = new GameSceneData
                {
                    Level = 2,
                    Span = 0.9f,
                    Speed = 1.0f,
                    Ratio = 6
                };
                Debug.Log("Level2");
                break;
            case BTNType.Level3:
                data = new GameSceneData
                {
                    Level = 3,
                    Span = 0.4f,
                    Speed = 1.5f,
                    Ratio = 8
                };
                Debug.Log("Level3");
                break;
            default:
                data = new GameSceneData(); // Handle default case if needed
                break;
        }
        SaveDataToPlayerPrefs(data);

        SceneManager.LoadScene("GameScene1");
    }

    // 데이터를 PlayerPrefs에 저장하는 함수
    void SaveDataToPlayerPrefs(GameSceneData data)
    {
        string jsonData = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("GameSceneData", jsonData);
        PlayerPrefs.Save();
    }

    // 게임 씬에 전달할 데이터 클래스
    [System.Serializable]
    public class GameSceneData
    {
        public int Level;
        public float Span;
        public float Speed;
        public int Ratio;
    }

// Start is called before the first frame update
void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
