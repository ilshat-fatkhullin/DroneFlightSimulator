﻿using UnityEngine;

public class UIPanelSwitcher : MonoBehaviour
{
    public GameObject ChooseButtonsPanel;

    public GameObject CancelButtonPanel;

    public UIManager UIManager;
    
    private void Start()
    {
        ChooseButtonsPanel.SetActive(true);
        CancelButtonPanel.SetActive(false);

        UIManager.CurrentUIState.OnChanged.AddListener(OnUIStateChanged);
    }

    private void OnUIStateChanged()
    {
        switch (UIManager.CurrentUIState.Value)
        {
            case UIManager.UIState.None:
                ChooseButtonsPanel.SetActive(true);
                CancelButtonPanel.SetActive(false);
                break;
            default:
                ChooseButtonsPanel.SetActive(false);
                CancelButtonPanel.SetActive(true);
                break;
        }
    }
}
