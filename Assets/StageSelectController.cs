using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectController : ViewController
{
    [SerializeField] private NavigationViewController navigationView;

    //뷰의 타이틀을 반환
    public override string Title
    {
        get
        {
            return "SELECT LEVEL";
        }
    }
}
