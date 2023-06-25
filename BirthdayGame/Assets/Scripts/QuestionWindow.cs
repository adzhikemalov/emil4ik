using System.Text;
using TMPro;
using UnityEngine;

public class QuestionWindow : MonoBehaviour
{
    public TextMeshProUGUI Description;
    public TMP_InputField Answer;
    public TextMeshProUGUI Counter;
    public QuestionData Data;
    public Main MainScript;
    public void Init(QuestionData data)
    {
        Description.text = data.Question;
        Answer.text = string.Empty;
        var sb = new StringBuilder();
        for (int i = 0; i < data.Answer.Length; i++)
        {
            sb.Append("_");
        }

        Data = data;
        Counter.text = sb.ToString();
    }

    public void Back()
    {
        MainScript.OpenMain();
    }

    public void CheckAnswer()
    {
        if (Answer.text.ToLower() == Data.Answer.ToLower())
        {
            MainScript.OpenAnswer(Data);
        }
    }
}