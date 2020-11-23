using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cancel_Game05_Ball : MonoBehaviour
{
    private string[] playerName = new string[4] { "Dog", "Ribb", "Turtle", "Cat" };
    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < playerName.Length; i++)
        {
            if (other.gameObject.name == playerName[i] && gameObject.name == "ball(Clone)")
            {
                Cancel_Game05_Manager.ballNumber[i] += 1;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < playerName.Length; i++)
        {
            if (other.gameObject.name == playerName[i] && gameObject.name == "ball(Clone)")
            {
                Cancel_Game05_Manager.ballNumber[i] -= 1;
            }
        }
    }
}
