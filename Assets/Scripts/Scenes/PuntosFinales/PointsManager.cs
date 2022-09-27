using UnityEngine;

public class PointsManager : MonoBehaviour 
{	
	public float timeForAnimStart = 2.5f;
	float time = 0;	
	
	public Vector2[] moneyPosition;
	public Vector2 moneyScale;
	public GUISkin skinMoney;
	
	public Vector2 winnerPosition;
	public Vector2 winnerScale;
	public Texture2D[] winnerTex;
	public GUISkin skinWinner;
	
	public GameObject backgroundGO;
	
	public float waitTimeForRestart = 10;
	
	
	public float blinkTime = 0.7f;
	float blinkCurrentTime = 0;
	bool firstBlinkimage = true;
	
	public bool animsActive = false;

	LoadScene loadScreen;

	void Start () 
	{		
		loadScreen = FindObjectOfType<LoadScene>();
	}

	void Update () 
	{	
		if(animsActive)
		{
			blinkCurrentTime += Time.deltaTime;
			
			if(blinkCurrentTime >= blinkTime)
			{
				blinkCurrentTime = 0;
				
				if(firstBlinkimage)
					firstBlinkimage = false;
				else
				{
					blinkCurrentTime += 0.1f;
					firstBlinkimage = true;
				}
			}
		}	
		
		if(!animsActive)
		{
			time += Time.deltaTime;
			if(time >= timeForAnimStart)
			{
				time = 0;
				animsActive = true;
			}
		}
		
		
	}
	
	public void ButtonPressed(string stl) {
		if (loadScreen != null)
			loadScreen.StartLoadingScene(stl);
	}
}