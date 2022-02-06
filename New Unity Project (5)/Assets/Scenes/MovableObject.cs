using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.Events;
using UnityEngine.Scripting;   
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class MovableObject : MonoBehaviour
{
    #region Private Properties
    float ZPosition;
    Vector3 OffSet;
    bool Dragging;
    #endregion

    #region Inspector Variables
    public Camera MainCamera;
    [Space]
    [SerializeField]
    public UnityEvent OnBeginDrag;
    [SerializeField]
    public UnityEvent OnEndDrag;
    #endregion

    #region Unity Functions
    void Start ()
    {
        ZPosition=MainCamera.WorldToScreenPoint(transform.position).z;
    }

    void Update()
    {
        if(Dragging)
        {
            Vector3 position=new Vector3 (Input.mousePosition.x, Input.mousePosition.y, ZPosition);
            transform.position=MainCamera.ScreenToWorldPoint (position+new Vector3(OffSet.x, OffSet.y));
        }
    }
    void OnMouseDown()
    {
        if (!Dragging)
        {
            BeginDrag();
        }
    }

    void OnMouseUp()
    {
        EndDrag();
    }
    #endregion

    #region User Interface
    public void BeginDrag()
    {
        OnBeginDrag.Invoke();
        Dragging=true;
        OffSet=MainCamera.WorldToScreenPoint(transform.position)-Input.mousePosition;
    }

    public void EndDrag()
    {
        OnEndDrag.Invoke();
        Dragging=false;
    }
    #endregion
}