using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject WebCamWindow;
    public GameObject QuestionWindow;
    public GameObject AnswerWindow;
    public GameObject ResultWindow;
    public List<QuestionData> Data;
    public Dictionary<string, string> AnswersDone;

    [SerializeField] private QuestionWindow QuestionWindowScript;
    [SerializeField] private QrCodeScanner WebCamWindowScript;
    [SerializeField] private AnswerWindow AnswerWindowScript;
    [SerializeField] private ResultWindow ResultWindowScript;

    [SerializeField] private TextMeshProUGUI Counter;
    public void OpenWebcamWindow()
    {
        CloseAll();
        WebCamWindow.SetActive(true);
        WebCamWindowScript.Init();
    }

    public void Start()
    {
        AnswersDone = new Dictionary<string, string>();
    }

    public void Update()
    {
        Counter.text = $"Осталось {Data.Count - AnswersDone.Count}";
    }

    public void OpenResult()
    {
        CloseAll();
        ResultWindow.SetActive(true);
        ResultWindowScript.Init(AnswersDone);
    }

    public void OpenMain()
    {
        CloseAll();
        this.gameObject.SetActive(true);
    }

    public void OpenQuestion(string number)
    {
        CloseAll();
        foreach (QuestionData questionData in Data)
        {
            if (String.Equals(questionData.Number, number, StringComparison.CurrentCultureIgnoreCase))
            {
                QuestionWindow.SetActive(true);
                QuestionWindowScript.Init(questionData);
                return;
            }
        }

        OpenWebcamWindow();
    }

    public void OpenAnswer(QuestionData data)
    {
        CloseAll();
        AnswersDone[data.Number] = data.Code;
        AnswerWindow.SetActive(true);
        AnswerWindowScript.Init(data);
    }

    public void CloseAll()
    {
        WebCamWindowScript.Dispose();
        this.gameObject.SetActive(false);
        WebCamWindow.SetActive(false);
        QuestionWindow.SetActive(false);
        AnswerWindow.SetActive(false);
        ResultWindow.SetActive(false);
    }
}