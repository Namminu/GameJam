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


    public float ChangeFaceHp;
    public GameObject Face_Idle;
	public GameObject Face_lessHp;



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

        if(hpPercent > ChangeFaceHp)
        { 
            hpBar.color = new Color(0, 215, 0, 255);
            Face_Idle.SetActive(true);
			Face_lessHp.SetActive(false);
		}
        else 
        {
			hpBar.color = new Color(255, 0, 0, 255);
			Face_Idle.SetActive(false);
			Face_lessHp.SetActive(true);
		}



		if (player_CurrentHp < 0)
		{
			PlayerDie();
		}

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
        //플레이어 사망
        Debug.Log("플레이어 사망");
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "DonutItem")
		{
            Debug.Log("도넛 : 체력 회복");
			player_CurrentHp += 10;
            Destroy(collision.gameObject);

            //체력 회복 사운드
		}
	}


}
