using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Sirenix.OdinInspector;
using Cysharp.Threading.Tasks;

public class DialogueBubble : MonoBehaviour {

    [Required] public ClientManager ClientManager;
    private Queue<string> SentencesQueue;

    [Title("Animation Properties")]
    public float DisplaySpeed = 0.2f;
    public float PunchDuration = 0.2f;
    public float PunchIntensity = 0.1f;
    public int PuchVibration = 1;
    public float PunchElasticity = 0.1f;

    [Title("Prefab")]
    [Required] public TextMeshProUGUI DialogueText;
    [Required] public Button ContinueDialogueButton;

    RectTransform rect;

    void Start() {
        this.gameObject.SetActive(false);
        SentencesQueue = new Queue<string>();

        rect = this.GetComponent<RectTransform>();
        ContinueDialogueButton.onClick
        .AddListener(delegate () {
            DisplayNextSentence(true);
        });
    }

    void OnEnable() {
        Show();
    }

    public async void StartDialogue(Client _Client, List<string> _DialogueList) {
        Debug.LogWarning("Starting dialogue with " + _Client);
        SentencesQueue.Clear();
        DialogueText.text = " ";

        foreach (string sentence in _DialogueList) {
            SentencesQueue.Enqueue(sentence);
        }
        await Show();
        DisplayNextSentence(false);
    }

    public void DisplayNextSentence(bool playAnimation) {
        if (SentencesQueue.Count <= 0) {
            EndDialogue();
            return;
        }
        string sentence = SentencesQueue.Dequeue();
        DialogueText.text = sentence;
        if (playAnimation) NextSentenceAnimation();
    }

    void EndDialogue() {
        Hide();
        DialogueText.text = " ";
        Debug.Log("End of conversation");
    }

    public async UniTask Show() {
        rect.localScale = Vector3.zero;
        this.gameObject.SetActive(true);
        Tween t = rect.DOScale(1, 0.2f).SetEase(Ease.OutBack);
        await UniTask.WaitUntil(() => t.IsPlaying() == false);
        rect.localScale = Vector3.one;
    }

    public async UniTask NextSentenceAnimation() {
        Vector2 memAnchor = rect.pivot;
        CustomMethods.SetPivot(rect, Vector2.one * 0.5f);
        Tween t = rect.DOPunchScale(Vector3.one * PunchIntensity, PunchDuration, PuchVibration, PunchElasticity);
        await UniTask.WaitUntil(() => t.IsPlaying() == false);
        CustomMethods.SetPivot(rect, memAnchor);
    }

    public async void Hide() {
        Tween t = rect.DOScale(0, 0.2f).SetEase(Ease.InBack);
        await UniTask.WaitUntil(() => t.IsPlaying() == false);
        this.gameObject.SetActive(false);
    }
}