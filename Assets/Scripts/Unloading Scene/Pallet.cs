using UnityEngine;
using System.Collections;

public class Pallet : MonoBehaviour 
{
	public float time;
	public GameObject reciever = null;
	public GameObject porter = null;

	public float timeOnConveyor = 1.5f;
	public float currentTimeOnConveyor = 0;
	
	public enum Values {Value1 = 100000, Value2 = 250000, Value3 = 500000}
	public Values value;

	public float smoothTime = 0.3f;
	float currSmoothTime = 0;
	public bool isSmooth = false;

	ReceptionHand receptHand;
	
	void Start()
	{
		if (porter != null)
			receptHand = porter.GetComponent<ReceptionHand>();
		Passing();
	}
	
	void LateUpdate () 
	{
		if(porter != null)
		{
			if(isSmooth)
			{
				currSmoothTime += Time.deltaTime;
				if(currSmoothTime >= smoothTime)
				{
					isSmooth = false;
					currSmoothTime = 0;
				}
				else
				{
					if(receptHand != null)
						transform.position = porter.transform.position - Vector3.up * 1.2f;
					else
						transform.position = Vector3.Lerp(transform.position, porter.transform.position, Time.deltaTime * 10);
				}
				
			}
			else
			{
				if(receptHand != null)
					transform.position = porter.transform.position - Vector3.up * 1.2f;
				else
					transform.position = porter.transform.position;
					
			}
		}
			
	}
	
	public void Passing()
	{
		isSmooth = true;
		currSmoothTime = 0;
	}
}
