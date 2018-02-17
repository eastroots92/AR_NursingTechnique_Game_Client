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
