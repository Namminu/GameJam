using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObstacle : MonoBehaviour
{
    private Obstacle obstacle;
    private AutoScroll autoscroll;

    private bool isHit = false;
	private float hp;


    // Start is called before the first frame update
    void Start()
    {
        obstacle = GetComponent<Obstacle>();
        autoscroll = GetComponent<AutoScroll>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        float timer = 0f;
        float obstacleSlow = 1f;
		float currentAutoScrollSpeed;

        if(collision.tag == "Obstacle")
        {
            Debug.Log("장애물이다");
            if (isHit) return;

            timer += Time.deltaTime;
            isHit = true;
			//currentAutoScrollSpeed = autoscroll.speed;
			//autoscroll.speed -= 0.1f;  //오토스크롤 속도 줄이기
			//hp -= obstacle.ob_Damage;  //체력 감소
			
			{
				//         while (timer < obstacleSlow)
				//         {
				//             Debug.Log("장애물 충돌 시작");

				//             isHit = true;
				//             timer += Time.deltaTime;
				//             //autoscroll.speed -= 0.1f;

				//             //아직 데미지 입력 X
				//             //hp -= obstacle.ob_Damage;

				//             if (timer > obstacleSlow) return;
				//}

				//while (timer> obstacleSlow)
				//         {
				//	Debug.Log("장애물 충돌 종료");

				//	isHit = false;
				//	timer -= Time.deltaTime;
				//             //autoscroll.speed -= 0.1f;

				//             if (timer == 0) return;
				//}
			}
		}

		if (timer >= obstacleSlow)
		{
			Debug.Log("스크롤 속도 원상복귀");
			//autoscroll.speed = currentAutoScrollSpeed;
			isHit = false;
		}
	}


}
