using System.Collections.Generic;

public class BiddingSystem
{
    private List<Player> _players;
    private List<PlayerView> _views;

    private int _currentPlayer;
    private bool _waitingForInput;

    public bool IsFinished { get; private set; }
    public int TakerId { get; private set; } = -1;

    public BiddingSystem(List<Player> players, List<PlayerView> views, int currentDealerId) {
        _players = players;
        _views = views;

        _currentPlayer = (currentDealerId + 1) % players.Count;
        AskNextPlayer();
    }

    private void AskNextPlayer() {
        if (_currentPlayer >= _players.Count)
        {
            IsFinished = true;
            return;
        }

        _waitingForInput = true;

        _views[_currentPlayer].ShowHand(_players[_currentPlayer], null);
        _views[_currentPlayer].AskForBid(OnBidSelected);
    }

    private void OnBidSelected(BidType bid) {
        _waitingForInput = false;

        if (bid != BidType.Pass) {
            TakerId = _currentPlayer;
            IsFinished = true;
            return;
        }

        _currentPlayer++;
        AskNextPlayer();
    }

    public void Update() { }
}
