using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class ResultWindow : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public Main MainScript;
    public void Init(Dictionary<string, string> data)
    {
        var sb = new StringBuilder();
        foreach (KeyValuePair<string,string> keyValuePair in data)
        {
            sb.Append(keyValuePair.Value);
            sb.AppendLine();
        }

        Text.text = sb.ToString();
    }
    
    public void Back()
    {
        MainScript.OpenMain();
    }
}