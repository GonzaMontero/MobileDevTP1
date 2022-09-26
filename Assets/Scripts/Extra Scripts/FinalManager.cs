using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalManager : MonoBehaviour {
    [SerializeField] TextMeshProUGUI textPj1;
    [SerializeField] TextMeshProUGUI textPj2;

    [SerializeField] GameObject[] imagenGanadores;
    [SerializeField] GuardarPuntos puntos;
    void Start() {

        puntos = FindObjectOfType<GuardarPuntos>();
        textPj1.text = "$" + puntos.puntosP1;
        textPj2.text = "$" + puntos.puntosP2;

        Destroy(puntos.gameObject, 0.33f);
        if (puntos.puntosP1 > puntos.puntosP2) {
            imagenGanadores[0].SetActive(true);
            imagenGanadores[1].SetActive(false);
        }
        else {
            imagenGanadores[0].SetActive(false);
            imagenGanadores[1].SetActive(true);
        }
    }
}
