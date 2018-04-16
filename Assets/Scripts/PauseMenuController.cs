using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour {

    private GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    public void OnClickCloseBtn()
    {
        gm.IsStart = true;
        gameObject.SetActive(false);
    }

    public void OnClickHomeBtn()
    {
        StartCoroutine(GameSceneManager.instance.ChangeScene(2));
    }

    public void OnClickRestartBtn()
    {
        StartCoroutine(GameSceneManager.instance.ChangeScene(3));
    }
}
