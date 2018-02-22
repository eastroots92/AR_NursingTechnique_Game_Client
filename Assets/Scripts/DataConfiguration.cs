using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class DataConfiguration
{
    [SerializeField] private SignUp signup { get; }
    [SerializeField] private SignIn signin { get; }
    [SerializeField] private SignOut signout { get; }
    [SerializeField] private ListClinical list_clinical { get; }
    [SerializeField] private RandomItem random_item { get; }
    [SerializeField] private RandomContent random_content { get; }
    [SerializeField] private GameRecord game_record { get; }
}

[Serializable]
public struct GameRecord
{
    [SerializeField] private Respon respon;
    [SerializeField] private Info info;
}

[Serializable]
public struct RandomContent
{
    [SerializeField] private Respon respon;
    [SerializeField] private Info info;
    [SerializeField] private ListDetail list;
}

[Serializable]
public struct RandomItem
{
    [SerializeField] private Respon respon;
    [SerializeField] private Info info;
    [SerializeField] private ListDetail list;
}

[Serializable]
public struct ListClinical
{
    [SerializeField] private Respon respon;
    [SerializeField] private Info info;
    [SerializeField] private ListDetail list;
}

[Serializable]
public struct SignOut
{
    [SerializeField] private Respon respon;
    [SerializeField] private Result result;
}
[Serializable]
public struct SignUp
{
    [SerializeField] private Respon respon;
    [SerializeField] private Result result;
}

[Serializable]
public struct SignIn
{
    [SerializeField] private Respon respon;
    [SerializeField] private Result result;
}

[Serializable]
public struct Respon
{
    [SerializeField] private string success;
}

[Serializable]
public struct Result
{
    [SerializeField] private string uid;
    [SerializeField] private string token;
    [SerializeField] private string code;
    [SerializeField] private string reason;
}

[Serializable]
public struct Info
{
    [SerializeField] private ListSize listSize;
    [SerializeField] private string id;
    [SerializeField] private string title;
    [SerializeField] private string difficulty;
    [SerializeField] private string name;
    [SerializeField] private string score;
}

[SerializeField]
public struct ListSize
{
    [SerializeField] private string listSize;
}

[SerializeField]
public struct ListDetail
{
    [SerializeField] private string id;
    [SerializeField] private string title;
    [SerializeField] private string difficulty;
    [SerializeField] private string name;
    [SerializeField] private string rating;
    [SerializeField] private string index;
    [SerializeField] private string content;
}