using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DGL_DATA_READER;

public class GuildEmblemChange : UIWindowPopup
{
    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private UILabel m_TitleLabel;

    [SerializeField] private UI2DSprite m_EmblemBeforeSprite;
    [SerializeField] private UI2DSprite m_EmblemAfterSprite;

    [SerializeField] private UILabel m_PopupTextLabel;
    
    [SerializeField] private GameObject m_ConfirmButton;
    [SerializeField] private UILabel m_ConfirmButtonLabel;

    [SerializeField] private GameObject m_CancleButton;
    [SerializeField] private UILabel m_CancleButtonLabel;

    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private GuildModify m_Parent = null;

    private ulong m_kGuildKey = 0;

    private int m_iGuildMarkChangeCountDia = 0;
    private byte m_kEmblemNumber = 0;

    //===================================================================================
    //
    // Default Method
    //
    //===================================================================================
    protected override void Awake()
    {
        if (m_ConfirmButton != null) UIEventListener.Get(m_ConfirmButton).onClick = OnConfirm;
        if (m_CancleButton != null) UIEventListener.Get(m_CancleButton).onClick = OnClickBack;
    }

    protected override void OnDestroy()
    {
        if (m_ConfirmButton != null) UIEventListener.Get(m_ConfirmButton).onClick -= OnConfirm;
        if (m_CancleButton != null) UIEventListener.Get(m_CancleButton).onClick -= OnClickBack;
    }

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    public void Init(GuildModify Parent)
    {
        m_Parent = Parent;

        m_TitleLabel.text = StringTableManager.GetData(6595);        // 6595 길드 마크 변경.
        m_ConfirmButtonLabel.text = StringTableManager.GetData(2);   // 2 확인.
        m_CancleButtonLabel.text = StringTableManager.GetData(3);    // 3 취소.

        m_iGuildMarkChangeCountDia = (int)CDATA_FIXED_CONSTANTS.Get(DATA_FIXED_CONSTANTS._enConstant.Guild_MarkChange_Count_Dia).Value;

        // 6289	다이아 {0}개를 소비하여 \n길드 마크를 변경하시겠습니까?
        m_PopupTextLabel.text = string.Format(StringTableManager.GetData(6289), m_iGuildMarkChangeCountDia);
    }
    
    public void SetEmbelemInfo(ulong kGuildKey, Sprite BeforeSprite, Sprite AfterSprite, byte EmblemNumber)
    {
        m_kGuildKey = kGuildKey;

        m_EmblemBeforeSprite.sprite2D = BeforeSprite;
        m_EmblemAfterSprite.sprite2D = AfterSprite;

        m_kEmblemNumber = EmblemNumber;
    }

    //===================================================================================
    //
    // Event
    //
    //===================================================================================
    /// <summary>
    /// 길드마크 변경 패킷 보냄.
    /// </summary>
    /// <param name="go"></param>
    private void OnConfirm(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        if (UserInfo.Instance.iDiaCount < (ulong)m_iGuildMarkChangeCountDia)
        {
            // 6290 길드 마크를 변경하기 위한 \n다이아가 부족합니다.
            SystemPopupWindow.Instance.SetSystemPopup(enSystemPopupType.Ok, StringTableManager.GetData(4300), StringTableManager.GetData(6290));
            CloseUI();
            m_Parent.GuildEmblemPopupClose();
            return;
        }

        _stGuildChangeMarkReq stGuildChangeMarkReq = new _stGuildChangeMarkReq();
        stGuildChangeMarkReq.kGuildKey = m_kGuildKey;
        stGuildChangeMarkReq.kNewGuildMark = m_kEmblemNumber;

        CNetManager.Instance.SendPacket(CNetManager.Instance.GuildProxy.GuildChangeMark, stGuildChangeMarkReq, typeof(_stGuildChangeMarkAck));
    }
}
