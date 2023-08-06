using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField]
    private AudioClip[] bgms;

    [SerializeField] private AudioClip gameOver;
    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioClip getItem;
    [SerializeField] private AudioClip waterFlip;
    [SerializeField] private AudioClip dash;
    [SerializeField] private AudioClip dashing;

    private AudioSource audioSource;


    private Coroutine cor;

    private void Awake()
    {
        if(Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if(Instance != this) Destroy(gameObject);

        audioSource = GetComponent<AudioSource>();
    }
    
    void OnEnable()
    {
        // 씬 매니저의 sceneLoaded에 체인을 건다.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    
    // 체인을 걸어서 이 함수는 매 씬마다 호출된다.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Title")
        {
            audioSource.clip = bgms[0];
        }
        else
        {
            audioSource.clip = bgms[1];
        }
        audioSource.Stop();
        audioSource.Play();
    }

    public void PlayHit()
    {
        audioSource.PlayOneShot(hit);
    }

    public void GetItem()
    {
        audioSource.PlayOneShot(getItem);
    }

    public void WaterFlip()
    {
        audioSource.PlayOneShot(waterFlip);
    }

    public void Dash()
    {
        audioSource.PlayOneShot(dash);
        cor = StartCoroutine(Dashing());
    }

    public void DashEnd()
    {
        if(cor == null) return;
        StopCoroutine(cor);
    }

    private IEnumerator Dashing()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.3f);
            audioSource.PlayOneShot(dashing);
        }
    }

    public void GameOver()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(gameOver);
    }
    
}
