using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHealButton : MonoBehaviour
{
    public Unit Player;
    public BattleHud playerhud;
    void OnHealbutton()
    {
        Player.Heal(10);
       
        playerhud.SetHP(Player.currentHP);
    
    
    
    }
   

}
