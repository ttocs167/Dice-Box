using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalHelper : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetNormal();
        }
    }
    
    private void GetNormal()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Hit object normal: " + hit.normal);
        }
        else
        {
            Debug.Log("No object hit");
        }
        //
    }
}
