using UnityEngine;
using UnityEngine.UI;

public class UIHeader : MonoBehaviour
{
    public Text Header;

    public UIManager UIManager;

    private void Start()
    {
        UIManager.CurrentUIState.OnChanged.AddListener(OnUIStateChange);
    }

    private void OnUIStateChange()
    {
        switch (UIManager.CurrentUIState.Value)
        {
            case UIManager.UIState.ChoosingSourcePoint:
                Header.text = StringConstants.CHOOSE_SOURCE_POINT;
                break;
            case UIManager.UIState.ChoosingDestinationPoint:
                Header.text = StringConstants.CHOOSE_DESTINATION_POINT;
                break;
            case UIManager.UIState.None:
                Header.text = "";
                break;
        }
    }
}
