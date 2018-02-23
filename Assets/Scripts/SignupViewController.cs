using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignupViewController : ViewController
{
    [SerializeField] private InputField idInput;
    [SerializeField] private InputField nameInput;
    [SerializeField] private InputField pwInput;
    [SerializeField] private InputField rePwInput;
    [SerializeField] private ToggleGroup jobGroup;
    [SerializeField] private Button sendBtn;
    [SerializeField] private Button closeBtn;
    [SerializeField] private GameObject loadingObj;

    private string errTitle = "";

    private static GameObject prefab = null;

    public static SignupViewController Show()
    {
        if (prefab == null)
        {
            prefab = Resources.Load("SignUpView") as GameObject;
        }

        GameObject obj = Instantiate(prefab) as GameObject;
        SignupViewController signUpView = obj.GetComponent<SignupViewController>();
        signUpView.SetSignUpView();

        return signUpView;
    }

    public void SetSignUpView()
    {
        loadingObj.SetActive(false);

        nameInput.onEndEdit.AddListener(delegate { CheckNameInput(nameInput); });
        pwInput.onEndEdit.AddListener(delegate { CheckPWInput(pwInput); });
        rePwInput.onEndEdit.AddListener(delegate { CheckRePWInput(rePwInput); });
        sendBtn.onClick.AddListener(delegate { OnPressSend(); });
        closeBtn.onClick.AddListener(delegate { OnPressClose(); });
    }

    private void CheckNameInput(InputField input)
    {
        if (input.text.Length != 0 && (input.text.Length > 10 || input.text.Length < 2))
        {
            string errMessage = "올바른 이름을 입력하세요.";
            AlertViewController.Show(errTitle, errMessage);
            return;
        }
    }

    private void CheckPWInput(InputField input)
    {
        if (input.text.Length != 0 && input.text.Length < 6)
        {
            string errMessage = "비밀번호는 최소 6자 이상입니다.";
            AlertViewController.Show(errTitle, errMessage);
            return;
        }
    }

    private void CheckRePWInput(InputField input)
    {
        if (input.text.Length != 0 && !input.text.Equals(pwInput.text))
        {
            string errMessage = "비밀번호가 일치하지 않습니다.";
            AlertViewController.Show(errTitle, errMessage);
            return;
        }
    }

    private void OnPressSend()
    {
        if (idInput.text.Length == 0 || pwInput.text.Length == 0 || rePwInput.text.Length == 0 || nameInput.text.Length == 0 || !jobGroup.AnyTogglesOn())
        {
            string message = "빈칸을 입력해주세요";
            AlertViewController.Show(errTitle, message);
            return;
        }

        IEnumerable<Toggle> activeToggles = jobGroup.ActiveToggles();
        string job = "";

        foreach (Toggle tg in activeToggles)
        {
            job = tg.name;
        }

        loadingObj.SetActive(true);

        DataManager.instance.OnLoadingImage += onLoadingImage;
        DataManager.instance.SendSignUp(idInput.text, pwInput.text, nameInput.text, job);
    }

    private void OnPressClose()
    {
        Destroy(gameObject);
    }

    private void onLoadingImage(string success)
    {
        loadingObj.SetActive(false);
        if (success.Equals("true"))
        {
            string message = "회원가입을 완료했습니다";
            AlertViewController.Show(errTitle, message);
            OnPressClose();
        }
        else
        {
            string message = "아이디가 중복됐습니다. 다시 시도하세요.";
            AlertViewController.Show(errTitle, message);
        }
    }
}
              