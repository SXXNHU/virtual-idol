using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Transform[] views;
    private int currentViewIndex = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        { 
            currentViewIndex = (currentViewIndex + 1) % views.Length;
            SwitchCamerView();
        }
    }

    void SwitchCamerView()
    {
        Transform targetView = views[currentViewIndex];
        Debug.Log("Switching to view: " + currentViewIndex); // ·Î±× 
        transform.position = targetView.position;
        transform.rotation = targetView.rotation;
    }
}
