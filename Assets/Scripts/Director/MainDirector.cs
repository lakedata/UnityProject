using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainDirector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
    }
    public void next()
    {
        SceneManager.LoadScene("TalkScene");
    }

    public void SceneChange()
    {
        SceneManager.LoadScene("LevelScene");
    }
}
