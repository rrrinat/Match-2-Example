using DG.Tweening;
using UnityEngine;

namespace Match2.Scripts.Core.Level
{
    public class ChipView : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer renderer;

        public void Initialize(Sprite sprite)
        {
            renderer.sprite = sprite;
        }

        public void Decline()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOLocalRotate(new Vector3(0f, 0f, 45f), 0.1f)).
                     Append(transform.DOLocalRotate(new Vector3(0f, 0f, -45f), 0.1f)).
                     Append(transform.DOLocalRotate(Vector3.zero, 0.1f)).
                     Play();
        }
        
        public void SetSortingOrder(int sortingOrder)
        {
            renderer.sortingOrder = sortingOrder;
        }
        
        public void SetParent(CellView parent)
        {
            transform.SetParent(parent.transform);
        }

        public void Hide()
        {
            var sequence = DOTween.Sequence();
            var scaleTo = 
                transform.DOScale(Vector3.zero, 0.1f).SetEase(Ease.OutBack);
            sequence.Append(scaleTo);
        }

        public Tween MoveTo(Vector3 position, float duration)
        {
            var sequence = DOTween.Sequence();
            var moveTo = transform.DOMove(position, duration);
            //.SetEase(Ease.OutBack);
            sequence.Append(moveTo);

            return sequence;
        }
    }
}
