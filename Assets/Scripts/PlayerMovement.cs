using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float verticalSpeed;
    [SerializeField] private float horizontalSpeed;

    [SerializeField] private GameObject waterFlip;
    
    [Header("Jump Property")]
    [SerializeField] private float waterHeight;
    [SerializeField] private float maxJumpHeight;
    [SerializeField] private float flyingTime;

    [SerializeField] private AnimationCurve jumpCurve;

    private Animator playerAnim;

    private float jumpBuffer;
    
    private float maxPosX;
    private float minPosX;
    private float minPosY;

    private bool isJumping;
    
    private void Start()
    {
        Collider2D col = GetComponent<Collider2D>();
        playerAnim = GetComponent<Animator>();
        
        maxPosX = Camera.main.ViewportToWorldPoint(new Vector3(0.99f, 0)).x - col.bounds.size.x / 2;
        minPosX = Camera.main.ViewportToWorldPoint(new Vector3(0.01f, 0f)).x + col.bounds.size.x / 2;
        minPosY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.01f)).y + col.bounds.size.y / 2;
    }

    public void Move(float h, float v)
    {
        if (isJumping) return;

        if (h < 0f)
            playerAnim.SetBool("left", true);
        else if (h > 0f)
            playerAnim.SetBool("left", false);

        Vector2 nomal = new Vector2(h, v).normalized;

        float speedScale = Mathf.Min(1 + (GameManager.Instance.GetIncreasementSpeed() - 1) * 0.6f, 1.22f);
        
        float playerPosX = Mathf.Clamp(transform.position.x + nomal.x * horizontalSpeed * speedScale * Time.deltaTime, minPosX, maxPosX);
        float playerPosY = Mathf.Clamp(transform.position.y + nomal.y * verticalSpeed * speedScale * Time.deltaTime, minPosY, waterHeight);
        
        if(playerPosY >= waterHeight && v > 0)
        {
            jumpBuffer += Time.deltaTime;
            if(jumpBuffer > 0.06f)
            {
                StartCoroutine(Jump());
                isJumping = true;
                return;
            }
        }
        else
        {
            jumpBuffer = 0;
        }
        transform.position = new Vector3(playerPosX, playerPosY, 0);
        
        
    }
    

    

    IEnumerator Jump()
    {
        playerAnim.SetTrigger("jump");

        float upDownTime = 0.34f;
        float timer = 0;
        
        float offset = maxJumpHeight - waterHeight;
        GameObject wf1 = Instantiate(waterFlip, transform.position - new Vector3(0, 0.25f, 0), Quaternion.identity);
        wf1.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        while (timer < upDownTime)
        {
            timer += Time.deltaTime;
            
            float currentPosY = jumpCurve.Evaluate(timer / upDownTime) * offset;
            transform.position = new Vector3(transform.position.x, currentPosY + waterHeight);
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, maxJumpHeight);
        timer = upDownTime;
        yield return new WaitForSeconds(flyingTime);
        
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            
            float currentPosY = jumpCurve.Evaluate(timer / upDownTime) * offset;
            transform.position = new Vector3(transform.position.x, currentPosY + waterHeight);
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, waterHeight);
        GameObject wf2 = Instantiate(waterFlip, transform.position - new Vector3(0, 0.25f, 0), Quaternion.identity);
        wf2.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        isJumping = false;
    }
}
