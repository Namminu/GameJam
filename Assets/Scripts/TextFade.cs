using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFade : MonoBehaviour
{
	private Text text;
    // Start is called before the first frame update
    void Start()
    {
		text = GetComponent<Text>();
		StartCoroutine(FadeOut());
    }


	IEnumerator FadeOut()
	{
		float fadeCount = 0f;
		while (fadeCount < 1.0f) 
		{
			fadeCount += 0.05f;
			yield return new WaitForSeconds(0.1f);
			text.color -= new Color(0, 0, 0, fadeCount);
		}
		Destroy(gameObject); 
	}
}
