using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum ResetOn { ON, OFF }

[System.Serializable]
public class LoopSetting
{
    public Transform resetPos;
    public float maxCameraToDistance;
}

public class AutoScroll : MonoBehaviour
{
    public ResetOn isLoop;
    private Transform mainCamera;

    Vector2 scrollPosition;
    public float speed;

    public LoopSetting loopSetting;

    private void Awake()
    {
        mainCamera = Camera.main.GetComponent<Transform>();
    }

    private void Start()
    {
        if (isLoop == ResetOn.ON)
            StartCoroutine(LoopObject());

        scrollPosition = transform.position;
    }

    void Update()
    {
        scrollPosition.x -= Time.deltaTime * speed * GameManager.Instance.speedIncreasement;
        transform.position = scrollPosition;
    }

    private IEnumerator LoopObject()
    {
        while (true)
        {
            if (Mathf.Abs(mainCamera.position.x - transform.position.x) > loopSetting.maxCameraToDistance)
            {
                scrollPosition.x = loopSetting.resetPos.position.x;
                transform.position = scrollPosition;
            }
            yield return null;
        }
    }
}
