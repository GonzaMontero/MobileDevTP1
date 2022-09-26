﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    [SerializeField] ControllerTutorial p1;
    [SerializeField] ControllerTutorial p2;
    [SerializeField] LoadScene pc;
    bool cambiandoEscena;
    [SerializeField] GameObject[] cameras;
    [SerializeField] GameObject principalCamera;

    [SerializeField] GameObject[] botones;
    [SerializeField] Camera camP1;
    [SerializeField] GameObject Escena2;
    GameplayDataHolder mg;
    void Start() {
        principalCamera.SetActive(false);
        mg = FindObjectOfType<GameplayDataHolder>();
        pc = FindObjectOfType<LoadScene>();
        for (int i = 0; i < botones.Length; i++)
            if (botones[i] != null)
                botones[i].SetActive(false);
#if UNITY_EDITOR 
        if (mg.GetPlayerAmount() == GameplayDataHolder.PlayerAmount.SinglePlayer) {
            camP1.rect = new Rect(0, 0, 1, 1);
            Escena2.SetActive(false);
        }
#elif UNITY_ANDROID || UNITY_IOS
        botones[0].SetActive(true);
        if(mg.GetCantJugadores() == ManagerGameplay.CantJugadores.Uno) {
            camP1.rect = new Rect(0, 0, 1, 1);
            Escena2.SetActive(false);
        }
        else{
            botones[4].SetActive(true);
        }
#endif

    }

    // Update is called once per frame
    void Update() {
        if (mg.GetPlayerAmount() == GameplayDataHolder.PlayerAmount.MultiPlayer) {
            if (p1.GetTutorialTerminado() && p2.GetTutorialTerminado() && !cambiandoEscena) {
                cambiandoEscena = true;
                principalCamera.SetActive(true);
                cameras[0].SetActive(false);
                cameras[1].SetActive(false);
                pc.StartLoadingScene("conduccion9");
            }
        }
        else {
            if (p1.GetTutorialTerminado() && !cambiandoEscena) {
                cambiandoEscena = true;
                principalCamera.SetActive(true);
                cameras[0].SetActive(false);
                pc.StartLoadingScene("conduccion9");
            }
        }
    }
}
