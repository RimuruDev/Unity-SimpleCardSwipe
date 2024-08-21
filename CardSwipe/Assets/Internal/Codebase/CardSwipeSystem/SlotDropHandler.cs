using UnityEngine;
using UnityEngine.EventSystems;

namespace RimuruDev.Internal.Codebase.CardSwipeSystem
{
    public class SlotDropHandler : MonoBehaviour, IDropHandler
    {
        private Board board;

        private void Start()
        {
            board = Board.Instance;
        }

        public void OnDrop(PointerEventData eventData)
        {
            var draggedCard = eventData.pointerDrag;
            if (draggedCard != null)
            {
                var originalSlot = board.GetOriginalSlot();

                if (board.CanPlaceCardInSlot(transform))
                {
                    board.PlaceCardInSlot(draggedCard.transform, transform);
                }
                else if (transform.childCount > 0 && originalSlot != null)
                {
                    var currentCard = transform.GetChild(0);

                    currentCard.SetParent(originalSlot);
                    currentCard.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

                    board.PlaceCardInSlot(draggedCard.transform, transform);
                }
            }
        }
    }
}