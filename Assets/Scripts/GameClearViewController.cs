using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearViewController : MonoBehaviour
{
    [SerializeField] private GameObject success;
    [SerializeField] private GameObject gameover;

    private GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        if (gm.IsClear)
        {
            success.SetActive(true);
            gameover.SetActive(false);
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
