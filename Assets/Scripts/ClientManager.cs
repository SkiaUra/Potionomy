using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Sirenix.OdinInspector;

public class ClientManager : MonoBehaviour {
    [Title("Bindings")]
    [Required] public CanvasManager CanvasManager;

    [Title("Prefab")]
    [Required] public DialogueBubble DialogueBubble;
    [Required] public Button BeginDialogueButton;

    void Start() {
        BeginDialogueButton.onClick
        .AddListener(delegate () {
            PotionRequest SelectedRequest = FindNewRequest();
            DialogueBubble.StartDialogue(SelectedRequest.LinkedClient, SelectedRequest.SentencesRequest);
        });
    }

    public PotionRequest FindNewRequest() {
        ActiveRequest SelectedRequest = CanvasManager.GameProperties.AllRequests
        .Where(i => i.IsDone == false)
        .Where(i => i.PotionRequest.CheckAvailability() == true)
        .FirstOrDefault();
        return SelectedRequest.PotionRequest;
    }


}
