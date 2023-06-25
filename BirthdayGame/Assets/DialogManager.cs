using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    private GameObject _currentDialog;
    private DialogWindow _dialogContent;

    public GameObject DialogPrefab;
    public GameObject Joystick;
    public GameObject DialogBackPrefab;
    private Button _closeButton;
    public DialogData EmilTot;
    public DialogData Emine;
    public DialogData Ismet;
    public DialogData Nataly;
    public DialogData EmilMav;
    public DialogData Cat;
    public AudioSource Sound;

    public void ShowDialog(InteractableObject.DialogId currentObjectDialog)
    {
        if (_currentDialog)
            Destroy(_currentDialog);
        Joystick.SetActive(false);
        _currentDialog = GameObject.Instantiate(DialogBackPrefab,  this.transform);
        var go = GameObject.Instantiate(DialogPrefab, _currentDialog.transform);
        _dialogContent = go.GetComponent<DialogWindow>();
        SetupButton();
        SetupDialogContent(currentObjectDialog);
    }

    private void SetupDialogContent(InteractableObject.DialogId dialogId)
    {
        switch (dialogId)
        {
            case InteractableObject.DialogId.EmilTot:
                CreateDialog(EmilTot);
                break;
        }
        switch (dialogId)
        {
            case InteractableObject.DialogId.Emine:
                CreateDialog(Emine);
                break;
        }
        switch (dialogId)
        {
            case InteractableObject.DialogId.Ismet:
                CreateDialog(Ismet);
                break;
        }
        switch (dialogId)
        {
            case InteractableObject.DialogId.Nataly:
                CreateDialog(Nataly);
                break;
        }
        switch (dialogId)
        {
            case InteractableObject.DialogId.EmilMav:
                CreateDialog(EmilMav);
                break;
        }
        switch (dialogId)
        {
            case InteractableObject.DialogId.Cat:
                CreateDialog(Cat);
                break;
        }
    }

    private void CreateDialog(DialogData data)
    {
        if (_dialogContent == null)
            return;
        
        _dialogContent.Image.sprite = data.Avatar;
        _dialogContent.Text.text = data.Text;
        Sound.PlayOneShot(data.Sound);
    }

    private void SetupButton()
    {
        _closeButton = _currentDialog.gameObject.GetComponentInChildren<Button>();
        if (_closeButton)
            _closeButton.onClick.AddListener(RemoveDialog);
    }

    public void RemoveDialog()
    {
        if (_currentDialog)
            Destroy(_currentDialog);
        if (_closeButton)
            _closeButton.onClick.RemoveAllListeners();
        Joystick.SetActive(true);
    }
}