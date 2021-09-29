using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;


public class CameraController : MonoBehaviour {

    private static CameraController _instance;
    public Vector2 StartTouchPos;
    public Vector2 CurrentTouchPos;
    public Vector2 EndTouchPos;
    public bool LockCamera = false;
    public bool IsCameraRotating = false;
    public float percent;

    [Range(0, 1)] public float SwipePercent;
    public float RotationSpeed;
    public Ease RotationEasing;

    void Awake() {
        _instance = this;
    }

    void Update() {
        Swipe();
    }

    public void Swipe() {
        if (LockCamera) return;
        if (Input.GetMouseButtonDown(0)) {
            StartTouchPos = Vector2.zero;
            StartTouchPos = Input.mousePosition;
        }

        CurrentTouchPos = Input.mousePosition;

        if (Input.GetMouseButtonUp(0)) {
            EndTouchPos = CurrentTouchPos;
            float distX = EndTouchPos.x - StartTouchPos.x;

            // Calcul du mvt par rapport a la taille de l'ecran
            // Screen.width;
            percent = distX / Screen.width;

            Vector3 endRot = Vector3.zero;
            if (percent >= SwipePercent) {
                endRot = transform.rotation.eulerAngles + Vector3.up * 90f;
                RotateCamera(endRot);
            } else if (percent <= -SwipePercent) {
                endRot = transform.rotation.eulerAngles + Vector3.up * -90f;
                RotateCamera(endRot);
            }
        }
    }

    public async UniTask RotateCamera(Vector3 _FinalRotation) {
        if (!IsCameraRotating) {
            IsCameraRotating = true;
            Tween TweenCam = transform.DORotate(_FinalRotation, RotationSpeed).SetEase(RotationEasing);
            await UniTask.WaitUntil(() => TweenCam.IsPlaying() == false);
            IsCameraRotating = false;
        }
    }

    public static void ToggleCamLock() {
        _instance.LockCamera = !_instance.LockCamera;
    }

    public static void CancelSwipe() {
        _instance.percent = 0f;
        _instance.StartTouchPos = Vector2.zero;
        _instance.CurrentTouchPos = Vector2.zero;
        _instance.EndTouchPos = Vector2.zero;
    }
}