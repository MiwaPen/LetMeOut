using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _mainCam;
    private List<Camera> _cameraStack;

    private void Start()
    {
        _cameraStack = _mainCam.GetUniversalAdditionalCameraData().cameraStack;
    }
    
    public void AddOverlayCameraToStack(Camera cam)
    {
        if (cam.GetUniversalAdditionalCameraData().renderType==CameraRenderType.Overlay)
        {
            _cameraStack.Add(cam);
        }
    }
    
    public void RemoveOverlayCameraFromStack(Camera cam)
    {
        if (_cameraStack.Contains(cam))
        {
            _cameraStack.Remove(cam);
        }
    }
}
