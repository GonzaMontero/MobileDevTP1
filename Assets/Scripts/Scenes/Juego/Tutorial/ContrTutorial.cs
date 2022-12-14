using UnityEngine;
using System.Collections;

public class ContrTutorial : MonoBehaviour 
{
	public Player Pj;
	public float TiempTuto = 15;
	public float Tempo = 0;
	
	public bool Finalizado = false;
	bool Iniciado = false;
	
	GameManager GM;
	
	//------------------------------------------------------------------//

	// Use this for initialization
	void Start () 
	{
		GM = GameObject.Find("GameMgr").GetComponent<GameManager>();
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		/*
		if(Iniciado)
		{
			if(Tempo < TiempTuto)
			{
				Tempo += T.GetDT();
				if(Tempo >= TiempTuto)
				{
					Finalizar();
				}
			}
		}
		*/
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<Player>() == Pj)
			Finalizar();
	}
	
	//------------------------------------------------------------------//
	
	public void Iniciar()
	{
		Pj.GetComponent<SlowingManager>().RestoreVelocity();
		Iniciado = true;
	}
	
	public void Finalizar()
	{
		Finalizado = true;
		Pj.GetComponent<SlowingManager>().Stop();
		Pj.GetComponent<Rigidbody>().velocity = Vector3.zero;
		Pj.EmptyInventory();
	}
}
