using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalletMover : PalletManager {

    public MoveType miInput;
    public enum MoveType {
        WASD,
        Arrows
    }

    public PalletManager Desde, Hasta;
    bool segundoCompleto = false;
    bool terminado;
    [SerializeField] GameObject[] botones;

    private void Start() {
#if UNITY_EDITOR
        for (int i = 0; i < botones.Length; i++)
            if (botones[i] != null)
                botones[i].SetActive(false);
#elif UNITY_ANDROID || UNITY_IOS
        for (int i = 0; i < botones.Length; i++)
            if (botones[i] != null)
                botones[i].SetActive(false);
        if (botones[0] != null)
            botones[0].SetActive(true);
#endif

    }

    private void Update() {
        switch (miInput) {
            case MoveType.WASD:
                if (!HasPallets() && Desde.HasPallets() && Input.GetKeyDown(KeyCode.A)) {
                    PrimerPaso();
                }
                if (HasPallets() && Input.GetKeyDown(KeyCode.S)) {
                    SegundoPaso();
                }
                if (segundoCompleto && HasPallets() && Input.GetKeyDown(KeyCode.D)) {
                    TercerPaso();
                }
                break;
            case MoveType.Arrows:
                if (!HasPallets() && Desde.HasPallets() && Input.GetKeyDown(KeyCode.LeftArrow)) {
                    PrimerPaso();
                }
                if (HasPallets() && Input.GetKeyDown(KeyCode.DownArrow)) {
                    SegundoPaso();
                }
                if (segundoCompleto && HasPallets() && Input.GetKeyDown(KeyCode.RightArrow)) {
                    TercerPaso();
                }
                break;
            default:
                break;
        }
    }

    public void BotonBolsa(int boton) {
        if (!HasPallets() && Desde.HasPallets() && boton == 1) {
            PrimerPaso();
            botones[1].SetActive(true);
            segundoCompleto = false;
        }
        else if (HasPallets() && boton == 2) {
            SegundoPaso();
            botones[2].SetActive(true);
            segundoCompleto = true;
        }
        else if (segundoCompleto && HasPallets() && boton == 3) {
            TercerPaso();
            botones[1].SetActive(false);
            botones[2].SetActive(false);
            segundoCompleto = false;
        }
    }

    void PrimerPaso() {
        Desde.Give(this);
        segundoCompleto = false;
    }
    void SegundoPaso() {
        base.Pallets[0].transform.position = transform.position;
        segundoCompleto = true;
    }
    void TercerPaso() {
        Give(Hasta);
        segundoCompleto = false;
    }

    public override void Give(PalletManager receptor) {
        if (HasPallets()) {
            if (receptor.RecievePallet(Pallets[0])) {
                Pallets.RemoveAt(0);
            }
        }
    }
    public override bool RecievePallet(Pallet pallet) {
        if (!HasPallets()) {
            pallet.porter = this.gameObject;
            base.RecievePallet(pallet);
            return true;
        }
        else
            return false;
    }
}
