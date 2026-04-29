using System;
using System.Collections.Generic;

public class PlayingSystem
{
    private List<Player> _players;
    private List<PlayerView> _views;

    private int _takerId;
    private Trick _currentTrick;
    private int _currentPlayer;

    private bool _waitingForInput;

    public bool IsFinished { get; private set; }

    public PlayingSystem(List<Player> players, int takerId, List<PlayerView> views)
    {
        _players = players;
        _takerId = takerId;
        _views = views;

        StartNewTrick();
    }

    private void StartNewTrick()
    {
        _currentTrick = new Trick(_players.Count, _currentPlayer);
        _currentPlayer = _currentTrick._leaderId;
    }

    public void Update()
    {
        if (IsFinished) return;

        if (!_waitingForInput)
        {
            AskPlayerToPlay();
        }
    }

    private void AskPlayerToPlay()
    {
        var player = _players[_currentPlayer];
        var view = _views[_currentPlayer];

        // End condition
        if (player._hand.Count == 0)
        {
            IsFinished = true;
            return;
        }

        _waitingForInput = true;

        view.ShowHand(player._hand, OnCardSelected);
    }

    private void OnCardSelected(int cardIndex)
    {
        var player = _players[_currentPlayer];

        if (cardIndex < 0 || cardIndex >= player._hand.Count)
            return;

        var card = player._hand[cardIndex];

        player._hand.RemoveAt(cardIndex);
        _currentTrick._cardsPlayed[_currentPlayer] = card;

        _waitingForInput = false;

        AdvanceTurn();
    }

    private void AdvanceTurn()
    {
        _currentPlayer = (_currentPlayer + 1) % _players.Count;

        if (TrickComplete())
        {
            ResolveTrick();
        }
    }

    private bool TrickComplete()
    {
        foreach (var c in _currentTrick._cardsPlayed)
            if (c == null) return false;

        return true;
    }

    private void ResolveTrick()
    {
        int winnerIndex = _currentTrick.GetWinningIndex();

        foreach (var card in _currentTrick._cardsPlayed)
        {
            _players[winnerIndex]._scoringPile.Add(card);
        }

        _currentPlayer = winnerIndex;

        StartNewTrick();
    }
}
