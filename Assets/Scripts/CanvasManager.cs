using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;
using System.Linq;

public enum ScreenTypes {
    CLIENTSCREEN,
    CRAFTSCREEN,
    BOOKSCREEN,
    DOORSCREEN
}

public class CanvasManager : MonoBehaviour {

    private static CanvasManager _instance;
    public GameProperties GameProperties;
    public RectTransform ScreensMover;
    public RectTransform ClientScreen;
    public RectTransform CraftScreen;

    public List<ScreenController> ScreensList = new List<ScreenController>();

    public ScreenTypes DisplayedScreen;

    public Button LeftScreenButton;
    public Button RightScreenButton;

    [Required] public RectTransform PopupInfo;
    [Required] public PopupRecipeDisplay PopupRecipeDisplay;

    void Awake() {
        _instance = this;
        LeftScreenButton.onClick
        .AddListener(delegate () {
            MoveScreens(1);
        });

        RightScreenButton.onClick
        .AddListener(delegate () {
            MoveScreens(-1);
        });

    }


    public void ChangeScreen(ScreenTypes _Screen) {
        ScreensList.FirstOrDefault(screen => screen.ScreenType == DisplayedScreen).HideScreen();
        ScreensList.FirstOrDefault(screen => screen.ScreenType == _Screen).DisplayScreen();
    }

    public void SetupScreens() {
        ClientScreen.localPosition = Vector3.zero;
        CraftScreen.localPosition = Vector3.right * this.GetComponent<RectTransform>().rect.width;
    }

    public void MoveScreens(int _Way) {
        // ScreensMover.localPosition += Vector3.right * this.GetComponent<RectTransform>().rect.width * _Way;
        float endPos = ScreensMover.localPosition.x + this.GetComponent<RectTransform>().rect.width * _Way;
        ScreensMover.DOLocalMoveX(endPos, 0.3f, true).SetEase(Ease.OutExpo);

    }
}
