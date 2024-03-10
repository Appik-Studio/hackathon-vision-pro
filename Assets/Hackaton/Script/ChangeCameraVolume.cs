using UnityEngine;
using Unity.PolySpatial;

namespace PolySpatial.Samples
{
    public class ChangeCameraVolume : MonoBehaviour
    {
        // Reference to the PolyspatialCameraManager
      VolumeCamera m_VolumeCamera;

        // Start is called before the first frame update
        void Start()
        {
            // Get the PolyspatialCameraManager component
            m_VolumeCamera = FindObjectOfType<VolumeCamera>();
            
          if (m_VolumeCamera == null)
            {
                Debug.LogError("VolumeCamera component not found in the scene.");
                return;
            }
        }

        // Function to change camera volume to immersive mode
        public void ChangeToImmersiveVolume()
        {
            // Check if the cameraManager is initialized
        if (m_VolumeCamera != null)
            {
                // Set the camera volume to immersive mode
               //m_VolumeCamera.Mode = ;
            }
        }


    }
}
