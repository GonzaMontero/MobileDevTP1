using UnityEngine;

/// <summary>
/// clase encargada de TODA la visualizacion
/// de cada player, todo aquello que corresconda a 
/// cada seccion de la pantalla independientemente
/// </summary>
public class Visualizer : MonoBehaviour {
    public enum Side { Left, Right }
    public Side currentSide;

    DirectionControl dirControl;
    Player thisPlayer;

    public Camera drivingCamera;
    public Camera UnloadingCamera;


    public Vector2[] moneyPosition;
    public Vector2 moneyScale = Vector2.zero;

    public GUISkin GS_Din;

    public Vector2[] wheelPos;
    public float WheelScale = 0;

    public GUISkin GS_Volante;

    public Vector2[] bgPos;
    public Vector2 bgScale = Vector2.zero;

    public Texture2D emptyTexture;
    public Texture2D bgTexture;

    public float flashing = 0.8f;
    public float TempParp = 0;
    public bool PrimIma = true;

    public Texture2D[] invLeftTex;
    public Texture2D[] invRightTex;

    public GUISkin GS_Inv;

    public Vector2 BonusPos = Vector2.zero;
    public Vector2 BonusEsc = Vector2.zero;

    public Color32 ColorFondoBolsa;
    public Vector2 ColorFondoPos = Vector2.zero;
    public Vector2 ColorFondoEsc = Vector2.zero;

    public Vector2 ColorFondoFondoPos = Vector2.zero;
    public Vector2 ColorFondoFondoEsc = Vector2.zero;

    public GUISkin GS_FondoBonusColor;
    public GUISkin GS_FondoFondoBonusColor;
    public GUISkin GS_Bonus;


    //CALIBRACION MAS TUTO BASICO
    public Vector2 ReadyPos = Vector2.zero;
    public Vector2 ReadyEsc = Vector2.zero;
    public Texture2D[] ImagenesDelTuto;
    public float Intervalo = 0.8f;//tiempo de cada cuanto cambia de imagen
    float TempoIntTuto = 0;
    int EnCurso = -1;
    public Texture2D ImaEnPosicion;
    public Texture2D ImaReady;
    public GUISkin GS_TutoCalib;

    //NUMERO DEL JUGADOR
    public Texture2D TextNum1;
    public Texture2D TextNum2;
    public GameObject Techo;




    Rect R;

    Renderer techoRend;

    void Start() {
        TempoIntTuto = Intervalo;

        dirControl = thisPlayer.dirControl;

        thisPlayer = GetComponent<Player>();
        techoRend = Techo.GetComponent<Renderer>();
    }

    void OnGUI() {
        switch (thisPlayer.currentState) {
            case Player.States.Driving:
                SetInv3();
                SetMoney();
                SetWheel();
                break;
            case Player.States.Dropping:
                SetInv3();
                SetBonus();
                SetMoney();
                break;
            case Player.States.Tutorial:
                SetInv3();
                SetWheel();
                break;
        }

        GUI.skin = null;
    }

    public void CambiarAConduccion() {

        drivingCamera.enabled = true;
        UnloadingCamera.enabled = false;
    }

    public void CambiarADescarga() {
        drivingCamera.enabled = false;
        UnloadingCamera.enabled = true;
    }

    void SetBonus() {
        if (thisPlayer.descentController.PEnMov != null) {
            //el fondo
            GUI.skin = GS_FondoFondoBonusColor; 

            R.width = ColorFondoFondoEsc.x * Screen.width / 100;
            R.height = ColorFondoFondoEsc.y * Screen.height / 100;
            R.x = ColorFondoFondoPos.x * Screen.width / 100;
            R.y = ColorFondoFondoPos.y * Screen.height / 100;
            if (currentSide == Visualizer.Side.Right)
                R.x += (Screen.width) / 2;
            GUI.Box(R, "");


            //el fondo
            GUI.skin = GS_FondoBonusColor;

            R.width = ColorFondoEsc.x * Screen.width / 100;
            R.height = (ColorFondoEsc.y * Screen.height / 100) * (thisPlayer.descentController.Bonus / (int)Pallet.Values.Value2);
            R.x = ColorFondoPos.x * Screen.width / 100;
            R.y = (ColorFondoPos.y * Screen.height / 100) - R.height;
            if (currentSide == Visualizer.Side.Right)
                R.x += (Screen.width) / 2;
            GUI.Box(R, "");


            //la bolsa
            GUI.skin = GS_Bonus;

            R.width = BonusEsc.x * Screen.width / 100;
            R.height = R.width / 2;
            R.x = BonusPos.x * Screen.width / 100;
            R.y = BonusPos.y * Screen.height / 100;
            if (currentSide == Visualizer.Side.Right)
                R.x += (Screen.width) / 2;
            GUI.Box(R, "     $" + thisPlayer.descentController.Bonus.ToString("0"));
        }
    }

