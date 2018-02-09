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

public class GameSceneManager : MonoBehaviour {

   
    [SerializeField] private Texture2D fadeOutTexture;
    [SerializeField] private float fadeSpeed = 0.8f;

    private PlayableDirector pd;
    private int drawDepth = -1000;
    private float alpha = 1.0f;
    private int fadeDir = -1;

    private SceneState sceneState;

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
                StartCoroutine(ChangeScene(1));
#endif
#if !UNITY_EDITOR
            if (pd.state == PlayState.Playing && Input.touchCount > 0)
                StartCoroutine(ChangeScene(1));
#endif
            if (10f < time)
            {
                pd.Pause();
                StartCoroutine(ChangeScene(1));
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
            sceneState = SceneState.Menu;
        else if (index == 3)
            sceneState = SceneState.Game;
    }

    private IEnumerator ChangeScene(int index)
    {
        BeginFade(1);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(index);
    }
}
