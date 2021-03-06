﻿using System.Collections;
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
    [SerializeField] private GameObject inventoryBtn;
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
    [SerializeField] private GameObject gameClear;
    [SerializeField] private AudioClip[] audioClip;
    private int life = 5;
    private bool isStart = false;
    private Draggable draggable;
    private Droppable droppable;
    public GameState currentGame;
    private Image[] checkImg;
    private float fillAmount = 1;
    private Text draggingText;
    private AudioSource audioSource;

    private List<string> successList = new List<string>();

    private string clinicalTitle;
    private string game_type;
    private bool isTimeOver = false;
    private int game_num =  0;
    private bool isClear;
    private float time = 0;

    private float game_time_counter = 0;
    private int game_time = 0;
    //준비물 게임 
    private List<Image> baseItemImage = new List<Image>();
    private List<Image> necessaryItemImage = new List<Image>();
    private List<Image> confusionItemImage = new List<Image>();

    #region GameManagerProperty
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

    public string ClinicalTitle
    {
        get
        {
            return clinicalTitle;
        }

        set
        {
            clinicalTitle = value;
        }
    }

    public bool IsClear
    {
        get
        {
            return isClear;
        }

        set
        {
            isClear = value;
        }
    }

    public int Life
    {
        get
        {
            return life;
        }

        set
        {
            life = value;
        }
    }
    #endregion

    void Awake()
    {
        fillAmount = 1;
        fill.fillAmount = 1;
        IsClear = false;
        
        inventoryBtn.SetActive(false);
        gameInfoUI.SetActive(true);
        orderDroppable.SetActive(false);
        supplyDroppable.SetActive(false);
        supplyTextObj.SetActive(false);
        settingUI.SetActive(false);
        pauseUI.SetActive(false);
        gameClear.SetActive(false);

        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        ClinicalTitle = DataManager.instance.ClinicalTitle;
        game_num =  DataManager.instance.GameNumber;

        ShuffleList(originItemTransform);
        
        if (DataManager.instance.RequestState == RequestState.randomItem)
            currentGame = GameState.OrderGame;
        else
            currentGame = GameState.SupplyGame;
    }

    void Update()
    {
        game_time_counter += Time.deltaTime;
        game_time = (int)game_time_counter;
        if (!IsStart)
            return;

        if (fillAmount >= 0)
        {
            time += Time.deltaTime;
            fillAmount = 1 - time / 100;
            UpdateBar();

            string minutes = ((int)(fillAmount * 100) / 60).ToString();
            string seconds = ((int)(fillAmount * 100 - 60)).ToString();

        }else{
            if (!isTimeOver){
                IsClear = false;
                setGameRecord_timeOver();
            }
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
            if (DataManager.instance.NecessaryRating.Count == SuccessList.Count && Life > 0)
            {
                Debug.Log("성공");
                // TODO Life 매개변수
                IsClear = true;
                game_type = "아이템";
                isFinishGame(game_num,Life,game_type,game_time);
                audioSource.PlayOneShot(audioClip[2]);
            }
        }
        else
        {
            checkImg[droppable.markImageIndex - 1].sprite = checkImageSource[1];
            checkImg[droppable.markImageIndex - 1].gameObject.GetComponent<Button>().interactable = true;
            if (droppable.index == DataManager.instance.RandomContentList.Count)
            {
                Debug.Log("성공");
                IsClear = true;
                // TODO Life 매개변수
                game_type ="순서";
                // isFinishGame(1,life,game_type);
                audioSource.PlayOneShot(audioClip[2]);
            }
        }
        audioSource.PlayOneShot(audioClip[0]);
        draggable.Success();
    }

    public void onFaile(GameObject obj = null)
    {
        draggable = obj.GetComponent<Draggable>();
        Life--;
        draggable.Faile();
        SetLife();
        audioSource.PlayOneShot(audioClip[1]);
    }

    public void onNothing(GameObject obj)
    {
        draggable = obj.GetComponent<Draggable>();
        draggable.Nothing();
    }

    public void SetLife()
    {
        int imgIndex=0;

        if (Life == 4)
        {
            lifeImg[0].sprite = lifeImgSource[1];
            imgIndex = 0;
        }
        else if (Life == 3)
        {
            lifeImg[1].sprite = lifeImgSource[1];
            imgIndex = 1;
        }
        else if (Life == 2)
        {
            lifeImg[2].sprite = lifeImgSource[1];
            imgIndex = 2;
        }
        else if (Life == 1)
        {
            lifeImg[3].sprite = lifeImgSource[1];
            imgIndex = 3;
        }
        else if (Life == 0)
        {
            IsClear = false;
            lifeImg[4].sprite = lifeImgSource[1];
            audioSource.PlayOneShot(audioClip[3]);
            isFinishGame(game_num, Life, game_type, game_time);
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
        draggingText = GameObject.Find("descriptionText").GetComponent<Text>();

        droppable.OnSuccess += onSuccess;
        droppable.OnFaile += onFaile;
        droppable.OnNothing += onNothing;

        checkImg = new Image[DataManager.instance.OriginList.Count];

        for (int i = 0; i < DataManager.instance.OriginList.Count; i++)
        {
            Image img = originItemTransform[i].gameObject.AddComponent<Image>();
            originItemTransform[i].gameObject.AddComponent<Draggable>();
            originItemTransform[i].gameObject.name = (i + 1).ToString();
            //img.preserveAspect = true;
            img.sprite = contentImage;
        }

        for (int i = 0; i < DataManager.instance.OriginList.Count; i++)
        {
            GameObject obj = new GameObject((i+1).ToString());
            Image img = obj.AddComponent<Image>();
            Button btn = obj.AddComponent<Button>();
            btn.onClick.AddListener(delegate { OnClickMarker(obj.name); });
            btn.interactable = false;
            obj.transform.SetParent(GameObject.Find("CheckList").transform);
            img.sprite = checkImageSource[0];
            checkImg[i] = img;
        }
    }

    //준비물게임
    private void SetOrderGame()
    {
        inventoryBtn.SetActive(true);
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
        }
        for (int p = 0; p < DataManager.instance.ConfusionRating.Count; p++)
        {
            Image img = originItemTransform[p + necessCount].gameObject.AddComponent<Image>();
            originItemTransform[p + necessCount].gameObject.AddComponent<Draggable>();
            confusionItemImage.Add(img);
            confusionItemImage[p].preserveAspect = true;
            confusionItemImage[p].sprite = Resources.Load<Sprite>(DataManager.instance.ConfusionRating[p]);
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

    public void setGameRecord_timeOver(){
        isTimeOver= true;
        int life = 0;
        game_time = 0;
        // TODO Timeover

        if(currentGame == GameState.OrderGame){
            game_type = "아이템";
        }else{
            game_type= "순서";
        }

        isFinishGame(game_num,life, game_type, 0);
    }

    public void isFinishGame(int clinical_id, int life, string game_type, int game_time){
        Debug.Log("시작");
        bool isCurrent = true;
        if(clinical_id <= 0 || clinical_id >18){
            isCurrent = false;
            Debug.Log("아이디 걸러짐");
        }

        if(life < 0 || life >5 ){
            isCurrent = false;
            Debug.Log("라이프 걸러짐");
        }

        if( game_type != "순서" && game_type != "아이템"){
            isCurrent = false;
            Debug.Log("순서 아이템 걸러짐");
        }

        if (isCurrent){
            Debug.Log("d이건 도는건가?");
            DataManager.instance.SendGameRecord(clinical_id.ToString(),life.ToString(),game_type, game_time.ToString());
        }
        gameClear.SetActive(true);
    }

    public void OnClickMarker(string name)
    {
        int index = System.Int32.Parse(name);
        if (DataManager.instance.RandomContentList.ContainsKey(index))
        {
            draggingText.text = DataManager.instance.RandomContentList[index];
        }
    }
}
