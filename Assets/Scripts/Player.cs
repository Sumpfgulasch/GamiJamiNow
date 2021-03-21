using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public System.Action OnLosschiessen; 
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
			RaycastHit2D hit = Physics2D.Raycast(
				playerRepresentation.transform.position,
				Camera.main.ScreenToWorldPoint(Input.mousePosition) - playerRepresentation.transform.position);

			//if hit
			if(hit.collider != null)
			{
				// activate the aiming circle and put int on the hit place
				if(!aimingGaol.activeSelf) aimingGaol.SetActive(true);
				aimingGaol.transform.position = hit.point;

				// activate line renderer and put it in position
				if (!lrAiming.gameObject.activeSelf) lrAiming.gameObject.SetActive(true);
				lrAiming.SetPosition(0, playerRepresentation.transform.position);
				lrAiming.SetPosition(1, hit.point);

			}

			else
			{
				if(lrAiming.gameObject.activeSelf) lrAiming.gameObject.SetActive(false);
				if (aimingGaol.activeSelf) aimingGaol.SetActive(false);
			}
		}
		
	}

	public void SpawnPlayer( Vector3 position)
	{
		playerRepresentation.gameObject.SetActive(true);
		playerRepresentation.transform.position = position;
	}

}
