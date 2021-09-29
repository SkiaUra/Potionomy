using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Cysharp.Threading.Tasks;
using System;

public class IngredientAsset : MonoBehaviour {
    public enum ShelfSlotType {
        SHELF,
        CAULDRON
    }
    public CraftManager CraftManager;
    public Ingredient IngreditentInSlot;
    public ShelfSlotType SlotType;
    public ShelfSlot MemSlotCauldron;

    public Transform SlotTransform;
    public GameConfigurator GameConfigurator;
    public GameProperties GameProperties;

    [Title("Input")]
    public bool IsDragging = false;
    private DateTime FirstInput;

    [Title("Prefab")]
    [Required] public Collider Collider;



    private float mZCoord;
    private Vector3 mOffset;

    void Start() {
        GameProperties = GameConfigurator.GameProperties;
        MoveSlotTo(SlotTransform);
    }

    void Update() {
        /*
        if (!IsDragging && DateTime.Now > FirstInput + TimeSpan.FromMilliseconds(300)) {
            CameraController.CancelSwipe();
            CameraController.ToggleCamLock();
        }
        if (IsDragging) {
            transform.position = GetMouseAsWorldPoint();
        }*/
    }

    void OnMouseDown() {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();

        FirstInput = DateTime.Now;
        CameraController.CancelSwipe();
        CameraController.ToggleCamLock();
    }

    async UniTask OnMouseUp() {
        IsDragging = false;
        // Si la distance avec le Cauldron > 10, je resnap l'ingrédient, sinon je le slot dans le cauldron
        float ReleaseDist = Vector3.Distance(this.transform.position, SlotTransform.position);
        Debug.Log("Release distance : " + ReleaseDist);
        if (ReleaseDist >= GameProperties.MinDistancetoReleaseOnCauldron) {
            MoveSlotTo(SlotTransform);
        }

        await UniTask.Delay(3);
        CameraController.CancelSwipe();
        CameraController.ToggleCamLock();
    }

    void OnMouseDrag() {
        // actualiser la pos de l'asset avec la mouse pos
        CameraController.CancelSwipe();
        transform.position = GetMouseAsWorldPoint();
    }

    private Vector3 GetMouseAsWorldPoint() {
        Vector3 mousePoint = Input.mousePosition;
        // z coordinate of game object on screen
        mousePoint.z = mZCoord;
        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    public void ClearSlot() {
        IngreditentInSlot = null;
        MemSlotCauldron = null;
    }

    public void UpdateSlot(Ingredient _IngredientToSlot) {
        IngreditentInSlot = _IngredientToSlot;
    }

    public void MoveSlotTo(Transform _Target) {
        //transform.DOMove(_Target.localPosition, 0.2f, true);
        transform.position = _Target.position;
    }
}
