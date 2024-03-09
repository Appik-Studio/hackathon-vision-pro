using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

using Unity.PolySpatial.InputDevices;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.LowLevel;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace AssetVision
{
    public class EarthController : MonoBehaviour
    {
        [Tooltip("The default rotation speed when no interaction")]
        public float rotationSpeed = 1f;
        [Tooltip("The maximum rotation speed")]
        public float rotationSpeedMax = 150f;
        [Tooltip("The rotation deceleration when starting or stopping (must be < 1)")]
        public float rotationDeceleration = 50f;

        Vector3 _startEuler;
        Vector3 _startPosition;
        float _currentRotationSpeed;
        float _targetRotationSpeed;
        float _lastEventTime;
        Vector3 _lastEventPosition;
        bool _isInside = false;

         [SerializeField] bool _isInteracted;

        public void PauseRotation(bool pause)
        {
            _targetRotationSpeed = pause ? 0f : rotationSpeed;
        }

        

        protected void OnEnable()
        {
            _targetRotationSpeed = rotationSpeed;
            _currentRotationSpeed = _targetRotationSpeed;

            // enable enhanced touch support to use active touches for properly pooling input phases
            EnhancedTouchSupport.Enable();
        }

        protected void Update()
        {

            var activeTouches = Touch.activeTouches;

            if (activeTouches.Count > 0)
            {
                var primaryTouchData = EnhancedSpatialPointerSupport.GetPointerState(activeTouches[0]);
                if (activeTouches[0].phase == TouchPhase.Began)
                {
                    // allow balloons to be popped with a poke or indirect pinch
                    if (primaryTouchData.Kind == SpatialPointerKind.IndirectPinch || primaryTouchData.Kind == SpatialPointerKind.Touch)
                    {
                        _isInteracted= true;

                        Debug.Log("begin : "+ primaryTouchData.targetObject);

                         _startEuler = transform.eulerAngles;
                        _startPosition = primaryTouchData.interactionPosition;
                        _startPosition.y = transform.position.y;

                        _lastEventTime = Time.time;
                        _lastEventPosition = _startPosition;

                       
                    
                    }

                  
                }  
                  if (activeTouches[0].phase == TouchPhase.Moved)
                        {

                             Debug.Log("MOve : "+ primaryTouchData.targetObject);
                            //Raycaseter 
                            Vector3 currentPosition = primaryTouchData.interactionPosition;
                            currentPosition.y = transform.position.y;

                            // Update the internal computation values
                            float deviceSpeed = -1f * Vector3.SignedAngle(_lastEventPosition, currentPosition, Vector3.down) / (Time.time - _lastEventTime);
                            _lastEventPosition = currentPosition;

                            if (_isInteracted)
                            {
                                _lastEventTime = Time.time;
                                _currentRotationSpeed += 0.1f * deviceSpeed;
                            }
                            else
                            {
                                _currentRotationSpeed += deviceSpeed;
                            }

                            _currentRotationSpeed = Mathf.Clamp(_currentRotationSpeed, -rotationSpeedMax, rotationSpeedMax);
                        }  

                         if(activeTouches[0].phase == TouchPhase.Ended || activeTouches[0].phase == TouchPhase.Canceled)
                        {
                             Debug.Log("end : "+ primaryTouchData.targetObject);
                               _isInteracted = false;
                        }
            }
                    

            float sign = Mathf.Sign(_currentRotationSpeed);
            _currentRotationSpeed = Mathf.Abs(_currentRotationSpeed);
            if (_currentRotationSpeed < _targetRotationSpeed) {
                _currentRotationSpeed += rotationDeceleration * Time.deltaTime;
                if (_currentRotationSpeed > _targetRotationSpeed)
                    _currentRotationSpeed = _targetRotationSpeed;
            }
            else if (_currentRotationSpeed > _targetRotationSpeed)
            {
                _currentRotationSpeed -= rotationDeceleration * Time.deltaTime;
                if (_currentRotationSpeed < _targetRotationSpeed)
                    _currentRotationSpeed = _targetRotationSpeed;
            }

            _currentRotationSpeed = sign * _currentRotationSpeed;
            float offset = _currentRotationSpeed * Time.deltaTime;
            transform.Rotate(0, offset, 0);
        }

        
    }

}