using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;


public class CameraController : MonoBehaviour {

    public Vector2 StartTouchPos;
    public Vector2 CurrentTouchPos;
    public Vector2 EndTouchPos;
    public bool StopTouch = false;
    public bool IsCameraRotating = false;
    public float percent;

    [Range(0, 1)] public float SwipePercent;
    public float RotationSpeed;
    public Ease RotationEasing;


    public void OnMouseDown() {
        StartTouchPos = Input.mousePosition;
    }

    public void OnMouseDrag() {
        CurrentTouchPos = Input.mousePosition;
    }

    void Update() {
        Swipe();
    }

    public void Swipe() {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("down");
            StartTouchPos = Vector2.zero;
            StartTouchPos = Input.mousePosition;
        }

        CurrentTouchPos = Input.mousePosition;

        if (Input.GetMouseButtonUp(0)) {
            Debug.Log("Up");
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
}