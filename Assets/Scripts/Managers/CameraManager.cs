using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Managers
{
    public enum CameraType
    {
        Game,
        LevelEnd
    }
    
    [Serializable]
    public class CinemachineCamera
    {
        public CinemachineVirtualCamera camera;
        public CameraType cameraType;

        public CinemachineCamera(CinemachineVirtualCamera cam, CameraType camType)
        {
            camera = cam;
            cameraType = camType;
        }
    }

    public class CameraManager : MonoBehaviour
    {
        #region Variables

        public static CameraManager Instance;

        [SerializeField] private CameraType defaultCameraType;
        [SerializeField] private List<CinemachineCamera> cinemachineCameras;

        private CinemachineCamera currentCamera;

        #endregion

        #region UnityFunctions

        private void Awake()
        {
            Instance = this;
        }

        #endregion

        #region Custom Functions

        internal void CameraStartFunctions()
        {
            SetDefaultCamera();
            //SetCameraFollowTransform(LevelManager.Instance.GetPlayerTransform());
        } 

        public void SetCameraFollowTransform(Transform followTransform)
        {
            currentCamera.camera.m_Follow = followTransform;
        }

        public void TransitionTo(CameraType cameraType)
        {
            cinemachineCameras.ForEach(x => x.camera.gameObject.SetActive(false));
            var foundCamera = cinemachineCameras.Find(x => x.cameraType == cameraType);
            currentCamera = foundCamera;
            foundCamera.camera.gameObject.SetActive(true);
        }

        private void SetDefaultCamera()
        {
            TransitionTo(defaultCameraType);
        }

        #endregion
    }
}