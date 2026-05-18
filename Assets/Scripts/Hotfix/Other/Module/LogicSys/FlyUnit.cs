// **********************************************
// Copyright(c) 2021 by com.ustar
// All right reserved
// 
// Author : Jian.Wang
// Date : 2023/07/20/10:51
// Ver : 1.0.0
// Description : FlyUnit.cs
// ChangeLog :
// **********************************************

using System;
using System.Collections;
using DG.Tweening;
using DragonPlus.Core;
using UnityEngine;

namespace TMGame
{
    public struct FlyUnit
    {
        private Vector2 _srcPos;
        private Vector2 _controlPos;
        private Transform _destTransform;
        private Vector3 _destPos;
        private float _timeTotal;
        private float _timeDelay;
        private Action _callBack;
        private float _tick;
        private Transform _transform;

        // Update is called once per frame
        IEnumerator Update()
        {
            while (true)
            {
                while (_timeDelay > 0.0f)
                {
                    _timeDelay -= Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }

                if (_transform == null)
                {
                    yield break;
                }

                if (_tick >= _timeTotal)
                {
                    _transform.DOPause();
                    _callBack?.Invoke();
                    _callBack = null;
                    yield break;
                }

                _tick += Time.deltaTime;
                var pos = Bezier(_tick / _timeTotal, _srcPos, _controlPos, _destTransform == null ? _destPos : _destTransform.position);
                // _transform.DOMove(new Vector3(pos.x, pos.y, _transform.position.z), Time.deltaTime);
                _transform.position = new Vector3(pos.x, pos.y, _transform.position.z);

                yield return new WaitForEndOfFrame();
            }
        }

        public void Start(Vector2 srcPos, Transform targetTransform, Vector2 controlPos, Transform transform, float totalTime = 1.0f, float delay = 0, Action callback = null)
        {
            _srcPos = srcPos;
            _destTransform = targetTransform;
            _controlPos = controlPos;
            _timeTotal = totalTime;
            _timeDelay = delay;
            _callBack = callback;
            _transform = transform;
            _tick = 0.0f;

            SDKUtil.Unity.StartCoroutine(Update());
        }

        public void Start(Vector2 srcPos, Vector3 targetPos, Vector2 controlPos, Transform transform, float totalTime = 1.0f, float delay = 0, Action callback = null)
        {
            _srcPos = srcPos;
            _destTransform = null;
            _destPos = targetPos;
            _controlPos = controlPos;
            _timeTotal = totalTime;
            _timeDelay = delay;
            _callBack = callback;
            _transform = transform;
            _tick = 0.0f;

            SDKUtil.Unity.StartCoroutine(Update());
        }

        private static Vector2 Bezier(float t, Vector2 a, Vector2 b, Vector2 c)
        {
            var ab = Vector2.Lerp(a, b, t);
            var bc = Vector2.Lerp(b, c, t);
            return Vector2.Lerp(ab, bc, t);
        }
    }
}