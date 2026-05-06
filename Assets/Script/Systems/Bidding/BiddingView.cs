using UnityEngine;
using UnityEngine.UI;
using System;

public enum BidType
{
    Pass,
    Take/*,
    Garde,
    GardeSans,
    GardeContre*/
}

public class BiddingView : MonoBehaviour
{
    [SerializeField] private Button _passButton;
    [SerializeField] private Button _takeButton;
    // [SerializeField] private Button _gardeButton;
    // [SerializeField] private Button _gardeSansButton;
    // [SerializeField] private Button _gardeContreButton;

    private Action<BidType> _onBidSelected;

    public void Show(Action<BidType> onBidSelected)
    {
        gameObject.SetActive(true);
        _onBidSelected = onBidSelected;

        _passButton.onClick.AddListener(() => Select(BidType.Pass));
        _takeButton.onClick.AddListener(() => Select(BidType.Take));
        // _gardeButton.onClick.AddListener(() => Select(BidType.Garde));
        // _gardeSansButton.onClick.AddListener(() => Select(BidType.GardeSans));
        // _gardeContreButton.onClick.AddListener(() => Select(BidType.GardeContre));
    }

    private void Select(BidType bid)
    {
        _onBidSelected?.Invoke(bid);
        if (bid == BidType.Take) {
            Hide();
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);

        _passButton.onClick.RemoveAllListeners();
        _takeButton.onClick.RemoveAllListeners();
        // _gardeButton.onClick.RemoveAllListeners();
        // _gardeSansButton.onClick.RemoveAllListeners();
        // _gardeContreButton.onClick.RemoveAllListeners();
    }
}
