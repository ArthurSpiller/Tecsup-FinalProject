using System.Collections.Generic;

public class ChienHandlingSystem
{
    private List<Player> _players;
    private int _takerId;

    public bool IsFinished { get; private set; }

    public ChienHandlingSystem(List<Player> players, int takerId)
    {
        _players = players;
        _takerId = takerId;

        StartChienPhase();
    }

    private void StartChienPhase()
    {
        // TODO:
        // - give dog cards
        // - open UI
    }

    public void Update()
    {
        // wait for player input
    }

    public void ConfirmDiscard(List<PlayingCard> selected)
    {
        foreach (var card in selected)
        {
            _players[_takerId]._scoringPile.Add(card);
            _players[_takerId]._hand.Remove(card);
        }

        IsFinished = true;
    }
}
