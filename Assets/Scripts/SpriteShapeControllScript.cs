using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SpriteShapeControllScript : MonoBehaviour
{
    public SpriteShapeController spriteShapeController;

    public Vector3[] positions;

    private void Awake()
    {
        spriteShapeController = this.GetComponent<SpriteShapeController>();

        positions = new Vector3[]
        {
            new Vector3(1,1,0),
            new Vector3(1,-1,0),
            new Vector3(-1,-1,0),
            new Vector3(-1,1,0)
        };
    }

    private void Start()
    {
        print("start");
        for (int i = 0; i < spriteShapeController.spline.GetPointCount(); i++)
        {
            print("spline " + spriteShapeController.spline.GetPosition(i));
            spriteShapeController.spline.SetPosition(i, positions[i]);
            print("spline " + spriteShapeController.spline.GetPosition(i));
        }
    }


}
