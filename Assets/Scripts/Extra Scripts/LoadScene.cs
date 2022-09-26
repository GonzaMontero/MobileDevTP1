using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    static public LoadScene Instance;

    [SerializeField] GameObject loadImage;
    [SerializeField] GameObject[] objectsToDisable;

    private void Awake()
    {
        if (loadImage != null && loadImage.activeSelf)
            loadImage.gameObject.SetActive(false);

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
        Instance = this;
    }

    private void Start()
    {
        ScenesManager.ButtonClicked += StartLoadingScene;
    }

    private void OnDisable()
    {
        ScenesManager.ButtonClicked -= StartLoadingScene;
    }

    public void StartLoadingScene(string name)
    {
        if (name == "Close")
        {
            Application.Quit();
            return;
        }
        StartCoroutine(ShowLoadingScene(name));
    }

    IEnumerator ShowLoadingScene(string name)
    {
        Canvas c = FindObjectOfType<Canvas>();
        if (c != null)
            c.gameObject.SetActive(false);

        loadImage.gameObject.SetActive(true);
        SceneManager.LoadScene(name);
        while (!name.Equals(SceneManager.GetActiveScene().name))
            yield return null;

        loadImage.gameObject.SetActive(false);
        StopCoroutine(ShowLoadingScene(base.name));
        yield return null;
    }
}
