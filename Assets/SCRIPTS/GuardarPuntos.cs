using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuardarPuntos : MonoBehaviour {

    static GuardarPuntos instancia { get; set; }

    public float puntosP1;
    public float puntosP2;

    [SerializeField] Player p1;
    [SerializeField] Player p2;

    private void Awake() {
        if(instancia != null) {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
        instancia = this;
    }

    void Update() {
        if (p1 != null)
            puntosP1 = p1.Dinero;
        if (p2 != null)
            puntosP2 = p2.Dinero;
    }
}
