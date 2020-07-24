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
        #region 玩家的死亡判定

        bool isdead0 = player0unit.TakeDamage(player1unit.damage| player2unit.damage| player3unit.damage);
        bool isdead1 = player1unit.TakeDamage(player0unit.damage| player2unit.damage | player3unit.damage);
        bool isdead2 = player2unit.TakeDamage(player0unit.damage|player1unit.damage|player3unit.damage);
        bool isdead3 = player3unit.TakeDamage(player0unit.damage| player1unit.damage| player2unit.damage);
        #endregion

        #region 玩家的血量更新

        player0Hud.SetHP(player0unit.currentHP);
        player1Hud.SetHP(player1unit.currentHP);
        player2Hud.SetHP(player2unit.currentHP);
        player3Hud.SetHP(player3unit.currentHP);
        #endregion


        if (isdead0|| isdead1||isdead2||isdead3)
        {
            //如果死亡結束戰鬥
            state = BattleState.Won;
            //EndBattle();
        }



        else
        {
            state = BattleState.ChooseTurn;
            //回到戰鬥畫面


        }
    
    
    
    
    
    }
    #endregion

    
   
   
}
