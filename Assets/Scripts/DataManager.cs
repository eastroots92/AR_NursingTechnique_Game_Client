using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// test ID : admin, pw : 123456, token : kdwznmgcshoabijyqevr
/// </summary>
public enum RequestState
{
    None,
    listClinical,
    randomContent,
    randomItem,
    gameRecord,
    userInfo,
}

public class DataManager : MonoBehaviour
{
    public delegate void LoadingImageHandler(string success, Result result = null);
    public event LoadingImageHandler OnLoadingImage;

    public static DataManager instance = null;

    private RequestState requestState;
    private int requestCount=0;
    public bool isPlay = true;

	private string token = "kdwznmgcshoabijyqevr";
    
	private List<int> lowDifficulty;
    private List<int> middleDifficulty;
    private List<int> highDifficulty;

    private List<string> baseRating;       //항시
    private List<string> necessaryRating;  //필수
    private List<string> confusionRating;  //헷갈

    private Dictionary<int, string> randomContentList;
    private List<int> originList;

    private string userName;
    private int score;
    private int success;

    #region ClinicalURL Data & Property
    [SerializeField] private string signUpUrl = "http://52.78.120.239/user/signup.json?";
    [SerializeField] private string signInUrl = "http://52.78.120.239/user/signin.json?";
    [SerializeField] private string signOutUrl = "http://52.78.120.239/user/signout.json?token=";
    //간호술기 목차 보기 : "info" : {"listSize" : "목차 총 개수"}, "list" {"id": "순서","title":"제목","difficulty":"난이도:"}
    [SerializeField] private string listClinicalUrl = "http://52.78.120.239/game/list_clinical.json?token=";
    //"info" : {"id" : "해당 간호술기 번호(호출 번호)" ,"title" : "간호술기 제목" , "difficulty" : "난이도" } , " list" : { "index" : "순서" , "content" : "내용"}}}
    [SerializeField] private string randomContentUrl = "http://52.78.120.239/game/random_content.json?token=";
    //"info" : {"id" : "해당 간호술기 번호(호출 번호)" ,"title" : "간호술기 제목" , "difficulty" : "난이도" } , " list" : { "name" : "아이템 명" , "rating" : "중요도"}}}
    //rating : 중요도, 필수-필수로 필요한 아이템, 항시-항시 준비되어 있는 물품, 혼동-헷갈리게 하는 물품
    [SerializeField] private string randomItemUrl = "http://52.78.120.239/game/random_item.json?token=";
    [SerializeField] private string gameRecordUrl = "http://52.78.120.239/game/game_record.json?";
    [SerializeField] private string userInfoUrl = "http://52.78.120.239/user/user_info.json?token=";

    public string Token { set { token = value; } }

    public List<string> BaseRating
    {
        get
        {
            return baseRating;
        }

        set
        {
            baseRating = value;
        }
    }

    public List<string> NecessaryRating
    {
        get
        {
            return necessaryRating;
        }

        set
        {
            necessaryRating = value;
        }
    }

    public List<string> ConfusionRating
    {
        get
        {
            return confusionRating;
        }

        set
        {
            confusionRating = value;
        }
    }

    public Dictionary<int, string> RandomContentList
    {
        get
        {
            return randomContentList;
        }

        set
        {
            randomContentList = value;
        }
    }

    public List<int> OriginList
    {
        get
        {
            return originList;
        }

        set
        {
            originList = value;
        }
    }

    public RequestState RequestState
    {
        get
        {
            return requestState;
        }

        set
        {
            requestState = value;
        }
    }

    public string UserName
    {
        get
        {
            return userName;
        }

        set
        {
            userName = value;
        }
    }

