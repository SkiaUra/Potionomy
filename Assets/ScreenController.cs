using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ScreenController : MonoBehaviour {
    public CanvasGroup CanvasGroup;

    public ScreenTypes ScreenType;
    public Vector3 Angle;

    void Start() {
        CanvasGroup.alpha = 0;
    }

    public void DisplayScreen() {
        CanvasGroup.DOFade(1, 0.2f);
    }

    public void HideScreen() {
        CanvasGroup.DOFade(0, 0.2f);
    }

}
