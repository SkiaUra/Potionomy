using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

[CreateAssetMenu(fileName = "NewRequest", menuName = "CreateNew/PotionRequest")]
public class PotionRequest : ScriptableObject {
    [Required] public GameProperties GameProperties;

    public int Priority = 0;
    [Required] public Client LinkedClient;
    public List<Condition> ConditionsToBeAvailable = new List<Condition>();

    [TextArea(0, 3)] public List<String> SentencesRequest = new List<string>();
    public List<String> SentencesReturnFromBadReqest = new List<string>();

    public List<Recipe> GoodResultRecipes = new List<Recipe>();
    public List<String> SentencesSucces = new List<string>();

    public List<Recipe> BadResultRecipes = new List<Recipe>();
    public List<String> SentencesFailure = new List<string>();


    // regarde si les conditions de cette requete sont remplie et renvoie true si oui
    public bool CheckAvailability() {
        bool ConditionPassed = true;

        if (ConditionsToBeAvailable.Count == 0) return true;

        foreach (Condition _checkedCondition in ConditionsToBeAvailable) {
            switch (_checkedCondition.CheckIf) {
                case ConditionCheck.RequestDone:
                    ActiveRequest RequestToCheck = GameProperties.AllRequests
                    .Where(i => i.PotionRequest == _checkedCondition.PotionRequest)
                    .FirstOrDefault();

                    if (RequestToCheck.IsDone == false) ConditionPassed = false;
                    break;
                case ConditionCheck.RequestUnlocked:
                    Debug.Log("Bug dans le check des condition de la quête :" + this.name);
                    break;
            }
            if (ConditionPassed == false) return false;
        }
        return true;
    }
}

[Serializable]
public class Condition {
    public ConditionCheck CheckIf;

    [ShowIf("@CheckIf == ConditionCheck.RequestDone || CheckIf == ConditionCheck.RequestUnlocked")]
    public PotionRequest PotionRequest;
}

[Serializable]
public enum ConditionCheck {
    RequestDone,
    RequestUnlocked,
    // ClientStoryAtStep
}