using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayDataHolder : MonoBehaviour {

    static public GameplayDataHolder Instance;

    public enum Dificulty {
        Easy,
        Normal,
        Hard
    }
    public Dificulty currentDificulty;

    public enum PlayerAmount {
        SinglePlayer,
        MultiPlayer
    }

    public PlayerAmount currentPlayerAmount;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
        Instance = this;
    }

    public void SetPlayerAmount (PlayerAmount j) {
        currentPlayerAmount = j;
    }
    public PlayerAmount GetPlayerAmount() {
        return currentPlayerAmount;
    }
    
    public void SetDificultyLevel(Dificulty d) {
        currentDificulty = d;
    }
    public Dificulty GetDificultyLevel() {
        return currentDificulty;
    }

}
