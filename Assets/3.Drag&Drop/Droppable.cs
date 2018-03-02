using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Droppable : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public delegate void DropHandler(GameObject obj = null);
    public event DropHandler OnSuccess;
    public event DropHandler OnFaile;

    public void OnDrop(PointerEventData eventData)
    {
        //드래그하고 있었던 아이콘의 Image 컴포넌트를 가져온다
        Image droppedImage = eventData.pointerDrag.GetComponent<Image>();
        if (DataManager.instance.NecessaryRating.Contains(droppedImage.sprite.name))
        {
            Debug.Log("정답");
            DataManager.instance.NecessaryRating.Remove(droppedImage.sprite.name);
            OnSuccess(eventData.pointerDrag);
        }
        else if (DataManager.instance.ConfusionRating.Contains(droppedImage.sprite.name))
        {
            Debug.Log("오답");
            OnFaile(eventData.pointerDrag);
        }
    }

    //마우스 커서가 영역에 들어왔을 때 호출된다.
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.dragging)
        {
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.dragging)
        {
        }
    }
}
