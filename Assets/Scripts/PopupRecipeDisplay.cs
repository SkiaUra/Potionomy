using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;

public class PopupRecipeDisplay : PopupController {

    void Awake() {
        this.gameObject.SetActive(false);
        if (BlackBG) BlackBG.color = Color.clear;
    }

    void Start() {
        CloseButton.onClick
        .AddListener(delegate () {
            Hide();
        });
    }

    public override void Show(Recipe _Recipe) {
        BlackBG.color = Color.clear;
        BlackBG.DOFade(1, 0.3f);

        // Update Text
        this.gameObject.SetActive(true);
        Image.sprite = _Recipe.Sprite;
        NameText.text = _Recipe.name;
        // DescriptionText.text = _Recipe.Description;

        Vector3 screenPosition = this.GetComponent<RectTransform>().position; // pass the world position
        this.transform.localScale = Vector3.one;
        Tween t = this.transform.DOLocalMoveY(250f, 0.5f, true).SetEase(Ease.OutBack);
    }

    public override async void Hide() {
        Tween fade = BlackBG.DOFade(1, 0.3f);
        await UniTask.WaitUntil(() => fade.IsPlaying() == false);
        BlackBG.color = Color.clear;

        Tween t = this.transform.DOScale(0, 0.2f).SetEase(Ease.InBack);
        await UniTask.WaitUntil(() => t.IsPlaying() == false);
        this.gameObject.SetActive(false);
        this.transform.localPosition -= Vector3.up * 250f;
    }
}