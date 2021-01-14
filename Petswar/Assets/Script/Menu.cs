using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using KID;

public class Menu : MonoBehaviour
{
    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameStart()
    {
        RandomScene.SetAllScene("GameScene", "Game02_running", "Game03_Balance", "Game04_Tagyoure it");
        string randomscene = RandomScene.GetRandomScene();
        SceneManager.LoadScene(randomscene);
    }

    public void StoryMode()
    {
        SceneManager.LoadScene("StoryScene");
    }

    public void GameQuite()
    {
        Application.Quit();
    }

    public void Restart()
    {
        for (int i = 0; i < 4; i++)
        {
            ScoreSystem.PlayerScore[i] = 0;
        }
        SceneManager.LoadScene("MenuScene");
    }
}
