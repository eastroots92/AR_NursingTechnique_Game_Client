using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStarViewController : MonoBehaviour
{
    [SerializeField] private Sprite[] titleImg;
    [SerializeField] private Image title;
    [SerializeField] private Text orderText;
    [SerializeField] private Text supplyText;
    [SerializeField] private GameObject[] orderGroup;
    [SerializeField] private GameObject[] supplyGroup;

    private string clinicalTitle;
    private GameManager gm;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        clinicalTitle = gm.ClinicalTitle;

        if(gm.currentGame == GameManager.GameState.OrderGame)
        {
            foreach(GameObject obj in orderGroup)
                obj.SetActive(true);

            title.sprite=titleImg[0];
            orderText.text = "이제 시작할 게임은 " + "<color=#ff0000>" + clinicalTitle + "</color>" + "의 준비물 게임입니다.";
        }
        else
        {
            foreach (GameObject obj in supplyGroup)
                obj.SetActive(true);

            title.sprite = titleImg[1];
        }
    }
}
