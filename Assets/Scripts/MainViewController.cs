using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainViewController : ViewController {

    [SerializeField] private NavigationViewController navigationView;
    [SerializeField] private MyChartViewController mychartView;
    [SerializeField] private HowToPlayViewController howtoplayView;
    [SerializeField] private Button playButton;

    //뷰의 타이틀
    public override string Title
    {
        get
        {
            return "Home";
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

    public void OnPressPlayButton()
    {
        if (!DataManager.instance.isPlay)
        {
            DataManager.instance.SendRandomItem(1);
            StartCoroutine(GameSceneManager.instance.ChangeScene (3));
			DataManager.instance.isPlay = true;
        }
        else
        {
            DataManager.instance.SendRandomItem(1);
            StartCoroutine(GameSceneManager.instance.ChangeScene(4));
            DataManager.instance.isPlay = false;
        }
    }
}
