using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using KID;

public class Menu : MonoBehaviour
{
    private void Awake()
    {
        RandomScene.SetAllScene("GameScene","Game02_running","Game04_Tagyoure it","Game05_volleyball");
        ScoreSystem.PlayerScore.Add(0);
        ScoreSystem.PlayerScore.Add(0);
        ScoreSystem.PlayerScore.Add(0);
        ScoreSystem.PlayerScore.Add(0);
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
}
