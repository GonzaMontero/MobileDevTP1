using UnityEngine;
using System.Collections;

public class DescentController : MonoBehaviour 
{
	System.Collections.Generic.List<Pallet.Values> Ps = new System.Collections.Generic.List<Pallet.Values>();
	
	int counter = 0;
	
	Deposit Dep;
	
	public GameObject[] Components;
	
	public Player Player;
	MeshCollider TruckCollider;
	
	public Pallet PEnMov = null;
	
	public GameObject DrivingCamera;
	public GameObject UnloadingCamera;
	
	public GameObject Pallet1;
	public GameObject Pallet2;
	public GameObject Pallet3;
	
	public Estanteria Est1;
	public Estanteria Est2;
	public Estanteria Est3;
	
	public Cinta Cin2;
	
	public float Bonus = 0;
	float BonusTime;
	
	
	public AnimMngDesc AnimatedObject;

	void Start () 
	{
		for (int i = 0; i < Components.Length; i++)
		{
			Components[i].SetActive(false);
		}
		
		TruckCollider = Player.myCollider;

		Player.SetDescentController(this);

		if(AnimatedObject != null)
			AnimatedObject.ContrDesc = this;
	}
	
	void Update () 
	{
		if(PEnMov != null)
		{
			if(BonusTime > 0)
			{
				Bonus = (BonusTime * (float)PEnMov.value) / PEnMov.time;
		
				BonusTime -= Time.deltaTime;
			}
			else
			{
				Bonus = 0;
			}
		}
		
		
	}
	public void Activate(Deposit d)
	{
		Dep = d;
		DrivingCamera.SetActive(false);
			
		for (int i = 0; i < Components.Length; i++)
		{
			Components[i].SetActive(true);
		}
				
		TruckCollider.enabled = false;
		Player.SwapToUnloading();
				
		GameObject go;

		for(int i = 0; i < Player.bags.Length; i++)
		{
			if(Player.bags[i] != null)
			{
				counter++;
				
				switch(Player.bags[i].Monto)
				{
				case Pallet.Values.Value1:
					go = (GameObject) Instantiate(Pallet1);
					Est1.RecievePallet(go.GetComponent<Pallet>());
					break;
					
				case Pallet.Values.Value2:
					go = (GameObject) Instantiate(Pallet2);
					Est2.RecievePallet(go.GetComponent<Pallet>());
					break;
					
				case Pallet.Values.Value3:
					go = (GameObject) Instantiate(Pallet3);
					Est3.RecievePallet(go.GetComponent<Pallet>());
					break;
				}
			}
		}
		AnimatedObject.Entrar();
	}
	
	public void LeavePallet(Pallet p)
	{
		PEnMov = p;
		BonusTime = p.time;
		Player.RemoveBags();
	}
	
	public void ArrivedToSlider(Pallet p)
	{
		PEnMov = null;
		counter--;
		
		Player.moneyGained += Bonus;
		Player.SwapMoney();

		if(counter <= 0)
		{
			Finalized();
		}
		else
		{
			Est2.EncenderAnim();
		}
	}
	
	public void GameFinished()
	{
		Est2.enabled = false;
		Cin2.enabled = false;
	}
	
	void Finalized()
	{
		AnimatedObject.Salir();
	}
	
	public Pallet GetMovingPallet()
	{
		return PEnMov;
	}
	
	public void AnimationFinished()
	{
		Est2.EncenderAnim();
	}
	
	public void FinishedExitAnimation()
	{
		for (int i = 0; i < Components.Length; i++)
		{
			Components[i].SetActive(false);
		}
		
		DrivingCamera.SetActive(true);
		
		TruckCollider.enabled = true;
		
		Player.SwapToDriving();
		
		Dep.Drop();	
	}	
}
