using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DGL_DATA_READER;

public class DispatchMissionItemRight : MonoBehaviour
{
    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private UILabel m_titleLabel;                          // 파견 이름.
    [SerializeField] private UILabel m_ConditionLabel;                      // 조건.
    [SerializeField] private UILabel m_TimeLabel;                           // 소요시간.

    [SerializeField] private UI2DSprite m_RewardSprite;                     // 보상 아이콘.
    [SerializeField] private UILabel m_RewardLabel;                         // 보상.

    [SerializeField] private GameObject m_DispatchingObj;                   // 파견진행중 오브젝트.
    [SerializeField] private UILabel m_DispatchingTitleLabel;               // 파견진행중 타이틀 라벨.
    [SerializeField] private UISprite m_DispatchingGageSprtie;              // 파견진행중 게이지.
    [SerializeField] private UILabel m_DispatchingGageLabel;                // 파견진행중 게이지 라벨.

    [SerializeField] private GameObject _ClearStage;                        // 특정 스테이지 클리어후 오픈 된다는 이미지.
    [SerializeField] private UILabel _ClearStageLabel;                      

    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private DATA_DISPATCH m_DispatchTableData = null;
    public DATA_DISPATCH DispatchTableData { get { return m_DispatchTableData; } }

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    public void Init(DATA_DISPATCH DispatchTableData)
    {
        m_DispatchTableData = DispatchTableData;

        SetDispatchInfo();
    }

    private void SetDispatchInfo()
    {
        // 스테이지 조건 검사.
        {
            _ClearStage.SetActive(false);
            _ClearStageLabel.gameObject.SetActive(false);

            CUserGameStageInfo stagetInfo = UserInfo.Instance.UserGameStageInfo;

            if (stagetInfo.IsClearStage(m_DispatchTableData.RequireStage) == false)
            {
                UIEventListener.Get(gameObject).onClick = null;

                _ClearStage.SetActive(true);
                _ClearStageLabel.gameObject.SetActive(true);

                DATA_MAP_STAGE MapStageTableData = CDATA_MAP_STAGE.Get(m_DispatchTableData.RequireStage);
                if (MapStageTableData != null)
                {
                    int iAreaID = UtilFunc.GetRegionIndex(MapStageTableData.m_iIndex);
                    int iStageID = UtilFunc.GetStageIndex(MapStageTableData.m_iIndex);
                    _ClearStageLabel.text = string.Format("{0} {1} - {2}", StringTableManager.GetData(MapStageTableData.iAreaName), iAreaID, iStageID + 1);
                }
            }
        }

        m_DispatchingObj.SetActive(false);

        // 6849	(최소 {0}인)
        string str = string.Format(StringTableManager.GetData(6849), m_DispatchTableData.RequireNumber);
        m_titleLabel.text = string.Format("{0} {1}", StringTableManager.GetData(int.Parse(m_DispatchTableData.String_Name)), str);

        // 6847    Lv {0} 이상 {1}성 이상
        m_ConditionLabel.text = string.Format(StringTableManager.GetData(6847), m_DispatchTableData.RequireLevel, (int)m_DispatchTableData.RequireGrade);

        int ihour = m_DispatchTableData.RequireTime / 60;
        int iMinutes = m_DispatchTableData.RequireTime - (60 * ihour);
        if (ihour > 0)
        {
            // 6740	소요 시간 : {0}시간 {1}분
            m_TimeLabel.text = string.Format(StringTableManager.GetData(6740), ihour, iMinutes);
        }
        else
        {
            // 6741	소요 시간 : {0}분
            m_TimeLabel.text = string.Format(StringTableManager.GetData(6741), iMinutes);
        }

        Dictionary<int, DATA_REWARD_NEW> RewardData = CDATA_REWARD_NEW.Get(m_DispatchTableData.RewardItemEnum);
        if (RewardData == null)
            return;     // error.

        foreach(KeyValuePair<int, DATA_REWARD_NEW> data in RewardData)
        {
            DATA_ITEM_NEW item = CDATA_ITEM_NEW.Get(data.Value.RewardValue);
            if (item == null)
                continue;

            m_RewardSprite.sprite2D = UIResourceMgr.CreateSprite(BUNDLELIST.TEXTURE_ICON_ITEMICON, item.m_szIconName);
            m_RewardLabel.text = data.Value.RewardCount.ToString();
        }

        //m_RewardSprite.spriteName
        //m_RewardLabel.text = m_DispatchTableData.QuickCost.ToString();

        _stDispatchInfoAck stDispatchInfoAck = UserInfo.Instance.DispatchInfoAck;
        if (stDispatchInfoAck == null)
            return;     // error.

        int iCount = stDispatchInfoAck.vDispatch.Count;
        for (int i = 0; i < iCount; ++i)
        {
            CDispatch dispatch = stDispatchInfoAck.vDispatch[i];
            if (dispatch == null)
                continue;

            if (dispatch.kDispatchState == _enDispatchState.eDispatchState_None)
                continue;

            if (m_DispatchTableData.DispatchCategory == dispatch.kMissionCategory && m_DispatchTableData.DispatchEnum == dispatch.kDispatchSubID)
            {
                UIEventListener.Get(gameObject).onClick = null;
                m_DispatchingObj.SetActive(true);

                DateTime kEndTime = dispatch.kEndTime.GetDateTime();
                DateTime kCurrentTime = TimeManager.Instance.GetServerTime();
                TimeSpan timeresult = kEndTime - kCurrentTime;

                if (kCurrentTime.Ticks >= kEndTime.Ticks)       // 임무완료.
                {            
                    m_DispatchingGageSprtie.fillAmount = 1;
                        
                    m_DispatchingGageLabel.text = StringTableManager.GetData(3395);         // 3395	완료.
                    m_DispatchingTitleLabel.text = StringTableManager.GetData(6848);        // 6848	임무 완료.
                }
                else
                {
                    m_DispatchingTitleLabel.text = StringTableManager.GetData(6742);            // 6742	임무 수행 중

                    m_DispatchingGageSprtie.fillAmount = 1 - (float)(timeresult.TotalSeconds / (m_DispatchTableData.RequireTime * 60));

                    if (timeresult.Hours > 0)
                    {
                        // 4915	{0}시간 {1}분 남음
                        m_DispatchingGageLabel.text = string.Format(StringTableManager.GetData(4915), timeresult.Hours, timeresult.Minutes);
                    }
                    else if (timeresult.Minutes > 0)
                    {
                        // 4916	{0}분 남음
                        m_DispatchingGageLabel.text = string.Format(StringTableManager.GetData(4916), timeresult.Minutes);
                    }
                    else if (timeresult.Seconds > 0)
                    {
                        // 4959	1분 미만
                        m_DispatchingGageLabel.text = StringTableManager.GetData(4959);
                    }
                }
            }
        }
    }
}