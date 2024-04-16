using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ThreeDMathTest : MonoBehaviour
{
    Vector2 vector2;
    Vector3 vector3;
    Vector4 vector4;
    Matrix4x4 matrix4x4;
    Quaternion quaternion;
    
    //测试实现Vector3的全部API的使用方法，并给出例子

    private Vector3 testIn;
    private Vector3 testNormal;
    [Button]
    void TestReflect(Vector3 testIn, Vector3 testNormal)
    {
        this.testIn = testIn;
        this.testNormal = testNormal;

        vector3 = Vector3.Reflect(testIn, testNormal);
        Debug.Log("Reflect: " + vector3);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(testIn, 0.1f);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(testNormal, 0.1f);
        
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(vector3, 0.1f);
    }
}
