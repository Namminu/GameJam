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
	public float blinkInterval = 0.05f;
	public int blinkCount = 5;
	private bool isBlinking = false;

	float timer = 0f;

	// Start is called before the first frame update
	void Start()
    {
        obstacle = GetComponent<Obstacle>();
        autoscroll = GetComponent<AutoScroll>();

		spriteRenderer = GetComponent<SpriteRenderer>();

	}

	private void OnTriggerEnter2D(Collider2D collision)
    {

        float obstacleSlow = 1f;
		//float currentAutoScrollSpeed;

        if(collision.tag == "Obstacle")
        {

            if (isHit) return;
			Debug.Log("��ֹ��̴�");
			//timer = 0f;

            isHit = true;
			StartBlink();

			//currentAutoScrollSpeed = autoscroll.speed;
			//autoscroll.speed -= 0.1f;  //���佺ũ�� �ӵ� ���̱�
			//hp -= obstacle.ob_Damage;  //ü�� ����
			
			{
				//         while (timer < obstacleSlow)
				//         {
				//             Debug.Log("��ֹ� �浹 ����");

				//             isHit = true;
				//             timer += Time.deltaTime;
				//             //autoscroll.speed -= 0.1f;

				//             //���� ������ �Է� X
				//             //hp -= obstacle.ob_Damage;

				//             if (timer > obstacleSlow) return;
				//}

				//while (timer> obstacleSlow)
				//         {
				//	Debug.Log("��ֹ� �浹 ����");

				//	isHit = false;
				//	timer -= Time.deltaTime;
				//             //autoscroll.speed -= 0.1f;

				//             if (timer == 0) return;
				//}
			}

			//if (timer >= obstacleSlow)
			//{
			//	Debug.Log("��ũ�� �ӵ� ���󺹱�");
			//	//autoscroll.speed = currentAutoScrollSpeed;
			//	isHit = false;
			//}
		}

		
	}

	void StartBlink()
	{
		if(!isBlinking)
		{
			StartCoroutine(Blink());
		}
	}
	IEnumerator Blink()
	{
		isBlinking = true; 
		int blinkTimes = 0;

		while (blinkTimes < blinkCount)
		{
			spriteRenderer.enabled = false; 
			yield return new WaitForSeconds(blinkInterval); 
			spriteRenderer.enabled = true;
			yield return new WaitForSeconds(blinkInterval); 
			blinkTimes++; 
		}
		isBlinking = false;

		isHit = false;
	}
}
