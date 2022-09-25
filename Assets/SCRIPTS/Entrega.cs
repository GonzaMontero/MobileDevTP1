using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrega : MonoBehaviour {

    bool moving;
    int faseEntrega = 1;
    bool entregando = false;

    enum Teclas {
        ASDW,
        FLECHAS
    }

    [SerializeField] Teclas t;
    void Start() {

    }

    void Update() {
        switch (t) {
            case Teclas.ASDW:
                if (!moving) {
                    if (faseEntrega == 1) {
                        if (Input.GetKeyDown(KeyCode.A)) {
                            faseEntrega = 2;
                        }
                    }
                    else if (faseEntrega == 2) {
                        if (Input.GetKeyDown(KeyCode.D)) {
                        }
                    }
                }
                break;
            case Teclas.FLECHAS:
                if (!moving) {
                    if (faseEntrega == 1) {
                        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                            faseEntrega = 2;
                        }
                    }
                    else if (faseEntrega == 2) {
                        if (Input.GetKeyDown(KeyCode.RightArrow)) {
                        }
                    }
                }
                break;
        }
    }

}
