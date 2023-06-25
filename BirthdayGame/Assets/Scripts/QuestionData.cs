using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "QuestData", order = 0)]
public class QuestionData : ScriptableObject
{
    public string Number;
    public string Question;
    public string Answer;
    public string Code;
}