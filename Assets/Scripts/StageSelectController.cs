using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectController : ViewController
{
    [SerializeField] private NavigationViewController navigationView;
    [SerializeField] private Transform scroll;
    [SerializeField] private GameObject levelOrigin;

    private Dictionary<int, GameObject> levels = new Dictionary<int, GameObject>();
    //뷰의 타이틀을 반환
    public override string Title
    {
        get
        {
            return "SELECT LEVEL";
        }
    }

    private void Awake()
    {
        int index = (DataManager.instance.LowDifficulty.Count + DataManager.instance.MiddleDifficulty.Count + DataManager.instance.HighDifficulty.Count)*2;
        for (int i=0; i<index; i++)
        {
            GameObject obj = Instantiate(levelOrigin) as GameObject;
            obj.name = (i + 1).ToString();
            obj.transform.SetParent(scroll);
        }
    }
}
