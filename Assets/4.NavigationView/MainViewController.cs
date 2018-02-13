using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainViewController : ViewController {

    [SerializeField] private NavigationViewController navigationView;
    [SerializeField] private MyChartViewController mychartView;
    [SerializeField] private HowToPlayViewController howtoplayView;

    //뷰의 타이틀
    public override string Title
    {
        get
        {
            return "MAIN";
        }
    }

    protected void Start()
    {
        if(navigationView != null)
        {
            navigationView.Push(this);
        }
    }

    //public void OnPressButton(ShopItemTableViewCell cell)
    public void OnPressButton(string pageName)
    {
        if(navigationView != null)
        {
            //선택된 셀로부터 아이템의 데이터를 가져와서 아이템 상세 화면의 내용을 갱신
            //detailView.UpdataContent(tableData[cell.DataIndex])

            if (pageName.Equals("MyChart"))
            {
                navigationView.Push(mychartView); //MyChart 화면으로 넘어간다.
            }
            else if (pageName.Equals("HowToPlay"))
            {
                navigationView.Push(howtoplayView); //게임 방법 화면으로 넘어간다.
            }
        }
    }
}
