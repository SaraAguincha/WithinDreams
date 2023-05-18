using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    [SerializeField] List<string> lines;
    [SerializeField] string title;


    public List<string> Lines
    {
        get { return lines; }
    }

    public string Title
    {
        get { return title; }
    }
}