using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObstacle : MonoBehaviour
{
    private Obstacle obstacle;
    private AutoScroll autoscroll;
	private PlayerHp playerHp;


    public bool isHit = false;
    public bool isDash = false;
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
		playerHp = GetComponent<PlayerHp>();
		blinkInterval = invincibilityTime / blinkCount / 2;
    }

	private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Obstacle"))
        {

            if (isHit || isDash) return;
			isHit = true;
			SoundManager.Instance.PlayHit();
			GotHit();
            playerHp.GotDamage(collision.GetComponent<IObstacle>().damage);
            playerHp.GotHit();
        }
	}

	private void GotHit()
	{
		StartCoroutine(Blink());
		StartCoroutine(Invincibility());
	}

	IEnumerator Blink() //���� ȿ��
	{
		int blinkTimes = 0;

		while (blinkTimes < blinkCount)
		{
			Color color = spriteRenderer.color;
			color.a = 0.1f;
			spriteRenderer.color = color;
			yield return new WaitForSeconds(blinkInterval);
			color = spriteRenderer.color;
			color.a = 1;
			spriteRenderer.color = color;
			yield return new WaitForSeconds(blinkInterval); 
			blinkTimes++; 
		}
	}

	IEnumerator Invincibility() //����
	{
		GameManager.Instance.DecreaseSpeedRatio(obstacleSlowRatio);
        yield return new WaitForSeconds(invincibilityTime);
		GameManager.Instance.InitSlowSpeedRatio();
		isHit = false;
	}
}
