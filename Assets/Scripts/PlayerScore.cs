using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
	public int fishScore = 0;
	public int timeScore = 0;
	public Text TextScore;

	private float startTime;

	public Image fishBar;
	public float player_MaxFish;
	public float player_CurrentFish;

	// Start is called before the first frame update
	void Start()
	{ 
		startTime = Time.time;

		player_MaxFish = 100f;
		player_CurrentFish = 0.0f;
	}
	private void Update()
	{
		int elapsedTime = Mathf.FloorToInt(Time.time - startTime); // 경과 시간(초)을 계산합니다
		timeScore = elapsedTime; // 경과 시간(초)을 점수로 변환합니다

		TextScore.text = string.Format($"Score  :  {timeScore + fishScore}");

		float fishPercent = player_CurrentFish / player_MaxFish;
		fishBar.fillAmount = fishPercent;
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "FishItem")
		{
			Debug.Log("물고기 : 점수 및 게이지 증가");
			fishScore += 10;
			player_CurrentFish += 10;
			Destroy(collision.gameObject);

			//도넛 사운드
		}
	}

}
