using UnityEngine;
using System.Collections;

public class Cinta : PalletManager {
    public bool Encendida;//lo que hace la animacion
    bool ConPallet = false;
    public float Velocidad = 1;
    public GameObject Mano;
    public float Tiempo = 0.5f;
    float Tempo = 0;
    Transform ObjAct = null;

    //animacion de parpadeo
    public float Intervalo = 0.7f;
    public float Permanencia = 0.2f;
    float AnimTempo = 0;
    public GameObject ModelCinta;
    public Color32 ColorParpadeo;
    Color32 ColorOrigModel;

    //------------------------------------------------------------//

    void Start() {
        ColorOrigModel = ModelCinta.GetComponent<Renderer>().material.color;
    }

    void Update() {
        //animacion de parpadeo
        if (Encendida) {
            AnimTempo += Time.deltaTime;
            if (AnimTempo > Permanencia) {
                if (ModelCinta.GetComponent<Renderer>().material.color == ColorParpadeo) {
                    AnimTempo = 0;
                    ModelCinta.GetComponent<Renderer>().material.color = ColorOrigModel;
                }
            }
            if (AnimTempo > Intervalo) {
                if (ModelCinta.GetComponent<Renderer>().material.color == ColorOrigModel) {
                    AnimTempo = 0;
                    ModelCinta.GetComponent<Renderer>().material.color = ColorParpadeo;
                }
            }
        }

        //movimiento del pallet
        for (int i = 0; i < Pallets.Count; i++) {
            if (Pallets[i].GetComponent<Renderer>().enabled) {
                if (!Pallets[i].GetComponent<Pallet>().isSmooth) {
                    Pallets[i].GetComponent<Pallet>().enabled = false;
                    Pallets[i].currentTimeOnConveyor += Time.deltaTime;

                    Pallets[i].transform.position += transform.right * Velocidad * Time.deltaTime;
                    Vector3 vAux = Pallets[i].transform.localPosition;
                    vAux.y = 3.61f;//altura especifica
                    Pallets[i].transform.localPosition = vAux;

                    if (Pallets[i].currentTimeOnConveyor >= Pallets[i].timeOnConveyor) {
                        Pallets[i].currentTimeOnConveyor = 0;
                        ObjAct.gameObject.SetActiveRecursively(false);
                    }
                }
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        PalletManager recept = other.GetComponent<PalletManager>();
        if (recept != null) {
            Give(recept);
        }
    }


    //------------------------------------------------------------//

    public override bool RecievePallet(Pallet p) {
        Tempo = 0;
        controller.ArrivedToSlider(p);
        p.porter = this.gameObject;
        ConPallet = true;
        ObjAct = p.transform;
        base.RecievePallet(p);
        //p.GetComponent<Pallet>().enabled = false;
        Apagar();

        return true;
    }

    public void Encender() {
        Encendida = true;
        ModelCinta.GetComponent<Renderer>().material.color = ColorOrigModel;
    }
    public void Apagar() {
        Encendida = false;
        ConPallet = false;
        ModelCinta.GetComponent<Renderer>().material.color = ColorOrigModel;
    }
}
