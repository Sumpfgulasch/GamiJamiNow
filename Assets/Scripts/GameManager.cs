using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	System.Action OnLevelWon;
	System.Action OnRestart;

	public bool playerInGame;
	public static GameManager Instance;

	public void Awake()
	{
		Instance = this;
	}
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void StartLevel() 
	{
		
	}

	public void Restart()
	{

	}

	public void ClearLevel()
	{
		// delete player and paths
		playerInGame = false;

		// reset edges states
	}
}
