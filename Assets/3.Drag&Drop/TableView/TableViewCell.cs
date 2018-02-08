using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//테이블 뷰에서 셀의 위치나 크기를 제어하기 위한 몇 가지 속성을 정의
public class TableViewCell<T> : ViewController {

    //셀의 내용을 갱신하는 메서드
	public virtual void UpdateContent(T itemData)
    {
         // 실제 처리는 상속한 클래스에서 구현
    }

    //셀에 대응하는 리스트 항목의 인덱스를 저장
    public int DataIndex { get; set; }

    //셀의 높이를 나타내는 속성
    public float Height
    {
        get { return CachedRectTransform.sizeDelta.y;  }
        set
        {
            Vector2 sizeDelta = CachedRectTransform.sizeDelta;
            sizeDelta.y = value;
            CachedRectTransform.sizeDelta = sizeDelta;
        }
    }

    //셀 위쪽 끝의 위치를 나타내는 속성
    public Vector2 Top
    {
        get
        {
            Vector3[] corners = new Vector3[4];
            CachedRectTransform.GetLocalCorners(corners);
            return CachedRectTransform.anchoredPosition + new Vector2(0.0f, corners[1].y);
        }
        set
        {
            Vector3[] corners = new Vector3[4];
            CachedRectTransform.GetLocalCorners(corners);
            CachedRectTransform.anchoredPosition = value - new Vector2(0.0f, corners[1].y);
        }
    }

    //셀 아래쪽 끝의 위치를 나타내는 속성
    public Vector2 Bottom
    {
        get
        {
            Vector3[] corners = new Vector3[4];
            CachedRectTransform.GetLocalCorners(corners);
            return CachedRectTransform.anchoredPosition + new Vector2(0.0f, corners[3].y);
        }
        set
        {
            Vector3[] corners = new Vector3[4];
            CachedRectTransform.GetLocalCorners(corners);
            CachedRectTransform.anchoredPosition = value - new Vector2(0.0f, corners[3].y);
        }
    }
}
