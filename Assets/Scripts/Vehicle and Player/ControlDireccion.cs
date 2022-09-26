using UnityEngine;
using System.Collections;

public class ControlDireccion : MonoBehaviour {
    public enum TipoInput { AWSD, Arrows }
    public TipoInput InputAct;

    public Transform ManoDer;
    public Transform ManoIzq;

    public float MaxAng = 90;
    public float DesSencibilidad = 90;

    float Giro = 0;

    public enum Sentido { Der, Izq }
    Sentido DirAct;

    public bool Habilitado = true;
    //float Diferencia;

    //---------------------------------------------------------//

    // Use this for initialization

    // Update is called once per frame
    void Update() {
        switch (InputAct) {
            case TipoInput.AWSD:
                if (Habilitado) {
                    if (Input.GetKey(KeyCode.A)) {
                        gameObject.SendMessage("SetGiro", -1);
                    }
                    if (Input.GetKey(KeyCode.D)) {
                        gameObject.SendMessage("SetGiro", 1);
                    }
                }
                break;
            case TipoInput.Arrows:
                if (Habilitado) {
                    if (Input.GetKey(KeyCode.LeftArrow)) {
                        gameObject.SendMessage("SetGiro", -1);
 
                    }
                    if (Input.GetKey(KeyCode.RightArrow)) {
                        gameObject.SendMessage("SetGiro", 1);
                    }
                }
                break;
        }
    }

    public float GetGiro() {
        /*
		switch(DirAct)
			{
			case Sentido.Der:
				if(Angulo() <= MaxAng)
					return Angulo() / MaxAng;
				else
					return 1;
				break;
				
			case Sentido.Izq:
				if(Angulo() <= MaxAng)
					return (Angulo() / MaxAng) * (-1);
				else
					return (-1);
				break;
			}
		*/

        return Giro;
    }

    float Angulo() {
        Vector2 diferencia = new Vector2(ManoDer.localPosition.x, ManoDer.localPosition.y)
                           - new Vector2(ManoIzq.localPosition.x, ManoIzq.localPosition.y);

        return Vector2.Angle(diferencia, new Vector2(1, 0));
    }

}
