using UnityEngine;
using Unity.Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineCamera[] cameras;

    public CinemachineCamera distractionCam1;
    public CinemachineCamera distractionCam2;

    public CinemachineCamera fpsCamera;
    private CinemachineCamera currentCamera;

    private void Start()
    {
        currentCamera = fpsCamera;

        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] == currentCamera)
            {
                cameras[i].Priority = 20;
            }
            else
            {
                cameras[i].Priority = 10;
            }
        }
    }

    public void SwitchCamera(CinemachineCamera newCam)
    {
        currentCamera = newCam;

        currentCamera.Priority = 20;

        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] != currentCamera)
            {
                cameras[i].Priority = 10;
            }
        }
    }
}   
