using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour 
{
	public Player thisPlayer;
	CheakPoint CPAct;
	CheakPoint CPAnt;
	
	public float maxAngle = 90;
	int VerifPorCuadro = 20;
	int Contador = 0;
	
	public float RangMinDer = 0;
	public float RangMaxDer = 0;
	
	bool IgnorandoColision = false;
	public float TiempDeNoColision = 2;
	float Tempo = 0;

	Rigidbody rb;
	Visualizer vis;
	DirectionControl dirControl;

	void Start () 
	{
		Physics.IgnoreLayerCollision(8,9,false);
		rb = thisPlayer.myRigidBody;
		vis = thisPlayer.myVisualization;
		dirControl = thisPlayer.dirControl;
	}

	void Update ()
	{
		if(CPAct != null)
		{
			Contador++;
			if(Contador == VerifPorCuadro)
			{
				Contador = 0;
				if(maxAngle < Quaternion.Angle(transform.rotation, CPAct.transform.rotation))
				{
					Respawnear();
				}
			}
		}
		
		if(IgnorandoColision)
		{
			Tempo += Time.deltaTime;
			if(Tempo > TiempDeNoColision)
			{
				IgnorarColision(false);
			}
		}
		
	}

	public void Respawnear()
	{
		rb.velocity = Vector3.zero;

		dirControl.SetTurn(0f);
		
		if(CPAct.Habilitado())
		{
			if(vis.currentSide == Visualizer.Side.Right)
				transform.position = CPAct.transform.position + CPAct.transform.right * Random.Range(RangMinDer, RangMaxDer);
			else 
				transform.position = CPAct.transform.position + CPAct.transform.right * Random.Range(RangMinDer * (-1), RangMaxDer * (-1));
			transform.forward = CPAct.transform.forward;
		}
		else if(CPAnt != null)
		{
			if(vis.currentSide == Visualizer.Side.Right)
				transform.position = CPAnt.transform.position + CPAnt.transform.right * Random.Range(RangMinDer, RangMaxDer);
			else
				transform.position = CPAnt.transform.position + CPAnt.transform.right * Random.Range(RangMinDer * (-1), RangMaxDer * (-1));
			transform.forward = CPAnt.transform.forward;
		}
		
		IgnorarColision(true);
	}
	
	public void Respawnear(Vector3 pos, Vector3 dir)
	{
		rb.velocity = Vector3.zero;

		dirControl.SetTurn(0f);
		
		transform.position = pos;
		transform.forward = dir;
		
		IgnorarColision(true);
	}
	
	public void AgregarCP(CheakPoint cp)
	{
		if(cp != CPAct)
		{
			CPAnt = CPAct;
			CPAct = cp;
		}
	}
	
	void IgnorarColision(bool b)
	{
		Physics.IgnoreLayerCollision(8,9,b);
		IgnorandoColision = b;	
		Tempo = 0;
	}
}
