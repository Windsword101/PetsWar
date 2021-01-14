using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KID;

public class BackgroundMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ScoreSystem.PlayerScore.Add(0);
        ScoreSystem.PlayerScore.Add(0);
        ScoreSystem.PlayerScore.Add(0);
        ScoreSystem.PlayerScore.Add(0);
        Application.LoadLevel("MenuScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
