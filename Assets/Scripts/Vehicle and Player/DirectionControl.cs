using UnityEngine;

public class DirectionControl : MonoBehaviour {
    public Player thisPlayer;

    public enum InputType { AWSD, Arrows }
    public InputType currentInput;

    public Transform rightHand;
    public Transform leftHand;

    public float MaxAng = 90;
    public float DesSencibilidad = 90;

    float turn = 0;

    public bool Habilitado = true;

    void Update() {
        switch (currentInput) {
            case InputType.AWSD:
                if (Habilitado) {
                    if (Input.GetKey(KeyCode.A)) {
                        SetTurn(-1f);
                    }
                    if (Input.GetKey(KeyCode.D)) {
                        SetTurn(1);
                    }
                }
                break;
            case InputType.Arrows:
                if (Habilitado) {
                    if (Input.GetKey(KeyCode.LeftArrow)) {
                        SetTurn(-1);
                    }
                    if (Input.GetKey(KeyCode.RightArrow)) {
                        SetTurn(1);
                    }
                }
                break;
        }
    }

    public float GetTurn() {
        return turn;
    }

    public void SetTurn(float g)
    {
        turn = g;
    }
}
