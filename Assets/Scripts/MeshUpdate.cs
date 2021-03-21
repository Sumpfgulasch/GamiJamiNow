using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MeshUpdate : MonoBehaviour
{
    /// <summary>
    /// Douplicate most of the line renderer vertices to prevent it from unwanted bending.
    /// </summary>
    public static Vector3[] PreventLineRendFromBending(Vector3[] positions)
    {
        List<Vector3> newPositions = positions.ToList();
        int counter = 0;

        // skip first and last position
        for (int i = 1; i < positions.Length - 1; i++)
        {
            Vector3 curPos = positions[i];
            newPositions.Insert(i + counter, curPos);
            newPositions.Insert(i + counter, curPos);
            counter += 2;
        }

        return newPositions.ToArray();
    }
}
