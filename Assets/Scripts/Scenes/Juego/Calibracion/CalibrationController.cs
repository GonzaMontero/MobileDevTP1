using UnityEngine;
using System.Collections;

public class CalibrationController : MonoBehaviour
{
	public float CalibrationWaitTime = 3;
	float Tempo2 = 0;
	
	public enum States{Calibrating, Tutorial, Finished}
	public States CurrentStates = States.Calibrating;
	
	public PalletManager beginning;
	public PalletManager ending;

	public Pallet P;

	Renderer startRenderer;
	Collider startCollider;

	Renderer arrivalRenderer;
	Collider arrivalCollider;

	Renderer pRend;

	void Start () 
	{
		if (startRenderer != null) {
			startRenderer = beginning.GetComponent<Renderer>();
			startCollider = beginning.GetComponent<Collider>();
		}
		if(ending != null) {
			arrivalRenderer = ending.GetComponent<Renderer>();
			arrivalCollider = ending.GetComponent<Collider>();
		}
		if(P != null) {
			pRend = P.GetComponent<Renderer>();
		}

		beginning.RecievePallet(P);
		
		SetComponents(false);
	}
	
	void Update ()
	{
		if(CurrentStates == CalibrationController.States.Tutorial)
		{
			if(Tempo2 < CalibrationWaitTime)
			{
				Tempo2 += Time.deltaTime;
				if(Tempo2 > CalibrationWaitTime)
				{
					 SetComponents(true);
				}
			}
		}
		
	}

	public void FinishTutorial()
	{
		CurrentStates = CalibrationController.States.Finished;
	}
	
	void SetComponents(bool estado)
	{
		if (startRenderer != null)
			startRenderer.enabled = estado;
		if(startCollider)
			startCollider.enabled = estado;
		if (arrivalRenderer != null)
			arrivalRenderer.enabled = estado;
		if(arrivalCollider != null)
			arrivalCollider.enabled = estado;
		if(pRend != null)
			pRend.enabled = estado;
	}
}
