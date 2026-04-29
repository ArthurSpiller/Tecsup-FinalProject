public class PlayingCard {
    public Suit _suit;
    public int _rank;

    public PlayingCard(Suit suit, int rank) {
        _suit = suit;
        _rank = rank;
    }

    public float GetScore() {
        if (_suit == Suit.None)
            return 0f;
        if (_suit == Suit.Trumps)
            return (_rank == 0 || _rank == 1 || _rank == 21 ? 4.5f : 0.5f);
        return _rank switch {
            14 => 4.5f,
            13 => 3.5f,
            12 => 2.5f,
            11 => 1.5f,
            _ => 0.5f
        };
    }

    public override string ToString() {
        if (_rank > 10 || _rank == 1) {
            switch (_rank) {
            case 11:
                return $"Jack of {_suit}";
                break;
            case 12:
                return $"Cavalier of {_suit}";
                break;
            case 13:
                return $"Queen of {_suit}";
                break;
            case 14:
                return $"King of {_suit}";
                break;
            case 1:
                return $"Ace of {_suit}";
                break;
            }
        }
        if (_suit == Suit.Trumps && _rank == 0)
            return "The Excuse";
        return $"{_rank} of {_suit}";
    }
}
