using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    private Deck _deck;
    private List<Player> _players;
    private Trick _currentTrick;

    public List<PlayerView> _playerViews;

    public int _currentDealerId;

    void Start() {
        StartGame();
    }

    public void StartGame() {
        _deck = new Deck();
        _deck.InitDeck();
        _players = new List<Player>();
        for (int i = 0; i < _playerViews.Count; i++)
            _players.Add(new Player(i));
        _currentDealerId = 0;

        Deal();

        UpdateAllViews();
    }

    public void Deal() {
        int playerToDealId = (_currentDealerId + 1) % _players.Count;
        int deckIndex = 0;

        while (_deck.Count - deckIndex > 3) {
            Player playerToDeal = _players[playerToDealId];
            for (int i = 0; i < 3; i++) {
                playerToDeal._hand.Add(_deck.DrawTop());
                deckIndex++;
            }
            playerToDealId = (playerToDealId + 1) % _players.Count;
        }
    }
}
