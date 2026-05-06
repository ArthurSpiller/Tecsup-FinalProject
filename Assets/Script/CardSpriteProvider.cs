using UnityEngine;
using System.Collections.Generic;

public class CardSpriteProvider : MonoBehaviour {
    [SerializeField] private List<Sprite> _cardSprites;

    private Dictionary<(Suit, int), Sprite> _spriteMap;

    void Awake() {
        _spriteMap = new Dictionary<(Suit, int), Sprite>();

        foreach (var sprite in _cardSprites) {
            var parts = sprite.name.Split('_');
            if (parts.Length == 2) {
                Suit suit = System.Enum.Parse<Suit>(parts[0]);
                int rank = int.Parse(parts[1]);
                _spriteMap[(suit, rank)] = sprite;
            }
        }
    }

    public Sprite GetSprite(Suit suit, int rank) {
        if (_spriteMap == null) {
            return null;
        }
        if (_spriteMap.TryGetValue((suit, rank), out Sprite sprite))
            return sprite;
        Debug.LogError($" not found for card: Suit={suit}, Rank={rank}");
        return null;
    }
}