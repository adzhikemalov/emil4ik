using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "DialogData", menuName = "Create Dialog Data", order = 0)]
public class DialogData : ScriptableObject
{
    public AudioClip Sound;
    public string Text;
    public Sprite Avatar;
}