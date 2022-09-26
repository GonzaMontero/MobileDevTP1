using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class JoystickMenuController : MonoBehaviour {

    [SerializeField] GameObject baseButton;

    void Update() {
        if (EventSystem.current.currentSelectedGameObject != null)
            return;

        EventSystem.current.SetSelectedGameObject(baseButton);
    }
}
