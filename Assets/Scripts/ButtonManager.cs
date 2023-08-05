using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void StartInGame()
    {
        GameManager.Instance.Init();
        Time.timeScale = 1;
        SceneManager.LoadScene("Animation");
    }

    public void ToTitle()
    {
        GameManager.Instance.Init();
        Time.timeScale = 1;
        SceneManager.LoadScene("Title");
    }
}
