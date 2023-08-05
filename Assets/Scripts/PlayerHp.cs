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

    public Sprite[] gotHitSprites;
    public Sprite lessHpSprite;
    private Sprite originalSprite;

    private bool isGotHit = false;

	[SerializeField] private float hpHealAmount = 10;
	

    // Start is called before the first frame update
    void Start()
    {
        originalSprite = Face_Idle.GetComponent<Image>().sprite;
        playerScore = GetComponent<PlayerScore>();

        player_CurrentHp = player_MaxHp;
	}

    // Update is called once per frame
    void Update() 
    {
        float hpPercent = player_CurrentHp / player_MaxHp;
        hpBar.fillAmount = hpPercent;

        player_CurrentHp -= timeDecreaseHp / 100;

        if (!isGotHit)
        {
            if (hpPercent > ChangeFaceHp)
            {
                Face_Idle.GetComponent<Image>().sprite = originalSprite;
            }
            else
            {
                Face_Idle.GetComponent<Image>().sprite = lessHpSprite;
            }
        }

		if (player_CurrentHp < 0)
		{
			PlayerDie();
		}

	}

    public void GotHit()
    {
        StartCoroutine(GotHitIconAnimationPlay());
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
	    GameOverMenu.Instance.GameOver();
        //�÷��̾� ���
        Debug.Log("�÷��̾� ���");
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

    IEnumerator GotHitIconAnimationPlay()
    {
        isGotHit = true;

        float invincibilityTime = GetComponent<PlayerObstacle>().invincibilityTime;
        for (int i = 0; i < gotHitSprites.Length; i++)
        {
            Face_Idle.GetComponent<Image>().sprite = gotHitSprites[i];
            yield return new WaitForSeconds(invincibilityTime / gotHitSprites.Length);
        }
        Face_Idle.GetComponent<Image>().sprite = originalSprite;

        isGotHit=false;
    }
}
