using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialog
{
    [SerializeField] List<string> lines;
    [SerializeField] string title;
    [SerializeField] string imagePath;

    public List<string> Lines
    {
        get { return lines; }
    }

    public string Title
    {
        get { return title; }
    }

    public string ImagePath
    {
        get { return imagePath; }
    }
}