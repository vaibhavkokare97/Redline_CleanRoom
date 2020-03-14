using UnityEngine;
using Redline.Manager;
using System.Collections;
using System;

namespace Redline.Game
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Vector2 _deltaJoystickValueInVector2;
        private Vector3 _deltaJoystickValueInVector3;
        private UI.JoystickController _joystickController;
        private LayerMask _groundLayer;

        private void Awake()
        {
            _joystickController = UIManager.instance._joystickController;
            _groundLayer = LayerMask.NameToLayer("Ground");
        }

        private void Update()
        {
            PlayerMovement();
            ApplyColorOnGround();
        }

        private void PlayerMovement()
        {
            // Motion
            _deltaJoystickValueInVector2 = _joystickController.joystickDelta.normalized * _speed;
            _deltaJoystickValueInVector3 = new Vector3(_deltaJoystickValueInVector2.x, 0, _deltaJoystickValueInVector2.y);
            transform.position += _deltaJoystickValueInVector3 * Time.deltaTime;
            transform.LookAt(transform.position + _deltaJoystickValueInVector3);
        }

        private bool ApplyColorOnGround()
        {
            // Raycast
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.position, Vector3.down, out hitInfo, 10f, ~_groundLayer))
            {
                GameObject g = GameManager.instance.brushPatchPool.Dequeue();
                g.transform.position = hitInfo.point + Vector3.up * 0.05f;
                GameManager.instance.brushPatchPool.Enqueue(g);
                return true;
            }
            return false;
        }
    }
}
