using UnityEngine;
using System.Collections;

public class Deposit : MonoBehaviour 
{	
	Player currentPlayer;

	public bool Empty = true;

	public DescentController Controller1;
	public DescentController Controller2;
	
	Collider[] CharacterCollider;

	void Start () 
	{
		Physics.IgnoreLayerCollision(8,9,false);
	}
	
	void Update () 
	{
		if(!Empty)
		{
			currentPlayer.transform.position = transform.position;
			currentPlayer.transform.forward = transform.forward;
		}
	}
	
	public void Drop()
	{
		currentPlayer.EmptyInventory();
		currentPlayer.slowing.RestaurarVel();
		currentPlayer.myRespawn.Respawnear(transform.position,transform.forward);
		
		currentPlayer.myRigidBody.useGravity = true;

		for(int i = 0; i < CharacterCollider.Length; i++)
			CharacterCollider[i].enabled = true;
		
		Physics.IgnoreLayerCollision(8,9,false);
		
		currentPlayer = null;
		Empty = true;		
	}
	
	public void Enter(Player pj)
	{
		if(pj.HasBags())
		{			
			currentPlayer = pj;
			
			CharacterCollider = currentPlayer.myColliders;

			for(int i = 0; i < CharacterCollider.Length; i++)
				CharacterCollider[i].enabled = false;

			currentPlayer.myRigidBody.useGravity = false;
			
			currentPlayer.transform.position = transform.position;
			currentPlayer.transform.forward = transform.forward;
			
			Empty = false;
			
			Physics.IgnoreLayerCollision(8,9,true);
			
			Enter();
		}
	}
	
	public void Enter()
	{		
		if(currentPlayer.playerId == 0)
			Controller1.Activate(this);
		else
			Controller2.Activate(this);
	}
}
