using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObstacle : MonoBehaviour
{
    private Obstacle obstacle;
    private AutoScroll autoscroll;

    private bool isHit = false;
	private float hp;

	private SpriteRenderer spriteRenderer;

	public float invincibilityTime = 2f;
	private float blinkInterval;
	public int blinkCount = 5;

	public float obstacleSlowRatio;
	// Start is called before the first frame update
	void Start()
    {
        obstacle = GetComponent<Obstacle>();
        autoscroll = GetComponent<AutoScroll>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		blinkInterval = invincibilityTime / blinkCount / 2;
    }

	private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Obstacle")
        {
            if (isHit) return;
			isHit = true;
			GotHit();
		}
	}

	private void GotHit()
	{
		StartCoroutine(Blink());
		StartCoroutine(Invincibility());
	}

	IEnumerator Blink()
	{
		int blinkTimes = 0;

		while (blinkTimes < blinkCount)
		{
			spriteRenderer.enabled = false; 
			yield return new WaitForSeconds(blinkInterval); 
			spriteRenderer.enabled = true;
			yield return new WaitForSeconds(blinkInterval); 
			blinkTimes++; 
		}
	}

	IEnumerator Invincibility()
	{
		float tempSpeed = GameManager.Instance.GetIncreasementSpeed();
		GameManager.Instance.DecreaseSpeedRatio(obstacleSlowRatio);
        yield return new WaitForSeconds(invincibilityTime);
		GameManager.Instance.ChangeSpeedRatio(tempSpeed);
		isHit = false;
	}
}
