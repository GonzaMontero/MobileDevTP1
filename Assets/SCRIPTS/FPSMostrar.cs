using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSMostrar : MonoBehaviour {
    [SerializeField] TextMeshProUGUI text;
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        text.text = ((int)(1.0f / Time.deltaTime)).ToString();
    }
}
