using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHp : MonoBehaviour
{
    private PlayerScore playerScore;

    public float timeDecreaseHp;

    public Image hpBar;
    public float player_MaxHp;
    public float player_CurrentHp;


    // Start is called before the first frame update
    void Start()
    {
        playerScore = GetComponent<PlayerScore>();

        player_CurrentHp = player_MaxHp;

	}

    // Update is called once per frame
    void Update()
    {
        float hpPercent = player_CurrentHp / player_MaxHp;
        hpBar.fillAmount = hpPercent;

        player_CurrentHp -= timeDecreaseHp / 100;

		if (player_CurrentHp < 0)
		{
			PlayerDie();
		}
		//Debug.Log(playerScore.timeScore / playerScore.timeScore);
	}

    public void GotDamage(float damage)
    {
		player_CurrentHp -= damage;

		if (player_CurrentHp < 0)
		{
			PlayerDie();
		}
	}

    void PlayerDie()
    {
        //�÷��̾� ���
        Debug.Log("�÷��̾� ���");
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "DonutItem")
		{
            Debug.Log("���� : ü�� ȸ��");
			player_CurrentHp += 10;
            Destroy(collision.gameObject);

            //ü�� ȸ�� ����
		}
	}
}
