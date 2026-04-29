using System.Collections.Generic;

public class Deck {
    private List<PlayingCard> _deck = new List<PlayingCard>();
    public int Count => _deck.Count;

    public PlayingCard this[int index] {
        get => _deck[index];
    }
    
    public void InitDeck() {
        _deck.Clear();
        for (int rank = 1; rank <= 14; rank++) {
            _deck.Add(new PlayingCard(Suit.Hearts, rank));
            _deck.Add(new PlayingCard(Suit.Spades, rank));
            _deck.Add(new PlayingCard(Suit.Diamonds, rank));
            _deck.Add(new PlayingCard(Suit.Clubs, rank));
        }
        for (int rank = 0; rank <= 21; rank++)
            _deck.Add(new PlayingCard(Suit.Trumps, rank));
        Shuffle();
    }

    public void Shuffle() {
        for (int i = 0; i < _deck.Count; i++) {
            int randIndex = UnityEngine.Random.Range(0, _deck.Count);
            (_deck[i], _deck[randIndex]) = (_deck[randIndex], _deck[i]);
        }
    }

    public void Cut(float cutPercentage) {
        if (_deck.Count == 0)
            return;
        cutPercentage = Mathf.Clamp01(cutPercentage);
        int cutIndex = Mathf.FloorToInt(_deck.Count * cutPercentage);

        if (cutIndex == 0 || cutIndex == _deck.Count)
            return;
        var topChunk = _deck.GetRange(0, cutIndex);
        _deck.RemoveRange(0, cutIndex);
        _deck.AddRange(topChunk);
    }

    public PlayingCard DrawTop() {
        if (_deck.Count == 0)
            return null;
        PlayingCard topCard = _deck[0];
        _deck.removeAt(0);
        return topCard;
    }
}
