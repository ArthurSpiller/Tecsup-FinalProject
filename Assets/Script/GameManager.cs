using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    private GameState _currentState;
    private Deck _deck;
    private List<Player> _players;
    private Trick _currentTrick;

    public List<PlayerView> _playerViews;
    
    [SerializeField] private DealSystem _dealSystem;
    [SerializeField] private BiddingSystem _biddingSystem;
    [SerializeField] private ChienHandlingSystem _chienHandlingSystem;
    [SerializeField] private PlayingSystem _playingSystem;
    [SerializeField] private CuttingSystem _cuttingSystem;

    private int _currentDealerId;
    private int _takerId;

    void Start() {
        ChangeState(GameState.Setup);
    }

    void Update() {
        UpdateState();
    }
    
    public void ChangeState(GameState state) {
        _currentState = state;

        switch (_currentState) {
        case GameState.Setup:
            EnterSetup();
            break;
        case GameState.Dealing:
            EnterDealing();
            break;
        case GameState.Bidding:
            EnterBidding();
            break;
        case GameState.ChienHandling:
            EnterChienHandling();
            break;
        case GameState.Playing:
            EnterPlaying();
            break;
        case GameState.Scoring:
            EnterScoring();
            break;
        case GameState.EndRound:
            EnterEndRound();
            break;
        case GameState.Cutting:
            EnterCutting();
            break;
        }
    }

    private void UpdateState() {
        switch (_currentState) {
        case GameState.Dealing:
            UpdateDealing();
            break;
        case GameState.Bidding:
            UpdateBidding();
            break;
        case GameState.ChienHandling:
            UpdateChienHandling();
            break;
        case GameState.Playing:
            UpdatePlaying();
            break;
        case GameState.Cutting:
            UpdateCutting();
            break;
        }
    }

    private void EnterSetup() {
        _deck = new Deck();
        _deck.InitDeck();

        _players = new List<Player>();

        for (int i = 0; i < _playerViews.Count; i++)
            _players.Add(new Player(i));

        _currentDealerId = 0;

        ChangeState(GameState.Dealing);
    }

    private void EnterDealing() {
        _dealSystem = new DealSystem(_players, _deck, _currentDealerId);
    }

    private void UpdateDealing() {
        _dealSystem.Update();

        if (_dealSystem.IsFinished)
            ChangeState(GameState.Bidding);
    }

    private void EnterBidding() {
        _takerId = -1;
        _biddingSystem = new BiddingSystem(_players);
    }

    private void UpdateBidding() {
        _biddingSystem.Update();

        if (_biddingSystem.IsFinished) {
            _takerId = _biddingSystem.TakerId;

            if (_takerId == -1)
                ChangeState(GameState.EndRound);
            else
                ChangeState(GameState.ChienHandling);
        }
    }

    private void EnterChienHandling() {
        _chienHandlingSystem = new ChienHandlingSystem(_players, _takerId);
    }

    private void UpdateChienHandling() {
        _chienHandlingSystem.Update();

        if (_chienHandlingSystem.IsFinished)
            ChangeState(GameState.Playing);
    }

    private void EnterPlaying() {
        _playingSystem = new PlayingSystem(_players, _takerId, _playerViews);
    }

    private void UpdatePlaying() {
        _playingSystem.Update();

        if (_playingSystem.IsFinished)
            ChangeState(GameState.Scoring);
    }

    private void EnterScoring() {
        float score = 0;

        foreach (Player player in _players)
            if (player._isTaker)
                score += player.CountPoints();
        Debug.Log($"Score: {score}");
        ChangeState(GameState.EndRound);
    }

    private void EnterEndRound() {
        _currentDealerId = (_currentDealerId + 1) % _players.Count;

        _deck.Clear();
        foreach (Player player in _players) {
            _deck.AddRange(player._scoringPile);
        }
        ChangeState(GameState.Cutting);
    }

    private void EnterCutting() {
        int cutterId = (_currentDealerId + 1) % _players.Count;

        _cuttingSystem = new CuttingSystem(_deck, cutterId, _playerViews[cutterId]);
    }

    private void UpdateCutting() {
        _cuttingSystem.Update();

        if (_cuttingSystem.IsFinished)
        {
            ChangeState(GameState.Dealing);
        }
    }
}