    void SetMoney() {
        GUI.skin = GS_Din;

        R.width = moneyScale.x * Screen.width / 100;
        R.height = moneyScale.y * Screen.height / 100;
        R.x = moneyPosition[0].x * Screen.width / 100;
        R.y = moneyPosition[0].y * Screen.height / 100;
        if (currentSide == Visualizer.Side.Right)
            R.x = moneyPosition[1].x * Screen.width / 100;
        //R.x = (Screen.width) - (Screen.width/2) - R.x;
        GUI.Box(R, "$" + PrepareNumber(thisPlayer.moneyGained));
    }

    void SetWheel() {
        GUI.skin = GS_Volante;

        R.width = WheelScale * Screen.width / 100;
        R.height = WheelScale * Screen.width / 100;
        R.x = wheelPos[0].x * Screen.width / 100;
        R.y = wheelPos[0].y * Screen.height / 100;

        if (currentSide == Visualizer.Side.Right)
            R.x = wheelPos[1].x * Screen.width / 100;
        //R.x = (Screen.width) - ((Screen.width/2) - R.x);

        Vector2 centro;
        centro.x = R.x + R.width / 2;
        centro.y = R.y + R.height / 2;
        float angulo = 100 * dirControl.GetTurn();

        GUIUtility.RotateAroundPivot(angulo, centro);

        GUI.Box(R, "");

        GUIUtility.RotateAroundPivot(angulo * (-1), centro);
    }

    void SetInv3() {
        GUI.skin = GS_Inv;

        R.width = bgScale.x * Screen.width / 100;
        R.height = bgScale.y * Screen.width / 100;
        R.x = bgPos[0].x * Screen.width / 100;
        R.y = bgPos[0].y * Screen.height / 100;

        int contador = 0;
        for (int i = 0; i < 3; i++) {
            if (thisPlayer.bags[i] != null)
                contador++;
        }

        if (currentSide == Visualizer.Side.Right) {
            //R.x = (Screen.width) - (Screen.width/2) - R.x;
            R.x = bgPos[1].x * Screen.width / 100;

            if (contador < 3)
                GS_Inv.box.normal.background = invRightTex[contador];
            else {
                TempParp += Time.deltaTime;

                if (TempParp >= flashing) {
                    TempParp = 0;
                    if (PrimIma)
                        PrimIma = false;
                    else
                        PrimIma = true;
                }

                if (PrimIma) {
                    GS_Inv.box.normal.background = invRightTex[3];
                }
                else {
                    GS_Inv.box.normal.background = invRightTex[4];
                }

            }
        }
        else {
            if (contador < 3)
                GS_Inv.box.normal.background = invLeftTex[contador];
            else {
                TempParp += Time.deltaTime;

                if (TempParp >= flashing) {
                    TempParp = 0;
                    if (PrimIma)
                        PrimIma = false;
                    else
                        PrimIma = true;
                }

                if (PrimIma) {
                    GS_Inv.box.normal.background = invLeftTex[3];
                }
                else {
                    GS_Inv.box.normal.background = invLeftTex[4];
                }
            }
        }

        GUI.Box(R, "");
    }

    public string PrepareNumber(int dinero) {
        string strDinero = dinero.ToString();
        string res = "";

        if (dinero < 1)
        {
            res = "";
        }
        else if (strDinero.Length == 6)
       {
            for (int i = 0; i < strDinero.Length; i++) {
                res += strDinero[i];

                if (i == 2) {
                    res += ".";
                }
            }
        }
        else if (strDinero.Length == 7)
       {
            for (int i = 0; i < strDinero.Length; i++) {
                res += strDinero[i];

                if (i == 0 || i == 3) {
                    res += ".";
                }
            }
        }

        return res;
    }
}