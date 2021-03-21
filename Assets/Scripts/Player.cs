using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public System.Action OnLosschießen;
	public System.Action OnTreffen;

	public GameObject playerRepresentation;

	// aiming
	public LineRenderer lrAiming;
	public GameObject aimingGaol;

	public LineRenderer lrPath;

	public bool aimingActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
	{
		//  wire input

		if (!GameManager.Instance.playerInGame) // if player is not in game
		{
			if (Input.GetMouseButtonDown(0)) // when you click
			{
				// if player is not in game spawn
				SpawnPlayer(Camera.main.ScreenToWorldPoint(Input.mousePosition)); // spawn player at the location of click
				GameManager.Instance.playerInGame = true; // tell the Game manager that from now on the player is in game
			}

		}
		else
		{
			// constantly raycast in the direction of mouse

		}
		
	}

	public void SpawnPlayer( Vector3 position)
	{
		playerRepresentation.gameObject.SetActive(true);
		playerRepresentation.transform.position = position;
	}

}
