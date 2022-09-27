using TMPro;
using UnityEngine;

public class EndingManager : MonoBehaviour {
    [SerializeField] TextMeshProUGUI player1Text;
    [SerializeField] TextMeshProUGUI player2Text;

    [SerializeField] GameObject[] winnerImages;
    [SerializeField] PointsSaver pointsSaver;
    void Start() {

        pointsSaver = PointsSaver.Instance;
        player1Text.text = "$" + pointsSaver.player1Points;
        player2Text.text = "$" + pointsSaver.player2Points;

        Destroy(pointsSaver.gameObject, 0.33f);
        if (pointsSaver.player1Points > pointsSaver.player2Points) {
            winnerImages[0].SetActive(true);
            winnerImages[1].SetActive(false);
        }
        else {
            winnerImages[0].SetActive(false);
            winnerImages[1].SetActive(true);
        }
    }
}
