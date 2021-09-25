using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "CreateNew/Recipe")]
public class Recipe : ScriptableObject {

    [PreviewField(Alignment = ObjectFieldAlignment.Right)] public Sprite Sprite;
    public string Name;
    [TextArea(2, 5)] public string Description;
    // public IngredientFamily Family;

    [SerializeField]
    public List<IngredientProperties> Properties = new List<IngredientProperties>(){
        IngredientProperties.NONE,
        IngredientProperties.NONE
    };
}