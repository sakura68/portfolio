using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PvpEnterancePopup : UIWindowPopup
{
    public enum EnterPopupType
    {
        pvpEnter,
        ReGacha,
    }

    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private UILabel _TitleLabel;

    [SerializeField] private UILabel _ContentLabel;

    //[SerializeField] private GameObject _Wealth;
    [SerializeField] private UILabel _WealthCountLabel;

    [SerializeField] private GameObject _CancleButton;
    [SerializeField] private UILabel _CancleButtonLabel;
    
    [SerializeField] private GameObject _EnteranceButton;
    [SerializeField] private UILabel _EnteranceButtonLabel;
    [SerializeField] private UILabel _EnteranceButtonDiaCountLabel;     // 재합성에 필요한 다이아 갯수.
    [SerializeField] private UISprite _redBackSprite;
    [SerializeField] private UISprite _grayBackSprite;

    //===================================================================================
    //
    // Variable
    //
    //===================================================================================

    //===================================================================================
    //
    // Default Method
    //
    //===================================================================================

    protected override void Awake()
    {
        if (_CancleButton != null) UIEventListener.Get(_CancleButton).onClick = OnClickBack;
    }

    protected override void OnDestroy()
    {
        if (_CancleButton != null) UIEventListener.Get(_CancleButton).onClick -= OnClickBack;
    }

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    public void Init(EnterPopupType type, ulong Dia)
    {
        _redBackSprite.gameObject.SetActive(false);
        _grayBackSprite.gameObject.SetActive(false);

        _CancleButtonLabel.text = StringTableManager.GetData(3);           // 3	   취소.
        _WealthCountLabel.text = string.Format(StringTableManager.GetData(6561), UtilFunc.CurrencyFormat(UserInfo.Instance.iDiaCount));     // 6561    {0}개 보유중

        switch (type)
        {
            case EnterPopupType.pvpEnter:
                {
                    UIEventListener.Get(_EnteranceButton).onClick -= OnEnterance;
                    UIEventListener.Get(_EnteranceButton).onClick = OnEnterance;

                    _TitleLabel.text = StringTableManager.GetData(8286);        // 8286    즉시 입장 안내

                    // 8287    결투장 열쇠가 부족합니다.\n다이아몬드 {0}개를 소모하여 즉시 입장할 수 있습니다.\n즉시 입장하시겠습니까?
                    _ContentLabel.text = string.Format(StringTableManager.GetData(8287), Dia);

                    _EnteranceButtonLabel.text = StringTableManager.GetData(8285);      // 8285    즉시 입장
                    _EnteranceButtonDiaCountLabel.text = Dia.ToString();

                    _redBackSprite.gameObject.SetActive(true);
                }
                break;

            case EnterPopupType.ReGacha:
                {
                    UIEventListener.Get(_EnteranceButton).onClick -= OnReGacha;
                    UIEventListener.Get(_EnteranceButton).onClick = OnReGacha;

                    _TitleLabel.text = StringTableManager.GetData(4300);

                    // 8777    다시 소환하시겠습니까?
                    _ContentLabel.text = StringTableManager.GetData(8777);

                    _EnteranceButtonLabel.text = StringTableManager.GetData(8775);      // 8775    다시 소환
                    _EnteranceButtonDiaCountLabel.text = Dia.ToString();

                    _grayBackSprite.gameObject.SetActive(true);
                }
                break;
        }
    }

    //===================================================================================
    //
    // Event
    //
    //===================================================================================
    private void OnEnterance(GameObject go)
    {
        SoundManager.Instance.PlayFX(enSoundFXUI.UI_BATTLE_START);

        _stMatchReq stMatchingReq = new _stMatchReq();
        stMatchingReq.kUseDia = true;

        CNetManager.Instance.SendPacket(CNetManager.Instance.MatchProxy.Match, stMatchingReq, typeof(_stMatchAck));
    }

    private void OnReGacha(GameObject go)
    {
        SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

#if !GMTOOLSHOP
        CashShopWindow win = UIControlManager.instance.GetWindow<CashShopWindow>(enUIType.SHOP);
        if (win != null)
        {
            win.BuyRequest(_enGachaDiscountType.enGachaDiscountType_Continue);
        }
#endif
    }
}
