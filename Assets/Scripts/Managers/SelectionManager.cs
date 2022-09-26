using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] GameplayDataHolder gameplayDataHolder;
    [SerializeField] GameObject modeButtons;
    [SerializeField] GameObject playerAmountButtons;
    [SerializeField] GameObject dificultyButtons;
    [SerializeField] LoadScene loadScreen;

    void Start()
    {
        gameplayDataHolder = GameplayDataHolder.Instance;
        loadScreen = LoadScene.Instance;
    }

    public void SelectPlayButton()
    {
        playerAmountButtons.SetActive(true);
        modeButtons.SetActive(false);
    }

    public void PlayerButtonSelected(int amount)
    {
        if (amount == 1)
            gameplayDataHolder.SetPlayerAmount(GameplayDataHolder.PlayerAmount.SinglePlayer);
        else gameplayDataHolder.SetPlayerAmount(GameplayDataHolder.PlayerAmount.MultiPlayer);

        playerAmountButtons.SetActive(false);
        dificultyButtons.SetActive(true);
    }

    public void DificultyButtonPressed(string button)
    {
        switch (button)
        {
            case "Easy":
                gameplayDataHolder.SetDificultyLevel(GameplayDataHolder.Dificulty.Easy);
                break;
            case "Normal":
                gameplayDataHolder.SetDificultyLevel(GameplayDataHolder.Dificulty.Normal);
                break;
            case "Hard":
                gameplayDataHolder.SetDificultyLevel(GameplayDataHolder.Dificulty.Hard);
                break;
            case "Back":
                dificultyButtons.SetActive(false);
                modeButtons.SetActive(true);
                break;
            default:
                Debug.LogError("Invalid Choice, button unknown");
                break;
        }
        if (button != "Back")
            loadScreen.StartLoadingScene("Tutorial");
    }
}
