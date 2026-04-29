using System.Collections.Generic;

public class Trick {
    public int _leaderId;
    public List<PlayingCard> _cardsPlayed;

    public Trick (int nbPlayers, int dealerId) {
        _leaderId = (dealerId + 1) % nbPlayers;
        _cardsPlayed = new List<PlayingCard>(new PlayingCard[nbPlayers]);
    }

    public int GetWinningIndex() {
        int winningIndex = 0;

        for (int i = 1; i < _cardsPlayed.Count; i++) {
            var current = _cardsPlayed[i];
            var best = _cardsPlayed[winningIndex];

            if (current._suit != best._suit) {
                if (current._suit == Suit.Trumps && current._rank != 0)
                        winningIndex = i;
                continue;
            }
            if (current._rank > best._rank)
                winningIndex = i;
        }
        return winningIndex;
    }
}
