using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.PolySpatial.InputDevices;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.LowLevel;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;
namespace AssetVision
{
    public class ModelViewBehaviours : MonoBehaviour
    {
        [SerializeField]
        GameObject m_GlobalEarth;

        [SerializeField]
        Transform contentViews;
        [SerializeField]
        Transform contentPins;

        [SerializeField]
        bool useShowGlobalEarth;


        protected void OnEnable()
            {
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
                            if(useShowGlobalEarth){
                                ResetAllPinPlusButton();
                                HideViews();
                                gameObject.SetActive(false);
                            }
                        }
                    }  
                }   
            }

        public void HideViews(){
            foreach(Transform child in contentViews){
                child.gameObject.SetActive(false);
            }

            m_GlobalEarth.SetActive(true);
        }

        public void ResetAllPinPlusButton(){
            foreach(Transform child in contentPins){
                Debug.Log("child is :" + child + " | get child 0 is :" +  child.GetChild(0)  );
                child.GetChild(1).GetComponent<PolySpatial.Samples.ActiveGoUI>().ResetPinButton();
            }
        }

    }
}