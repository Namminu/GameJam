using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
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
    

    private float jumpBuffer;
    
    private float maxPosX;
    private float minPosX;
    private float minPosY;

    private bool isJumping;
    private bool isDash = false;

    private PlayerScore playerScore;
    private PlayerObstacle playerObstacle;

    public float dashTimePer;

    public float increaseScrollSpeed;
    private void Start()
    {
        Collider2D col = GetComponent<Collider2D>();
        playerObstacle = GetComponent<PlayerObstacle>();
		playerScore = GetComponent<PlayerScore>();

		maxPosX = Camera.main.ViewportToWorldPoint(new Vector3(0.99f, 0)).x - col.bounds.size.x / 2;
        minPosX = Camera.main.ViewportToWorldPoint(new Vector3(0.01f, 0f)).x + col.bounds.size.x / 2;
        minPosY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.01f)).y + col.bounds.size.y / 2;
    }

    public void Move(float h, float v)
    {

        if (isJumping) return;

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
		float upDownTime = 0.34f;
        float timer = 0;
        
        float offset = maxJumpHeight - waterHeight;
        Instantiate(waterFlip, transform.position, Quaternion.identity);
        while (timer < upDownTime)
        {
            Dash();

            if (isDash&&playerScore.player_CurrentFish !=0) { break; }
            timer += Time.deltaTime;
            
            float currentPosY = jumpCurve.Evaluate(timer / upDownTime) * offset;
            transform.position = new Vector3(transform.position.x, currentPosY + waterHeight);
            yield return null;
        }
        
        while (timer > 0)
        {
            Dash();
			if (isDash) timer += Time.deltaTime;
			timer -= Time.deltaTime;

            timer = Mathf.Min(timer, maxJumpHeight);
            
            float currentPosY = jumpCurve.Evaluate(timer / upDownTime) * offset;
            transform.position = new Vector3(transform.position.x, currentPosY + waterHeight);
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, waterHeight);
        Instantiate(waterFlip, transform.position, Quaternion.identity);
        isJumping = false;
    }


    public void Dash()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!isDash&& playerScore.player_CurrentFish!=0)
                StartCoroutine(StartDash());
        }
    }

    IEnumerator StartDash()
	{
		dashTimePer = 10f;
		float dashTime = playerScore.player_CurrentFish / dashTimePer;
        //float tempTime = GameManager.Instance.GetIncreasementSpeed();

		GameManager.Instance.IncreaseSpeedRatio(increaseScrollSpeed);
		isDash = true;
		playerObstacle.isHit = true;

		float fishFill = playerScore.fishBar.fillAmount;
        float maxDashTime = dashTime;
		while (dashTime > 0f)
        {
            dashTime -= Time.deltaTime;
			playerScore.fishBar.fillAmount = Mathf.Lerp(0f, fishFill, dashTime / maxDashTime);
			yield return null;
		}

		GameManager.Instance.ChangeSpeedRatio();

		isDash = false;  
        playerScore.player_CurrentFish = 0;

		playerObstacle.isHit = false;
	}
}
