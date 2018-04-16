using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class NavigationViewController : ViewController {
    private Stack<ViewController> stackedViews =
            new Stack<ViewController>();                // 뷰의 계층을 저장하는 스택
    private ViewController currentView = null;          // 현재 뷰를 저장

    [SerializeField] private GameObject navigationBar;  // 내비게이션 바
    [SerializeField] private Text titleLabel;           // 내비게이션 바의 타이틀을 표시하는 텍스트
    [SerializeField] private Button backButton;         // 내비게이션 바의 백 버튼
    [SerializeField] private Text backButtonLabel;      // 백 버튼의 텍스트
    [SerializeField] private GameObject loadingObj;

    [SerializeField] private ViewController lastView;
    [SerializeField] private ViewController newView;

    // 인스턴스를 로드할 때 호출된다
    void Awake()
    {
        // 백 버튼의 이벤트 리스너를 설정한다
        backButton.onClick.AddListener(OnPressBackButton);
        // 처음에는 백 버튼과 내비게이션 바를 표시하지 않는다
        backButton.gameObject.SetActive(false);
        navigationBar.SetActive(false);
    }

    // 백 버튼이 눌러졌을 때 호출되는 메서드
    public void OnPressBackButton()
    {
        // 이전 계층의 뷰로 되돌아간다
        Pop();
    }

    // 사용자의 인터랙션을 유효화/무효화하는 메서드
    private void EnableInteraction(bool isEnabled)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = isEnabled;
    }

    // 다음 계층의 뷰로 옮겨가는 처리를 수행하는 메서드
    public void Push(ViewController new_view)
    {
        newView = new_view;
        if (currentView == null)
        {
            // 첫 뷰는 애니메이션 없이 표시한다
            newView.gameObject.SetActive(true);
            currentView = newView;
            return;
        }

        // 애니메이션 도중에는 사용자의 인터랙션을 무효화한다
        EnableInteraction(false);

        lastView = currentView;
        leftHideView();

        if (newView.Title == "MY PAGE"){
            leftGetUserData();
            Invoke("leftShowView",1);
        }else{
            leftShowView();
        }

    }

    // 이전 계층의 뷰로 되돌아가는 처리를 수행하는 메서드
    public void Pop()
    {
        if (stackedViews.Count < 1)
        {
            // 이전 계층의 뷰가 없으므로 아무것도 없는 상태다
            return;
        }

        // 애니메이션 도중에는 사용자의 인터랙션을 무효화한다
        EnableInteraction(false);
        lastView = currentView;

        rightHideView();
        rightShowView();
        
    }

    public void leftHideView(){
        // 현재 표시된 뷰를 화면 왼쪽 밖으로 이동시킨다
        stackedViews.Push(lastView);
        Vector2 lastViewPos = lastView.CachedRectTransform.anchoredPosition;
        lastViewPos.x = -this.CachedRectTransform.rect.width;
        lastView.CachedRectTransform.MoveTo(
            lastViewPos, 0.3f, 0.0f, iTween.EaseType.easeOutSine, () => {
                // 이동이 끝나면 뷰를 비활성화한다
                lastView.gameObject.SetActive(false);
            }); 
    }

    public void leftGetUserData(){
        loadingObj.SetActive(true);
        DataManager.instance.SendUserInfo();
        DataManager.instance.SendUserRank();
    }

    public void leftShowView(){
        loadingObj.SetActive(false);
        // 새로운 뷰를 화면 왼쪽 밖으로부터 중앙으로 이동시킨다
        newView.gameObject.SetActive(true);
        Vector2 newViewPos = newView.CachedRectTransform.anchoredPosition;
        newView.CachedRectTransform.anchoredPosition =
            new Vector2(this.CachedRectTransform.rect.width, newViewPos.y);
        newViewPos.x = 0.0f;
        newView.CachedRectTransform.MoveTo(
            newViewPos, 0.3f, 0.0f, iTween.EaseType.easeOutSine, () => {
                // 이동이 끝나면 사용자의 인터랙션을 유효화한다
                EnableInteraction(true);
                backButton.gameObject.SetActive(true);
                navigationBar.SetActive(true);
            });

        // 새로운 뷰를 현재의 뷰로서 저장하고 내비게이션 바의 타이틀을 변경한다
        currentView = newView;
        titleLabel.text = newView.Title;

        // 백 버튼의 레이블을 변경한다
        backButtonLabel.text = lastView.Title;
        // 백 버튼을 유효화한다
        // backButton.gameObject.SetActive(true);
        // navigationBar.SetActive(true);
    }

    public void rightHideView(){
        // 현재 표시된 뷰를 화면 오른쪽 밖으로 이동시킨다
        Vector2 lastViewPos = lastView.CachedRectTransform.anchoredPosition;
        lastViewPos.x = this.CachedRectTransform.rect.width;
        lastView.CachedRectTransform.MoveTo(
            lastViewPos, 0.3f, 0.0f, iTween.EaseType.easeOutSine, () => {
                // 이동이 끝나면 뷰를 비활성화한다
                lastView.gameObject.SetActive(false);
            });
    }

    public void rightShowView(){
        // 이전 계층의 뷰를 스택으로부터 다시 가져오고 화면 왼쪽 밖으로부터 중앙으로 이동시킨다
        ViewController poppedView = stackedViews.Pop();
        poppedView.gameObject.SetActive(true);
        Vector2 poppedViewPos = poppedView.CachedRectTransform.anchoredPosition;
        poppedViewPos.x = 0.0f;
        poppedView.CachedRectTransform.MoveTo(
            poppedViewPos, 0.3f, 0.0f, iTween.EaseType.easeOutSine, () => {
                // 이동이 끝나면 사용자의 인터랙션을 유효화한다
                EnableInteraction(true);
            });

        // 스택에서 다시 가져온 뷰를 현재의 뷰로 저장하고 내비게이션 바의 타이틀을 변경한다
        currentView = poppedView;
        titleLabel.text = poppedView.Title;

        // 이전 계층의 뷰가 있을 때 백 버튼의 레이블을 변경해서 유효화한다
        if (stackedViews.Count >= 1)
        {
            backButtonLabel.text = stackedViews.Peek().Title;
            backButton.gameObject.SetActive(true);
            navigationBar.SetActive(true);
        }
        else
        {
            backButton.gameObject.SetActive(false);
            navigationBar.SetActive(false);
        }
    }

}

