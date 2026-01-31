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
        [SerializeField] private int _stepsCount = 3;
        [SerializeField, Range(0, 1)] private float _maxLevel = 0.5f;

        public float _currentCutLevel;

        public float _step;

        private void Start()
        {
            _currentCutLevel = 1;
            _material.SetFloat(CutLevel, _currentCutLevel);
            _step = (1 - _maxLevel) / _stepsCount;
        }

        private void OnDestroy()
        {
            _material.SetFloat(CutLevel, 1);
        }

        [Button]
        public void SetCutLevel(float level)
        {
            level = Mathf.Clamp(level, 0, 1);
            StopAllCoroutines();
            StartCoroutine(SmoothFloatChangeCoroutine(_currentCutLevel, level, CutLevel, _animationTime));
        }

        [Button]
        public void AddStep()
        {
            var nextLevel = _currentCutLevel - _step;

            nextLevel = Mathf.Clamp(nextLevel, _maxLevel, 1);
            StopAllCoroutines();
            StartCoroutine(SmoothFloatChangeCoroutine(_currentCutLevel, nextLevel, CutLevel, _animationTime));
        }

        private IEnumerator SmoothFloatChangeCoroutine(float startValue, float endValue, int propertyNameID,
            float animationTime)
        {
            float t = 0;

            while (t <= animationTime)
            {
                var currentValue = Mathf.Lerp(startValue, endValue, _animationCurve.Evaluate(t / animationTime));
                _currentCutLevel = currentValue;
                _material.SetFloat(propertyNameID, currentValue);
                t += Time.deltaTime;
                yield return null;
            }

            _material.SetFloat(propertyNameID, endValue);
            _currentCutLevel = endValue;
        }
    }
}