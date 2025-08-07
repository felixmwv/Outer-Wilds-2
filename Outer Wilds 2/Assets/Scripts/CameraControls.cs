using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public CameraManager cameraManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            cameraManager.SwitchCamera(cameraManager.distractionCam1);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            cameraManager.SwitchCamera(cameraManager.fpsCamera);
        }
    }
}
