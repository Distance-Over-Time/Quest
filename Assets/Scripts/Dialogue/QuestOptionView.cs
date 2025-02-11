using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class QuestOptionView : OptionView
{
    [SerializeField] private Text optionText; 
    [SerializeField] private Button optionButton; 
    [SerializeField] private Image background; // For highlighting

    private Color defaultColor = Color.white;
    private Color selectedColor = Color.yellow;

    public new DialogueOption Option
    {
        get => base.Option;
        set
        {
            base.Option = value;
            optionText.text = value.Line.Text.Text;
            optionButton.interactable = value.IsAvailable;
            optionButton.onClick.RemoveAllListeners();
            optionButton.onClick.AddListener(() => InvokeOptionSelected());
        }
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void SetHighlighted(bool isHighlighted)
    {
        background.color = isHighlighted ? selectedColor : defaultColor;
    }
}