using UnityEngine;

public class PointsSaver : MonoBehaviour {

    static public PointsSaver Instance;

    public float player1Points;
    public float player2Points;

    [SerializeField] Player player1;
    [SerializeField] Player player2;

    private void Awake() {

        if(Instance != null) {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
        Instance = this;
    }

    void Update() {
        if (player1 != null)
            player1Points = player1.moneyGained;
        if (player2 != null)
            player2Points = player2.moneyGained;
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
