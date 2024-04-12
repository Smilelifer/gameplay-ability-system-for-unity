using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Profiling;

public class PoinitEventTest : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveSpeed = 10;
//定义对象移动的速度。

        float horizontalInput = Input.GetAxis("Horizontal");
//获取 Horizontal 输入轴的值。

        float verticalInput = Input.GetAxis("Vertical");
//获取 Vertical 输入轴的值。

        Debug.Log("HorizontalInput: " + horizontalInput + " VerticalInput: " + verticalInput);

        transform.Translate(new Vector3(horizontalInput, 0, verticalInput) * moveSpeed * Time.deltaTime);
//将对象移动到分别定义为 horizontalInput、0 和 verticalInput 的 XYZ 坐标。


        var fingerCount = 0;
        print("Input.touches.Length: " + Input.touches.Length);
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
            {
                fingerCount++;
            }
        }
        if (fingerCount > 0)
        {
            print("User has " + fingerCount + " finger(s) touching the screen");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.LogError("OnPointerClick" + eventData.ToString());
    }
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.LogError("OnPointerEnter" + eventData.ToString());
    }

    
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.LogError("OnPointerExit" + eventData.ToString())    ;
        
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.LogError("OnPointerDown" + eventData.ToString());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.LogError("OnPointerUp" + eventData.ToString());
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.LogError("OnBeginDrag" + eventData.ToString());
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.LogError("OnDrag" + eventData.ToString());
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.LogError("OnEndDrag" + eventData.ToString());
    }

    public void OnMouseDown()
    {
        Profiler.BeginSample("OnMouseDown");
        Debug.LogError("OnMouseDown");
        Profiler.EndSample();
    }

    public void OnTestUIButtonClick()
    {
        Debug.LogError("OnTestUIButtonClick");
        List<int> list = new List<int>();
        TestFun(list);
    }

    public void TestFun<T>(List<T> x)
    {
        
    }
}
