using UnityEngine;
using System;

[Serializable]
public struct DataConfiguration
{
    [SerializeField] private SignUp signup;
    [SerializeField] private SignIn signin;
    [SerializeField] private SignOut signout;
    [SerializeField] private ListClinical list_clinical;
    [SerializeField] private RandomItem random_item;
    [SerializeField] private RandomContent random_content;
    [SerializeField] private GameRecord game_record;

    public SignUp Signup
    {
        get
        {
            return signup;
        }

        set
        {
            signup = value;
        }
    }

    public SignIn Signin
    {
        get
        {
            return signin;
        }

        set
        {
            signin = value;
        }
    }

    public SignOut Signout
    {
        get
        {
            return signout;
        }

        set
        {
            signout = value;
        }
    }

    public ListClinical List_clinical
    {
        get
        {
            return list_clinical;
        }

        set
        {
            list_clinical = value;
        }
    }

    public RandomItem Random_item
    {
        get
        {
            return random_item;
        }

        set
        {
            random_item = value;
        }
    }

    public RandomContent Random_content
    {
        get
        {
            return random_content;
        }

        set
        {
            random_content = value;
        }
    }

    public GameRecord Game_record
    {
        get
        {
            return game_record;
        }

        set
        {
            game_record = value;
        }
    }
}

//
// 1.1 회원가입
//
[Serializable]
public struct SignUp
{
    [SerializeField] private Respon respon;
    [SerializeField] private Result result;

    public Respon Respon
    {
        get
        {
            return respon;
        }

        set
        {
            respon = value;
        }
    }

    public Result Result
    {
        get
        {
            return result;
        }

        set
        {
            result = value;
        }
    }
}

//
// 1.2 로그인
//
[Serializable]
public struct SignIn
{
    [SerializeField] private Respon respon;
    [SerializeField] private Result result;

    public Respon Respon
    {
        get
        {
            return respon;
        }

        set
        {
            respon = value;
        }
    }

    public Result Result
    {
        get
        {
            return result;
        }

        set
        {
            result = value;
        }
    }
}

//
// 1.3 로그아웃
//
[Serializable]
public struct SignOut
{
    [SerializeField] private Respon respon;
    [SerializeField] private Result result;

    public Respon Respon
    {
        get
        {
            return respon;
        }

        set
        {
            respon = value;
        }
    }

    public Result Result
    {
        get
        {
            return result;
        }

        set
        {
            result = value;
        }
    }
}

[Serializable]
public struct Respon
{
    [SerializeField] private string success;

    public string Success
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
}

[Serializable]
public struct Result
{
    [SerializeField] private string uid;
    [SerializeField] private string token;
    [SerializeField] private string code;
    [SerializeField] private string reason;

    public string Uid
    {
        get
        {
            return uid;
        }

        set
        {
            uid = value;
        }
    }

    public string Token
    {
        get
        {
            return token;
        }

        set
        {
            token = value;
        }
    }

    public string Code
    {
        get
        {
            return code;
        }

        set
        {
            code = value;
        }
    }

    public string Reason
    {
        get
        {
            return reason;
        }

        set
        {
            reason = value;
        }
    }
}

//
//2.1 간호술기 목차 보기
//
[Serializable]
public struct ListClinical
{
    [SerializeField] private Respon respon;
    [SerializeField] private ListClinicalInfo info;
    [SerializeField] private ListClinical_listDetail list;

    public Respon Respon
    {
        get
        {
            return respon;
        }

        set
        {
            respon = value;
        }
    }

    public ListClinicalInfo Info
    {
        get
        {
            return info;
        }

        set
        {
            info = value;
        }
    }

    public ListClinical_listDetail List
    {
        get
        {
            return list;
        }

        set
        {
            list = value;
        }
    }
}

[Serializable]
public struct ListClinicalInfo
{
    [SerializeField] private ListSize listsize;

    public ListSize Listsize
    {
        get
        {
            return listsize;
        }

        set
        {
            listsize = value;
        }
    }
}

[SerializeField]
public struct ListSize
{
    [SerializeField] private string listsize;

    public string Listsize
    {
        get
        {
            return listsize;
        }

        set
        {
            listsize = value;
        }
    }
}

[Serializable]
public struct ListClinical_listDetail
{
    [SerializeField] private string id;
    [SerializeField] private string title;
    [SerializeField] private string difficulty;

    public string Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public string Title
    {
        get
        {
            return title;
        }

        set
        {
            title = value;
        }
    }

    public string Difficulty
    {
        get
        {
            return difficulty;
        }

        set
        {
            difficulty = value;
        }
    }
}

//
// 2.2 간호술기 순서
//
[Serializable]
public struct RandomContent
{
    [SerializeField] private Respon respon;
    [SerializeField] private Info info;
    [SerializeField] private RandomContentListDetail list;

    public Respon Respon
    {
        get
        {
            return respon;
        }

        set
        {
            respon = value;
        }
    }

    public Info Info
    {
        get
        {
            return info;
        }

        set
        {
            info = value;
        }
    }

    public RandomContentListDetail List
    {
        get
        {
            return list;
        }

        set
        {
            list = value;
        }
    }
}

[Serializable]
public struct RandomContentListDetail
{
    [SerializeField] private string index;
    [SerializeField] private string content;

    public string Index
    {
        get
        {
            return index;
        }

        set
        {
            index = value;
        }
    }

    public string Content
    {
        get
        {
            return content;
        }

        set
        {
            content = value;
        }
    }
}

//
// 2.3 간호술기 아이템
//
[Serializable]
public struct RandomItem
{
    [SerializeField] private Respon respon;
    [SerializeField] private Info info;
    [SerializeField] private RandomItemListDetail list;

    public Respon Respon
    {
        get
        {
            return respon;
        }

        set
        {
            respon = value;
        }
    }

    public Info Info
    {
        get
        {
            return info;
        }

        set
        {
            info = value;
        }
    }

    public RandomItemListDetail List
    {
        get
        {
            return list;
        }

        set
        {
            list = value;
        }
    }
}

[Serializable]
public struct RandomItemListDetail
{
    [SerializeField] private string name;
    [SerializeField] private string rating;

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public string Rating
    {
        get
        {
            return rating;
        }

        set
        {
            rating = value;
        }
    }
}

[Serializable]
public struct Info
{
    [SerializeField] private string id;
    [SerializeField] private string title;
    [SerializeField] private string difficulty;

    public string Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public string Title
    {
        get
        {
            return title;
        }

        set
        {
            title = value;
        }
    }

    public string Difficulty
    {
        get
        {
            return difficulty;
        }

        set
        {
            difficulty = value;
        }
    }
}

//
// 3.1 게임 결과 기록
//
[Serializable]
public struct GameRecord
{
    [SerializeField] private Respon respon;
    [SerializeField] private GameRecordInfo info;

    public Respon Respon
    {
        get
        {
            return respon;
        }

        set
        {
            respon = value;
        }
    }

    public GameRecordInfo Info
    {
        get
        {
            return info;
        }

        set
        {
            info = value;
        }
    }
}

[Serializable]
public struct GameRecordInfo
{
    [SerializeField] private string name;
    [SerializeField] private string score;

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public string Score
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
}