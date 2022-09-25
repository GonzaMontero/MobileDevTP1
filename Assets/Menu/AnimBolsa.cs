using UnityEngine;
using System.Collections;

public class AnimBolsa : MonoBehaviour {
    Vector3 PosIni;
    [SerializeField] float posMultiplier;
    [SerializeField] float rotSpeed;
    [SerializeField] float speed;
    void Start() {
        PosIni = transform.localPosition;
        StartCoroutine(Up());
    }

    // Update is called once per frame
    void Update() {
        transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime);
    }

    IEnumerator Up() {
        Vector3 newPos = PosIni + (Vector3.up * posMultiplier);
        while (transform.localPosition != newPos) {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, newPos, speed * Time.deltaTime);
            yield return null;
        }

        StopCoroutine(Up());
        StartCoroutine(Down());
    }
    IEnumerator Down() {
        Vector3 newPos = PosIni + (Vector3.down * posMultiplier);
        while (transform.localPosition != newPos) {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, newPos, speed * Time.deltaTime);
            yield return null;
        }

        StopCoroutine(Down());
        StartCoroutine(Up());
    }

}
