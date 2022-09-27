using UnityEngine;
using System.Collections;

public class PantallaCalibTuto : MonoBehaviour 
{
	public Texture2D[] ImagenesDelTuto;
	public float Intervalo = 1.2f;//tiempo de cada cuanto cambia de imagen
	float TempoIntTuto = 0;
	int EnCursoTuto = 0;
	
	public Texture2D[] ImagenesDeCalib;
	int EnCursoCalib = 0;
	float TempoIntCalib = 0;
	
	public Texture2D ImaReady;
	
	public CalibrationController ContrCalib;

	Renderer rend;

	// Use this for initialization
	void Start () 
	{
		rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
