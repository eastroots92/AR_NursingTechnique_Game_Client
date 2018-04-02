using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        OrderGame,
        SupplyGame,
    }

    [SerializeField] private Image fill;
    [SerializeField] private float fillAmount = 1;
    [SerializeField] private GameObject gamePlayBtn;
    [SerializeField] private GameObject gameInfoUI;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private Text timerText;
    [SerializeField] private Image[] lifeImg;
    [SerializeField] private Sprite[] lifeImgSource;
    [SerializeField] private Sprite[] checkImageSource;
    [SerializeField] private Sprite contentImage;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject orderDroppable;
    [SerializeField] private GameObject supplyDroppable;
    [SerializeField] private GameObject supplyTextObj;
    [SerializeField] private GameObject settingUI;

    private int life = 5;
    private bool isStart = false;
    private Draggable draggable;
    private Droppable droppable;
    private GameState currentGame;
    private Image[] checkImg;

    //준비물 게임 
    private List<Image> baseItemImage = new List<Image>();
    private List<Image> necessaryItemImage = new List<Image>();
    private List<Image> confusionItemImage = new List<Image>();

    public bool IsStart
    {
        get
        {
            return isStart;
        }

        set
        {
            isStart = value;
        }
    }

    void Start()
    {
        gameUI.SetActive(false);
        gameInfoUI.SetActive(true);
        menuUI.SetActive(false);
        orderDroppable.SetActive(false);
        supplyDroppable.SetActive(false);
        supplyTextObj.SetActive(false);
        settingUI.SetActive(false);

        if (DataManager.instance.RequestState == RequestState.randomItem)
            currentGame = GameState.OrderGame;
        else
            currentGame = GameState.SupplyGame;

       
    }

    void Update()
    {
        if (!IsStart)
            return;

        if (fillAmount >= 0)
        {
            fillAmount = 1 - Time.time / 100;
            UpdateBar();

            string minutes = ((int)(fillAmount * 100) / 60).ToString();
            string seconds = ((int)(fillAmount * 100 - 60)).ToString();

            timerText.text = "00:0" + minutes + ":" + seconds;
        }
    }

    public void OnClickStartBtn()
    {
        gameUI.SetActive(true);
        gameInfoUI.SetActive(false);
        menuUI.SetActive(true);
        IsStart = true;
        GameInitialize();
    }

    private void GameInitialize()
    {
        if (currentGame == GameState.OrderGame)
            SetOrderGame();
        else
            SetSupplyGame();
    }

    private void UpdateBar()
    {
        if (fillAmount != fill.fillAmount)
            fill.fillAmount = fillAmount;
    }

    public void onSuccess(GameObject obj)
    {
        draggable = obj.GetComponent<Draggable>();
        draggable.Success();
        if (currentGame == GameState.OrderGame)
        {
            if (DataManager.instance.NecessaryRating.Count == 0 && life > 0)
            {
                Debug.Log("성공");
            }
        }
        else
        {
            checkImg[droppable.markImageIndex - 1].sprite = checkImageSource[1];
            if (droppable.index == DataManager.instance.RandomContentList.Count)
            {
                Debug.Log("성공");
            }
        }
    }

    public void onFaile(GameObject obj = null)
    {
        draggable = obj.GetComponent<Draggable>();
        life--;
        draggable.Faile();
        SetLife();
    }

    public void onNothing(GameObject obj)
    {
        draggable = obj.GetComponent<Draggable>();
        draggable.Nothing();
    }

    public void SetLife()
    {
        int imgIndex=0;

        if (life == 4)
        {
            lifeImg[0].sprite = lifeImgSource[1];
            imgIndex = 0;
        }
        else if (life == 3)
        {
            lifeImg[1].sprite = lifeImgSource[1];
            imgIndex = 1;
        }
        else if (life == 2)
        {
            lifeImg[2].sprite = lifeImgSource[1];
            imgIndex = 2;
        }
        else if (life == 1)
        {
            lifeImg[3].sprite = lifeImgSource[1];
            imgIndex = 3;
        }
        else if (life == 0)
        {
            lifeImg[4].sprite = lifeImgSource[1];
            StartCoroutine(GameSceneManager.instance.ChangeScene(2));
        }

        StartCoroutine(DelLifeUI(imgIndex));
    }

    private IEnumerator DelLifeUI(int index)
    {
        yield return new WaitForSeconds(0.5f);
        lifeImg[index].gameObject.SetActive(false);
    }

    private List<int> ShuffleList(List<int> inputList)
    {
        var count = inputList.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = inputList[i];
            inputList[i] = inputList[r];
            inputList[r] = tmp;
        }
        return inputList;
    }

    private void SetSupplyGame()
    {
        supplyDroppable.SetActive(true);
        droppable = FindObjectOfType<Droppable>();
        supplyTextObj.SetActive(true);

        droppable.OnSuccess += onSuccess;
        droppable.OnFaile += onFaile;
        droppable.OnNothing += onNothing;

        checkImg = new Image[DataManager.instance.OriginList.Count];
        for (int i = 0; i < DataManager.instance.OriginList.Count; i++)
        {
            GameObject obj = new GameObject("marker");
            Image img = obj.AddComponent<Image>();
            obj.transform.SetParent(GameObject.Find("Item").transform);
            img.sprite = checkImageSource[0];
            checkImg[i] = img;
        }

        foreach (int index in ShuffleList(DataManager.instance.OriginList))
        {
            var r = UnityEngine.Random.Range(0, 2);
            if (r == 0)
            {
                GameObject obj = new GameObject(index.ToString());
                Image img = obj.AddComponent<Image>();
                img.preserveAspect = true;
                obj.AddComponent<Draggable>();
                obj.transform.SetParent(GameObject.Find("Items").transform);
                img.sprite = contentImage;
            }
            else
            {
                GameObject obj = new GameObject(index.ToString());
                Image img = obj.AddComponent<Image>();
                img.preserveAspect = true;
                obj.AddComponent<Draggable>();
                obj.transform.SetParent(GameObject.Find("Itemss").transform);
                img.sprite = contentImage;
            }
        }
    }

    private void SetOrderGame()
    {
        orderDroppable.SetActive(true);
        droppable = FindObjectOfType<Droppable>();

        droppable.OnSuccess += onSuccess;
        droppable.OnFaile += onFaile;
        droppable.OnNothing += onNothing;

        int i = 0; int j = 0; int l = 0;

        for (int k = 0; k < DataManager.instance.BaseRating.Count; k++)
        {
            GameObject obj = new GameObject("item");
            Image img = obj.AddComponent<Image>();
            obj.transform.SetParent(GameObject.Find("Item").transform);
            baseItemImage.Add(img);
        }
        for (int o = 0; o < DataManager.instance.NecessaryRating.Count; o++)
        {
            GameObject obj = new GameObject("items");
            Image img = obj.AddComponent<Image>();
            obj.AddComponent<Draggable>();
            obj.transform.SetParent(GameObject.Find("Items").transform);
            necessaryItemImage.Add(img);
        }
        for (int p = 0; p < DataManager.instance.ConfusionRating.Count; p++)
        {
            GameObject obj = new GameObject("itemss");
            Image img = obj.AddComponent<Image>();
            obj.AddComponent<Draggable>();
            obj.transform.SetParent(GameObject.Find("Itemss").transform);
            confusionItemImage.Add(img);
        }

        foreach (string name in DataManager.instance.BaseRating)
        {
            baseItemImage[i].preserveAspect = true;
            baseItemImage[i].sprite = Resources.Load<Sprite>(name);
            i++;
        }
        foreach (string name in DataManager.instance.NecessaryRating)
        {
            necessaryItemImage[j].preserveAspect = true;
            necessaryItemImage[j].sprite = Resources.Load<Sprite>(name);
            j++;
        }
        foreach (string name in DataManager.instance.ConfusionRating)
        {
            confusionItemImage[l].preserveAspect = true;
            confusionItemImage[l].sprite = Resources.Load<Sprite>(name);
            l++;
        }
    }

    public void OnClickSettingBtn()
    {
        IsStart = false;
        settingUI.SetActive(!settingUI.activeSelf);
    }
}
