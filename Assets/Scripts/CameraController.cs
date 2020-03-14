using UnityEngine;
using Redline.Manager;

namespace Redline.UI
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Vector3 _delta = new Vector3(0, 10, -10);
        [SerializeField] private float _cameraCatchUpSpeed = 1f;

        private Transform _broom;

        private void Awake()
        {
            _broom = GameManager.instance.broom;
        }

        private void Start()
        {
            transform.position = _broom.transform.position + _delta;
            transform.LookAt(_broom);
        }

        private void LateUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, _broom.transform.position + _delta, Time.deltaTime * _cameraCatchUpSpeed);
        }
    }
}
