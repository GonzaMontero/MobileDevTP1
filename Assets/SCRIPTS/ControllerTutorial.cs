using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTutorial : MonoBehaviour {
    bool tutorialTerminado = false;
    int faseTutorial = 0;
    bool moving = false;
    [SerializeField] Vector3[] pos;
    [SerializeField] GameObject bolsa;
    [SerializeField] Sprite[] images;
    [SerializeField] SpriteRenderer image;
    enum Teclas {
        ASDW,
        FLECHAS
    }

    [SerializeField] Teclas t;

    [SerializeField] GameObject[] botones;

    private void Start() {
        bolsa.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (!tutorialTerminado)
            switch (t) {
                case Teclas.ASDW:
                    if (!moving) {
                        if (faseTutorial == 0) {
                            if (Input.GetKeyDown(KeyCode.W)) {
                                image.sprite = images[0];
                                bolsa.gameObject.SetActive(true);
                                faseTutorial = 1;
                            }
                        }
                        else if (faseTutorial == 1) {
                            if (Input.GetKeyDown(KeyCode.A)) {
                                image.sprite = images[1];
                                faseTutorial = 2;
                                StartCoroutine(Move());
                            }
                        }
                        else if (faseTutorial == 2) {
                            if (Input.GetKeyDown(KeyCode.S)) {
                                image.sprite = images[2];
                                faseTutorial = 3;
                            }
                        }
                        else if (faseTutorial == 3) {
                            if (Input.GetKeyDown(KeyCode.D)) {
                                StartCoroutine(Move());
                                image.sprite = images[3];
                            }
                        }
                    }
                    break;
                case Teclas.FLECHAS:
                    if (!moving) {
                        if (faseTutorial == 0) {
                            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                                image.sprite = images[0];
                                bolsa.gameObject.SetActive(true);
                                faseTutorial = 1;
                            }
                        }
                        else if (faseTutorial == 1) {
                            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                                image.sprite = images[1];
                                faseTutorial = 2;
                                StartCoroutine(Move());
                            }
                        }
                        else if (faseTutorial == 2) {
                            if (Input.GetKeyDown(KeyCode.DownArrow)) {
                                image.sprite = images[2];
                                faseTutorial = 3;
                            }
                        }
                        else if (faseTutorial == 3) {
                            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                                StartCoroutine(Move());
                                image.sprite = images[3];
                            }
                        }
                    }
                    break;
            }
    }
    bool boton0tocado;
    bool boton1tocado;
    bool boton2tocado;
    bool boton3tocado;
    public void TocadoBoton(int b) {
        if(b == 0 && !boton0tocado) {
            image.sprite = images[0];
            bolsa.gameObject.SetActive(true);
            faseTutorial = 1;
            boton0tocado = true;
            botones[1].gameObject.SetActive(true);
        }
        else if (b == 1 && !boton1tocado) {
            image.sprite = images[1];
            faseTutorial = 2;
            StartCoroutine(Move());
            boton1tocado = true;
            botones[2].gameObject.SetActive(true);
        }
        else if (b == 2 && !boton2tocado) {
            image.sprite = images[2];
            faseTutorial = 3;
            boton2tocado = true;
            botones[3].gameObject.SetActive(true);
        }
        else if (b == 3 && !boton3tocado) {
            StartCoroutine(Move());
            image.sprite = images[3];
            boton3tocado = true;
        }
    }

    IEnumerator Move() {
        moving = true;
        while (bolsa.transform.position != pos[faseTutorial]) {
            bolsa.transform.position = Vector3.MoveTowards(bolsa.transform.position, pos[faseTutorial], 100 * Time.deltaTime);
            yield return null;
        }
        moving = false;

        if (faseTutorial == 3)
            tutorialTerminado = true;
        StopCoroutine(Move());
    }

    public bool GetTutorialTerminado() {
        return tutorialTerminado;
    }

}
