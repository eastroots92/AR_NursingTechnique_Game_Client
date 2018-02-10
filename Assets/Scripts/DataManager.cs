using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

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
    private bool isLodingStart = false;
    private Configuration config;

    private string postLogInUrl = "http://localhost:3000/api/v1/sessions";
    private string postSignUpUrl = "http://localhost:3000/api/v1/registrations";
    private string deleteLogOutUrl = "http://localhost:3000/api/v1/sessions?auth_token=";

    public bool IsLodingStart
    {
        get
        {
            return isLodingStart;
        }

        set
        {
            isLodingStart = value;
        }
    }

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

    public void PostSignUp(SignUpInform signUpInform)
    {
        WWWForm form = new WWWForm();
        form.AddField("email", signUpInform.emailAddr);
        form.AddField("name", signUpInform.name);
        form.AddField("password", signUpInform.password);
        form.AddField("password_confirmation", signUpInform.rePassword);

        WWW www = new WWW(postSignUpUrl, form);

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
        isLodingStart = true;

        yield return www;

        if (www.error == null)
        {
            Debug.Log(www.text);
            ReceiveData(www.text);
        }
        else
        {
            Debug.Log("WWW error: " + www.error);   // something wrong!
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
