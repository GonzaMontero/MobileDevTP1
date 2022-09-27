using UnityEngine;

public class Player : MonoBehaviour 
{
	public int moneyGained = 0;
	public int playerId = 0;
	
	public Bolsa[] bags;
	int activeBagAmount = 0;
	
	public enum States{Dropping, Driving, Calibrating, Tutorial}
	public States currentState = States.Driving;	
	

	[Header("Scripts Attached")]
	public DescentController descentController;
	public CalibrationController calibrationController;
	public SlowingManager slowing;
	public DirectionControl dirControl;
	public Visualizer myVisualization;
	public MeshCollider myCollider;
	public Respawn myRespawn;
	public Rigidbody myRigidBody;
	public Collider[] myColliders;
	public CarController carController;

	public enum Side { Left, Right }

	public Side currentSide;

	public delegate void BagGrabbed(int side, int b);
	public static event BagGrabbed OnBagGrabbed;

	public delegate void EntradaDescarga(int l);
	public static event EntradaDescarga OnEnteredUnload;

	public delegate void SalidaDescarga(int l);
	public static event SalidaDescarga OnExitedUnload;


	public delegate void PlataCambiada(int l, float p);
	public static event PlataCambiada MoneySwapped;

	void Start () 
	{
		for(int i = 0; i< bags.Length;i++)
			bags[i] = null;
		
		myVisualization = GetComponent<Visualizer>();
	}
	
	public bool AddBag(Bolsa b)
	{
		if(activeBagAmount + 1 <= bags.Length)
		{
			bags[activeBagAmount] = b;
			activeBagAmount++;

			moneyGained += (int)b.Monto;
			b.Desaparecer();

			if (MoneySwapped != null)
				MoneySwapped((int)currentSide, moneyGained);

			if (OnBagGrabbed != null)
				OnBagGrabbed((int)currentSide, activeBagAmount);

			return true;
		}
		else
		{
			return false;
		}
	}
	
	public void EmptyInventory()
	{
		for(int i = 0; i< bags.Length;i++)
			bags[i] = null;
		
		activeBagAmount = 0;
	}
	
	public bool HasBags()
	{
		for(int i = 0; i< bags.Length;i++)
		{
			if(bags[i] != null)
			{
				return true;
			}
		}
		return false;
	}
	
	public void SetDescentController(DescentController contr)
	{
		descentController = contr;
	}
	
	public DescentController GetDescentController()
	{
		return descentController;
	}
	
	public void SwapMoney() {
		if (MoneySwapped != null)
			MoneySwapped((int)currentSide, moneyGained);
	}
	
	public void SwapToDriving()
	{
		if (OnExitedUnload != null)
			OnExitedUnload((int)currentSide);
		myVisualization.CambiarAConduccion();
		currentState = Player.States.Driving;

		if (OnBagGrabbed != null)
			OnBagGrabbed((int)currentSide, 0);
		if (MoneySwapped != null)
			MoneySwapped((int)currentSide, moneyGained);
	}
	
	public void SwapToUnloading()
	{
		if (OnEnteredUnload != null)
			OnEnteredUnload((int)currentSide);
		myVisualization.CambiarADescarga();
		currentState = Player.States.Dropping;
	}
	
	public void RemoveBags()
	{
		for(int i = 0; i < bags.Length; i++)
		{
			if(bags[i] != null)
			{
				bags[i] = null;
				return;
			}				
		}
	}	
}
