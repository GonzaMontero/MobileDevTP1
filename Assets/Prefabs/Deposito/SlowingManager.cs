using UnityEngine;
using System.Collections;

public class SlowingManager : MonoBehaviour {

    public Player thisPlayer;

    public float entryVelocity = 0;
    public string depositTag = "Deposito";

    int counter = 0;
    int messageAmount = 10;
    float slowTime = 0.5f;
    float time = 0f;

    Vector3 destination;

    public bool slowing = false;

    DirectionControl dirControl;
    Rigidbody rb;

    void Start() {
        Stop();

        dirControl = thisPlayer.dirControl;
        rb = thisPlayer.myRigidBody;
    }

    void FixedUpdate() {

        if (slowing) {
            time += Time.fixedDeltaTime;
            if (time >= (slowTime / messageAmount) * counter) {
                counter++;
            }
            if (time >= slowTime) {
            }
        }

    }

    void OnTriggerEnter(Collider other) {

        if (other.tag == depositTag) {

            Deposit dep = other.GetComponent<Deposit>();

            dep.currentPlayer = thisPlayer;

            if (dep.Empty) {

                if (thisPlayer.HasBags()) {

                    dep.Enter(thisPlayer);
                    destination = other.transform.position;
                    transform.forward = destination - transform.position;
                    Stop();
                }
            }
        }

    }

    public void Stop() {
        if (dirControl != null)
            dirControl.enabled = false;

        thisPlayer.carController.SetAcceleration(0);

        if (rb != null)
            rb.velocity = Vector3.zero;

        slowing = true;

        time = 0;
        counter = 0;
    }

    public void RestoreVelocity() {
        dirControl.enabled = true;
        thisPlayer.carController.SetAcceleration(1);

        slowing = false;
        time = 0;
        counter = 0;
    }
}
