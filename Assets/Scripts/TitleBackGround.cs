using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleBackGround : MonoBehaviour
{
    [SerializeField]
    private Sprite[] backGrounds;

    private SpriteRenderer spriteRenderer;

    [SerializeField] private float filmFrame;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void StartGame()
    {
        StartCoroutine(CutScene());
    }

    private IEnumerator CutScene()
    {
        foreach (var t in backGrounds)
        {
            spriteRenderer.sprite = t;
            yield return new WaitForSeconds(filmFrame);
        }
        SceneManager.LoadScene("Animation");
    }
}
