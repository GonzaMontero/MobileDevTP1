using UnityEngine;
using System.Collections;

public class Bolsa : MonoBehaviour
{
	public Pallet.Valores Monto;
	//public int IdPlayer = 0;
	public string TagPlayer = "";
	public Texture2D ImagenInventario;
	Player Pj = null;
	
	bool Desapareciendo;
	public GameObject Particulas;
	public float TiempParts = 2.5f;

	// Use this for initialization
	Renderer rend;
	Collider coll;
	ParticleSystem particles;
	void Start () 
	{
		Monto = Pallet.Valores.Valor2;

		rend = GetComponent<Renderer>();
		coll = GetComponent<Collider>();

		if (Particulas != null) {
			particles = Particulas.GetComponent<ParticleSystem>();
			Particulas.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		if(Desapareciendo)
		{
			TiempParts -= Time.deltaTime;
			if(TiempParts <= 0)
			{
				rend.enabled = true;
				coll.enabled = true;

				particles.Stop();
				gameObject.SetActive(false);
			}
		}
		
	}
	
	void OnTriggerEnter(Collider coll)
	{
		if(coll.tag == TagPlayer)
		{
			Pj = coll.GetComponent<Player>();
			//if(IdPlayer == Pj.IdPlayer)
			//{
				if(Pj.AgregarBolsa(this))
					Desaparecer();
			//}
		}
	}
	
	public void Desaparecer()
	{
		particles.Play();
		Desapareciendo = true;
		
		rend.enabled = false;
		coll.enabled = false;
		
		if(Particulas != null)
		{
			particles.Play();
		}
	
	}
}
