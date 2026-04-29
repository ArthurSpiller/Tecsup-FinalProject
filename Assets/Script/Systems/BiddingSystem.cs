public class BiddingSystem
{
    private List<Player> _players;
    private int _currentPlayer;
    private int _passes;

    public int TakerId { get; private set; } = -1;
    public bool IsFinished { get; private set; }

    public BiddingSystem(List<Player> players)
    {
        _players = players;
        _currentPlayer = 0;
    }

    public void Update()
    {
        if (IsFinished) return;

        AskPlayer();
    }

    private void AskPlayer()
    {
        // Replace later with PlayerView interaction
        bool takes = Random.value > 0.7f;

        if (takes)
        {
            TakerId = _currentPlayer;
            IsFinished = true;
            return;
        }

        _passes++;
        _currentPlayer = (_currentPlayer + 1) % _players.Count;

        if (_passes >= _players.Count)
        {
            IsFinished = true;
        }
    }
}
