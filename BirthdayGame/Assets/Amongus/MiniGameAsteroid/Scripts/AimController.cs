using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) // определение положения прицела
        {
            Vector2 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
            newPosition = new Vector2(newPosition.x - Screen.width / 2, newPosition.y - Screen.height / 2);
            transform.position = newPosition;
        }
        
    }
}
