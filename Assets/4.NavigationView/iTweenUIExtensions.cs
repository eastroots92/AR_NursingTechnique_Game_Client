using UnityEngine;

// iTween이 발생시킨 이벤트를 제어하는 클래스
public class iTweenEventHandler : MonoBehaviour
{
    // 이동 애니메이션이 한 스텝 진행될 때마다 호출되는 콜백 메서드
    public System.Action<Vector2> OnUpdateMoveDelegate { get; set; }

    // 이동 애니메이션이 한 스텝 진행될 때마다 호출되는 메서드
    public void OnUpdateMove(Vector2 value)
    {
        if (OnUpdateMoveDelegate != null)
        {
            OnUpdateMoveDelegate.Invoke(value);
        }
    }

    // 애니메이션이 끝나면 호출되는 콜백 메서드
    public System.Action OnCompleteDelegate { get; set; }

    // 애니메이션이 끝나면 호출되는 메서드
    public void OnComplete()
    {
        if (OnCompleteDelegate != null)
        {
            OnCompleteDelegate.Invoke();
        }
    }
}

// 확장 메서드를 위한 정적 클래스
public static class iTweenUIExtensions
{
    // iTween이 발생시킨 이벤트를 제어하는 핸들러를 설정하는 메서드
    private static iTweenEventHandler SetUpEventHandler(GameObject targetObj)
    {
        iTweenEventHandler eventHandler = targetObj.GetComponent<iTweenEventHandler>();
        if (eventHandler == null)
        {
            eventHandler = targetObj.AddComponent<iTweenEventHandler>();
        }
        return eventHandler;
    }

    // RectTransform.MoveTo
    // Rect Transform을 현재 위치에서 지정한 위치로 이동시키는 애니메이션
    public static void MoveTo(this RectTransform target, Vector2 pos, float time, float delay, iTween.EaseType easeType, System.Action onCompleteDelegate = null)
    {
        // iTween이 발생시킨 이벤트를 제어하는 핸들러를 설정한다
        iTweenEventHandler eventHandler = SetUpEventHandler(target.gameObject);

        // 이동 애니메이션이 한 스텝 진행될 때마다 호출하는 콜백 메서드를 설정한다
        eventHandler.OnUpdateMoveDelegate = (Vector2 value) => {
            // Rect Transform의 위치를 갱신한다
            target.anchoredPosition = value;
        };

        // 애니메이션이 끝났을 때 호출되는 콜백 메서드를 설정한다
        eventHandler.OnCompleteDelegate = onCompleteDelegate;

        // iTween의 ValueTo 메서드를 호출해 애니메이션을 시작한다
        iTween.ValueTo(target.gameObject, iTween.Hash(
            "from", target.anchoredPosition,
            "to", pos,
            "time", time,
            "delay", delay,
            "easetype", easeType,
            "onupdate", "OnUpdateMove",
            "onupdatetarget", eventHandler.gameObject,
            "oncomplete", "OnComplete",
            "oncompletetarget", eventHandler.gameObject
        ));
    }
}
