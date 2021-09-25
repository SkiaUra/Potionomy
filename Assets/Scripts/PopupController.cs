using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;

public class PopupController : MonoBehaviour {
    [Required] public CanvasManager CanvasManager;

    [Required] public TextMeshProUGUI NameText;
    [Required] public TextMeshProUGUI DescriptionText;
    [Required] public Image Image;
    [Required] public Button CloseButton;
    public Image BlackBG;

    void Awake() {
        if (BlackBG) BlackBG.color = Color.clear;
    }

    void Start() {
        this.gameObject.SetActive(false);
    }

    public virtual void Show() {
    }

    public virtual void Show(RectTransform _RectTransform) {
    }

    public virtual void Show(Recipe _Recipe) {
    }

    public virtual async void Hide() {
        Tween t = this.transform.DOScale(0, 0.2f).SetEase(Ease.InBack);
        await UniTask.WaitUntil(() => t.IsPlaying() == false);
        this.gameObject.SetActive(false);
    }
}
