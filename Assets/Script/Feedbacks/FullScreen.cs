using System;
using System.Collections;
using EasyButtons;
using UnityEngine;

namespace GameJam
{
    public class FullScreenEffectController : MonoBehaviour
    {
        private static readonly int CutLevel = Shader.PropertyToID("_CutLevel");
        [SerializeField] private Material _material;
        [SerializeField] private float _animationTime = .2f;
        [SerializeField] private AnimationCurve _animationCurve;


        private float _currentCutLevel;

        private void Start()
        {
            _currentCutLevel = _material.GetFloat(CutLevel);
        }

        private void OnDestroy()
        {
            _material.SetFloat(CutLevel, 1);
        }

        [Button]
        public void SetCutLevel(float level)
        {
            StartCoroutine(SmoothFloatChangeCoroutine(_currentCutLevel, level, CutLevel, _animationTime));
        }

        
        
        private IEnumerator SmoothFloatChangeCoroutine(float startValue, float endValue, int propertyNameID,
            float animationTime)
        {
            float t = 0;

            while (t <= animationTime)
            {
                var currentValue = Mathf.Lerp(startValue, endValue, _animationCurve.Evaluate(t / animationTime));
                _material.SetFloat(propertyNameID, currentValue);
                t += Time.deltaTime;
                yield return null;
            }

            _material.SetFloat(propertyNameID, endValue);
            _currentCutLevel = endValue;
        }
    }
}