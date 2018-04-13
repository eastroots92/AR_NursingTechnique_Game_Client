using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyChartViewController : ViewController {

    [SerializeField] private NavigationViewController navigationView;
    [SerializeField] private Sprite[] myCharImageSource;
    [SerializeField] private Image mycharImage;
    [SerializeField] private Text nameTxt;
    [SerializeField] private Text levelTxt;
    [SerializeField] private Text totalPointTxt;
    [SerializeField] private Text rankTxt;
    [SerializeField] private Text successTxt;


    //private ShopItemData itemData;					// 아이템 데이터를 저장
    
    //뷰의 타이틀을 반환
    public override string Title
    {
        get
        {
            return "MY PAGE";
        }
    }

    private void OnEnable()
    {
                
        nameTxt.text = DataManager.instance.UserName + " 간호사";

        if(DataManager.instance.Score < 3000)
        {
            levelTxt.text = "실습";
            mycharImage.sprite = myCharImageSource[0]; 
        }
        else if (DataManager.instance.Score < 9000)
        {
            levelTxt.text = "신입";
            mycharImage.sprite = myCharImageSource[1];
        }
        else if (DataManager.instance.Score < 15000)
        {
            levelTxt.text = "수";
            mycharImage.sprite = myCharImageSource[2];
        }
        else
        {
            levelTxt.text = "책임";
            mycharImage.sprite = myCharImageSource[3];
        }

        totalPointTxt.text = DataManager.instance.Score.ToString(); 

        string rank = DataManager.instance.MyRank.ToString() +" / " + DataManager.instance.TotalUser.ToString();
        rankTxt.text = rank;

        successTxt.text = setSuccessText(DataManager.instance.Wins, DataManager.instance.Count);

    }

    private string setSuccessText(int Wins, int Count){
        string result = "";
        
        double percent = 0;

        if( Count != 0 ){
            percent = (double) Wins/ (double) Count;
            percent = percent*100;
        }

        result = Wins.ToString() + "/" + Count.ToString() +" ( " + percent.ToString("#.##")  +"% ) ";
        
        return result;
    }

    //// 아이템 상세 화면의 내용을 갱신하는 메서드
    //public void UpdateContent(ShopItemData itemData)
    //{
    //    // 아이템의 데이터를 저장해둔다
    //    this.itemData = itemData;

    //    iconImage.sprite =
    //        SpriteSheetManager.GetSpriteByName("IconAtlas", itemData.iconName);
    //    nameLabel.text = itemData.name;
    //    priceLabel.text = itemData.price.ToString();
    //    descriptionLabel.text = itemData.description;
    //}
}
