using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectController : ViewController
{
    [System.Serializable]
    public class LevelInfo
    {
        public int unlocked;
        public bool isInteractable;
        public string game_type;
        public int clinical_id;
        public int life;

        public LevelInfo(int clinical_id, string game_type)
        {
            this.clinical_id = clinical_id;
            this.game_type = game_type;

            foreach(Level level in DataManager.instance.Levels)
            {
                if(level.Clinical_id == clinical_id && level.Game_type == game_type)
                {
                    unlocked = 1;
                    life = level.Life;
                    isInteractable = true;
                }
                else
                {
                    unlocked = 0;
                    life = 0;
                    isInteractable = false;
                }
            }
        }
        public Button.ButtonClickedEvent onClickEvent;
    }

    [SerializeField] private NavigationViewController navigationView;
    [SerializeField] private Transform scroll;
    [SerializeField] private GameObject levelOrigin;
    [SerializeField] private Sprite[] starImage;
    [SerializeField] private Sprite[] levelImage;

    public List<GameObject> levelObj = new List<GameObject>();
    public List<LevelInfo> levelList = new List<LevelInfo>();
    int unlockedCount = 0;

    //뷰의 타이틀을 반환
    public override string Title
    {
        get
        {
            return "SELECT LEVEL";
        }
    }

    private void Awake()
    {
        int index = 0;
        
        SetLevelIndex();

        foreach (LevelInfo levelInfo in levelList)
        {
            index++;
            GameObject obj = Instantiate(levelOrigin) as GameObject;
            levelObj.Add(obj);
            LevelIcon levelIcon = obj.GetComponent<LevelIcon>();
            levelIcon.LevelText.text = index.ToString();

            levelIcon.Clinical_id = levelInfo.clinical_id;
            levelIcon.Life = levelInfo.life;
            levelIcon.UnLocked = levelInfo.unlocked;
            levelIcon.Game_type = levelInfo.game_type;

            if (levelInfo.unlocked == 1)
            {
                unlockedCount++;

                obj.GetComponent<Image>().sprite = levelImage[0];
                obj.GetComponent<Button>().interactable = true;

                if (levelInfo.life > 0 && levelInfo.life <= 2)
                    levelIcon.Star.sprite = starImage[0];
                else if(levelInfo.life <= 4)
                    levelIcon.Star.sprite = starImage[1];
                else if(levelInfo.life <= 5)
                    levelIcon.Star.sprite = starImage[2];

                levelIcon.Star.gameObject.SetActive(true);
                levelIcon.LevelText.gameObject.SetActive(true);
            }
            else
            {
                obj.GetComponent<Image>().sprite = levelImage[1];
                obj.GetComponent<Button>().interactable = false;
            }

            obj.GetComponent<Button>().onClick.AddListener(() => LoadLevel(levelIcon.Clinical_id, levelIcon.Game_type));
            obj.transform.SetParent(scroll);
        }

        levelObj[unlockedCount].GetComponent<Image>().sprite = levelImage[0];
        levelObj[unlockedCount].GetComponent<Button>().interactable = true;
        LevelIcon icon = levelObj[unlockedCount].GetComponent<LevelIcon>();
        icon.LevelText.gameObject.SetActive(true);
    }

    private void SetLevelIndex()
    {
        foreach (int i in DataManager.instance.LowDifficulty)
        {
            levelList.Add(new LevelInfo(i, "준비"));
            levelList.Add(new LevelInfo(i, "순서"));
        }
        foreach (int i in DataManager.instance.MiddleDifficulty)
        {
            levelList.Add(new LevelInfo(i, "준비"));
            levelList.Add(new LevelInfo(i, "순서"));
        }
        foreach (int i in DataManager.instance.HighDifficulty)
        {
            levelList.Add(new LevelInfo(i, "준비"));
            levelList.Add(new LevelInfo(i, "순서"));
        }
    }

    private void LoadLevel(int id, string game_type)
    {
        if(game_type.Equals("준비"))
        {
            DataManager.instance.SendRandomItem(id);
        }
        else if (game_type.Equals("순서"))
        {
            DataManager.instance.SendRandomContent(id);
        }

        GameSceneManager.instance.ChangeScene(4);
    }
}
