using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        OrderGame, //준비물게임
        SupplyGame, //순서게임
    }

    [SerializeField] private Image fill;
    [SerializeField] private GameObject gamePlayBtn;
    [SerializeField] private GameObject gameInfoUI;
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private Text timerText;
    [SerializeField] private Image[] lifeImg;
    [SerializeField] private Sprite[] lifeImgSource;
    [SerializeField] private Sprite[] checkImageSource;
    [SerializeField] private Sprite contentImage;
    [SerializeField] private GameObject orderDroppable;
    [SerializeField] private GameObject supplyDroppable;
    [SerializeField] private GameObject supplyTextObj;
    [SerializeField] private GameObject settingUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private List<Transform> originItemTransform;

    private int life = 5;
    private bool isStart = false;
    private Draggable draggable;
    private Droppable droppable;
    public GameState currentGame;
    private Image[] checkImg;
    private float fillAmount = 1;

    private List<string> successList = new List<string>();
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

    public List<string> SuccessList
    {
        get
        {
            return successList;
        }

        set
        {
            successList = value;
        }
    }

    void Start()
    {
        fillAmount = 1;
        fill.fillAmount = 1;

        gameInfoUI.SetActive(true);
        orderDroppable.SetActive(false);
        supplyDroppable.SetActive(false);
        supplyTextObj.SetActive(false);
        settingUI.SetActive(false);
        pauseUI.SetActive(false);

        ShuffleList(originItemTransform);

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
        gameInfoUI.SetActive(false);
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
        
        if (currentGame == GameState.OrderGame)
        {
            if (DataManager.instance.NecessaryRating.Count == SuccessList.Count && life > 0)
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
        draggable.Success();
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

    private List<Transform> ShuffleList(List<Transform> inputList)
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

    //순서게임
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

    //준비물게임
    private void SetOrderGame()
    {
        orderDroppable.SetActive(true);
        droppable = FindObjectOfType<Droppable>();

        droppable.OnSuccess += onSuccess;
        droppable.OnFaile += onFaile;
        droppable.OnNothing += onNothing;

        int necessCount = DataManager.instance.NecessaryRating.Count;

        for (int o = 0; o < necessCount; o++)
        {
            Image img = originItemTransform[o].gameObject.AddComponent<Image>();
            originItemTransform[o].gameObject.AddComponent<Draggable>();
            necessaryItemImage.Add(img);
            necessaryItemImage[o].preserveAspect = true;
            necessaryItemImage[o].sprite = Resources.Load<Sprite>(DataManager.instance.NecessaryRating[o]);
            //originItemTransform[o].gameObject.AddComponent<FollowCamera>();
        }
        for (int p = 0; p < DataManager.instance.ConfusionRating.Count; p++)
        {
            Image img = originItemTransform[p + necessCount].gameObject.AddComponent<Image>();
            originItemTransform[p + necessCount].gameObject.AddComponent<Draggable>();
            confusionItemImage.Add(img);
            confusionItemImage[p].preserveAspect = true;
            confusionItemImage[p].sprite = Resources.Load<Sprite>(DataManager.instance.ConfusionRating[p]);
            //originItemTransform[p + necessCount].gameObject.AddComponent<FollowCamera>();
        }
    }

    public void OnClickSettingBtn()
    {
        IsStart = false;
        settingUI.SetActive(!settingUI.activeSelf);
    }

    public void OnClickPauseBtn()
    {
        IsStart = false;
        pauseUI.SetActive(!pauseUI.activeSelf);
    }

    public void OnClickInventory()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }
}
