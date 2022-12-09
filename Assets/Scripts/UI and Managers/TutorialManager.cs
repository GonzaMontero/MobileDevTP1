using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    [SerializeField] ControllerTutorial player1TutorialController;
    [SerializeField] ControllerTutorial Player2TutorialController;

    [SerializeField] LoadScene SceneLoader;

    bool switchingScene;

    [SerializeField] GameObject[] cameras;
    [SerializeField] GameObject mainCamera;

    [SerializeField] GameObject[] buttons;
    [SerializeField] Camera player1Camera;
    [SerializeField] GameObject scene1;

    GameplayDataHolder gameplayDataHolder;

    void Start() {
        mainCamera.SetActive(false);

        gameplayDataHolder = GameplayDataHolder.Instance;
        SceneLoader = LoadScene.Instance;

        for (int i = 0; i < buttons.Length; i++)
            if (buttons[i] != null)
                buttons[i].SetActive(false);

#if UNITY_EDITOR 

        if (gameplayDataHolder.GetPlayerAmount() == GameplayDataHolder.PlayerAmount.SinglePlayer) {
            player1Camera.rect = new Rect(0, 0, 1, 1);
            scene1.SetActive(false);
        }

#elif UNITY_ANDROID || UNITY_IOS

        buttons[0].SetActive(true);
        if(gameplayDataHolder.GetPlayerAmount() == GameplayDataHolder.PlayerAmount.SinglePlayer) {
            player1Camera.rect = new Rect(0, 0, 1, 1);
            scene1.SetActive(false);
        }
        else{
            buttons[4].SetActive(true);
        }
#endif
    }

    void Update() {
        if (gameplayDataHolder.GetPlayerAmount() == GameplayDataHolder.PlayerAmount.MultiPlayer) 
        {
            if (player1TutorialController.GetFinishedTutorial() && Player2TutorialController.GetFinishedTutorial() && !switchingScene) {
                switchingScene = true;
                mainCamera.SetActive(true);
                cameras[0].SetActive(false);
                cameras[1].SetActive(false);
                SceneLoader.StartLoadingScene("Main Gameplay");
            }
        }
        else 
        {
            if (player1TutorialController.GetFinishedTutorial() && !switchingScene) {
                switchingScene = true;
                mainCamera.SetActive(true);
                cameras[0].SetActive(false);
                SceneLoader.StartLoadingScene("Main Gameplay");
            }
        }
    }
}
