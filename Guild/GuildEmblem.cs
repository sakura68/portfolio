using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using DGL_DATA_READER;

public class GuildEmblem : UIWindowPopup
{
    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private UILabel m_GuildEmblemTitleLabel;

    [SerializeField] private UIScrollView m_EmblemScrollview;
    [SerializeField] private UIGrid m_EmblemGrid;

    [SerializeField] private EmblemElement m_OriginGuildEmblemObj;

    [SerializeField] private GameObject m_EmblemCancleButton;
    [SerializeField] private UILabel m_CancleButtonLabel;

    [SerializeField] private GameObject m_EmblemConfrimButton;
    [SerializeField] private UILabel m_ConfrimButtonLabel;

    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private List<EmblemElement> m_EmblemList = new List<EmblemElement>();

    private GuildCreate m_GuildCreate = null;
    private GuildModify m_GuildModify = null;

    private EmblemElement m_SelectEmblem = null;

    //===================================================================================
    //
    // Default Method
    //
    //===================================================================================
    protected override void Awake()
    {
        if (m_EmblemCancleButton != null) UIEventListener.Get(m_EmblemCancleButton).onClick = OnClickBack;
    }

    protected override void OnDestroy()
    {
        for(int i = 0; i < m_EmblemList.Count; ++i)
        {
            EmblemElement emblem = m_EmblemList[i];
            if (emblem == null)
                continue;

            emblem.onSelectEvent -= SelectEmblem;
        }

        if (m_EmblemCancleButton != null) UIEventListener.Get(m_EmblemCancleButton).onClick -= OnClickBack;

        if (m_EmblemConfrimButton != null) UIEventListener.Get(m_EmblemConfrimButton).onClick -= OnCinfrimToGuildCreate;
        if (m_EmblemConfrimButton != null) UIEventListener.Get(m_EmblemConfrimButton).onClick -= OnCinfrimToGuildModify;
    }

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    public void Init(GuildCreate guildCreate)
    {
        m_GuildCreate = guildCreate;

        CreateEmblemItems();

        // 6266 길드 마크.
        m_GuildEmblemTitleLabel.text = StringTableManager.GetData(6266);
        m_CancleButtonLabel.text = StringTableManager.GetData(3);       // 3 취소.
        m_ConfrimButtonLabel.text = StringTableManager.GetData(2);      // 2 확인.

        if (m_EmblemConfrimButton != null) UIEventListener.Get(m_EmblemConfrimButton).onClick = OnCinfrimToGuildCreate;
    }

    public void Init(GuildModify GuildModify)
    {
        m_GuildModify = GuildModify;

        CreateEmblemItems();

        // // 6266 길드 마크 변경.
        m_GuildEmblemTitleLabel.text = StringTableManager.GetData(6595);
        m_CancleButtonLabel.text = StringTableManager.GetData(3);       // 3 취소.
        m_ConfrimButtonLabel.text = StringTableManager.GetData(2);

        if (m_EmblemConfrimButton != null) UIEventListener.Get(m_EmblemConfrimButton).onClick = OnCinfrimToGuildModify;
    }

    /// <summary>
    /// 마크 생성.
    /// </summary>
    private void CreateEmblemItems()
    {
        int iCount = CDATA_GUILDMARK.GetCount();
        for(int i = 1; i < iCount; ++i)
        {
            EmblemElement emblem = UIResourceMgr.InstantiateGameObject(m_OriginGuildEmblemObj, m_EmblemGrid.transform, SetTransformType.Default, i.ToString());
            DATA_GUILDMARK guildMark = CDATA_GUILDMARK.GetByIndex(i);
            emblem.Init(guildMark.GuildMarkIcon);
            emblem.onSelectEvent += SelectEmblem;

            m_EmblemList.Add(emblem);
        }

        m_OriginGuildEmblemObj.gameObject.SetActive(false);

        m_EmblemGrid.sorting = UIGrid.Sorting.Custom;
        m_EmblemGrid.onCustomSort = UtilFunc.SortByNumber;

        //m_EmblemGrid.sorting = UIGrid.Sorting.Alphabetic;
        m_EmblemScrollview.ResetPosition();
    }

    //===================================================================================
    //
    // Event
    //
    //===================================================================================
    /// <summary>
    /// 길드 창설 창에서 확인버튼.
    /// </summary>
    /// <param name="go"></param>
    private void OnCinfrimToGuildCreate(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        if (m_SelectEmblem != null)
        {
            if (m_GuildCreate != null)
            {
                m_GuildCreate.SetEmblem(m_SelectEmblem);
            }

            CloseUI();
        }
    }

    /// <summary>
    /// 길드 관리창에서 확인버튼.
    /// </summary>
    /// <param name="go"></param>
    private void OnCinfrimToGuildModify(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        if (m_SelectEmblem != null)
        {
            if (m_GuildModify != null)
            {
                m_GuildModify.SetEmblem(m_SelectEmblem);
            }
        }
    }

    private void SelectEmblem(EmblemElement emblem)
    {
        if(m_SelectEmblem != null)
        {
            m_SelectEmblem.ActiveSelect(false);
        }

        m_SelectEmblem = emblem;
    }
}