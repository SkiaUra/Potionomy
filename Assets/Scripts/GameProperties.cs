using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "GameProperties", menuName = "GameProperties")]
public class GameProperties : SerializedScriptableObject {

    public List<Recipe> RecipesList = new List<Recipe>();

    [TableList] public List<ActiveRequest> AllRequests = new List<ActiveRequest>();
    /*
        [Title("Entity To Spawn")]
        public CreatureAsset[] creatureAssets;

        [Space]
        [BoxGroup("Battle Tester")]
        [Title("Entity To Spawn")]
        public GameObject MeleeEntity;
        [BoxGroup("Battle Tester")]
        public GameObject RangedEntity;
        [BoxGroup("Battle Tester")]
        public GameObject EnemyPrefab;


        [BoxGroup("Battle Tester")]
        [ButtonGroup("Battle Tester/Add Entity")]
        private void Rusher() {
            AllyFormation.Add(MeleeEntity);
        }

        [BoxGroup("Battle Tester")]
        */
}

[Serializable]
public struct ActiveRequest {

    [TableColumnWidth(60, false)] public bool IsDone;

    public PotionRequest PotionRequest;
}