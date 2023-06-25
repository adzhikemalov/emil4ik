
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public DialogManager DialogManager;
    public MiniGameManager MiniGameManager;
    public GameObject InteractableButton;
    public Text ButtonText;
    private InteractableObject _currentObject;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        _currentObject = other.gameObject.GetComponent<InteractableObject>();
        if (_currentObject)
        {
            InteractableButton.SetActive(true);
            if (_currentObject.ObjectType == InteractableObject.InteractableType.Dialog)
            {
                ButtonText.text = "Говорить";
            }
            if (_currentObject.ObjectType == InteractableObject.InteractableType.MiniGame)
            {
                ButtonText.text = "Чинить";
            }
        }
    }

    public void OnButtonPressed()
    { 
        if (_currentObject == null)
        {
            return;
        }
        if (_currentObject.ObjectType == InteractableObject.InteractableType.Dialog)
        {
            DialogManager.ShowDialog(_currentObject.Dialog);
        }

        if (_currentObject.ObjectType == InteractableObject.InteractableType.MiniGame)
        {
            MiniGameManager.ShowMiniGame(_currentObject.MiniGame);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _currentObject = null;
        InteractableButton.SetActive(false);
    }
}