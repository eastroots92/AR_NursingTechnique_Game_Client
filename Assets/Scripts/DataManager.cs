using System;
using System.Collections;
using UnityEngine;

//{"signup":{"respon":{"success":"true"},"result":{"uid":"hansam2396","token":"hfkgwdyrcvpuomsneiqt"}}}

[Serializable]
public class Configuration
{
    [SerializeField] private bool success;
    [SerializeField] private string info;
    [SerializeField] private TokenData data;

    public string Info
    {
        get
        {
            return info;
        }
    }

    public bool Success
    {
        get
        {
            return success;
        }
    }

    public TokenData Data
    {
        get
        {
            return data;
        }
    }
}

[Serializable]
public class TokenData
{
    [SerializeField] private UserData user;
    [SerializeField] private string auth_token;

    public string Auth_token
    {
        get
        {
            return auth_token;
        }

        set
        {
            auth_token = value;
        }
    }

    public UserData User
    {
        get
        {
            return user;
        }
    }
}

[Serializable]
public class UserData
{
    [SerializeField] private string id;
    [SerializeField] private string email;
    [SerializeField] private string authentication_token;
    [SerializeField] private string name;
    [SerializeField] private string created_at;
    [SerializeField] private string updated_at;

    public string Name
    {
        get
        {
            return name;
        }
    }
}

public class DataManager : MonoBehaviour {
   
    public static DataManager instance = null;
    private Configuration config;

    private string postLogInUrl = "http://localhost:3000/api/v1/sessions";
    private string signUpUrl = "http://52.78.158.73/user/signup.json?";
    private string deleteLogOutUrl = "http://localhost:3000/api/v1/sessions?auth_token=";

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void PostLogIn(LogInInform loginInform)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", loginInform.id);
        form.AddField("password", loginInform.pw);

        WWW www = new WWW(postLogInUrl, form);

        StartCoroutine(WaitForRequest(www));
    }

    public void SignUp(string id, string pw, string name, string job)
    {
        string url = signUpUrl + "uid=" + id + "&password=" + pw + "&name=" + name + "&age=" + 27;
        WWW www = new WWW(url);

        StartCoroutine(WaitForRequest(www));
    }

    //    else if (Type == EType.PUT)
    //    {
    //        // PUT
    //        string url = "http://127.0.0.1:3000/method_put_test/user/id/8/ddddd";
    //        HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
    //        httpWebRequest.ContentType = "text/json";
    //        httpWebRequest.Method = "PUT";

    //        HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
    //        using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
    //        {
    //            string responseText = streamReader.ReadToEnd();
    //            //Now you have your response.
    //            //or false depending on information in the response
    //            Debug.Log(responseText);
    //        }
    //    }
    //    else if (Type == EType.DELETE)
    //    {
    //        // DELETE
    //        string url = "http://127.0.0.1:3000/method_del_test/user/id/8";
    //        HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
    //        httpWebRequest.ContentType = "text/json";
    //        httpWebRequest.Method = "DELETE";

    //        HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
    //        using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
    //        {
    //            string responseText = streamReader.ReadToEnd();
    //            //Now you have your response.
    //            //or false depending on information in the response
    //            Debug.Log(responseText);
    //        }
    //    }
    //}

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        if (www.error == null)
        {
            Debug.Log(www.text);
            ReceiveData(www.text);
        }
        else
        {
            Debug.Log("WWW error: " + www.error);   // something wrong!
            string message = "다시 시도해주세요.";
            AlertViewController.Show("", message);
        }
    }

    private void ReceiveData(string receiveData)
    {
        config = JsonUtility.FromJson<Configuration>(receiveData);

        if(config.Info.Equals("Logged in"))
        {
            //로그인 성공
        }
        else if(config.Info.Equals("Logged out"))
        {
            //로그아웃
        }
        else if (config.Info.Equals("login failed"))
        {
            //로그인 실패
        }
        else if (config.Info.Equals("Registered"))
        {
            //회원가입 성공
        }
        else
        {
            //회원 가입 실패
        }
    }
}
