using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeStateMachine : MonoBehaviour
{ 
    public bool PreviouslyHit{get; private set;} = false;

    public void SetHit(){
        print("I have been hit");
        this.PreviouslyHit = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
