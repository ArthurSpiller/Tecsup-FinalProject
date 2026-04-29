public class DealSystem
{
    private List<Player> _players;
    private Deck _deck;
    private int _dealerId;

    private int _currentPlayerId;
    private int _cardsDealt;
    private int _dogTarget;

    public bool IsFinished { get; private set; }

    public DealSystem(List<Player> players, Deck deck, int dealerId) {
        _players = players;
        _deck = deck;
        _dealerId = dealerId;

        _currentPlayerId = (dealerId + 1) % players.Count;
        _dogTarget = (players.Count == 5) ? 3 : 6;
    }

    public void Update() {
        if (IsFinished)
            return;
        DealStep();
    }

    private void DealStep() {
        for (int i = 0; i < 3; i++)
            _players[_currentPlayerId]._hand.Add(_deck.DrawTop());

        _currentPlayerId = (_currentPlayerId + 1) % _players.Count;

        // TODO: insert chien logic here later

        if (_deck.Count == _dogTarget)
            IsFinished = true;
    }
}
