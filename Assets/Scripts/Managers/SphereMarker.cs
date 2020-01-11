using UnityEngine;

public class SphereMarker : MonoBehaviour
{
    public float Size;

    private void Update()
    {
        if (!Camera.current)
            return;

        float distance = Vector3.Distance(Camera.current.transform.position, transform.position);
        transform.localScale = Vector3.one * Size * distance;
    }
}
