using System;
using UnityEngine;
using UnityEngine.UI;

public class LoginViewController : ViewController
{
	[SerializeField] private InputField	idInput;
	[SerializeField] private InputField pwInput;
	[SerializeField] private Toggle autoLogin;
	[SerializeField] private Toggle autoSetId;
	[SerializeField] private Button signInBtn;
	[SerializeField] private Button signUpBtn;
	[SerializeField] private GameObject loadingObj;

    private LogInInform logInInform;
    private LogInSettingsOption logInSettingsOption;

    private void Awake()
    {
        if (!LoadSettings())
            return;

        autoLogin.isOn = logInSettingsOption.isAutoLogIn;
        autoSetId.isOn = logInSettingsOption.isSetID;

        if (autoSetId.isOn)
        {
            string loadLoginData = PlayerPrefs.GetString("SavedLoginInform");
            LogInInform logInInform = JsonUtility.FromJson<LogInInform>(loadLoginData);
            idInput.text = logInInform.Id;
            pwInput.text = logInInform.Pw;
        }

        if (autoLogin.isOn)
        {
            //자동 로그인이 true면 
            //토큰 유무에 따라
            //자동 로그인 처리 로직 실행
        }
    }

    private void Start ()
    {
        logInInform = new LogInInform();

        signUpBtn.onClick.AddListener(delegate { SignUp(); });
        signInBtn.onClick.AddListener(delegate { LogIn(); });
        idInput.onEndEdit.AddListener(delegate { CheckIDInput(idInput); });
        pwInput.onEndEdit.AddListener(delegate { CheckPWInput(pwInput); });
    }

    private void CheckIDInput(InputField input)
    {
        if (input.text.Length != 0 && !(input.text.Contains("@") && input.text.Contains(".")))
        {
            AlertViewController.Show("", "메일 형식을 확인해주세요.");
            return;
        }

        logInInform.Id = input.text;
    }

    private void CheckPWInput(InputField input)
    {
        if (input.text.Length != 0 && input.text.Length < 6)
        {
            AlertViewController.Show("", "비밀번호는 최소 6자 이상입니다.");
            return;
        }

        logInInform.Pw = input.text;
    }
    
    //회원가입 버튼 클릭
    private void SignUp()
    {
        SignupViewController.Show();
    }

    //로그인 버튼 클릭
    private void LogIn()
    {
        if (idInput.text.Length == 0 || pwInput.text.Length == 0)
        {
            string message = "빈칸을 입력해주세요";
            AlertViewController.Show("", message);
            return;
        }

        loadingObj.SetActive(true);
        DataManager.instance.OnLoadingImage += onLoadingImage;
        DataManager.instance.SendSignIn(idInput.text, pwInput.text);
    }

    private void onLoadingImage(string success, Result result = null)
    {
        if(success.Equals("true"))
        {
            SaveLoginSettings();

            if(autoSetId.isOn)
            {
                OnAutoSetID();
            }
            string token = result.Token;

        }
        else if(success.Equals("false"))
        {
            if(result.Code.Equals("3"))
            {
                string message = "비밀번호가 일치하지 않습니다.";
                AlertViewController.Show("", message);
            }
            else if(result.Code.Equals("4"))
            {
                string message = "등록되지 않은 아이디입니다.";
                AlertViewController.Show("", message);
            }
        }
        else
        {
            string message = "네트워크가 불안정합니다. 잠시후 다시 시도해주세요.";
            AlertViewController.Show("", message);
        }

        DataManager.instance.OnLoadingImage -= onLoadingImage;
        loadingObj.SetActive(false);
    }

    //아이디 저장 
    private void OnAutoSetID()
    {
        if (autoSetId.isOn)
        {
            string jsonData = JsonUtility.ToJson(logInInform, true);
            PlayerPrefs.SetString("SavedLoginInform", jsonData);
        }
        else
        {
            if (!PlayerPrefs.HasKey("SavedLoginInform"))
                return;

            PlayerPrefs.DeleteKey("SavedLoginInform");
        }
    }

    //Toggle 설정 저장
    private void SaveLoginSettings()
    {
        logInSettingsOption = new LogInSettingsOption();
        logInSettingsOption.isAutoLogIn = autoLogin.isOn;
        logInSettingsOption.isSetID = autoSetId.isOn;

        string jsonData = JsonUtility.ToJson(logInSettingsOption, true);
        PlayerPrefs.SetString("SavedLoginInSettings", jsonData);
    }

    //저장된 데이터 유무 확인
    private bool LoadSettings()
    {
        if (!PlayerPrefs.HasKey("SavedLoginInSettings"))
            return false;
        else
        {
            //저장된 값을 파싱해 logInSettingsOption 객체에 담는다.
            string loadData = PlayerPrefs.GetString("SavedLoginInSettings");
            logInSettingsOption = JsonUtility.FromJson<LogInSettingsOption>(loadData);
            return true;
        }
    }
}
