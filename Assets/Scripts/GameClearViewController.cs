using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearViewController : MonoBehaviour
{
    [SerializeField] private GameObject success;
    [SerializeField] private GameObject gameover;
    [SerializeField] private Text scoreText;

    private GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        if (gm.IsClear)
        {
            success.SetActive(true);
            gameover.SetActive(false);
            int score = gm.Life * 100;
            scoreText.text = "Score : " + score.ToString();
        }
        else
        {
            success.SetActive(false);
            gameover.SetActive(true);
        }
    }

    public void OnClickHomeBtn()
    {
        StartCoroutine(GameSceneManager.instance.ChangeScene(2));
    }

    public void OnClickRestartBtn()
    {
        StartCoroutine(GameSceneManager.instance.ChangeScene(3));
    }

    public void OnClickLevelBtn()
    {
        StartCoroutine(GameSceneManager.instance.ChangeScene(2));
    }
}
