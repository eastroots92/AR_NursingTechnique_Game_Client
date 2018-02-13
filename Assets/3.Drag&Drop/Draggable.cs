using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Vector2 draggingOffset = new Vector2(0.0f, 40.0f); // 드래그 조작 중인 아이콘의 오프셋
    private GameObject draggingObject; //드래그 조작 중인 아이콘의 게임 오브젝트를 저장
    private RectTransform canvasRectTransform; //캔버스의 Rect Transform을 저장

    private void UpdateDraggingObjectPos(PointerEventData pointerEventData)
    {
        if (draggingObject != null)
        {
            //드래그 중인 아이콘의 스크린 좌표를 계산한다.
            Vector3 screenPos = pointerEventData.position + draggingOffset;

            //스크린 좌표를 월드 좌표로 변환
            Camera camera = pointerEventData.pressEventCamera;
            Vector3 newPos;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRectTransform, screenPos, camera, out newPos))
            {
                //드래그 중인 아이콘 위치를 월드 좌표로 설정
                draggingObject.transform.position = newPos;
                draggingObject.transform.rotation = canvasRectTransform.rotation;
            }
        }
    }

    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        if(draggingObject != null)
        {
            Destroy(draggingObject);
        }

        //본래의 아이콘의 Image 컴포넌트를 가져온다
        Image sourceImage = GetComponent<Image>();

        //드래그 조작 중인 아이콘의 게임 오브젝트를 생성한다.
        draggingObject = new GameObject("Dragging Object");
        //본래의 아이콘의 캔버스의 자식 요소로 종속시켜 가장 바깥면에 표시
        draggingObject.transform.SetParent(sourceImage.canvas.transform);
        draggingObject.transform.SetAsLastSibling(); //자식 오브젝트 중 마지막 위치로 보냄
        draggingObject.transform.localScale = Vector3.one;

        //Canvas Group 컴포넌트의 Block Raycasts  속성을 사용해 레이캐스트가 블록되지 않도록 한다.
        CanvasGroup canvasGroup = draggingObject.AddComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;

        //드래그 조작 중인 아이콘의 게임 오브젝트에 Image 컴포넌트를 추가한다.
        Image draggingImage = draggingObject.AddComponent<Image>();
        //본래의 아이콘과 같은 모습이 되게 설정한다.
        draggingImage.sprite = sourceImage.sprite;
        draggingImage.rectTransform.sizeDelta = sourceImage.rectTransform.sizeDelta;
        draggingImage.color = sourceImage.color;
        draggingImage.material = sourceImage.material;

        //캔버스의 Rect Transform을 저장해둔다 
        canvasRectTransform = draggingImage.canvas.transform as RectTransform;
        //드래그를 조작 중인 아이콘의 위치를 갱신한다.
        UpdateDraggingObjectPos(pointerEventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateDraggingObjectPos(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(draggingObject);
    }
}
