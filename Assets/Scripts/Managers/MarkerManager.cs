using UnityEngine;

public class MarkerManager : MonoBehaviour
{
    public GameObject SourcePointMarker;

    public GameObject DestinationPointMarker;

    public UIManager UIManager;

    private void Start()
    {
        UIManager.SourcePoint.OnChanged.AddListener(OnSourcePointChanged);
        UIManager.SourcePointSet.OnChanged.AddListener(OnSourcePointSetChanged);
        UIManager.DestinationPoint.OnChanged.AddListener(OnDestinationPointChanged);
        UIManager.DestinationPointSet.OnChanged.AddListener(OnDestinationPointSetChanged);
        OnSourcePointChanged();
        OnSourcePointSetChanged();
        OnDestinationPointChanged();
        OnDestinationPointSetChanged();
    }

    private void OnSourcePointChanged()
    {
        SourcePointMarker.transform.position = UIManager.SourcePoint.Value;
    }

    private void OnSourcePointSetChanged()
    {
        SourcePointMarker.SetActive(UIManager.SourcePointSet.Value);
    }

    private void OnDestinationPointChanged()
    {
        DestinationPointMarker.transform.position = UIManager.DestinationPoint.Value;
    }

    private void OnDestinationPointSetChanged()
    {
        DestinationPointMarker.SetActive(UIManager.DestinationPointSet.Value);
    }
}
