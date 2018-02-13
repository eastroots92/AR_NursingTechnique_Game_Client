using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class ShopItemTableViewController : TableViewController<ShopItemData>
// TableViewController<T>클래스를 상속
{
    // 리스트 항목의 데이터를 읽어 들이는 메서드
    private void LoadData()
    {
        // 일반적인 데이터는 데이터 소스로부터 가져오는데 여기서는 하드 코드를 사용해하여 정의한다
        tableData = new List<ShopItemData>() {
            new ShopItemData { iconName="drink1", name="WATER", description="Nothing else, just water." },
            new ShopItemData { iconName="drink2", name="SODA", description="Sugar free and low calorie." },
            new ShopItemData { iconName="drink3", name="COFFEE", description="How would you like your coffee?" },
            new ShopItemData { iconName="drink4", name="ENERGY DRINK", description="It will give you wings." },
            new ShopItemData { iconName="drink5", name="BEER", description="It's a drink for grown-ups." },
            new ShopItemData { iconName="drink6", name="COCKTAIL", description="A cocktail made of tropical fruits." },
            new ShopItemData { iconName="fruit1", name="CHERRY", description="Do you like cherries?" },
            new ShopItemData { iconName="fruit2", name="ORANGE", description="It contains much vitamin C." },
            new ShopItemData { iconName="fruit3", name="APPLE", description="Enjoy the goodness without peeling it." },
            new ShopItemData { iconName="fruit4", name="BANANA", description="Don't slip on its peel." },
            new ShopItemData { iconName="fruit5", name="GRAPE", description="It's not a grapefruit." },
            new ShopItemData { iconName="fruit6", name="PINEAPPLE", description="It's not a hand granade." },
            new ShopItemData { iconName="gun1", name="MINI GUN", description="A tiny concealed carry gun." },
            new ShopItemData { iconName="gun2", name="CLASSIC GUN", description="The gun that was used by a pirate." },
            new ShopItemData { iconName="gun3", name="STANDARD GUN", description="Just a standard weapon." },
            new ShopItemData { iconName="gun4", name="REVOLVER", description="It can hold a maximum of 6 bullets." },
            new ShopItemData { iconName="gun5", name="AUTO RIFLE", description="It can fire automatically and rapidly." },
            new ShopItemData { iconName="gun6", name="SPACE GUN", description="A weapon that comes from the future." },
        };

        // 스크롤시킬 내용의 크기를 갱신한다
        UpdateContents();
    }

    // 리스트 항목에 대응하는 셀의 높이를 반환하는 메서드
    protected override float CellHeightAtIndex(int index)
    {
        return 128.0f;
    }

    // 인스턴스를 로드할 때 호출된다
    protected override void Awake()
    {
        // 기반 클래스에 포함된 Awake 메서드를 호출한다
        base.Awake();

        // 아이콘의 스프라이트 시트에 포함된 스프라이트를 캐시해둔다
        SpriteSheetManager.Load("IconAtlas");
    }

    // 인스턴스를 로드할 때 Awake 메서드가 처리된 다음에 호출된다
    protected override void Start()
    {
        // 기반 클래스의 Start 메서드를 호출한다
        base.Start();

        // 리스트 항목의 데이터를 읽어 들인다
        LoadData();
    }
}
