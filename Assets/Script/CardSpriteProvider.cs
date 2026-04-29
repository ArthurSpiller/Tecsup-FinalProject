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
                Debug.Log($"Mapping sprite: {sprite.name} to Suit={suit}, Rank={rank}");
                _spriteMap[(suit, rank)] = sprite;
            }
        }
    }

    public Sprite GetSprite(Suit suit, int rank) {
        Debug.Log($"Requesting sprite for Suit={suit}, Rank={rank}");
        if (_spriteMap == null) {
            Debug.LogError("Sprite map is not initialized!");
            return null;
        }
        if (_spriteMap.TryGetValue((suit, rank), out Sprite sprite))
            return sprite;
        Debug.LogError($"Sprite not found for card: Suit={suit}, Rank={rank}");
        return null;
    }
}