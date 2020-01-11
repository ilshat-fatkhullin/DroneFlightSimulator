using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UserInputController : MonoBehaviour
{
    public static UserInputController Instance { get { return Singleton<UserInputController>.Instance; } }

    public PointSelectionEvent OnPointSelected = new PointSelectionEvent();

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                OnPointSelected.Invoke(hit.point);
            }
        }
    }
}

public class PointSelectionEvent : UnityEvent<Vector3>
{

}
