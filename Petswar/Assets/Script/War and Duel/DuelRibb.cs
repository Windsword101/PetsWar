using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelRibb : DuelDog
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
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (fire == true) Fire(1);
            fire = false;
        }
        // 假動作
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (fake == true) Fire(0);
            fake = false;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (protection == true) StartCoroutine("Protection");
            protection = false;
        }
    }
}
