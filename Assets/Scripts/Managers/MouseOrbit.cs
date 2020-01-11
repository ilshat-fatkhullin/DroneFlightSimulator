using UnityEngine;

public class MouseOrbit: MonoBehaviour
{

    public Transform Target;
    public float Distance = 5.0f;
    public float XSpeed = 120.0f;
    public float YSpeed = 120.0f;

    public float YMinLimit = -20f;
    public float YMaxLimit = 80f;

    public float DistanceMin = .5f;
    public float DistanceMax = 15f;

    float x = 0.0f;
    float y = 0.0f;

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    void LateUpdate()
    {
        if (Target)
        {
            x += Input.GetAxis("Mouse X") * XSpeed * Distance * 0.02f;
            y -= Input.GetAxis("Mouse Y") * YSpeed * 0.02f;

            y = ClampAngle(y, YMinLimit, YMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            Distance = Mathf.Clamp(Distance - Input.GetAxis("Mouse ScrollWheel"), DistanceMin, DistanceMax);

            RaycastHit hit;
            if (Physics.Linecast(Target.position, transform.position, out hit))
            {
                Distance -= hit.distance;
            }
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -Distance);
            Vector3 position = rotation * negDistance + Target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}