// **********************************************
// Copyright(c) 2021 by com.ustar
// All right reserved
// 
// Author : Jian.Wang
// Date : 2023/07/13/12:33
// Ver : 1.0.0
// Description : FlyTransform.cs
// ChangeLog :
// **********************************************

using System;
using DG.Tweening;
using UnityEngine;

namespace TMGame
{
    public class FlyTransform : MonoBehaviour
    {
        private Vector3 _srcPos;
        private Vector2 _controlPos;
        private Transform _destTransform;
        private float _timeTotal;
        private float _timeDelay;
        private Action _onFinish;
        private float _tick;

        private void Update()
        {
            if (_timeDelay > 0.0f)
            {
                _timeDelay -= Time.deltaTime;
                return;
            }

            if (_tick >= _timeTotal)
            {
                if (_onFinish != null)
                {
                    _onFinish();
                }

                transform.DOPause();
                return;
            }

            _tick += Time.deltaTime;
            var pos = Bezier(_tick / _timeTotal, _srcPos, _controlPos, _destTransform.position);
            transform.DOMove(new Vector3(pos.x, pos.y, transform.position.z), Time.deltaTime);
        }

        public void InitData(Transform srcTransform, Vector2 controlPos, Transform destPos, float time = 1.0f,
            float delay = 0, Action action = null)
        {
            _srcPos = srcTransform.position;
            _controlPos = controlPos;
            _destTransform = destPos;
            _timeTotal = time;
            _timeDelay = delay;
            _onFinish = action;
            transform.position = new Vector3(_srcPos.x, _srcPos.y, transform.position.z);
            _tick = 0.0f;
        }

        public Vector2 Bezier(float t, Vector2 a, Vector2 b, Vector2 c)
        {
            var ab = Vector2.Lerp(a, b, t);
            var bc = Vector2.Lerp(b, c, t);
            return Vector2.Lerp(ab, bc, t);
        }
    }
}