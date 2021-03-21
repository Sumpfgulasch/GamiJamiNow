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

	public List<GameObject> outerVertices;
	public List<GameObject> innerVertices;
	public GameObject edgeObj;
	public Transform edgeContainer;
	public List<EdgeStateMachine> edges; // TO DO: richtiger script-name von till

	public void Awake()
	{
		Instance = this;
	}
	// Start is called before the first frame update
	void Start()
    {
		foreach (var gob in FindObjectsOfType<GameObject>().Where(o => o.name.StartsWith("Figure")))
		{
			// hack that works with kalas dummy objects
			gob.AddComponent<EdgeStateMachine>();
		}

		CreateEdges("InnerEdge", innerVertices);
		CreateEdges("OuterEdge", outerVertices);
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


	private void CreateEdges(string name, List<GameObject> vertexEmpties)
    {
		// Create all EdgeObjects that contain the colliders and states
		for (int i = 0; i < vertexEmpties.Count; i++)
		{
			var obj = Instantiate(edgeObj, edgeContainer);
			obj.transform.position = Vector3.zero;
			obj.name = name;
			obj.AddComponent<EdgeStateMachine>();
			var edgeCollider = obj.GetComponent<EdgeCollider2D>();
			//edgeCollider.points = new Vector2[2];
			//edgeCollider.points[0] = vertexEmpties[i].transform.position;
			//edgeCollider.points[1] = vertexEmpties[(i + 1) % vertexEmpties.Count].transform.position;
			edgeCollider.SetPoints(new List<Vector2>
			{
				vertexEmpties[i].transform.position,
				vertexEmpties[(i + 1) % vertexEmpties.Count].transform.position 
			});
			edges.Add(obj.GetComponent<EdgeStateMachine>());
		}
	}
}
