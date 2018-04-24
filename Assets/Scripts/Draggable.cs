using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Vector2 draggingOffset = new Vector2(0.0f, 40.0f); // 드래그 조작 중인 아이콘의 오프셋
    [SerializeField] private Font font;
    private GameObject draggingIamgeObject; //드래그 조작 중인 아이콘의 게임 오브젝트를 저장
    private GameObject draggingTextObject;
    private RectTransform canvasRectTransform; //캔버스의 Rect Transform을 저장
    private GameObject textObj;

    private void UpdateDraggingObjectPos(PointerEventData pointerEventData)
    {
        if (draggingIamgeObject != null)
        {
            //드래그 중인 아이콘의 스크린 좌표를 계산한다.
            Vector3 screenPos = pointerEventData.position + draggingOffset;

            //스크린 좌표를 월드 좌표로 변환
            Camera camera = pointerEventData.pressEventCamera;
            Vector3 newPos;
            if (DataManager.instance.RequestState == RequestState.randomItem)
            {
                if (RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRectTransform, screenPos, camera, out newPos))
                {
                    //드래그 중인 아이콘 위치를 월드 좌표로 설정
                    draggingIamgeObject.transform.position = newPos;
                    draggingIamgeObject.transform.rotation = canvasRectTransform.rotation;
                    draggingTextObject.transform.position = newPos;
                    draggingTextObject.transform.rotation = canvasRectTransform.rotation;
                }
            }
            else
            {
                if (RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRectTransform, screenPos, camera, out newPos))
                {
                    //드래그 중인 아이콘 위치를 월드 좌표로 설정
                    draggingIamgeObject.transform.position = newPos;
                    draggingIamgeObject.transform.rotation = canvasRectTransform.rotation;
                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        if (DataManager.instance.RequestState == RequestState.randomContent)
        {
            if (draggingIamgeObject != null)
            {
                Destroy(draggingIamgeObject);
            }
            //본래의 아이콘의 Image 컴포넌트를 가져온다
            Image sourceImage = GetComponent<Image>();
            sourceImage.color = new Color(1, 1, 1, 0);

            //드래그 조작 중인 아이콘의 게임 오브젝트를 생성한다.
            draggingIamgeObject = new GameObject("Dragging Object");
            textObj = GameObject.Find("descriptionText");

            //본래의 아이콘의 캔버스의 자식 요소로 종속시켜 가장 바깥면에 표시
            draggingIamgeObject.transform.SetParent(sourceImage.canvas.transform);
            draggingIamgeObject.transform.SetAsLastSibling(); //자식 오브젝트 중 마지막 위치로 보냄
            draggingIamgeObject.transform.localScale = Vector3.one;

            //Canvas Group 컴포넌트의 Block Raycasts  속성을 사용해 레이캐스트가 블록되지 않도록 한다.
            CanvasGroup canvasGroup1 = draggingIamgeObject.AddComponent<CanvasGroup>();
            canvasGroup1.blocksRaycasts = false;

            //드래그 조작 중인 아이콘의 게임 오브젝트에 Image 컴포넌트를 추가한다.
            Image draggingImage = draggingIamgeObject.AddComponent<Image>();
            Text draggingText = textObj.GetComponent<Text>();
            //본래의 아이콘과 같은 모습이 되게 설정한다.
            draggingImage.sprite = sourceImage.sprite;
            draggingImage.rectTransform.sizeDelta = sourceImage.rectTransform.sizeDelta;
            draggingImage.preserveAspect = true;

            if (DataManager.instance.RandomContentList.ContainsKey(Int32.Parse(gameObject.name)))
            {
                draggingText.text = DataManager.instance.RandomContentList[Int32.Parse(gameObject.name)];
            }
            draggingText.resizeTextForBestFit = true;

            //캔버스의 Rect Transform을 저장해둔다 
            canvasRectTransform = draggingImage.canvas.transform as RectTransform;
            //드래그를 조작 중인 아이콘의 위치를 갱신한다.
            UpdateDraggingObjectPos(pointerEventData);
        }
        else
        {
            if (draggingIamgeObject != null)
            {
                Destroy(draggingIamgeObject);
            }
            //본래의 아이콘의 Image 컴포넌트를 가져온다
            Image sourceImage = GetComponent<Image>();
            sourceImage.color = new Color(1, 1, 1, 0);

            //드래그 조작 중인 아이콘의 게임 오브젝트를 생성한다.
            draggingIamgeObject = new GameObject("Dragging Object");
            draggingTextObject = new GameObject("Dragging Text");

            //본래의 아이콘의 캔버스의 자식 요소로 종속시켜 가장 바깥면에 표시
            draggingIamgeObject.transform.SetParent(sourceImage.canvas.transform);
            draggingIamgeObject.transform.SetAsLastSibling(); //자식 오브젝트 중 마지막 위치로 보냄
            draggingIamgeObject.transform.localScale = Vector3.one;

            draggingTextObject.transform.SetParent(sourceImage.canvas.transform);
            draggingTextObject.transform.SetAsLastSibling(); //자식 오브젝트 중 마지막 위치로 보냄
            draggingTextObject.transform.localScale = Vector3.one;

            //Canvas Group 컴포넌트의 Block Raycasts  속성을 사용해 레이캐스트가 블록되지 않도록 한다.
            CanvasGroup canvasGroup1 = draggingIamgeObject.AddComponent<CanvasGroup>();
            canvasGroup1.blocksRaycasts = false;

            CanvasGroup canvasGroup2 = draggingTextObject.AddComponent<CanvasGroup>();
            canvasGroup2.blocksRaycasts = false;

            //드래그 조작 중인 아이콘의 게임 오브젝트에 Image 컴포넌트를 추가한다.
            Image draggingImage = draggingIamgeObject.AddComponent<Image>();
            Text draggingText = draggingTextObject.AddComponent<Text>();
            //본래의 아이콘과 같은 모습이 되게 설정한다.
            draggingImage.sprite = sourceImage.sprite;
            draggingImage.rectTransform.sizeDelta = sourceImage.rectTransform.sizeDelta;
            draggingImage.preserveAspect = true;

            draggingText.text = sourceImage.sprite.name;
            draggingText.font = font;
            draggingText.resizeTextForBestFit = true;

            //캔버스의 Rect Transform을 저장해둔다 
            canvasRectTransform = draggingImage.canvas.transform as RectTransform;
            //드래그를 조작 중인 아이콘의 위치를 갱신한다.
            UpdateDraggingObjectPos(pointerEventData);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateDraggingObjectPos(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(draggingIamgeObject);
        GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }

    public void Success()
    {
        if (DataManager.instance.RequestState == RequestState.randomItem)
        {
            Destroy(draggingIamgeObject);
            Destroy(draggingTextObject);
            Destroy(gameObject);
        }
        else
        {
            textObj.GetComponent<Text>().text = "";
            Destroy(draggingIamgeObject);
            Destroy(gameObject);
        }
    }

    public void Faile()
    {
        if (DataManager.instance.RequestState == RequestState.randomItem)
        {
            Destroy(draggingIamgeObject);
            Destroy(draggingTextObject);
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else
        {
            Destroy(draggingIamgeObject);
            textObj.GetComponent<Text>().text = "";
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
    }

    public void Nothing()
    {
        if (DataManager.instance.RequestState == RequestState.randomItem)
        {
            Destroy(draggingIamgeObject);
            Destroy(draggingTextObject);
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else
        {
            Destroy(draggingIamgeObject);
            textObj.GetComponent<Text>().text = "";
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
    }
}
