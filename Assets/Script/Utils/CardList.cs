/*public class CardList {
    private List<Card> _list = new List<Card>();
    public int Count => _list.Count;
    
    public Card this[int index] {
        get => _list[index];
    }

    public void Add(Card toAdd) {
        _list.Add(toAdd);
    }

    public void RemoveAt(int index) {
        if (index >= Count || index < 0)
            return;
        _list.RemoveAt(index);
    }
    
    public Card DrawTop() {
        if (_list.Count == 0)
            return null;

        Card topCard = _list[0];
        _list.RemoveAt(0);
        return topCard;
    }

    public void AddRange(CardList other) {
        for (int i = 0; i < other.Count; i++)
            _list.Add(other[i]);
    }

    public void Clear() {
        _list.Clear();
    }
}*/
