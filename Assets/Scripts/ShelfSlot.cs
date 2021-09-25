using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ShelfSlot : MonoBehaviour {

    public enum ShelfSlotType {
        SHELF,
        CAULDRON
    }
    public CraftManager CraftManager;
    public Ingredient IngreditentInSlot;
    public ShelfSlotType SlotType;
    public ShelfSlot MemSlotCauldron;

    [Title("Prefab")]
    [Required] public Image Sprite;
    [Required] public Button SlotButton;
    [Required] public ButtonLongPress ButtonLongPress;

    void Start() {
        SlotButton.onClick
        .AddListener(delegate () {
            CraftManager.SwapItem(this);
        });
    }

    public void ClearSlot() {
        Sprite.enabled = false;
        IngreditentInSlot = null;
        MemSlotCauldron = null;
    }

    public void UpdateSlot(Ingredient _IngredientToSlot) {
        Sprite.sprite = _IngredientToSlot.Sprite;
        Sprite.enabled = true;
        IngreditentInSlot = _IngredientToSlot;
    }
}
