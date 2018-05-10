using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public enum SceneState
{
    Title,
    Login,
    Menu,
    Game
}

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager instance = null;

    [SerializeField] private Texture2D fadeOutTexture;
    [SerializeField] private float fadeSpeed = 0.8f;

    private PlayableDirector pd;
    private int drawDepth = -1000;
    private float alpha = 1.0f;
    private int fadeDir = -1;
    private SceneState sceneState;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start ()
    {
        DontDestroyOnLoad(gameObject);
        sceneState = SceneState.Title;
        pd = FindObjectOfType<PlayableDirector>();
        StartCoroutine(FirstSceneCheck());
	}
	
	IEnumerator FirstSceneCheck()
    {
        float time = 0;
        while(SceneManager.GetActiveScene().buildIndex == 0)
        {
            time += Time.deltaTime;

#if UNITY_EDITOR
            if (pd.state == PlayState.Playing && Input.anyKeyDown)
                CheckAutoLogin();
#endif
#if !UNITY_EDITOR
            if (pd.state == PlayState.Playing && Input.touchCount > 0)
                CheckAutoLogin();
#endif
            if (10f < time)
            {
                pd.Pause();
                CheckAutoLogin();
            }
            yield return null;
        }
    }

    private void OnGUI()
    {
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
    }

    public float BeginFade(int direction)
    {
        fadeDir = direction;
        return (fadeSpeed);
    }

    private void OnLevelWasLoaded(int level)
    {
        BeginFade(-1);

        int index = SceneManager.GetActiveScene().buildIndex;

        if (index == 0)
            sceneState = SceneState.Title;
        else if (index == 1)
            sceneState = SceneState.Login;
        else if (index == 2)
        {
            sceneState = SceneState.Menu;
            DataManager.instance.SendListClinical();
            DataManager.instance.SendClearGameResult();
            //DataManager.instance.SendUserInfo();
            //DataManager.instance.SendUserRank();
        }
        else if (index == 3)
            sceneState = SceneState.Game;
    }

    public IEnumerator ChangeScene(int index)
    {
        BeginFade(1);
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(index);
    }

    private void CheckAutoLogin()
    {
        StopCoroutine(FirstSceneCheck());
// TODO : 여기 아래에 ! 붙여야함
        if (!PlayerPrefs.HasKey("SavedLoginInSettings"))
            StartCoroutine(ChangeScene(1));
        else
        {
            DataManager.instance.OnLoadingImage += onLoadingImage;

            string loadData = PlayerPrefs.GetString("SavedLoginInSettings");
            LogInSettingsOption logInSettingsOption = JsonUtility.FromJson<LogInSettingsOption>(loadData);
            if (logInSettingsOption.isAutoLogIn)
            {
                string loadLoginData = PlayerPrefs.GetString("SavedLoginInform");
                LogInInform logInInform = JsonUtility.FromJson<LogInInform>(loadLoginData);

                DataManager.instance.SendSignIn(logInInform.Id, logInInform.Pw);
            }
            else
                StartCoroutine(ChangeScene(1));
        }
    }

    private void onLoadingImage(string success, Result result = null)
    {
        DataManager.instance.OnLoadingImage -= onLoadingImage;

        if (success.Equals("true"))
        {
            DataManager.instance.Token = result.Token;
            StartCoroutine(ChangeScene(2));
        }
        else if (success.Equals("false"))
            StartCoroutine(ChangeScene(1));
    }
}