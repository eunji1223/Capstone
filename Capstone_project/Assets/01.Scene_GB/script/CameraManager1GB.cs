using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class CameraManager1GB : MonoBehaviour
{
    WebCamTexture camTexture;
    public RawImage cameraViewImage;

    void Start()
    {
        CamaraOn(); // 씬이 로드될 때 카메라를 켭니다.
    }

    public void CamaraOn()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }
        if (WebCamTexture.devices.Length == 0) // 오타 수정
        {
            Debug.Log("no camera!");
            return;
        }
        WebCamDevice[] devices = WebCamTexture.devices;
        int selectedCameraIndex = -1;

        // 전면 카메라를 찾도록 수정
        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing) // 전면 카메라 선택
            {
                selectedCameraIndex = i;
                break;
            }
        }
        if (selectedCameraIndex >= 0)
        {
            camTexture = new WebCamTexture(devices[selectedCameraIndex].name);
            camTexture.requestedFPS = 60; // 오타 수정: requestFPS -> requestedFPS
            cameraViewImage.texture = camTexture;
            camTexture.Play();
        }
    }
}
