using UnityEngine.EventSystems;
using UnityEngine;

namespace Redline.UI
{
    public class JoystickController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [HideInInspector] public Vector2 joystickDelta;
        [SerializeField] private RectTransform _joystickBall;

        private RectTransform _joystickContainer;
        private float _halfContainerRectWidth;
        
        public void Awake()
        {
            _joystickContainer = gameObject.GetComponent<RectTransform>();

            // width and height are same as it is a square (so we can use any one of them, otherwise we would be requiring one more parameter)
            _halfContainerRectWidth = gameObject.GetComponent<RectTransform>().rect.width / 2f;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Vector2 position = Vector2.zero;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickContainer,
                    eventData.position,
                    eventData.pressEventCamera,
                    out position);

            _joystickBall.anchoredPosition = (Vector2.Distance(position, Vector2.zero) > _halfContainerRectWidth) ?
                position.normalized * _halfContainerRectWidth : position;

            joystickDelta = position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 position = Vector2.zero;
            
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickContainer,
                    eventData.position,
                    eventData.pressEventCamera,
                    out position);

            _joystickBall.anchoredPosition = (Vector2.Distance(position, Vector2.zero) > _halfContainerRectWidth) ?
                position.normalized * _halfContainerRectWidth : position;

            joystickDelta = position;
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            _joystickBall.anchoredPosition = Vector2.zero;
        }
    }
}
