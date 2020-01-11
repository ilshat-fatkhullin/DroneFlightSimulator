using UnityEngine;
using UnityEngine.UI;

public class UISimulateButton : MonoBehaviour
{
    public UIManager UIManager;

    public Button Button;

    private void Start()
    {
        UIManager.SourcePointSet.OnChanged.AddListener(OnStateChanged);
        UIManager.DestinationPointSet.OnChanged.AddListener(OnStateChanged);
        OnStateChanged();
        Button.onClick.AddListener(OnClick);
    }

    private void OnStateChanged()
    {
        Button.interactable = UIManager.SourcePointSet.Value && UIManager.DestinationPointSet.Value;
    }

    private void OnClick()
    {
        FlightSimulationController.Instance.SimulateFlight();
    }
}
