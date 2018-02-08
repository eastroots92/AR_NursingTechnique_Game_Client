using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyChartViewController : ViewController {

    [SerializeField] private NavigationViewController navigationView;

    //
    //[SerializeField] private Image iconImage;       // 아이템의 아이콘을 표시하는 이미지
    //[SerializeField] private Text nameLabel;        // 아이템 이름을 표시하는 텍스트
    //[SerializeField] private Text descriptionLabel; // 설명을 표시할 텍스트

    //private ShopItemData itemData;					// 아이템 데이터를 저장
    
    //뷰의 타이틀을 반환
    public override string Title
    {
        get
        {
            return "My Chart";
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
