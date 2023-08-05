using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameOverMenu : MonoBehaviour
{
    public static GameOverMenu Instance;

    private Canvas canvas;
    
    private void Awake()
    {
        if(Instance == null) Instance = this;
        else if(Instance != this) Destroy(gameObject);
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    public void GameOver()
    {
        SoundManager.Instance.GameOver();
        Time.timeScale = 0;
        canvas.enabled = true;
    }
}
