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

	[SerializeField] private float hpHealAmount = 10;
	

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

            Face_Idle.SetActive(true);
			Face_lessHp.SetActive(false);
		}
        else 
        {

			Face_Idle.SetActive(false);
			Face_lessHp.SetActive(true);
		}
    }

    public void GotDamage(float damage)
    {
		player_CurrentHp -= damage;

		if (player_CurrentHp < 0)
		{
			GameOverMenu.Instance.GameOver();
		}
	}

    private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("DonutItem"))
		{
            Debug.Log("���� : ü�� ȸ��");
            player_CurrentHp = Mathf.Min(player_CurrentHp + hpHealAmount, player_MaxHp);
			Destroy(collision.gameObject);
			hpBar.fillAmount = player_CurrentHp / player_MaxHp;
  
			//ü�� ȸ�� ����
		}
	}
}
