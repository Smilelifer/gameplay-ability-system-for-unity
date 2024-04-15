using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestJoyStick : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public float range = 50;
    private RectTransform rectTransform;
    public Action<Vector2> OnJoystickMove;
    public Action OnJoystickUp;
    
    void Start()
    {
        rectTransform = this.GetComponent<RectTransform>();
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 anchoredPosition;
        // 将屏幕坐标转换为UI元素的局部坐标
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out anchoredPosition);
        Debug.Log("OnDrag 屏幕坐标 = " + eventData.position + "转换坐标 = " + anchoredPosition);
        // anchoredPosition.x = Mathf.Clamp(anchoredPosition.x, -range, range);
        // anchoredPosition.y = Mathf.Clamp(anchoredPosition.y, -range, range);
        //
        if (anchoredPosition.sqrMagnitude < range * range)
        {
            rectTransform.anchoredPosition = anchoredPosition;
        }
        else
        {
            rectTransform.anchoredPosition = anchoredPosition.normalized * range;
        }

        OnJoystickMove(anchoredPosition.normalized);
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = Vector2.zero;
        OnJoystickUp();
    }
}
