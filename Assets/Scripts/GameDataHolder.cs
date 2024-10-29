using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataHolder : MonoBehaviour
{
    public static GameDataHolder Instance;

    public LevelDirector.GameSceneData dataToPass;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
