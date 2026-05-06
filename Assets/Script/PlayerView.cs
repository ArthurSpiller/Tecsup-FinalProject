using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Transform _handContainer;
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private CardSpriteProvider _spriteProvider;
    [SerializeField] private BiddingView _biddingView;

    private Action<int> _onCardSelected;
    private List<GameObject> _spawnedCards = new List<GameObject>();

    public void AskForBid(Action<BidType> onBidSelected) {
        _biddingView.Show(onBidSelected);
    }

    public void HideBid() {
        _biddingView.Hide();
    }

    public void ShowHand(List<PlayingCard> hand, Action<int> onCardSelected) {
        _onCardSelected = onCardSelected;

        ClearHand();

        for (int i = 0; i < hand.Count; i++) {
            var cardGO = Instantiate(_cardPrefab, _handContainer);
            var cardView = cardGO.GetComponent<CardView>();

            int index = i;

            cardView.Init(hand[i], index, OnCardClicked, _spriteProvider);

            _spawnedCards.Add(cardGO);
        }
    }

    private void OnCardClicked(int index) {
        _onCardSelected?.Invoke(index);
        _onCardSelected = null;
    }

    private void ClearHand() {
        foreach (var go in _spawnedCards)
            Destroy(go);
        _spawnedCards.Clear();
    }
}
