using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Camera
{
    public class CameraPositionController : MonoBehaviour
    {
        [SerializeField] private Vector3 _gamePosition;
        [SerializeField] private Vector3 _gameRotation;
        
        [SerializeField] private Vector3 _idlePosiotion;
        [SerializeField] private Vector3 _idleRotation;

        private void Awake()
        {
            OnGameOver();
        }

        public void OnGameStarted()
        {

            StartCoroutine(InterpolateCoroutine(_gamePosition, _gameRotation));

        }

        public void OnGameOver()
        {
            StartCoroutine(InterpolateCoroutine(_idlePosiotion, _idleRotation));
        }
        
        private IEnumerator InterpolateCoroutine(Vector3 targetPosition, Vector3 targetRotation)
        {
            Vector3 startPosition = transform.localPosition;
            Quaternion startRotation = transform.localRotation;

            float elapsedTime = 0f;

            while (elapsedTime < 0.5)
            {
                float t = elapsedTime / 0.5f;

                transform.localPosition = Vector3.Lerp(startPosition, targetPosition, t);
                transform.localRotation = Quaternion.Euler(Vector3.Lerp(startRotation.eulerAngles, targetRotation, t));

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Ensure we reach the exact target position and rotation
            transform.localPosition = targetPosition;
            transform.localRotation = Quaternion.Euler(targetRotation);
        }
    }
}