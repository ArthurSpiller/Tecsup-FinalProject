using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CardView : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _label;
    [SerializeField] private Image _image;

    private int _index;
    private Action<int> _onClick;

    public void Init(PlayingCard card, int index, Action<int> onClick, CardSpriteProvider spriteProvider)
    {
        _index = index;
        _onClick = onClick;

        _label.text = card.ToString();
        if (spriteProvider == null) {
            Debug.LogError("CardSpriteProvider is null!");
            return;
        }
        _image.sprite = spriteProvider.GetSprite(card._suit, card._rank);
        if (_image.sprite == null)
            Debug.LogError($"Failed to set card image for {card}: Sprite is null");

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        _onClick?.Invoke(_index);
    }
}
