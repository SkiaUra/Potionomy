using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class PopupIngredient : PopupController {

    void Awake() {
        this.gameObject.SetActive(false);
    }

    void Start() {
        CloseButton.onClick
        .AddListener(delegate () {
            Hide();
        });
        this.transform.localScale = Vector3.zero;
    }

    public override void Show(RectTransform _RectTransform) {
        if (_RectTransform == null) _RectTransform = this.GetComponent<RectTransform>();

        var Slot = _RectTransform.GetComponent<ShelfSlot>();
        if (Slot.IngreditentInSlot != null && Slot.SlotButton.interactable) {
            DescriptionText.text = Slot.IngreditentInSlot.Description;
            Vector3 screenPosition = this.GetComponent<RectTransform>().position; // pass the world position
            this.transform.localScale = Vector3.zero;
            this.gameObject.SetActive(true);

            // Animation
            this.transform.position = _RectTransform.position; // set the UI Transform's position as it will accordingly adjust the RectTransform values
            this.transform.DOScale(1, 0.2f).SetEase(Ease.OutBack);
            //blockerButtonBG.gameObject.SetActive(true);
        }
    }
}
