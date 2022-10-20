using System;
using Match2.Scripts.Core.Level;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Match2.Scripts.Core.InputProcessor
{
    public class InputController : MonoBehaviour
    {
        [SerializeField]
        private LayerMask cellLayerMask;
        
        private InputControllerContext context;
        private Camera camera;
        private Collider2D[] colliders;
        
        public InputControllerContext Context => context;

        private void Awake()
        {
            camera = Camera.main;
            colliders = new Collider2D[5];
        }

        public void SetContext(InputControllerContext context)
        {
            this.context = context;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 inputPosition = camera.ScreenToWorldPoint(Input.mousePosition);

                var cellView = Select(inputPosition);
                if (cellView != null)
                {
                    cellView.OnCellClicked();
                }
            }
        }
        /*
        public void OnPointerDown(PointerEventData eventData)
        {
            Vector2 inputPosition = camera.ScreenToWorldPoint(Input.mousePosition);

            var cellView = Select(inputPosition);
            Debug.Log($"<color=red>OnPointerDown</color>");
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            
        }

        public void OnDrag(PointerEventData eventData)
        {
            
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            
        }
        */
        private CellView Select(Vector2 position)
        {
            var count = Physics2D.OverlapPointNonAlloc(position, colliders, cellLayerMask);
            if (count == 0)
            {
                return null;
            }
            foreach (var collider in colliders)
            {
                var cellView = collider.GetComponent<CellView>();
                if (cellView != null)
                {
                    return cellView;
                }
            }

            return null;
        }    
    }
}
