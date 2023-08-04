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
            Debug.Log("��ֹ��̴�");
            if (isHit) return;

            timer += Time.deltaTime;
            isHit = true;
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
		}

		if (timer >= obstacleSlow)
		{
			Debug.Log("��ũ�� �ӵ� ���󺹱�");
			//autoscroll.speed = currentAutoScrollSpeed;
			isHit = false;
		}
	}


}