    public int Success
    {
        get
        {
            return success;
        }

        set
        {
            success = value;
        }
    }

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
        }
    }
    #endregion

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        RequestState = RequestState.None;
    }
    
    public void SendSignUp(string id, string pw, string name, string job)
    {
        string url = signUpUrl + "uid=" + id + "&password=" + pw + "&name=" + name + "&job=" + job;
        WWW www = new WWW(url);

        StartCoroutine(WaitForRequest(www));
    }

    public void SendSignIn(string id, string pw)
    {
        string url = signInUrl + "uid=" + id + "&password=" + pw;
        WWW www = new WWW(url);

        StartCoroutine(WaitForRequest(www));
    }

    public void SendListClinical()
    {
        RequestState = RequestState.listClinical;

        string url = listClinicalUrl + token;
        WWW www = new WWW(url);

        StartCoroutine(WaitForRequest(www));
    }

    public void SendRandomItem(int num)
    {
        RequestState = RequestState.randomItem;

        string url = randomItemUrl + token + "&number=" + num;
        WWW www = new WWW(url);

        StartCoroutine(WaitForRequest(www));
    }

    public void SendRandomContent(int num)
    {
        RequestState = RequestState.randomContent;

        string url = randomContentUrl + token + "&number=" + num;
        WWW www = new WWW(url);

        StartCoroutine(WaitForRequest(www));
    }

    public void SendUserInfo()
    {
        RequestState = RequestState.userInfo;

        string url = userInfoUrl + token;
        WWW www = new WWW(url);

        StartCoroutine(WaitForRequest(www));
    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        if (www.error == null)
        {
            Debug.Log(www.text);
            StartCoroutine(ReceiveData(www.text));
        }
        else
        {
            Debug.Log("WWW error: " + www.error);   // something wrong!
            if (GameObject.Find("LodingLayout").activeSelf)
            {
                if (OnLoadingImage != null)
                    OnLoadingImage(www.error);
            }
            else
                RequestAgain();
        }
    }

    private IEnumerator ReceiveData(string receiveData)
    {
        DataConfiguration config = JsonUtility.FromJson<DataConfiguration>(receiveData);

        if (receiveData.Contains("signup"))
        {
            yield return new WaitForSeconds(1f);
            if (OnLoadingImage != null)
                OnLoadingImage(config.Signup.Respon.Success);
        }
        else if (receiveData.Contains("signin"))
        {
            yield return new WaitForSeconds(1f);
            if (OnLoadingImage != null)
                OnLoadingImage(config.Signin.Respon.Success, config.Signin.Result);
        }
        else if (receiveData.Contains("list_clinical"))
        {
            if (config.List_clinical.Respon.Success.Equals("false"))
                StartCoroutine(LostToken());
            else
            {
                highDifficulty = new List<int>();
                middleDifficulty = new List<int>();
                lowDifficulty = new List<int>();

                foreach (ListClinical_listDetail list in config.List_clinical.List)
                {
                    if (list.Difficulty.Equals("상"))
                        highDifficulty.Add(list.Id);
                    else if (list.Difficulty.Equals("중"))
                        middleDifficulty.Add(list.Id);
                    else if (list.Difficulty.Equals("하"))
                        lowDifficulty.Add(list.Id);
                }
            }
        }
        else if (receiveData.Contains("random_item"))
        {
            if (config.Random_item.Respon.Success.Equals("false"))
                StartCoroutine(LostToken());
            else
            {
                NecessaryRating = new List<string>();
                BaseRating = new List<string>();
                ConfusionRating = new List<string>();

                foreach (RandomItemListDetail list in config.Random_item.List)
                {
                    if (list.Rating.Equals("필수"))
                        NecessaryRating.Add(list.Name);
                    else if (list.Rating.Equals("항시"))
                        BaseRating.Add(list.Name);
                    else if (list.Rating.Equals("혼동"))
                        ConfusionRating.Add(list.Name);
                }
            }
        }
        else if (receiveData.Contains("random_content"))
        {
            if (config.Random_content.Respon.Success.Equals("false"))
                StartCoroutine(LostToken());
            else
            {
                RandomContentList = new Dictionary<int, string>();
                OriginList = new List<int>();

                foreach(RandomContentListDetail list in config.Random_content.List)
                {
                    RandomContentList.Add(list.Index, list.Content);
                    OriginList.Add(list.Index);
                }
            }
        }
        else if (receiveData.Contains("user_info"))
        {
            UserName = config.User_info.Info.Name;
            Score = config.User_info.List.Score;
        }
    }

    private void RequestAgain()
    {
        requestCount++;

        if (requestCount > 15)
        {
            string message = "서버가 불안정합니다. 잠시후 다시 시도하세요.";
            AlertViewController.Show("", message);
            return;
        }

        if (RequestState == RequestState.listClinical)
            SendListClinical();
    }

    private IEnumerator LostToken()
    {
        string message = "계정 정보가 없습니다. 다시 로그인 해주세요.";
        AlertViewController.Show("", message);
        yield return new WaitForSeconds(1f);
        token = "";
        GameSceneManager.instance.ChangeScene(1);
    }
}

