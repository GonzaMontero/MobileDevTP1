using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaDeCarga : MonoBehaviour {

    static PantallaDeCarga instancia { get; set; }

    [SerializeField] GameObject imagenDeCarga;
    [SerializeField] GameObject[] objetosADeshabilitar;

    private void Awake() {
        if (imagenDeCarga != null && imagenDeCarga.activeSelf)
            imagenDeCarga.gameObject.SetActive(false);

        if (instancia != null) {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
        instancia = this;
    }
    private void Start() {
        ScenesManager.ButtonClicked += CargarEscena;
    }
    private void OnDisable() {
        ScenesManager.ButtonClicked -= CargarEscena;
    }

    public void CargarEscena(string nombre) {
        if (nombre == "Close") {
            Application.Quit();
            return;
        }
        StartCoroutine(MostrarPantallaDeCarga(nombre));
    }

    IEnumerator MostrarPantallaDeCarga(string nombre) {

        Canvas c = FindObjectOfType<Canvas>();
        if (c != null)
            c.gameObject.SetActive(false);

        imagenDeCarga.gameObject.SetActive(true);
        SceneManager.LoadScene(nombre);
        while (!nombre.Equals(SceneManager.GetActiveScene().name))
            yield return null;

        imagenDeCarga.gameObject.SetActive(false);
        StopCoroutine(MostrarPantallaDeCarga(name));
        yield return null;
    }
}
