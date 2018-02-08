using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlerViewTest : ViewController {

	public void OnPressTestButton()
    {
        string title = "종료";
        string message = "정말 종료하시겠습니까?";

        //알림 뷰를 표시
        AlertViewController.Show(title, message, new AlertViewOptions
        {
            //취소 버튼의 타이틀과 버튼을 눌렀을 때 실행되는 델리게이트를 설정한다.
            cancelButtonTitle = "아니요", cancelButtonDelegate = () =>
            {
                Debug.Log("Cancel");
            },

            //OK 버튼의 타이틀과 버튼을 눌렀을 때 실행되는 델리게이트를 설정한다.
            okButtonTitle = "네", okButtonDelegate = () =>
            {
                Debug.Log("OK");
            },
        });
    }
}
