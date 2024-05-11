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
        CamaraOn(); // ���� �ε�� �� ī�޶� �մϴ�.
    }

    public void CamaraOn()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }
        if (WebCamTexture.devices.Length == 0) // ��Ÿ ����
        {
            Debug.Log("no camera!");
            return;
        }
        WebCamDevice[] devices = WebCamTexture.devices;
        int selectedCameraIndex = -1;

        // ���� ī�޶� ã���� ����
        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing) // ���� ī�޶� ����
            {
                selectedCameraIndex = i;
                break;
            }
        }
        if (selectedCameraIndex >= 0)
        {
            camTexture = new WebCamTexture(devices[selectedCameraIndex].name);
            camTexture.requestedFPS = 60; // ��Ÿ ����: requestFPS -> requestedFPS
            cameraViewImage.texture = camTexture;
            camTexture.Play();
        }
    }
}
