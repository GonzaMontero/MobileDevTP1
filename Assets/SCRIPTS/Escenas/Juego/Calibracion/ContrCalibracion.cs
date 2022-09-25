using UnityEngine;
using System.Collections;

public class ContrCalibracion : MonoBehaviour
{
	public float TiempEspCalib = 3;
	float Tempo2 = 0;
	
	public enum Estados{Calibrando, Tutorial, Finalizado}
	public Estados EstAct = Estados.Calibrando;
	
	public ManejoPallets Partida;
	public ManejoPallets Llegada;
	public Pallet P;

	Renderer partRend;
	Collider partColl;

	Renderer llegRend;
	Collider llegColl;

	Renderer pRend;

	void Start () 
	{
		if (partRend != null) {
			partRend = Partida.GetComponent<Renderer>();
			partColl = Partida.GetComponent<Collider>();
		}
		if(Llegada != null) {
			llegRend = Llegada.GetComponent<Renderer>();
			llegColl = Llegada.GetComponent<Collider>();
		}
		if(P != null) {
			pRend = P.GetComponent<Renderer>();
		}
        /*
		renderer.enabled = false;
		collider.enabled = false;
		*/
		

		Partida.Recibir(P);
		
		SetActivComp(false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(EstAct == ContrCalibracion.Estados.Tutorial)
		{
			if(Tempo2 < TiempEspCalib)
			{
				Tempo2 += Time.deltaTime;
				if(Tempo2 > TiempEspCalib)
				{
					 SetActivComp(true);
				}
			}
		}
		
	}

	
	public void IniciarTesteo()
	{
		EstAct = ContrCalibracion.Estados.Tutorial;
        //Reiniciar();
    }
	
	public void FinTutorial()
	{
		EstAct = ContrCalibracion.Estados.Finalizado;
	}
	
	void SetActivComp(bool estado)
	{
		//if(Partida.GetComponent<Renderer>() != null)
		//	Partida.GetComponent<Renderer>().enabled = estado;
		//Partida.GetComponent<Collider>().enabled = estado;
		//if(Llegada.GetComponent<Renderer>() != null)
		//	Llegada.GetComponent<Renderer>().enabled = estado;
		//Llegada.GetComponent<Collider>().enabled = estado;
		//P.GetComponent<Renderer>().enabled = estado;

		if (partRend != null)
			partRend.enabled = estado;
		if(partColl)
			partColl.enabled = estado;
		if (llegRend != null)
			llegRend.enabled = estado;
		if(llegColl != null)
			llegColl.enabled = estado;
		if(pRend != null)
			pRend.enabled = estado;
	}
}
