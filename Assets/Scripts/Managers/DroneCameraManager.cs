using UnityEngine;

/// <summary>
/// Drone Camera Manager.
/// Controls switching between first and third person cameras.
/// </summary>
public class DroneCameraManager : MonoBehaviour
{
    public Camera ThirdPersonCamera;

    public Camera FirstPersonCamera;

    private bool isFirstPerson;

    private void Start()
    {
        isFirstPerson = false;
        FirstPersonCamera.enabled = false;
        ThirdPersonCamera.enabled = true;
    }

    public void SwichCamera()
    {
        isFirstPerson = !isFirstPerson;

        if (isFirstPerson)
        {
            FirstPersonCamera.enabled = true;
            ThirdPersonCamera.enabled = false;
        }
        else
        {
            FirstPersonCamera.enabled = false;
            ThirdPersonCamera.enabled = true;
        }
    }
}
