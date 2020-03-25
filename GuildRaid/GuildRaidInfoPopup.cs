using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildRaidInfoPopup : UIWindow
{
    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private GameObject _CloseButton;
    [SerializeField] private UILabel _CloseButtonLabel;

    [SerializeField] private UILabel _TitleLabel;

    [SerializeField] private UILabel _Content1TitleLabel;
    [SerializeField] private UILabel _Content1Label;

    [SerializeField] private UILabel _Content2TitleLabel;
    [SerializeField] private UILabel _Content2Label;

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
        if (_CloseButton != null) UIEventListener.Get(_CloseButton).onClick = OnClickBack;
    }

    protected override void OnDestroy()
    {
        if (_CloseButton != null) UIEventListener.Get(_CloseButton).onClick -= OnClickBack;
    }

    protected override void Start()
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

    public override void Clear()
    {
    }

    public override void Init()
    {
        _CloseButtonLabel.text = StringTableManager.GetData(2);

        _TitleLabel.text = StringTableManager.GetData(4925);        // 4925 레이드 보상 안내

        _Content1TitleLabel.text = StringTableManager.GetData(8674); // 8674 점수와 진척도
        _Content1Label.text = StringTableManager.GetData(8675); // 8675 길드원과 함께 점수를 모아 더 많은 보상을 얻으세요. 전투에서 가한 피해량에 따라 점수를 얻게 됩니다.

        _Content2TitleLabel.text = StringTableManager.GetData(8676); // 8676 보상 안내
        _Content2Label.text = StringTableManager.GetData(8677); // 8677 제한 시간이 끝난 후 모든 길드원이 함께 보상을 받게 됩니다.
    }

    //===================================================================================
    //
    // Method
    //
    //===================================================================================

    //===================================================================================
    //
    // Event
    //
    //===================================================================================
}
