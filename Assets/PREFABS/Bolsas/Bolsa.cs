using System;
using UnityEngine;
using System.Collections;

public class Bolsa : MonoBehaviour
{
	public Pallet.Values Monto;

	public string tagToCompare;

	public Texture2D ImagenInventario;
	Player Pj = null;
	
	public float TiempParts = 2.5f;

	public Renderer rend;
	public Collider coll;
	public ParticleSystem particles;

	void Start () 
	{
		Monto = Pallet.Values.Value2;
	}
	
	void OnTriggerEnter(Collider coll)
	{
		if(coll.tag == tagToCompare)
		{
			Pj = coll.GetComponent<Player>();

			if(Pj.AddBag(this))
				Desaparecer();
		}
	}
	
	public void Desaparecer()
	{
		particles.Play();
		
		rend.enabled = false;
		coll.enabled = false;

		StartCoroutine(WaitForParticleEnd());
	}

	IEnumerator WaitForParticleEnd()
    {
		yield return new WaitForSeconds(particles.main.duration);

		this.gameObject.SetActive(false);
    }
}
