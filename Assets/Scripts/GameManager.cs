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
	public List<EdgeStateMachine> edges;
	public Color hitColor;
	public Color unhitColor;
	public Color selectedColor;

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
			var vert1 = vertexEmpties[i].transform.position;
			var vert2 = vertexEmpties[(i + 1) % vertexEmpties.Count].transform.position;

			var obj = Instantiate(edgeObj, edgeContainer);
			obj.transform.position = Vector3.zero;
			obj.name = name;

			var edgeCollider = obj.GetComponent<EdgeCollider2D>();
			edgeCollider.SetPoints(new List<Vector2>
			{
				vert1,
				vert2 
			});

			var lineRend = obj.GetComponent<LineRenderer>();
			lineRend.positionCount = 2;
			lineRend.SetPosition(0, vert1);
			lineRend.SetPosition(1, vert2);
			lineRend.material.color = unhitColor;

			edges.Add(obj.GetComponent<EdgeStateMachine>());
		}
	}
}
