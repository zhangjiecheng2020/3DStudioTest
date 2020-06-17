using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Vector3 mOffset;         // difference between object world position and mouse world position
    private Quaternion objRotation;  // initial roatational position of the object
    private float mZCoord;           // world z coordinate of the game object

    // the following things happens when mouse is clicked
    void OnMouseDown() 
    {
        // Screen space is defined in pixels, WorldToScreenPoint translates between 
        //      scene coordinates to screen pixel coordinates. Screen coordinate 
        //      still comes out a Vector3 though, with the z just the 3D z coordinate 
        //      in the world.
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        // The position/rotation of the gameObject is its CoM coordinate. We need to add an offsset
        //      it to the final position to avoid a initial position jump when clicking.
        //      mOffset = game world position - mouse world position
        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
        objRotation = gameObject.transform.rotation;
    }
    
    // Returns the world position of the mouse as a Vector3
    private Vector3 GetMouseAsWorldPoint()
    {
        //mousePoint(x,y,z) = (x,y,z) coordinate of the mouse on the screen
        Vector3 mousePoint = Input.mousePosition;

        //mousePoint(z) = z coordinate of game object in the 3D game world
        mousePoint.z = mZCoord;

        //Convert to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);
     }
    
    // the follwoing things happens during mouse drag
    void OnMouseDrag()
    {
        // notice if mouse does not move then the object stays at the same place
        transform.position = GetMouseAsWorldPoint() + mOffset;
        // prevent the object from rotating from collision
        transform.rotation = objRotation;
        // stop all movements when dragged by mouse
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(0,0,0);
    }

    // PREVENT MOVING INTO WALLS

}
