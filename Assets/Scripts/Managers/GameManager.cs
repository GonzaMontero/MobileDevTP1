using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float gameTime = 60;

    public enum GameState { Calibrating, Playing, Finished }
    public GameState CurrentState = GameState.Playing;

    public PlayerInfo PlayerInfo1 = null;
    public PlayerInfo PlayerInfo2 = null;

    public Player Player1;
    public Player Player2;


    [SerializeField] GameObject[] objectsToDisableEasy;
    [SerializeField] GameObject[] objectsToDisableNormal;
    [SerializeField] GameObject[] objectsToDisableHard;

    [SerializeField] GameplayDataHolder gameplayDataHolder;
    [SerializeField] LoadScene loadingScene;

    [SerializeField] GameObject[] player2Objects;

    [SerializeField] Camera mainPlayer1Camera;
    [SerializeField] Camera mainDeliveryPlayer1Camera;

    void Awake()
    {
        Instance = this;
        loadingScene = LoadScene.Instance;
        gameplayDataHolder = GameplayDataHolder.Instance;
    }

    void Start()
    {
        StartRace();
        
        StartCoroutine(Play());       

        if (gameplayDataHolder != null)

            switch (gameplayDataHolder.GetDificultyLevel())
            {
                case GameplayDataHolder.Dificulty.Easy:
                    GameObjectEnabler(objectsToDisableEasy, false);
                    break;
                case GameplayDataHolder.Dificulty.Normal:
                    GameObjectEnabler(objectsToDisableNormal, false);
                    break;
                case GameplayDataHolder.Dificulty.Hard:
                    GameObjectEnabler(objectsToDisableHard, false);
                    break;
            }

        GameObjectEnabler(player2Objects, false);

        if (gameplayDataHolder.GetPlayerAmount() == GameplayDataHolder.PlayerAmount.MultiPlayer)
        {
            GameObjectEnabler(player2Objects, true);
        }
        else
        {
            GameObjectEnabler(player2Objects, false);
            mainPlayer1Camera.rect = new Rect(0f, 0f, 1f, 1f);
            mainDeliveryPlayer1Camera.rect = new Rect(0f, 0f, 1f, 1f);
        }
    }

    void GameObjectEnabler(GameObject[] listToEnable, bool enabled)
    {
        for (int i = 0; i < listToEnable.Length; i++)
            if (listToEnable[i] != null)
                listToEnable[i].SetActive(enabled);
    }

    IEnumerator Play()
    {
        yield return new WaitForSeconds(gameTime);
        FinishRace();
        if (loadingScene != null)
            loadingScene.StartLoadingScene("Final Score");
        StopCoroutine(Play());
        yield return null;
    }

    void StartRace()
    {
        Player1.slowing.RestoreVelocity();
        Player1.dirControl.Habilitado = true;

        Player2.slowing.RestoreVelocity();
        Player2.dirControl.Habilitado = true;
    }

    void FinishRace()
    {
        CurrentState = GameManager.GameState.Finished;

        gameTime = 0;

        if (Player1.moneyGained > Player2.moneyGained)
        {
            if (PlayerInfo1.LadoAct == Visualizer.Side.Right)
                MatchData.WinnerSide = MatchData.Side.Der;
            else
                MatchData.WinnerSide = MatchData.Side.Izq;

            MatchData.WinnerPoints = Player1.moneyGained;
            MatchData.LoserPoints = Player2.moneyGained;
        }
        else
        {
            if (PlayerInfo2.LadoAct == Visualizer.Side.Right)
                MatchData.WinnerSide = MatchData.Side.Der;
            else
                MatchData.WinnerSide = MatchData.Side.Izq;

            MatchData.WinnerPoints = Player2.moneyGained;
            MatchData.LoserPoints = Player1.moneyGained;
        }

        Player1.GetComponent<SlowingManager>().Stop();
        if (gameplayDataHolder.GetPlayerAmount() == GameplayDataHolder.PlayerAmount.MultiPlayer)
            Player2.GetComponent<SlowingManager>().Stop();

        Player1.descentController.GameFinished();
        if (gameplayDataHolder.GetPlayerAmount() == GameplayDataHolder.PlayerAmount.MultiPlayer)
            Player2.descentController.GameFinished();
    }

    [System.Serializable]
    public class PlayerInfo
    {
        public PlayerInfo(int inputType, Player player)
        {
            this.inputType = inputType;
            this.player = player;
        }

        public bool finishCalibration = false;
        public bool finishTutorial1 = false;
        public bool finishTutorial2 = false;

        public Visualizer.Side LadoAct;

        public int inputType = -1;

        public Player player;
    }
}

