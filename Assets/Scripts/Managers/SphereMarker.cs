using UnityEngine;

public class SphereMarker : MonoBehaviour
{
    public float Size;

    private void Update()
    {
        float distance = Vector3.Distance(Camera.main.transform.position, transform.position);
        transform.localScale = Vector3.one * Size * distance;
    }
}
