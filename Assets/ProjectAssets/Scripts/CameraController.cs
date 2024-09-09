using System.Collections;
using Cinemachine;
using UnityEngine;

namespace ProjectAssets.Scripts
{
    public class CameraController : MonoBehaviour
    {
        private const float _defaultAmplitude = 0f;
        private const float _defaultFrequency = 0f;
        
        [SerializeField] private float _amplitude;
        [SerializeField] private float _frequency;
        [SerializeField] private float _duration;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;

        private CinemachineBasicMultiChannelPerlin _noise;
        

        private void Awake()
        {
            _noise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        public void ShakeCamera()
        {
            StartCoroutine(Shake());
        }

        private IEnumerator Shake()
        {
            _noise.m_AmplitudeGain = _amplitude;
            _noise.m_FrequencyGain = _frequency;

            yield return new WaitForSeconds(_duration);

            _noise.m_AmplitudeGain = _defaultAmplitude;
            _noise.m_FrequencyGain = _defaultFrequency;
        }
    }
}