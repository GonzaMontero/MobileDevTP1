using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGameplay : MonoBehaviour {

    static ManagerGameplay instancia { get; set; }
    public enum Dificultad {
        Easy,
        Normal,
        Dificil
    }
    public Dificultad dificultad;

    public enum CantJugadores {
        Uno,
        Dos
    }

    public CantJugadores cantJug;

    private void Awake() {
        if (instancia != null) {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
        instancia = this;
    }

    public void SetCantJugadores (CantJugadores j) {
        cantJug = j;
    }
    public CantJugadores GetCantJugadores() {
        return cantJug;
    }
    
    public void SetDificultad(Dificultad d) {
        dificultad = d;
    }
    public Dificultad GetDificultad() {
        return dificultad;
    }

}
