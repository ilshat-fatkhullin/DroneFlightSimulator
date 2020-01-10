using UnityEngine;
using UnityEngine.Events;

public class UserInputController : MonoBehaviour
{
    public static UserInputController Instance { get { return Singleton<UserInputController>.Instance; } }

    public PointSelectionEvent OnPointSelected = new PointSelectionEvent();

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                OnPointSelected.Invoke(hit.point);
                Debug.Log("Point selected: " + hit.point);
            }
        }
    }
}

public class PointSelectionEvent : UnityEvent<Vector3>
{

}
