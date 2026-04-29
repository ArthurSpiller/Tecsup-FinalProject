using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CardView : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _label;

    private int _index;
    private Action<int> _onClick;

    public void Init(PlayingCard card, int index, Action<int> onClick)
    {
        _index = index;
        _onClick = onClick;

        _label.text = card.ToString();

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        _onClick?.Invoke(_index);
    }
}
