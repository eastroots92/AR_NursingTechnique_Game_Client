using UnityEngine;
using System;

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
        get { return respon; }
    }

    public Result Result
    {
        get { return result; }
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
        get { return respon; }
    }

    public Result Result
    {
        get { return result; }
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
        get { return respon; }
    }

    public Result Result
    {
        get { return result; }
    }
}

[Serializable]
public struct Respon
{
    [SerializeField] private string success;

    public string Success
    {
        get { return success; }
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
        get { return uid; }
    }

    public string Token
    {
        get { return token; } 
    }

    public string Code
    {
        get { return code; }
    }

    public string Reason
    {
        get { return reason; } 
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

    public ListClinicalInfo Info
    {
        get { return info; }
    }

    public ListClinical_listDetail List
    {
        get { return list; }
    }
}

[Serializable]
public struct ListClinicalInfo
{
    [SerializeField] private ListSize listsize;

    public ListSize Listsize
    {
        get { return listsize; }
    }
}

[SerializeField]
public struct ListSize
{
    [SerializeField] private string listsize;

    public string Listsize
    {
        get { return listsize; }
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
        get { return id; }
    }

    public string Title
    {
        get { return title; }
    }

    public string Difficulty
    {
        get { return difficulty; }
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

    public Info Info
    {
        get { return info; }
    }

    public RandomContentListDetail List
    {
        get { return list; }
    }
}

[Serializable]
public struct RandomContentListDetail
{
    [SerializeField] private string index;
    [SerializeField] private string content;

    public string Index
    {
        get { return index; }
    }

    public string Content
    {
        get { return content; }
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

    public Info Info
    {
        get { return info; }
    }

    public RandomItemListDetail List
    {
        get { return list; }
    }
}

[Serializable]
public struct RandomItemListDetail
{
    [SerializeField] private string name;
    [SerializeField] private string rating;

    public string Name
    {
        get { return name; }
    }

    public string Rating
    {
        get { return rating; }
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
        get { return id; }
    }

    public string Title
    {
        get { return title; }
    }

    public string Difficulty
    {
        get { return difficulty; }
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

    public GameRecordInfo Info
    {
        get { return info; }
    }
}

[Serializable]
public struct GameRecordInfo
{
    [SerializeField] private string name;
    [SerializeField] private string score;

    public string Name
    {
        get { return name; }
    }

    public string Score
    {
        get { return score; }
    }
}