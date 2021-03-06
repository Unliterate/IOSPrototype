﻿using UnityEngine;
using System.Collections;

public class CamMove : MonoBehaviour
{
    private Camera MyCamera = null;

    Vector3 DesiredPosition = new Vector3(0, 0, -5);

    Vector2 MinRanges = new Vector2(-10, -10);
    Vector2 MaxRanges = new Vector2(10, 10);

    Vector3 Velocity = Vector3.zero;

    private void Awake()
    {
        MyCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                //TODO - Why is the height being added???
                Vector2 Pos = Input.GetTouch(0).position; 
				
                Pos.y -= Screen.height;

                CheckTouch(Pos);
            }
        }

        if (Input.touchCount > 1)
        {
            if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                MyCamera.orthographicSize += deltaMagnitudeDiff * 0.01f;
                MyCamera.orthographicSize = Mathf.Clamp(MyCamera.orthographicSize, 2, 10);
            }
        }
        else if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                DesiredPosition.x -= Input.GetTouch(0).deltaPosition.x * 0.01f;
                DesiredPosition.y -= Input.GetTouch(0).deltaPosition.y * 0.01f;

                DesiredPosition.x = Mathf.Clamp(DesiredPosition.x, MinRanges.x, MaxRanges.x);
                DesiredPosition.y = Mathf.Clamp(DesiredPosition.y, MinRanges.y, MaxRanges.y);
            }
        }

        transform.position = Vector3.SmoothDamp(transform.position, DesiredPosition, ref Velocity, 0.1f);
    }

    private void CheckTouch(Vector3 Position)
    {
        Camera C = GetComponent<Camera>();
        Vector2 TouchPos = C.ScreenToWorldPoint(new Vector3(Position.x, Position.y, C.nearClipPlane));

        

        Collider2D Hit = Physics2D.OverlapPoint(TouchPos);
        if(Hit != null)
        {
            InteractiveObject T = Hit.GetComponent<InteractiveObject>();
            if(T != null)
            {
                T.OnInteract();
            }
        }
    }
}
