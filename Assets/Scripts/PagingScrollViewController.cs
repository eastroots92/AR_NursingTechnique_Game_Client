using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ScrollRect))]
public class PagingScrollViewController : ViewController, IBeginDragHandler, IEndDragHandler {

    [SerializeField] private int PageCount;
    [SerializeField] private PageControl pageControl;
    private ScrollRect cachedScrollRect;
    
    public ScrollRect CachedScrollRect
    {
        get
        {
            if(cachedScrollRect == null)
            {
                cachedScrollRect = GetComponent<ScrollRect>();
            }
            return cachedScrollRect;
        }
    }

    private bool isAnimating = false; //애니메이션 재생 중임을 나타내는 플래그
    private Vector2 destPosition; //최종적인 스크롤 위치
    private Vector2 initialPosition; //자동 스크롤을 시작할 때의 스크롤 위치
    private AnimationCurve animationCurve; //자동 스크롤 애니메이션 커브
    private int prevPageIndex = 0; //이전 페이지의 인덱스 저장
    private Rect currentViewRect;//스크롤 뷰 사각형 모양을 저장

    //드래그 시작
    public void OnBeginDrag(PointerEventData eventData)
    {
        //애니메이션이 재생 중임을 나타내는 플래그 초기화
        isAnimating = false;
    }

    //드래그 조작이 끝날 때 호출되는 메서드
    public void OnEndDrag(PointerEventData eventData)
    {
        GridLayoutGroup grid = CachedScrollRect.content.GetComponent<GridLayoutGroup>();

        //스크롤 뷰가 현재 움직이고 있는 것을 멈춘다. 
        CachedScrollRect.StopMovement();

        //GridLayoutGroup의 cellSize와 Spacing을 토대로 한 페이지의 너비를 계산
        float pageWidth = -(grid.cellSize.x + grid.spacing.x);

        //현재 스크롤 위치를 토대로 페이지의 인덱스를 계산
        int pageIndex = Mathf.RoundToInt((CachedScrollRect.content.anchoredPosition.x) / pageWidth);

        if(pageIndex == prevPageIndex && Mathf.Abs(eventData.delta.x) >= 4)
        {
            //일정 속도 이상으로 드래그를 조작하고 있을 때는 그 방향으로 한 페이지를 넘긴다
            CachedScrollRect.content.anchoredPosition += new Vector2(eventData.delta.x, 0.0f);
            pageIndex += (int)Mathf.Sign(-eventData.delta.x);
        }

        //첫 페이지이거나 마지막 페이지일 때 그 이상 스크롤 하지 않는다. 
        if(pageIndex < 0)
        {
            pageIndex = 0;
        }
        else if(pageIndex > grid.transform.childCount - 1)
        {
            pageIndex = grid.transform.childCount - 1;
        }

        prevPageIndex = pageIndex;

        //최종적인 스크롤 위치를 계산한다.
        float destX = pageIndex * pageWidth;
        destPosition = new Vector2(destX, CachedScrollRect.content.anchoredPosition.y);

        //시작할 때의 스크롤 위치를 젖장해둔다
        initialPosition = CachedScrollRect.content.anchoredPosition;

        //0.3초 동안 부드럽게 변화하는 애니메이션 커브를 작성한다.
        Keyframe keyFrame1 = new Keyframe(Time.time, 0.0f, 0.0f, 1.0f);
        Keyframe keyFrame2 = new Keyframe(Time.time + 0.3f, 1.0f, 0.0f, 0.0f);
        animationCurve = new AnimationCurve(keyFrame1, keyFrame2);

        //애니메이션이 재생 중임을 나타내는 플래그 설정
        isAnimating = true;

        pageControl.SetCurrentPage(pageIndex);
    }

    private void LateUpdate()
    {
        if (isAnimating)
        {
            if(Time.time >= animationCurve.keys[animationCurve.length - 1].time)
            {
                //애니메이션 커브의 마지막 키 프레임을 지나면 애니메이션을 끝낸다.
                CachedScrollRect.content.anchoredPosition = destPosition;
                isAnimating = false;
                return;
            }

            //애니메이션 커브로부터 현재 스크롤 위치를 계산해서 스크롤 뷰를 이동시킨다. 
            Vector2 newPosition = initialPosition + (destPosition - initialPosition) * animationCurve.Evaluate(Time.time);
            CachedScrollRect.content.anchoredPosition = newPosition;
        }
    }

    private void Start()
    {
        //UpdateView();

        pageControl.SetNumberOfPages(PageCount);
        pageControl.SetCurrentPage(0);
    }

    //Scroll Content의 padding을 갱신하는 메서드 
    private void UpdateView()
    {
        currentViewRect = CachedRectTransform.rect;

        GridLayoutGroup grid = CachedScrollRect.content.GetComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(Screen.width, CachedRectTransform.rect.height);
    }
}
