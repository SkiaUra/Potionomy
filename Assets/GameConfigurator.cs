using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfigurator : MonoBehaviour {
    private static GameConfigurator _instance;

    public GameProperties GameProperties;

    private void awake() {
        _instance = this;
    }
}
