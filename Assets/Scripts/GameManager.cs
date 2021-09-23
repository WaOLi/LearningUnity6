using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    private void OnGUI()
    {
        if (player.transform.position.y < -5)
        {
            Debug.Log("dead");
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height),"bruh...\nyou lose");
            if(GUI.Button(new Rect(Screen.width - 100, Screen.height- 100, 100, 30), "again?"))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(0);
            }
        }
    }
}
