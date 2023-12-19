﻿using System;
using UnityEngine;

namespace Demo.Script
{
    public class InputListener : MonoBehaviour
    {
        private Action<Vector3> _onMove;
        private Action _onPressQ;
        private Action _onPressE;
        private Action _onPressR;
        private Action _onPressMouseLeft;
        private Action<Vector3> _onMousePosition;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _onMove?.Invoke(Vector2.left);
            }
            
            if (Input.GetKeyDown(KeyCode.D))
            {
                _onMove?.Invoke(Vector2.right);
            }
            
            if (Input.GetKeyDown(KeyCode.W))
            {
                _onMove?.Invoke(Vector2.up);
            }
            
            if (Input.GetKeyDown(KeyCode.S))
            {
                _onMove?.Invoke(Vector2.down);
            }
            
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _onPressQ?.Invoke();
            }
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                _onPressE?.Invoke();
            }
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                _onPressR?.Invoke();
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                _onPressMouseLeft?.Invoke();
            }
            
            _onMousePosition?.Invoke(Input.mousePosition);
        }
        
        public void RegisterOnMove(Action<Vector3> onMove)
        {
            _onMove = onMove;
        }
        
        public void RegisterOnPressQ(Action onPressQ)
        {
            _onPressQ = onPressQ;
        }
        
        public void RegisterOnPressE(Action onPressE)
        {
            _onPressE = onPressE;
        }
        
        public void RegisterOnPressR(Action onPressR)
        {
            _onPressR = onPressR;
        }
        
        public void RegisterOnPressMouseLeft(Action onPressMouseLeft)
        {
            _onPressMouseLeft = onPressMouseLeft;
        }
        
        public void RegisterOnMousePosition(Action<Vector3> onMousePosition)
        {
            _onMousePosition = onMousePosition;
        }
    }
}