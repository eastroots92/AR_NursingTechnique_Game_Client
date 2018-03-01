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

	[SerializeField]
	private List<Image> baseItemImage;
	[SerializeField]
	private List<Image> necessaryItemImage;
	[SerializeField]
	private List<Image> confusionItemImage;


	// Use this for initialization
	void Start () {
		GameUI.SetActive (true);
		int i = 0;int j = 0;int l = 0;

		for (int k = 0; k < DataManager.instance.BaseRating.Count; k++) {
			GameObject obj = new GameObject ("item");
			Image img = obj.AddComponent<Image> ();
			obj.transform.SetParent (GameObject.Find("Item").transform);
			baseItemImage.Add (img);
		}
		for (int o = 0; o < DataManager.instance.NecessaryRating.Count; o++) {
			GameObject obj = new GameObject ("items");
			Image img = obj.AddComponent<Image> ();
			obj.transform.SetParent (GameObject.Find("Items").transform);
			necessaryItemImage.Add (img);
		}
		for (int p = 0; p < DataManager.instance.ConfusionRating.Count; p++) {
			GameObject obj = new GameObject ("itemss");
			Image img = obj.AddComponent<Image> ();
			obj.transform.SetParent (GameObject.Find("Itemss").transform);
			confusionItemImage.Add (img);
		}
			
		foreach (string name in DataManager.instance.BaseRating) {
			baseItemImage [i].preserveAspect = true;
			baseItemImage [i].sprite = Resources.Load<Sprite> (name);
			i++;
		}
		foreach (string name in DataManager.instance.NecessaryRating) {
			necessaryItemImage [j].preserveAspect = true;
			necessaryItemImage [j].sprite = Resources.Load<Sprite> (name);
			j++;
		}
		foreach (string name in DataManager.instance.ConfusionRating) {
			confusionItemImage [l].preserveAspect = true;
			confusionItemImage [l].sprite = Resources.Load<Sprite> (name);
			l++;
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (fillAmount >= 0) {
			fillAmount = 1 - Time.time / 100;
			UpdateBar ();
			//Debug.Log (fillAmount);

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
