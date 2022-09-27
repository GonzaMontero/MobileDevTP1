using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalManager : MonoBehaviour {
    [SerializeField] TextMeshProUGUI textPj1;
    [SerializeField] TextMeshProUGUI textPj2;

    [SerializeField] GameObject[] imagenGanadores;
    [SerializeField] PointsSaver puntos;
    void Start() {

        puntos = FindObjectOfType<PointsSaver>();
        textPj1.text = "$" + puntos.player1Points;
        textPj2.text = "$" + puntos.player2Points;

        Destroy(puntos.gameObject, 0.33f);
        if (puntos.player1Points > puntos.player2Points) {
            imagenGanadores[0].SetActive(true);
            imagenGanadores[1].SetActive(false);
        }
        else {
            imagenGanadores[0].SetActive(false);
            imagenGanadores[1].SetActive(true);
        }
    }
}
