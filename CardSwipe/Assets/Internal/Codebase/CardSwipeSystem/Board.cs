using UnityEngine;
using UnityEngine.EventSystems;

namespace RimuruDev.Internal.Codebase.CardSwipeSystem
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public sealed class Board : MonoBehaviour
    {
        public static Board Instance;

        private Transform draggedCard;
        private Transform originalSlot;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        public bool CanPlaceCardInSlot(Transform slot) =>
            slot.childCount == 0;

        public void PlaceCardInSlot(Transform card, Transform slot)
        {
            if (CanPlaceCardInSlot(slot))
            {
                card.SetParent(slot);
                card.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
        }

        public void StartDrag(Transform card, Transform slot)
        {
            draggedCard = card;
            originalSlot = slot;
        }

        public void EndDrag(PointerEventData eventData)
        {
            if (eventData.pointerEnter != null && eventData.pointerEnter.GetComponent<SlotDropHandler>() != null)
            {
                var targetSlot = eventData.pointerEnter.transform;

                if (CanPlaceCardInSlot(targetSlot))
                {
                    PlaceCardInSlot(draggedCard, targetSlot);
                }
                else if (targetSlot.childCount > 0)
                {
                    var cardInTargetSlot = targetSlot.GetChild(0);
                    PlaceCardInSlot(cardInTargetSlot, originalSlot);
                    PlaceCardInSlot(draggedCard, targetSlot);
                }
            }
            else
            {
                PlaceCardInSlot(draggedCard, originalSlot);
            }

            draggedCard = null;
            originalSlot = null;
        }

        public Transform GetOriginalSlot()
        {
            return originalSlot;
        }
    }
}