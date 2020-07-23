using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    public Text nametext;
    public Slider hpSlider;

    public void SetHud(Unit unit)
    {
        nametext.text = unit.name;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
    
    
    
    }
    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    
    
    
    }

}
