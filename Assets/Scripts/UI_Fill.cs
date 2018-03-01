using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Fill : MonoBehaviour {
	
	[SerializeField]
	private Image fill;

	[SerializeField]
	private float fillAmount =1;

	[SerializeField]
	private GameObject button;
	[SerializeField]
	private GameObject GameStart;
	[SerializeField]
	private GameObject GameUI;

	[SerializeField]
	private Text timerText;

	// Use this for initialization
	void Start () {
		GameUI.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		if (fillAmount >= 0) {
			fillAmount = 1 - Time.time / 100;
			UpdateBar ();
			Debug.Log (fillAmount);

			string minutes = ((int) (fillAmount*100)/60).ToString();
			string seconds = ((int)(fillAmount*100-60)).ToString();

			timerText.text = "00:0" + minutes + ":" + seconds;
		}
		else
			button.SetActive (true);
	}


	private void UpdateBar(){
		if(fillAmount != fill.fillAmount)
			fill.fillAmount = fillAmount;
	}
}
