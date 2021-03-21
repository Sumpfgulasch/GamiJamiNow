using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	//evants
	public System.Action OnLosschiessen; 
	public System.Action OnTreffen;

	// genereal
	[SerializeField] private GameObject playerRepresentation;

	// aiming
	[SerializeField] private LineRenderer lrAiming;
	[SerializeField] private GameObject aimingGaol;

	// travelling
	[SerializeField] private LineRenderer lrPath;
	[SerializeField] private Vector3 currentTarget;
	[SerializeField] private float travellingSpeed;
    private EdgeStateMachine currentAim = null;

	public enum State
	{
		none,
		isAiming,
		isTraveling,
	}

	public State state;

	private void Awake()
	{
		playerRepresentation.SetActive(false);
		lrAiming.gameObject.SetActive(false);
		aimingGaol.gameObject.SetActive(false);
		lrPath.gameObject.SetActive(false);
	}

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
                if(currentAim?.PreviouslyHit != true) 
                {
                    // Game over?
                    return;
                }

                currentAim.SetHit();

				// if player is not in game, spawn
				SpawnPlayer(Camera.main.ScreenToWorldPoint(Input.mousePosition)); // spawn player at the location of click
				GameManager.Instance.playerInGame = true; // tell the Game manager that from now on the player is in game
				ChangeStateTo(State.isAiming);
			}

			return;
		}


		if (state == State.isAiming)
		{
			// constantly perform a raycast to show aiming gizmo
			RaycastHit2D hit = Physics2D.Raycast(
				playerRepresentation.transform.position,
				Camera.main.ScreenToWorldPoint(Input.mousePosition) - playerRepresentation.transform.position);

			//if hit
			if (hit.collider != null)
			{
                this.currentAim = GetComponent<Collider>().gameObject.GetComponent<EdgeStateMachine>();
				// activate the aiming circle and put int on the hit place
				if (!aimingGaol.activeSelf) aimingGaol.SetActive(true);
				aimingGaol.transform.position = hit.point;

				// activate line renderer and put it in position
				if (!lrAiming.gameObject.activeSelf) lrAiming.gameObject.SetActive(true);
				lrAiming.SetPosition(0, playerRepresentation.transform.position);
				lrAiming.SetPosition(1, hit.point);

				// when the player clicks LMB send him in that direction

				if (Input.GetMouseButtonDown(0))
				{
					SetTarget(hit.point);
					SpawnNewPathPoint(playerRepresentation.transform.position);
					ChangeStateTo(State.isTraveling);
					OnLosschiessen?.Invoke();
				}
			}

			else // if raycast hits nothing (which should never happen)

			{   // deactivate the line renderer and the image
				if (lrAiming.gameObject.activeSelf) lrAiming.gameObject.SetActive(false);
				if (aimingGaol.activeSelf) aimingGaol.SetActive(false);
			}

		}

		else if(state == State.isTraveling)
		{
            this.currentAim = null;
			// move the player
			Vector3 newPosition = Vector3.MoveTowards(playerRepresentation.transform.position, currentTarget, travellingSpeed * Time.deltaTime);
			playerRepresentation.transform.position = newPosition;
			SetEndOfTrail(newPosition);

			if (Vector3.Distance(playerRepresentation.transform.position,currentTarget) < 0.1f) 
			{
				ChangeStateTo(State.isAiming);
				OnTreffen?.Invoke();
			}
		}

	}

	public void SpawnPlayer( Vector3 position)
	{
		playerRepresentation.gameObject.SetActive(true);
		playerRepresentation.transform.position = position;
		SpawnNewPathPoint(position);
	}

	public void ChangeStateTo(State state)
	{
		this.state = state;
	}

	public void SetTarget( Vector3 targetPosition)
	{
		currentTarget = targetPosition;
	}

	public void SpawnNewPathPoint(Vector3 position)
	{
		if (!lrPath.gameObject.activeSelf) lrPath.gameObject.SetActive(true);
		lrPath.positionCount += 1;
		SetEndOfTrail(position);
	}

	public void SetEndOfTrail(Vector3 position)
	{
		lrPath.SetPosition(lrPath.positionCount -1, position);
	}
}
