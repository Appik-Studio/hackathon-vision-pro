using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

namespace PolySpatial.Samples
{
    public class ActiveGoUI : MonoBehaviour
    {
            [SerializeField]
            bool useImage = false;
            [SerializeField]
            Image m_Image;
            [SerializeField]
            Color m_DefaultColor;
            [SerializeField]
             Color m_SelectedColor;

            [SerializeField]
            SpatialUIButton m_Button;

            [SerializeField]
            List<GameObject> m_ObjectsToShow;
            [SerializeField]
            List<GameObject> m_ObjectsToHide;

            [SerializeField]
            bool isShowing = false;




            void Start()
            {
                if(useImage){
                    m_DefaultColor = m_Image.color;
                }
               
                foreach(GameObject go in m_ObjectsToShow) {
                    go.SetActive(false);
                }
            }

            void OnEnable()
            {
                m_Button.WasPressed += WasPressed;
            }

           void WasPressed(string buttonText, MeshRenderer meshrenderer)
            {
                if(m_ObjectsToHide.Count !=0){
                     foreach(GameObject go in m_ObjectsToHide) {
                        go.SetActive(false);
                     }
                } 
                
                foreach(GameObject go in m_ObjectsToShow) {
                    go.SetActive(true);
                }

                if(useImage){
                    m_Image.color = isShowing ? m_SelectedColor : m_DefaultColor;
                }

                isShowing = true;
            }

            public void ResetPinButton(){
                if(useImage){
                  m_Image.color = m_DefaultColor;
                }
                  isShowing = false;
            }

    }
}