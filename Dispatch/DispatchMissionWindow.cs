using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DGL_DATA_READER;

public class DispatchMissionWindow : UIWindow
{
    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private GameObject m_CloseButton;

    [SerializeField] private UILabel m_TitleLabel;                  // 파견
    [SerializeField] private UILabel m_TitleContentLabel;           // 임무선택
    [SerializeField] private UILabel m_RegionTitleLabel;            // 지역
    [SerializeField] private UILabel m_MissionTitleLabel;           // 임무

    // left
    [SerializeField] private UIScrollView m_SelectRegionScrollView;
    [SerializeField] private UIGrid m_SelectRegionGrid;

    // right
    [SerializeField] private UIScrollView m_SelectMissionScrollView;
    [SerializeField] private UIGrid m_SelectMissionGrid;

    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private List<DATA_DISPATCH> m_DispatchTableData = new List<DATA_DISPATCH>();

    private List<DispatchMissionItemLeft> m_SelectRegionList = new List<DispatchMissionItemLeft>();
    private List<DispatchMissionItemRight> m_SelectMissionList = new List<DispatchMissionItemRight>();

    private DispatchWindow m_DispatchWindow = null;

    //private DispatchTeamWindow m_DispatchTeamWindow = null;

    private GameObject m_SelectRegionObj = null;

    private CDispatch m_DispatchRecvData = null;
    public CDispatch DispatchRecvData { get { return m_DispatchRecvData; } }

    //===================================================================================
    //
    // Default Method
    //
    //===================================================================================
    protected override void Awake()
    {
        UIEventListener.Get(m_CloseButton).onClick = OnClickBack;
    }

    protected override void OnDestroy()
    {
        UIEventListener.Get(m_CloseButton).onClick -= OnClickBack;

        //if(m_DispatchTeamWindow != null) m_DispatchTeamWindow.CloseUI();

        DestroySelectRegion();
        DestroySelectMission();

        m_DispatchTableData.Clear();
    }

    protected override void Start()
    {
    }

    public override void Init()
    {
    }

    public override void Clear()
    {
    }

    protected override void OnEnable()
    {
    }

    protected override void OnDisable()
    {
    }

    protected override void Update()
    {
    }

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    public void Init(DispatchWindow parnet)
    {
        m_DispatchWindow = parnet;

        m_TitleLabel.text = StringTableManager.GetData(6722);                   // 6722	파견
        m_TitleContentLabel.text = StringTableManager.GetData(6735);            // 6735	임무 선택
        m_RegionTitleLabel.text = StringTableManager.GetData(6736);             // 6736	지역
        m_MissionTitleLabel.text = StringTableManager.GetData(6737);            // 6737	임무

        int iDispatchCount = CDATA_DISPATCH.GetCount();
        for (int i = 0; i < iDispatchCount; ++i)
        {
            DATA_DISPATCH DispatchTableData = CDATA_DISPATCH.GetByIndex(i);
            if (DispatchTableData == null)
                continue;

            m_DispatchTableData.Add(DispatchTableData);
        }
    }

    public void SetData(CDispatch DispatchRecvData)
    {
        m_DispatchRecvData = DispatchRecvData;

        DestroySelectRegion();
        DestroySelectMission();

        List<DATA_DISPATCH_CATEGORY._enCategory> DispatchCategoryList = new List<DATA_DISPATCH_CATEGORY._enCategory>();

        int iDispatchCount = m_DispatchTableData.Count;
        for (int i = 0; i < iDispatchCount; ++i)
        {
            DATA_DISPATCH DispatchTableData = m_DispatchTableData[i];
            if (DispatchTableData == null)
                continue;

            DATA_DISPATCH_CATEGORY._enCategory DispatchCategory = DispatchTableData.DispatchCategory;
            if (DispatchCategory == DATA_DISPATCH_CATEGORY._enCategory.Dispatch_World_None || DispatchCategory == DATA_DISPATCH_CATEGORY._enCategory._enCategory_Max)
                continue;

            if (DispatchCategoryList.Contains(DispatchCategory) == true)
                continue;

            DispatchCategoryList.Add(DispatchCategory);

            DispatchMissionItemLeft regin = UIResourceMgr.CreatePrefab<DispatchMissionItemLeft>(BUNDLELIST.PREFABS_UI_DISPATCH, m_SelectRegionGrid.transform, "DispatchMissionItemLeft");
            regin.Init(DispatchTableData);
            UIEventListener.Get(regin.gameObject).onClick = OnSelectRegion;

            m_SelectRegionList.Add(regin);
        }

        if (m_SelectRegionList.Count > 0)
        {
            OnSelectRegion(m_SelectRegionList[0].gameObject);
        }

        DispatchCategoryList.Clear();

        ResetPositionRegion();
    }

