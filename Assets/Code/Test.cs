using Clock;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] MeshRenderer[] _renderers;

    [SerializeField] MeshRenderer _currentItem;

    LineRenderer lineRenderer;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>(); 
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            StopHighlight();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                _currentItem = _renderers.Closest(hit.point);
                lineRenderer.SetPosition(0,_currentItem.transform.position);
                lineRenderer.SetPosition(1, hit.point);
                Higlhit();               
            }
           
        }
    }

    public void Higlhit()
    {
        
        _currentItem.material.color = Color.red;
        
    }

    public void StopHighlight()
    {
        if (_currentItem)
        {

        _currentItem.material.color = Color.black; 
        }
    }
}
