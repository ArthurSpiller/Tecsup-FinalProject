public class CuttingSystem
{
    private Deck _deck;
    private int _playerId;
    private PlayerView _playerView;

    public bool IsFinished { get; private set; }

    public CuttingSystem(Deck deck, int playerId, PlayerView view) {
        _deck = deck;
        _playerId = playerId;
        _playerView = view;

        AskPlayerToCut();
    }

    private void AskPlayerToCut() {
        _playerView.ShowCutUI(OnCutChosen);
    }

    private void OnCutChosen(int cutIndex) {
        _deck.Cut(cutIndex / _deck.Count);
        IsFinished = true;
    }

    public void Update() { }
}
