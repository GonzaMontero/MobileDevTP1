using UnityEngine;
using System.Collections;

public class FadeInicioFinal : MonoBehaviour 
{
	public float Duracion = 2;
	public float Vel = 2;
	float TiempInicial;
	
	
	Color aux;
	
	bool MngAvisado = false;

	// Use this for initialization
	void Start ()
	{
		//renderer.material = IniFin;
		
		aux = GetComponent<Renderer>().material.color;
		aux.a = 0;
		GetComponent<Renderer>().material.color = aux;
	}

}
