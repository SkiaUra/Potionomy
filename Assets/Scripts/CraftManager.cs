using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Sirenix.OdinInspector;

public class CraftManager : MonoBehaviour {

    [Required] public CanvasManager CanvasManager;

    public List<Ingredient> IngredientList = new List<Ingredient>();

    [BoxGroup("Shelves")] [Required] public GridLayoutGroup ShelfLayout;
    [BoxGroup("Shelves")] [Required] public GridLayoutGroup CauldronLayout;

    [BoxGroup("Shelves")] public ShelfSlot[] ShelfSlots;
    [BoxGroup("Shelves")] public ShelfSlot[] CauldronSlots;

    [Button]
    [BoxGroup("Shelves")]
    private void SetupShelvesSlots() {
        Array.Clear(ShelfSlots, 0, ShelfSlots.Length);
        Array.Resize(ref ShelfSlots, ShelfLayout.transform.childCount);
        int i = 0;
        foreach (Transform child in ShelfLayout.transform) {
            ShelfSlot slot = child.GetComponent<ShelfSlot>();
            ShelfSlots[i] = slot;
            i++;
        }

        Array.Clear(CauldronSlots, 0, CauldronSlots.Length);
        Array.Resize(ref CauldronSlots, CauldronLayout.transform.childCount);
        i = 0;
        foreach (Transform child in CauldronLayout.transform) {
            ShelfSlot slot = child.GetComponent<ShelfSlot>();
            CauldronSlots[i] = slot;
            i++;
        }
        Debug.LogWarning("Update Shelves !");
    }

    [Title("Craft")]
    public Button CraftButton;

    [Required] RectTransform PotionPopup;
    [Required] TextMeshProUGUI TextPotion;


    void Start() {
        AddIngredients();
        CraftButton.onClick
        .AddListener(delegate () {
            CookPotion();
        });
    }

    void AddIngredients() {
        for (int i = 0; i < ShelfSlots.Length; i++) {
            ShelfSlots[i].CraftManager = this;
            ShelfSlots[i].ClearSlot();
            if (i < IngredientList.Count) {
                Ingredient ing = IngredientList[i];
                ShelfSlots[i].UpdateSlot(ing);
            } else {
                ShelfSlots[i].SlotButton.interactable = false;
            }
            ShelfSlots[i].SlotType = ShelfSlot.ShelfSlotType.SHELF;
        }
        foreach (ShelfSlot slot in CauldronSlots) {
            slot.CraftManager = this;
            slot.ClearSlot();
            slot.SlotType = ShelfSlot.ShelfSlotType.CAULDRON;
        }
    }

    public void CookPotion() {
        // check si les ingrédients correspondent à une potion dans la liste.
        if (!CauldronSlots[0].IngreditentInSlot || !CauldronSlots[1].IngreditentInSlot) return;
        List<Recipe> RecipeList = CanvasManager.GameProperties.RecipesList;

        // get properties in the cauldron
        IngredientProperties a = CauldronSlots[0].IngreditentInSlot.FirstProperty;
        IngredientProperties b = CauldronSlots[1].IngreditentInSlot.FirstProperty;

        var CraftedRecipe = RecipeList.FirstOrDefault(recipe => recipe.Properties.Contains(a) && recipe.Properties.Contains(b));

        if (CraftedRecipe != null) {
            Debug.Log("New potion crafted: " + CraftedRecipe.name);
            // Show the UI
            CanvasManager.PopupRecipeDisplay.Show(CraftedRecipe);

            foreach (ShelfSlot slot in CauldronSlots) {
                slot.ClearSlot();
            }
            foreach (ShelfSlot slot in ShelfSlots) {
                if (!slot.SlotButton.interactable) {
                    slot.ClearSlot();
                    Debug.LogWarning("Clear " + slot);
                }
            }
        } else {
            Debug.Log("No potion match these ingredients");
        }

    }

    public void SwapItem(ShelfSlot _Item) {
        if (_Item.SlotType == ShelfSlot.ShelfSlotType.SHELF) {
            if (CauldronSlots[0].IngreditentInSlot == null) {
                //Debug.Log("Add " + _Item.IngreditentInSlot.name + " in the 1st slot");
                CauldronSlots[0].UpdateSlot(_Item.IngreditentInSlot);
                CauldronSlots[0].MemSlotCauldron = _Item;
                _Item.SlotButton.interactable = false;
                _Item.Sprite.enabled = false;
            } else {
                if (CauldronSlots[1].IngreditentInSlot == null) {
                    //Debug.Log("Add " + _Item.IngreditentInSlot.name + " in the 2st slot");
                    CauldronSlots[1].UpdateSlot(_Item.IngreditentInSlot);
                    CauldronSlots[1].MemSlotCauldron = _Item;
                    _Item.SlotButton.interactable = false;
                    _Item.Sprite.enabled = false;
                } else {
                    Debug.Log("Cauldron is full");
                }
            }
        } else {
            // click on cauldron slot
            if (_Item.IngreditentInSlot == null) return;
            _Item.MemSlotCauldron.SlotButton.interactable = true;
            _Item.MemSlotCauldron.Sprite.enabled = true;
            _Item.ClearSlot();
        }

    }
}