using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManagement : MonoBehaviour
{
    


    void Start()
    {
        Player.instance.OnTreffen += ChangeColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangeColor(GameObject hitEdge, Vector2 pos)
    {
        var stateMachine = hitEdge.GetComponent<EdgeStateMachine>();

        var material = hitEdge.GetComponent<LineRenderer>().material;
        material.SetColor("Color_647f5e77a415409f9d3ac06cc7c7ffc4", GameManager.Instance.hitColor);     // hastag fun

        //if (stateMachine != null)
        //{
        //    if (stateMachine.PreviouslyHit)
        //    {
                
        //    }
        //}
    }
}
