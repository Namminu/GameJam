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
    
    
    [Header("Jump Property")]
    [SerializeField] private float waterHeight;
    [SerializeField] private float maxJumpHeight;
    [SerializeField] private float flyingTime;

    [SerializeField] private AnimationCurve jumpCurve;
    

    private float jumpBuffer;
    
    private float maxPosX;
    private float minPosX;
    private float minPosY;

    private bool isJumping;
    
    private void Start()
    {
        Collider2D col = GetComponent<Collider2D>();
        
        maxPosX = Camera.main.ViewportToWorldPoint(new Vector3(0.99f, 0)).x - col.bounds.size.x / 2;
        minPosX = Camera.main.ViewportToWorldPoint(new Vector3(0.01f, 0f)).x + col.bounds.size.x / 2;
        minPosY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.01f)).y + col.bounds.size.y / 2;
    }

    public void Move(float h, float v)
    {

        if (isJumping) return;

        Vector2 nomal = new Vector2(h, v).normalized;
        
        float playerPosX = Mathf.Clamp(transform.position.x + nomal.x * horizontalSpeed * Time.deltaTime, minPosX, maxPosX);
        float playerPosY = Mathf.Clamp(transform.position.y + nomal.y * verticalSpeed * Time.deltaTime, minPosY, waterHeight);
        
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

        float upDownTime = 0.5f;
        float timer = 0;
        
        float offset = maxJumpHeight - waterHeight;

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
        isJumping = false;
    }
}
