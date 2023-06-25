using TMPro;
using UnityEngine;

public class AnswerWindow : MonoBehaviour
{
    public TextMeshProUGUI Code;
    public Main MainScript;

    public void Init(QuestionData data)
    {
        Code.text = data.Code;
    }
    
    public void Back()
    {
        MainScript.OpenMain();
    }

}