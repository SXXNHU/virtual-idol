using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Transform[] views;
    private int currentViewIndex = 0;

    private void Start()
    {

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            currentViewIndex = (currentViewIndex + 1) % views.Length;
        }
        transform.position = views[currentViewIndex].position;
        transform.rotation = views[currentViewIndex].rotation;
    }
}
