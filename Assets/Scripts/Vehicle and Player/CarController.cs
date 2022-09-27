using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

    public List<WheelCollider> throttleWheels = new List<WheelCollider>();
    public List<WheelCollider> steeringWheels = new List<WheelCollider>();
    public float throttleCoefficient = 20000f;
    public float maxTurn = 20f;
    float acceleration = 1f;

    public enum Side {
        Left,
        Right
    }

    public Side currentSide;

	void FixedUpdate () {
        foreach (var wheel in throttleWheels) {
            wheel.motorTorque = throttleCoefficient * Time.fixedDeltaTime * acceleration;
        }
        foreach (var wheel in steeringWheels) {
            if(currentSide == Side.Left)
                wheel.steerAngle = maxTurn * InputManager.Instance.GetAxis("Horizontal1");
            else if (currentSide == Side.Right)
                wheel.steerAngle = maxTurn * InputManager.Instance.GetAxis("Horizontal2");
        }
    }

    public void SetAcceleration(float val) {
        acceleration = val;
    }
}
