using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BiddingSystem : MonoBehaviour {
    private List<Player> _players;
    private int _currentPlayer;
    private int _passes;

    public int TakerId { get; private set; } = -1;
    public bool IsFinished { get; private set; }
    [SerializeField] private Transform _biddingPanel;
    [SerializeField] private Button _passButton;
    [SerializeField] private Button _takeButton;
    private bool _awaitingInput;

    public BiddingSystem(List<Player> players, int currentDealerId)
    {
        _players = players;
        _currentPlayer = (currentDealerId + 1) % players.Count;

        if (_biddingPanel != null)
            _biddingPanel.gameObject.SetActive(true);
        _awaitingInput = false;
        if (_passButton != null)
            _passButton.onClick.AddListener(PlayerPasses);
        if (_takeButton != null)
            _takeButton.onClick.AddListener(PlayerTakes);
        AskPlayer();
    }

    public void SetBiddingPanel(Transform panel)
    {
        _biddingPanel = panel;
    }

    public void Update() {
        if (IsFinished) {
            if (_biddingPanel != null)
                _biddingPanel.gameObject.SetActive(false);
            return;
        }
    }

    public void PlayerPasses() {
        if (IsFinished || !_awaitingInput)
            return;

        _passes++;
        _awaitingInput = false;

        if (_passes >= _players.Count) {
            IsFinished = true;
            if (_biddingPanel != null)
                _biddingPanel.gameObject.SetActive(false);
            return;
        }

        _currentPlayer = (_currentPlayer + 1) % _players.Count;
        AskPlayer();
    }

    public void PlayerTakes() {
        if (IsFinished || !_awaitingInput)
            return;
        TakerId = _currentPlayer;
        IsFinished = true;
        _awaitingInput = false;
        if (_biddingPanel != null)
            _biddingPanel.gameObject.SetActive(false);
    }

    private void AskPlayer() {
        _awaitingInput = true;
    }
}
