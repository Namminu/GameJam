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

	private ShowText showText;

	// Start is called before the first frame update
	void Start()
	{ 
		startTime = Time.time;

		player_MaxFish = 100f;
		player_CurrentFish = 0.0f;

		showText = GetComponent<ShowText>();
	}
	private void Update()
	{
		int elapsedTime = Mathf.FloorToInt(Time.time - startTime); // ���?�ð�(��)�� ����մϴ�?
		timeScore = elapsedTime; // ���?�ð�(��)�� ������ ��ȯ�մϴ�

		TextScore.text = string.Format($"Score  :  {timeScore + fishScore}");

		float fishPercent = player_CurrentFish / player_MaxFish;
		fishBar.fillAmount = fishPercent;
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("FishItem"))
		{
			SoundManager.Instance.GetItem();
			fishScore += 10;
			player_CurrentFish += 10;
			Destroy(collision.gameObject);

			Vector3 fishPosition = collision.gameObject.transform.position;

			showText.FishTextUIAt(fishPosition);
		}
	}

}
