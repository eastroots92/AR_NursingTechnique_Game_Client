using UnityEngine;
using UnityEngine.UI;

public class LevelIcon : MonoBehaviour
{
    [SerializeField] private Text levelText;
    [SerializeField] private Image star;

    private int clinical_id;
    private int life;
    private int unLocked;
    private string game_type;

    #region property
    public Text LevelText
    {
        get
        {
            return levelText;
        }

        set
        {
            levelText = value;
        }
    }

    public Image Star
    {
        get
        {
            return star;
        }

        set
        {
            star = value;
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

    public int Clinical_id
    {
        get
        {
            return clinical_id;
        }

        set
        {
            clinical_id = value;
        }
    }

    public int UnLocked
    {
        get
        {
            return unLocked;
        }

        set
        {
            unLocked = value;
        }
    }

    public string Game_type
    {
        get
        {
            return game_type;
        }

        set
        {
            game_type = value;
        }
    }


    #endregion
}
