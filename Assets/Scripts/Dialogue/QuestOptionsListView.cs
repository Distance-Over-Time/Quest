using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Yarn.Unity;

public class QuestOptionsListView : OptionsListView
{
    [SerializeField] private QuestOptionView optionPrefab; // Prefab for each option
    [SerializeField] private Transform optionsParent; // Parent container
    private List<QuestOptionView> optionInstances = new List<QuestOptionView>();

    private int selectedIndex = 0;

    private void Update()
    {
        HandleNavigation();
    }

    public void SetOptions(IEnumerable<DialogueOption> options, Action<DialogueOption> optionHandler)
    {
        // Clear existing options
        ClearOptions();
        int index = 0;

        // Populate new options
        foreach (var option in options)
        {
            var optionInstance = Instantiate(optionPrefab, optionsParent);
            optionInstance.Option = option;
            optionInstance.OnOptionSelected = optionHandler; // ✅ Works now
            optionInstances.Add(optionInstance);
        
            if (index == 0) {
                EventSystem.current.SetSelectedGameObject(optionInstance.gameObject);
            }

            index++;
        }

        selectedIndex = 0;
        UpdateSelection();
    }

    private void HandleNavigation()
    {
        if (optionInstances.Count == 0) return;

        Vector2 navigation = OptionsInputManager.Instance.GetNavigationInput();

        if (navigation.y > 0) // Up
        {
            selectedIndex = Mathf.Max(0, selectedIndex - 1);
            UpdateSelection();
        }
        else if (navigation.y < 0) // Down
        {
            selectedIndex = Mathf.Min(optionInstances.Count - 1, selectedIndex + 1);
            UpdateSelection();
        }

        if (OptionsInputManager.Instance.IsSubmitPressed()) // Submit
        {
            optionInstances[selectedIndex].InvokeOptionSelected();
        }
    }

    private void UpdateSelection()
    {
        for (int i = 0; i < optionInstances.Count; i++)
        {
            optionInstances[i].SetHighlighted(i == selectedIndex);
        }
    }

    public void ClearOptions()
    {
        foreach (var option in optionInstances)
        {
            Destroy(option.gameObject);
        }
        optionInstances.Clear();
    }
}
