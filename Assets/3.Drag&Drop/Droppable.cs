using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Droppable : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    //드롭 영역에 들어온 아이콘
    [SerializeField] private Image iconImage;
    //드롭 영역에 들어온 아이콘의 하이라이트 색
    [SerializeField] private Color highlightedColor;
    //드롭 영역에 들어온 아이콘의 본래 색을 저장
    private Color normalColor;

    private void Start()
    {
        //드롭 영역에 들어온 아이콘의 본래 색을 저장해둔다
        normalColor = iconImage.color;
    }

    public void OnDrop(PointerEventData eventData)
    {
        //드래그하고 있었던 아이콘의 Image 컴포넌트를 가져온다
        Image droppedImage = eventData.pointerDrag.GetComponent<Image>();
        //드롭 영역에 들어온 아이콘의 스프라이트를 드롭된 아이콘과 동일한 스프라이트로 변경해 색을 본래의 색으로 되돌린다
        iconImage.sprite = droppedImage.sprite;
        iconImage.color = normalColor;
    }

    //마우스 커서가 영역에 들어왔을 때 호출된다.
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.dragging)
        {
            //드래그 도중이였다면 드롭 영역에 들어온 아이콘 색을 하이라이트 색으로 변경한다.
            iconImage.color = highlightedColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.dragging)
        {
            //드래그 도중이라면 드롭 영역에 들어온 아이콘의 색을 본래 색으로 되돌린다.
            iconImage.color = normalColor;
        }
    }
}
