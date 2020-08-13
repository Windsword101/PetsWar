using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelCat : DuelDog
{
    void Update()
    {
        if (DuelSceneManager.timer >= 4.5f)
        {
            fire = true;
            fake = true;
            protection = true;
        }
        if (DuelSceneManager.pause == false && dead == false) Power();
    }
    private void Power()
    {
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            if (fire == true) Fire(1);
            fire = false;
        }
        // 假動作
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            if (fake == true) Fire(0);
            fake = false;
        }

        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            if (protection == true) StartCoroutine("Protection");
            protection = false;
        }
    }
}
