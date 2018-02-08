using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertViewOptions
{
    public string cancelButtonTitle;
    public System.Action cancelButtonDelegate;  //cancel을 눌렀을 때 실행되는 델리게이트
    public string okButtonTitle;
    public System.Action okButtonDelegate;      //ok 눌렀을 때 실행되는 델리게이트
}

public class AlertViewController : ViewController
{
    [SerializeField] private Text titleLabel;
    [SerializeField] private Text messageLabel;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Text cancelButtonLabel;
    [SerializeField] private Button okButton;
    [SerializeField] private Text okButtonLabel;

    private static GameObject prefab = null;        //알림 뷰의 프리팹 저장
    private System.Action cancelButtonDelegate;     //취소 버튼을 눌렀을 때 실행되는 델리게이트 저장
    private System.Action okButtonDelegate;         //OK 버튼을 눌렀을 때 실행되는 델리게이트 저장

    //알림 뷰를 표시하는 static 메서드
    public static AlertViewController Show(string title, string message, AlertViewOptions options = null)
    {
        if(prefab == null)
        {
            prefab = Resources.Load("AlertView") as GameObject;
        }

        GameObject obj = Instantiate(prefab) as GameObject;
        AlertViewController alertView = obj.GetComponent<AlertViewController>();
        alertView.UpdateContent(title, message, options);

        return alertView;
    }

    //알림 뷰의 내용을 갱신하는 메서드
    public void UpdateContent(string title, string message, AlertViewOptions options = null)
    {
        titleLabel.text = title;
        messageLabel.text = message;

        if(options != null)
        {
            //표시 옵션이 지정돼 있을 때 옵션의 내용에 맞춰 버튼을 표시하거나 표시하지 않는다.
            cancelButton.transform.parent.gameObject.SetActive(options.cancelButtonTitle != null || options.okButtonTitle != null);
            cancelButton.gameObject.SetActive(options.cancelButtonTitle != null);
            cancelButtonLabel.text = options.cancelButtonTitle ?? "";
            cancelButtonDelegate = options.cancelButtonDelegate;

            okButton.gameObject.SetActive(options.okButtonTitle != null);
            okButtonLabel.text = options.okButtonTitle ?? "";
            okButtonDelegate = options.okButtonDelegate;
        }
        else
        {
            cancelButton.gameObject.SetActive(false);
            okButton.gameObject.SetActive(true);
            okButtonLabel.text = "OK";
        }
    }
	
    //알림 뷰를 닫는 메서드
    public void Dismiss()
    {
        Destroy(gameObject);
    }

    //취소 버튼을 눌렀을 때 호출되는 메서드
    public void OnPressCancelButton()
    {
        if(cancelButtonDelegate != null)
        {
            cancelButtonDelegate.Invoke();
        }
        Dismiss();
    }

    //Ok 버튼을 눌렀을 때 호출되는 메서드
    public void OnPressOKButton()
    {
        if (okButtonDelegate != null)
        {
            okButtonDelegate.Invoke();
        }
        Dismiss();
    }
}
