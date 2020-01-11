using UnityEngine;

public class MarkerManager : MonoBehaviour
{
    public GameObject SourcePointMarker;

    public GameObject DestinationPointMarker;

    public SpaceNavMesh SpaceNavMesh;

    public UIManager UIManager;

    private void Start()
    {
        UIManager.SourcePoint.OnChanged.AddListener(OnSourcePointChanged);
        UIManager.SourcePointSet.OnChanged.AddListener(OnSourcePointSetChanged);
        UIManager.DestinationPoint.OnChanged.AddListener(OnDestinationPointChanged);
        UIManager.DestinationPointSet.OnChanged.AddListener(OnDestinationPointSetChanged);
        OnSourcePointSetChanged();
        OnDestinationPointSetChanged();
    }

    private void OnSourcePointChanged()
    {
        SourcePointMarker.transform.position = SpaceNavMesh.GetClosestFreePosition(UIManager.SourcePoint.Value);
    }

    private void OnSourcePointSetChanged()
    {
        SourcePointMarker.SetActive(UIManager.SourcePointSet.Value);
    }

    private void OnDestinationPointChanged()
    {
        DestinationPointMarker.transform.position = SpaceNavMesh.GetClosestFreePosition(UIManager.DestinationPoint.Value);
    }

    private void OnDestinationPointSetChanged()
    {
        DestinationPointMarker.SetActive(UIManager.DestinationPointSet.Value);
    }
}
