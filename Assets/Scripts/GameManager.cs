using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [Range(1f, 10f)]
    public float speedScale;
    private float timer;

    private float speedIncreasement;
    private float speedNormal;

    public bool isSlow = false;
    public bool isFast = false;
    private float slowRatio;
    private float fastRatio;

    public static GameManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (instance == null)
                    Debug.Log("no Singleton obj");
            }
            return instance;
        }
    }
     
    public float GetIncreasementSpeed() { return speedIncreasement; }
    public void DecreaseSpeedRatio(float ratio) { isSlow = true; slowRatio = ratio; }
    public void IncreaseSpeedRatio(float ratio) { isFast = true; fastRatio = ratio; }
    public void InitSlowSpeedRatio() { isSlow = false; }
    public void InitFastSpeedRatio() { isFast = false; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Init();
        Debug.Log("½ÃÀÛ");
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(isFast && !isSlow)
        {
            speedIncreasement = speedNormal / fastRatio;
        }
        else if(!isFast && isSlow)
        {
            speedIncreasement = speedNormal * slowRatio;
        }
        else if(isFast && isSlow)
        {
            speedIncreasement = speedNormal * slowRatio / fastRatio;
        }
        else
        {
            speedIncreasement = speedNormal;
        }

        if (timer > 1f)
        {
            speedIncreasement += 0.007f * speedScale;
            Debug.Log(speedIncreasement);
			speedNormal += 0.007f * speedScale;
			timer = 0f;
        }
    }

    public void Init()
    {
        speedIncreasement = 1f;
        speedNormal = 1f;
        timer = 0f;
    }
}
