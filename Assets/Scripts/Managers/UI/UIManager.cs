using UnityEngine;

public class UIManager : MonoBehaviour
{
    public enum UIState { None, ChoosingSourcePoint, ChoosingDestinationPoint, Simulation }

    public Changable<UIState> CurrentUIState = new Changable<UIState>(UIState.None);

    public Changable<Vector3> SourcePoint = new Changable<Vector3>(Vector3.zero);

    public Changable<bool> SourcePointSet = new Changable<bool>(false);

    public Changable<Vector3> DestinationPoint = new Changable<Vector3>(Vector3.zero);

    public Changable<bool> DestinationPointSet = new Changable<bool>(false);

    private void Start()
    {
        UserInputController.Instance.OnPointSelected.AddListener(OnPointSelected);
        FlightSimulationController.Instance.FlightFinished.AddListener(OnFlightFinished);
    }

    private void OnPointSelected(Vector3 point)
    {
        switch (CurrentUIState.Value)
        {
            case UIState.ChoosingSourcePoint:
                SourcePoint.Value = point;
                SourcePointSet.Value = true;
                CurrentUIState.Value = UIState.None;
                break;
            case UIState.ChoosingDestinationPoint:
                DestinationPoint.Value = point;
                DestinationPointSet.Value = true;
                CurrentUIState.Value = UIState.None;
                break;
        }
    }

    public void CmdChooseSourcePoint()
    {
        CurrentUIState.Value = UIState.ChoosingSourcePoint;
    }

    public void CmdChooseDestinationPoint()
    {
        CurrentUIState.Value = UIState.ChoosingDestinationPoint;
    }

    public void CmdSimulate()
    {
        CurrentUIState.Value = UIState.Simulation;
        FlightSimulationController.Instance.SimulateFlight();
    }

    public void CmdCancel()
    {
        if (CurrentUIState.Value == UIState.Simulation)
        {
            FlightSimulationController.Instance.CancelFlight();
        }
        CurrentUIState.Value = UIState.None;
    }

    private void OnFlightFinished()
    {
        CurrentUIState.Value = UIState.None;
    }
}
