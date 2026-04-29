using System.Collections.Generic;

public class Player {
    public int _id;
    public List<PlayingCard> _hand = new List<PlayingCard>();
    public List<PlayingCard> _scoringPile = new List<PlayingCard>();
    public bool _isTaker;

    public Player(int id) {
        _id = id;
    }

    public float CountPoints() {
        float score = 0f;

        foreach (var playingCard in _scoringPile) {
            score += playingCard.GetScore();
        }
        return score;
    }
}
