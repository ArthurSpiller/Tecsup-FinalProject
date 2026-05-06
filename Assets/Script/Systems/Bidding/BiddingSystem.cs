using System.Collections.Generic;
using UnityEngine;

public class BiddingSystem
{
    private List<Player> _players;
    private List<PlayerView> _views;

    private int _currentPlayer;
    private int _dealerId;
    private bool _waitingForInput;
    private bool flag = false;

    public bool IsFinished { get; private set; }
    public int TakerId { get; private set; } = -1;

    public BiddingSystem(List<Player> players, List<PlayerView> views, int dealerId) {
        _players = players;
        _views = views;
        _dealerId = dealerId;

        _currentPlayer = (_dealerId + 1) % players.Count;
        AskNextPlayer();
    }

    private void AskNextPlayer() {
        if (_currentPlayer == (_dealerId + 1) % _players.Count && flag) {
            IsFinished = true;
            return;
        }
        flag = true;
        Debug.Log($"Asking player {_currentPlayer} for bid");
        _waitingForInput = true;

        _views[_currentPlayer].ShowHand(_players[_currentPlayer]._hand, null);
        _views[_currentPlayer].AskForBid(OnBidSelected);
    }

    private void OnBidSelected(BidType bid) {
        _waitingForInput = false;

        Debug.Log($"Player {_currentPlayer} selected bid {bid}");

        if (bid != BidType.Pass) {
            TakerId = _currentPlayer;
            IsFinished = true;
            return;
        }

        _currentPlayer = (_currentPlayer + 1) % _players.Count;
        AskNextPlayer();
    }

    public void Update() { }
}
