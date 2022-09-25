using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public int Dinero = 0;
	public int IdPlayer = 0;
	
	public Bolsa[] Bolasas;
	int CantBolsAct = 0;
	public string TagBolsas = "";
	
	public enum Estados{EnDescarga, EnConduccion, EnCalibracion, EnTutorial}
	public Estados EstAct = Estados.EnConduccion;
	
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
		for(int i = 0; i< Bolasas.Length;i++)
			Bolasas[i] = null;
		
		MiVisualizacion = GetComponent<Visualizacion>();
	}
	
	public bool AgregarBolsa(Bolsa b)
	{
		if(CantBolsAct + 1 <= Bolasas.Length)
		{
			Bolasas[CantBolsAct] = b;
			CantBolsAct++;
			Dinero += (int)b.Monto;
			b.Desaparecer();

			if (CambiadaPlata != null)
				CambiadaPlata((int)lado, Dinero);

			if (AgarradaBolsa != null)
				AgarradaBolsa((int)lado, CantBolsAct);
			return true;
		}
		else
		{
			return false;
		}
	}
	
	public void VaciarInv()
	{
		for(int i = 0; i< Bolasas.Length;i++)
			Bolasas[i] = null;
		
		CantBolsAct = 0;
	}
	
	public bool ConBolasas()
	{
		for(int i = 0; i< Bolasas.Length;i++)
		{
			if(Bolasas[i] != null)
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
			CambiadaPlata((int)lado, Dinero);
	}

	public void CambiarACalibracion()
	{
		MiVisualizacion.CambiarACalibracion();
		EstAct = Player.Estados.EnCalibracion;
	}
	
	public void CambiarATutorial()
	{
		MiVisualizacion.CambiarATutorial();
		EstAct = Player.Estados.EnTutorial;
	}
	
	public void CambiarAConduccion()
	{
		if (DescargaSalida != null)
			DescargaSalida((int)lado);
		MiVisualizacion.CambiarAConduccion();
		EstAct = Player.Estados.EnConduccion;

		if (AgarradaBolsa != null)
			AgarradaBolsa((int)lado, 0);
		if (CambiadaPlata != null)
			CambiadaPlata((int)lado, Dinero);
	}
	
	public void CambiarADescarga()
	{
		if (DescargaEntrada != null)
			DescargaEntrada((int)lado);
		MiVisualizacion.CambiarADescarga();
		EstAct = Player.Estados.EnDescarga;
	}
	
	public void SacarBolasa()
	{
		for(int i = 0; i < Bolasas.Length; i++)
		{
			if(Bolasas[i] != null)
			{
				Bolasas[i] = null;
				return;
			}				
		}
	}
	
	
}
