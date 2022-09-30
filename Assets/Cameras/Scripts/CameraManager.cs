using UnityEngine;
using UnityEngine.Rendering.Universal;
using Cinemachine;
using DG.Tweening;

public class CameraManager : MonoSingleton<CameraManager>
{
    [SerializeField] private Camera mainCamera;
    public Camera MainCamera => mainCamera;

    [SerializeField] private CinemachineVirtualCamera followCamera;

    private UniversalAdditionalCameraData cameraData;

    private Transform followTarget;

    private bool isShaking = false;

    private void Start()
    {
        SetCameraStack();

        followTarget = followCamera.m_Follow;
    }

    private void SetCameraStack()
    {
        cameraData = mainCamera.GetUniversalAdditionalCameraData();

        AddCameraToStack(UIManager.Instance.UICamera);
    }

    private void AddCameraToStack(Camera camera)
    {
        var cameraData = camera.GetUniversalAdditionalCameraData();
        cameraData.renderType = CameraRenderType.Overlay;

        this.cameraData.cameraStack.Add(camera);
    }

    public void ShakeCamera(float camShakeStrength, float camShakeDuration, int camShakeVibrato = 10, float camShakeElasticity = 1f)
    {
        DOTween.Kill("CamShake");

        followTarget.localPosition = Vector3.zero;
        Vector3 shakeDirection = mainCamera.transform.right;

        followTarget.DOPunchPosition(followTarget.InverseTransformDirection(shakeDirection * camShakeStrength), camShakeDuration, camShakeVibrato, camShakeElasticity).SetId("CamShake");
    }
}
