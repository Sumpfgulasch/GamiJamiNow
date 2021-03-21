using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	System.Action OnLevelWon;
	System.Action OnRestart;

	public bool playerInGame;
	public static GameManager Instance;

	public List<GameObject> vertices;
	public GameObject edge;
	public Transform edgeContainer;
	// public List<EdgeStateMachine> edges; // TO DO: richtiger script-name von till

	public void Awake()
	{
		Instance = this;
	}
	// Start is called before the first frame update
	void Start()
    {
		// Create all EdgeObjects that contain the colliders and states
        for (int i=0; i<vertices.Count; i++)
        {
			var obj = Instantiate(edge, edgeContainer);
			obj.AddComponent<EdgeStateMachine>();
			var edgeCollider = obj.GetComponent<EdgeCollider2D>();
			edgeCollider.points = new Vector2[2];
			edgeCollider.points[0] = vertices[i].transform.position;
			edgeCollider.points[1] = vertices[(i+1)%vertices.Count].transform.position;
			// edges.Add(obj.GetComponent<EdgeStateMachine>(); // to do: richtiger state machine name
		}
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
