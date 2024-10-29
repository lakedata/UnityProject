using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDirector : MonoBehaviour
{
    public int level = 1;
    GameSceneData GetGameSceneData()
    {
        GameSceneData data;

        switch (level)
        {
            case 1:
                Debug.Log("1");
                data = new GameSceneData
                {
                    Level = 1,
                    Span = 2.0f,
                    Speed = 0.5f,
                    Ratio = 2
                };
                break;
            case 2:
                data = new GameSceneData
                {
                    Level = 2,
                    Span = 0.9f,
                    Speed = 1.0f,
                    Ratio = 6
                };
                Debug.Log("2");
                break;
            case 3:
                Debug.Log("3");
                data = new GameSceneData
                {
                    Level = 3,
                    Span = 0.4f,
                    Speed = 1.5f,
                    Ratio = 8
                };
                break;
            default:
                data = new GameSceneData(); // Handle default case if needed
                break;
        }

        return data;
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
}