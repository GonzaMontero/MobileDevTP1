using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour {
    [SerializeField] GameObject[] buttons;
    public delegate void ClickedButton(string nombre);
    public static event ClickedButton ButtonClicked;
    public void LoadScene(string stl) {
        if (ButtonClicked != null)
            ButtonClicked(stl);
        //Debug.Log("Escena a cargar: " + stl);
    }
}