    private void ResetPositionRegion()
    {
        m_SelectRegionGrid.Reposition();
        m_SelectRegionScrollView.ResetPosition();
    }

    private void ResetPositionMission()
    {
        m_SelectMissionGrid.Reposition();
        m_SelectMissionScrollView.ResetPosition();
    }

    private void DestroySelectRegion()
    {
        for (int i = 0; i < m_SelectRegionList.Count; ++i)
        {
            DestroyImmediate(m_SelectRegionList[i].gameObject);
        }

        m_SelectRegionList.Clear();
    }

    private void DestroySelectMission()
    {
        for(int i = 0; i < m_SelectMissionList.Count; ++i)
        {
            DestroyImmediate(m_SelectMissionList[i].gameObject);
        }

        m_SelectMissionList.Clear();
    }

    //===================================================================================
    //
    // Event
    //
    //===================================================================================
    private void OnSelectRegion(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        if (m_SelectRegionObj != null && m_SelectRegionObj == go)
            return;

        m_SelectRegionObj = go;

        DATA_DISPATCH_CATEGORY._enCategory SelectDispatchCategory = DATA_DISPATCH_CATEGORY._enCategory.Dispatch_World_None;
        for (int i = 0; i < m_SelectRegionList.Count; ++i)
        {
            DispatchMissionItemLeft region = m_SelectRegionList[i];
            if (region == null)
                continue;

            if (region.gameObject == go)
            {
                SelectDispatchCategory = region.DispatchTableData.DispatchCategory;
                region.SetActiveSelect(true);
            }
            else
            {
                region.SetActiveSelect(false);
            }
        }

        DestroySelectMission();

        int iCount = m_DispatchTableData.Count;
        for (int i = 0; i < iCount; ++i)
        {
            DATA_DISPATCH DispatchData = m_DispatchTableData[i];
            if (DispatchData == null)
                continue;

            if (SelectDispatchCategory == DATA_DISPATCH_CATEGORY._enCategory.Dispatch_World_None || SelectDispatchCategory == DATA_DISPATCH_CATEGORY._enCategory._enCategory_Max)
                continue;

            if (SelectDispatchCategory != DispatchData.DispatchCategory)
                continue;

            DispatchMissionItemRight mission = UIResourceMgr.CreatePrefab<DispatchMissionItemRight>(BUNDLELIST.PREFABS_UI_DISPATCH, m_SelectMissionGrid.transform, "DispatchMissionItemRight");
            UIEventListener.Get(mission.gameObject).onClick = OnSelectMission;
            mission.Init(DispatchData);

            m_SelectMissionList.Add(mission);
        }

        ResetPositionMission();
    }

    private void OnSelectMission(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        DispatchMissionItemRight mission = m_SelectMissionList.Find((data) => data.gameObject == go);

        m_DispatchWindow.OpenDispatchTeamWindow(m_DispatchRecvData, mission.DispatchTableData);

        //if (m_DispatchTeamWindow == null)
        //{
        //    m_DispatchTeamWindow = UIResourceMgr.CreatePrefab<DispatchTeamWindow>(BUNDLELIST.PREFABS_UI_DISPATCH, transform, "DispatchTeamWindow");
        //}

        //m_DispatchTeamWindow.Init(m_DispatchRecvData, mission.DispatchTableData);
        //m_DispatchTeamWindow.OpenUI();
    }
}