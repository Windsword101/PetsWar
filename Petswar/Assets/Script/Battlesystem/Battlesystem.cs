using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 定義四種狀態,開始,選擇動作,戰鬥結果計算,勝利,落敗
/// </summary>
public enum BattleState { START,ChooseTurn,BattleTurn,Won,Lost }

public class Battlesystem : MonoBehaviour
{
    #region 單位宣告

    public BattleState state;
    public Button Startbutton;
    public BattleHud player0Hud;
    public BattleHud player1Hud;
    public BattleHud player2Hud;
    public BattleHud player3Hud;
    Unit player0unit;
    Unit player1unit;
    Unit player2unit;
    Unit player3unit;
    #endregion

    #region 戰鬥開始

    void Start()
    {
        state = BattleState.START;
        SetupBattle();
        Startbutton.interactable = true;

        state = BattleState.ChooseTurn;
        ChooseTurn();

    }
    #endregion

    #region 戰鬥面板

    void SetupBattle()
    {

        player0Hud.SetHud(player0unit);
        player0Hud.SetHud(player1unit);
        player0Hud.SetHud(player2unit);
        player0Hud.SetHud(player3unit);


    }
    #endregion

    #region 戰鬥動作選擇

    void ChooseTurn()
    { 
    
    
    
    }
    #endregion

    #region 死亡判定

    void Battleturn()
    {
        //bool isdead 
    
    
    
    
    
    
    }
    #endregion

    
   
   
}
