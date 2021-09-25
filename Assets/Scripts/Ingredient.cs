using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "NewIngredient", menuName = "CreateNew/Ingredient")]
public class Ingredient : ScriptableObject {

    [PreviewField(Alignment = ObjectFieldAlignment.Right)] public Sprite Sprite;

    public string Name;
    [TextArea(2, 5)] public string Description;

    public IngredientFamily Family;

    public IngredientProperties FirstProperty;
    // public IngredientProperties SecondProperty;


}
