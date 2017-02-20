using UnityEngine;
using System.Collections.Generic;

// Very simple smooth mouselook modifier for the MainCamera in Unity
// by Francis R. Griffiths-Keam - www.runningdimensions.com

namespace ControlWrapping
{
    class KeyMouseInputWrapper
    {
        public delegate void UpdateStateDel(float tick);

        Vector2 _mouseAbsolute;
        Vector2 _smoothMouse;

        public Vector2 clampInDegrees = new Vector2(360, 180);
        private bool lockCursor;
        private float sensitivityX = 2;
        private float sensitivityY = 2;
        private float smoothingX = 3;
        private float smoothingY = 3;
        public Vector2 sensitivity = new Vector2(2, 2);
        public Vector2 smoothing = new Vector2(3, 3);
        public Vector2 targetDirection;
        public Vector2 targetCharacterDirection;


        public KeyMouseInputWrapper()
        {
            // Set target direction to the camera's initial orientation.
            targetDirection = new Vector2(GetRawAxis(AxisCode.MouseX), GetRawAxis(AxisCode.MouseY));
            LockCursor = lockCursor;
        }

        public bool LockCursor
        {
            get
            {
                return lockCursor;
            }

            set
            {
                lockCursor = value;
                Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.Confined;
                Cursor.visible = !lockCursor;
            }
        }

        public void UpdateState(float tick)
        {
            
        }

        bool GetKey(ActionKeyCode key)
        {
            if ((int)key > 400)
            {
                Debug.LogError("Not A Key!");
                return false;
            }
            return Input.GetKey((KeyCode)((int)key));
        }

        bool GetKeyDown(ActionKeyCode key)
        {
            if ((int)key > 400)
            {
                Debug.LogError("Not A Key!");
                return false;
            }
            return Input.GetKeyDown((KeyCode)((int)key));
        }

        bool GetKeyUp(ActionKeyCode key)
        {
            if ((int)key > 400)
            {
                Debug.LogError("Not A Key!");
                return false;
            }
            return Input.GetKeyUp((KeyCode)((int)key));
        }

        float GetSmoothedAxis(AxisCode axis)
        {
            if ((int)axis < 400)
            {
                return Input.GetKeyUp((KeyCode)((int)axis)) ? 1f : 0f;
            }
            else if ((int)axis > 500)
            {
                if (!LockCursor)
                {
                    return Input.GetAxisRaw(AxisLookup(axis));
                }
                else
                {
                    
                }
            }
            return 0f;
        }

        float GetRawAxis(AxisCode axis)
        {
            if ((int)axis < 400)
            {
                return Input.GetKeyUp((KeyCode)((int)axis)) ? 1f : 0f;
            }
            else if ((int)axis > 500)
            {
                Input.GetAxisRaw(AxisLookup(axis));
            }
            return 0f;
        }

        private string AxisLookup(AxisCode axis)
        {
            switch (axis)
            {
                case AxisCode.MouseX:
                    return "Mouse X";
                case AxisCode.MouseY:
                    return "Mouse y";
                default:
                    Debug.LogError("Not a mouse axis");
                    return "";
            }

        }

        //TODO: Mouse smoothing
        //void MouseSmoothing()
        //{
        //    // Allow the script to clamp based on a desired target value.
        //    Quaternion targetOrientation = Quaternion.Euler(targetDirection);
        //    Quaternion targetCharacterOrientation = Quaternion.Euler(targetCharacterDirection);

        //    // Get raw mouse input for a cleaner reading on more sensitive mice.
        //    var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        //    // Scale input against the sensitivity setting and multiply that against the smoothing value.
        //    mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity.x * smoothing.x, sensitivity.y * smoothing.y));

        //    // Interpolate mouse movement over time to apply smoothing delta.
        //    _smoothMouse.x = Mathf.Lerp(_smoothMouse.x, mouseDelta.x, 1f / smoothing.x);
        //    _smoothMouse.y = Mathf.Lerp(_smoothMouse.y, mouseDelta.y, 1f / smoothing.y);

        //    // Find the absolute mouse movement value from point zero.
        //    _mouseAbsolute += _smoothMouse;

        //    // Clamp and apply the local x value first, so as not to be affected by world transforms.
        //    if (clampInDegrees.x < 360)
        //        _mouseAbsolute.x = Mathf.Clamp(_mouseAbsolute.x, -clampInDegrees.x * 0.5f, clampInDegrees.x * 0.5f);

        //    var xRotation = Quaternion.AngleAxis(-_mouseAbsolute.y, targetOrientation * Vector3.right);
        //    transform.localRotation = xRotation;

        //    // Then clamp and apply the global y value.
        //    if (clampInDegrees.y < 360)
        //        _mouseAbsolute.y = Mathf.Clamp(_mouseAbsolute.y, -clampInDegrees.y * 0.5f, clampInDegrees.y * 0.5f);

        //    transform.localRotation *= targetOrientation;

        //    // If there's a character body that acts as a parent to the camera
        //    if (characterBody)
        //    {
        //        var yRotation = Quaternion.AngleAxis(_mouseAbsolute.x, characterBody.transform.up);
        //        characterBody.transform.localRotation = yRotation;
        //        characterBody.transform.localRotation *= targetCharacterOrientation;
        //    }
        //    else
        //    {
        //        var yRotation = Quaternion.AngleAxis(_mouseAbsolute.x, transform.InverseTransformDirection(Vector3.up));
        //        transform.localRotation *= yRotation;
        //    }
        //}
    }
}
