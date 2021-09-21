using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;

public class CameraManager : MonoBehaviour
{
    WebCamTexture camTexture;

    public RawImage cameraViewImage; //카메라가 보여질 화면

    public void CameraOn() //카메라 켜기
    {
        //카메라 권한 확인
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }

        if (WebCamTexture.devices.Length == 0) //카메라가 없다면..
        {
            Debug.Log("no camera!");
            return;
        }

        WebCamDevice[] devices = WebCamTexture.devices; //스마트폰의 카메라 정보를 모두 가져옴.
        int selectedCameraIndex = -1;

        //후면 카메라 찾기
        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing == false)
            {
                selectedCameraIndex = i;
                break;
            }
        }

        //카메라 켜기
        if (selectedCameraIndex >= 0)
        {
            //선택된 후면 카메라를 가져옴.
            camTexture = new WebCamTexture(devices[selectedCameraIndex].name);

            camTexture.requestedFPS = 30; //카메라 프레임설정

            cameraViewImage.texture = camTexture; //영상을 raw Image에 할당.

            camTexture.Play(); // 카메라 시작하기
        }

    }

}