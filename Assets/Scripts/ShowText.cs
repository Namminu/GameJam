using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class ShowText : MonoBehaviour
{
	public GameObject FishText;
	public GameObject HpText;
	public Canvas canvas;
	public Transform farmingPoint;

	// Start is called before the first frame update
	public void Start()
	{
		

	}

	// 아이템을 먹은 위치(pos)에서 Text UI를 생성하고 위치 이동시킴
	public void FishTextUIAt(Vector3 pos)
	{
		GameObject textUI = Instantiate(FishText, pos, Quaternion.identity);
		textUI.transform.SetParent(canvas.transform, false);
		textUI.transform.position = farmingPoint.position;
	}

	public void HpTextUIAt(Vector3 pos)
	{
		GameObject textUI = Instantiate(HpText, pos, Quaternion.identity);
		textUI.transform.SetParent(canvas.transform, false);
		textUI.transform.position = farmingPoint.position;
	}
}