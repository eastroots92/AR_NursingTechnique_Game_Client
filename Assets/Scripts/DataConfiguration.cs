﻿using UnityEngine;
using System;

[Serializable]
public class DataConfiguration
{
    [SerializeField] private SignUp signup;
    [SerializeField] private SignIn signin;
    [SerializeField] private SignOut signout;
    [SerializeField] private ListClinical list_clinical;
    [SerializeField] private RandomItem random_item;
    [SerializeField] private RandomContent random_content;
    [SerializeField] private GameRecord game_record;

    #region DataConfiguration Property
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
#endregion
}

//
// 1.1 회원가입
//
[Serializable]
public class SignUp
{
    [SerializeField] private Respon respon;
    [SerializeField] private Result result;

    #region SignUp Property
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
#endregion
}

//
// 1.2 로그인
//
[Serializable]
public class SignIn
{
    [SerializeField] private Respon respon;
    [SerializeField] private Result result;

    #region SignIn Property
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
#endregion
}

//
// 1.3 로그아웃
//
[Serializable]
public class SignOut
{
    [SerializeField] private Respon respon;
    [SerializeField] private Result result;

    #region SignOut Property
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
#endregion
}

[Serializable]
public class Respon
{
    [SerializeField] private string success;

#region Respon Property
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
#endregion
}

[Serializable]
public class Result
{
    [SerializeField] private string uid;
    [SerializeField] private string token;
    [SerializeField] private string code;
    [SerializeField] private string reason;

#region Result Property
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
#endregion
}

//
//2.1 간호술기 목차 보기
//
[Serializable]
public class ListClinical
{
    [SerializeField] private Respon respon;
    [SerializeField] private ListClinicalInfo info;
    [SerializeField] private ListClinical_listDetail list;

    #region ListClinical Property
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
#endregion
}

[Serializable]
public class ListClinicalInfo
{
    [SerializeField] private ListSize listsize;

    #region ListClinicalInfo Property
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
#endregion
}

[SerializeField]
public class ListSize
{
    [SerializeField] private string listsize;

    #region ListSize Property
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
#endregion
}

[Serializable]
public class ListClinical_listDetail
{
    [SerializeField] private string id;
    [SerializeField] private string title;
    [SerializeField] private string difficulty;

    #region ListClinical_listDetail Property
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
#endregion
}

//
// 2.2 간호술기 순서
//
[Serializable]
public class RandomContent
{
    [SerializeField] private Respon respon;
    [SerializeField] private Info info;
    [SerializeField] private RandomContentListDetail list;

    #region RandomContent Property
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
#endregion
}

[Serializable]
public class RandomContentListDetail
{
    [SerializeField] private string index;
    [SerializeField] private string content;

    #region RandomContentListDetail Property
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
#endregion
}

//
// 2.3 간호술기 아이템
//
[Serializable]
public class RandomItem
{
    [SerializeField] private Respon respon;
    [SerializeField] private Info info;
    [SerializeField] private RandomItemListDetail list;

    #region RandomItem Property
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
#endregion
}

[Serializable]
public class RandomItemListDetail
{
    [SerializeField] private string name;
    [SerializeField] private string rating;

    #region RandomItemListDetail Property
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
#endregion
}

[Serializable]
public class Info
{
    [SerializeField] private string id;
    [SerializeField] private string title;
    [SerializeField] private string difficulty;

    #region Info Property
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
#endregion
}

//
// 3.1 게임 결과 기록
//
[Serializable]
public class GameRecord
{
    [SerializeField] private Respon respon;
    [SerializeField] private GameRecordInfo info;

    #region GameRecord Property
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
#endregion
}

[Serializable]
public class GameRecordInfo
{
    [SerializeField] private string name;
    [SerializeField] private string score;

    #region GameRecordInfo Property
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
    #endregion
}

//
// 로그인 정보
//
[Serializable]
public struct LogInInform
{
    [SerializeField] private string id;
    [SerializeField] private string pw;

    #region LogInInform Property
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

    public string Pw
    {
        get
        {
            return pw;
        }

        set
        {
            pw = value;
        }
    }
#endregion
}

//
// Login View Option 정보
//
[Serializable]
public struct LogInSettingsOption
{
    public bool isAutoLogIn;
    public bool isSetID;
}

[Serializable]
public struct TokenData
{
    [SerializeField] private string token;

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

    public TokenData(string token)
    {
        this.token = token;
    }
}