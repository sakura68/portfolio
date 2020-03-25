using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

using DGL_DATA_READER;

public class GuildGoddnessItemInfo : UIWindowPopup
{
    //===================================================================================
    //
    // Inner Class
    //
    //===================================================================================
    public class GoddnessItemInfo
    {
        public DATA_GUILD_TRIBUTE._enTributeEnum _TributeEnum = DATA_GUILD_TRIBUTE._enTributeEnum.None;
        public DATA_REWARD_ENUM._enRewardEnum _RewardType = 0;
    }

    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private GameObject _CloseButton;
    [SerializeField] private UILabel _CloseButtonLabel;

    [SerializeField] private UILabel _TitleLabel;
    [SerializeField] private UILabel _ContentLabel;

    [SerializeField] private UIScrollView _ItemInfoScrollView;
    [SerializeField] private UIGrid _ItemInfoGrid;

    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private List<GuildTributeRewardItem> _TributeRewardItems = new List<GuildTributeRewardItem>();

    //===================================================================================
    //
    // Default Method
    //
    //===================================================================================
    protected override void Awake()
    {
        if (_CloseButton != null) UIEventListener.Get(_CloseButton).onClick = OnClickBack;
    }

    protected override void OnDestroy()
    {
        if (_CloseButton != null) UIEventListener.Get(_CloseButton).onClick -= OnClickBack;

        DestroyTributeRewardItem();
    }

    public override void Init()
    {
        // 2	확인
        _CloseButtonLabel.text = StringTableManager.GetData(2);

        // 6898    봉헌 감사 선물 정보
        _TitleLabel.text = StringTableManager.GetData(6898);

        // 6899    아래 아이템들 중 한 가지를 랜덤하게 획득합니다.
        _ContentLabel.text = StringTableManager.GetData(6899);
    }

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    public void SetData(DATA_GUILD_TRIBUTE._enTributeEnum enTribute)
    {
        DestroyTributeRewardItem();

        DATA_GUILD_TRIBUTE GuildTributeData = CDATA_GUILD_TRIBUTE.Get(enTribute);
        if (GuildTributeData == null)
        {
#if DEBUG_LOG
            Debug.Log("<color=red> CDATA_GUILD_TRIBUTE.Get(enTribute) is Null  </color>");
#endif
            return;
        }

        Dictionary<int, DATA_REWARD_NEW> RewardData = CDATA_REWARD_NEW.Get(GuildTributeData.enTributeReward);
        if (RewardData == null)
        {
#if DEBUG_LOG
            Debug.Log("<color=red> CDATA_REWARD_NEW.Get(GuildTributeData.enTributeReward) is Null  </color>");
#endif
            return;
        }

        foreach (KeyValuePair<int, DATA_REWARD_NEW> data in RewardData)
        {
            DATA_REWARD_NEW reward = data.Value;
            if (reward == null)
                continue;

            GuildTributeRewardItem TributeRewardItem = UIResourceMgr.CreatePrefab<GuildTributeRewardItem>(BUNDLELIST.PREFABS_UI_GUILD, _ItemInfoGrid.transform, "GuildTributeRewardItem");
            TributeRewardItem.Init(reward);

            _TributeRewardItems.Add(TributeRewardItem);
        }

        ResetPosition();
    }
    
    private void ResetPosition()
    {
        _ItemInfoGrid.Reposition();
        _ItemInfoScrollView.ResetPosition();
    }

    private void DestroyTributeRewardItem()
    {
        for(int i = 0; i < _TributeRewardItems.Count; ++i)
        {
            GuildTributeRewardItem TributeRewardItem = _TributeRewardItems[i];
            if (TributeRewardItem == null)
                continue;

            DestroyImmediate(TributeRewardItem.gameObject);
        }

        _TributeRewardItems.Clear();
    }

    //===================================================================================
    //
    // Event
    //
    //===================================================================================
}
