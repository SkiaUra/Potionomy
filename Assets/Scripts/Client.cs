using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewCustomer", menuName = "CreateNew/Customer")]
public class Client : ScriptableObject {

    public Sprite Sprite;
    public List<StoryStep> StorySteps = new List<StoryStep>();

}

[Serializable]
public class StoryStep {
    public Recipe NeededRecipe;
    public List<String> SentencesList = new List<string>();
}