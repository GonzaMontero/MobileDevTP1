using UnityEngine;

public class Player : MonoBehaviour 
{
	public int moneyGained = 0;
	public int playerId = 0;
	
	public Bolsa[] bags;
	int activeBagAmount = 0;
	
	public enum States{Dropping, Driving, Calibrating, Tutorial}
	public States currentState = States.Driving;
	
	public bool EnConduccion = true;
	public bool EnDescarga = false;
	
	public ControladorDeDescarga ContrDesc;
	public ContrCalibracion ContrCalib;

	Visualizacion MiVisualizacion;

	public enum Lado {
		Izq,
		Der
	}

	public Lado lado;

	public delegate void BolsaAgarrada(int lad, int b);
	public static event BolsaAgarrada AgarradaBolsa;

	public delegate void EntradaDescarga(int l);
	public static event EntradaDescarga DescargaEntrada;

	public delegate void SalidaDescarga(int l);
	public static event SalidaDescarga DescargaSalida;


	public delegate void PlataCambiada(int l, float p);
	public static event PlataCambiada CambiadaPlata;

	void Start () 
	{
		for(int i = 0; i< bags.Length;i++)
			bags[i] = null;
		
		MiVisualizacion = GetComponent<Visualizacion>();
	}
	
	public bool AgregarBolsa(Bolsa b)
	{
		if(activeBagAmount + 1 <= bags.Length)
		{
			bags[activeBagAmount] = b;
			activeBagAmount++;
			moneyGained += (int)b.Monto;
			b.Desaparecer();

			if (CambiadaPlata != null)
				CambiadaPlata((int)lado, moneyGained);

			if (AgarradaBolsa != null)
				AgarradaBolsa((int)lado, activeBagAmount);
			return true;
		}
		else
		{
			return false;
		}
	}
	
	public void VaciarInv()
	{
		for(int i = 0; i< bags.Length;i++)
			bags[i] = null;
		
		activeBagAmount = 0;
	}
	
	public bool ConBolasas()
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
	
	public void SetContrDesc(ControladorDeDescarga contr)
	{
		ContrDesc = contr;
	}
	
	public ControladorDeDescarga GetContr()
	{
		return ContrDesc;
	}
	
	public void CambioPlata() {
		if (CambiadaPlata != null)
			CambiadaPlata((int)lado, moneyGained);
	}

	public void CambiarACalibracion()
	{
		MiVisualizacion.CambiarACalibracion();
		currentState = Player.States.Calibrating;
	}
	
	public void CambiarATutorial()
	{
		MiVisualizacion.CambiarATutorial();
		currentState = Player.States.Tutorial;
	}
	
	public void CambiarAConduccion()
	{
		if (DescargaSalida != null)
			DescargaSalida((int)lado);
		MiVisualizacion.CambiarAConduccion();
		currentState = Player.States.Driving;

		if (AgarradaBolsa != null)
			AgarradaBolsa((int)lado, 0);
		if (CambiadaPlata != null)
			CambiadaPlata((int)lado, moneyGained);
	}
	
	public void CambiarADescarga()
	{
		if (DescargaEntrada != null)
			DescargaEntrada((int)lado);
		MiVisualizacion.CambiarADescarga();
		currentState = Player.States.Dropping;
	}
	
	public void SacarBolasa()
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
