using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DGL_DATA_READER;
using CodeStage.AntiCheat.ObscuredTypes;

public class PvpDetailInfo : UIWindowPopup
{
    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private UILabel _rank;
    [SerializeField] private UILabel _level;
    [SerializeField] private UILabel _userName;

    [SerializeField] private GameObject _closeButton;

    [SerializeField] private List<Transform> _creatureIconTransforms = new List<Transform>();

    [SerializeField] private List<Transform> _itemIconTransforms = new List<Transform>();

    [SerializeField] private UILabel[] _statContentsTitle;     // enCreatureStatType 순서에 맞게 넣어야한다.
    [SerializeField] private UILabel[] _statValueTitle;        // enCreatureStatType 순서에 맞게 넣어야한다.

    [SerializeField] private UI2DSprite _skill0Sprite;
    [SerializeField] private UILabel _skill0Label;

    [SerializeField] private UI2DSprite _activeSkillSprite;
    [SerializeField] private UILabel _activeSkillLabel;

    [SerializeField] private UI2DSprite _passiveSkillSprite;
    [SerializeField] private UILabel _passiveSkillLabel;

    [SerializeField] private GameObject _skillToolTip;
    [SerializeField] private UILabel _skillName;
    [SerializeField] private UILabel _skillContents;

    [SerializeField] private GameObject _reportButton;
    [SerializeField] private UILabel _reportButtonLabel;

    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private List<CreatureIcon> _creatureIconList = new List<CreatureIcon>();
    private List<ItemBaseIcon> _itemIconList = new List<ItemBaseIcon>();

    private PvPCharacterData _pvpCharacterData = null;

    private UIPanel _panel = null;

    private ObscuredInt _skill0;
    private ObscuredInt _activeSkill;
    private ObscuredInt _passiveSkill;

    private ObscuredULong _selectCreatureKey;

    private ObscuredString _charName;

    //===================================================================================
    //
    // Default Method
    //
    //===================================================================================
    protected override void Awake()
    {
        _panel = GetComponent<UIPanel>();

        CCreatureAbilityUI.SetContents(_statContentsTitle);

        _reportButtonLabel.text = StringTableManager.GetData(8893);     // 8893    신고하기
        _skill0Label.text = StringTableManager.GetData(3143);
        _activeSkillLabel.text = StringTableManager.GetData(22);
        _passiveSkillLabel.text = StringTableManager.GetData(24);

        UIEventListener.Get(_skill0Sprite.gameObject).onPress = OnPressSkill0;
        UIEventListener.Get(_passiveSkillSprite.gameObject).onPress = OnPressPassive;
        UIEventListener.Get(_activeSkillSprite.gameObject).onPress = OnPressSkill1;
        UIEventListener.Get(_closeButton).onClick = OnClickBack;
        UIEventListener.Get(_reportButton).onClick = OnReport;
    }

    protected override void OnDestroy()
    {
        UIEventListener.Get(_skill0Sprite.gameObject).onPress -= OnPressSkill0;
        UIEventListener.Get(_passiveSkillSprite.gameObject).onPress -= OnPressPassive;
        UIEventListener.Get(_activeSkillSprite.gameObject).onPress -= OnPressSkill1;
        UIEventListener.Get(_closeButton).onClick -= OnClickBack;
        UIEventListener.Get(_reportButton).onClick -= OnReport;

        _pvpCharacterData = null;

        ClearCreatureIcon();
        ClearItemIcon();
    }

    public override void CloseUI()
    {
        base.CloseUI();
    }

    public override void Init()
    {
        _selectCreatureKey = 0;

        ClearCreatureIcon();
        ClearItemIcon();

        for (int i = 0; i < _statValueTitle.Length; i++)
            _statValueTitle[i].text = "-";

        _skill0Sprite.sprite2D = null;
        _activeSkillSprite.sprite2D = null;
        _passiveSkillSprite.sprite2D = null;

        _passiveSkill = Global.InvaildValue;
        _skill0 = Global.InvaildValue;
        _activeSkill = Global.InvaildValue;
    }

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    private void ClearCreatureIcon()
    {
        for (int i = 0; i < _creatureIconList.Count; i++)
        {
            UIEventListener.Get(_creatureIconList[i].gameObject).onClick -= OnClickIcon;
            DestroyImmediate(_creatureIconList[i].gameObject);
        }

        _creatureIconList.Clear();
    }

    private void ClearItemIcon()
    {
        for (int i = 0; i < _itemIconList.Count; i++)
        {
            DestroyImmediate(_itemIconList[i].gameObject);
        }

        _itemIconList.Clear();
    }

    public void SetData(PvPCharacterData pvpOpponentData)
    {
        _pvpCharacterData = null;
        _pvpCharacterData = pvpOpponentData;

        SetRankInfo(pvpOpponentData.ranking, pvpOpponentData.nickName);
        CreateCreatureIcon(_pvpCharacterData.creaturesInfo);

        for(int i = 0; i < _creatureIconTransforms.Count; i++)
        {
            CreatureIcon icon = _creatureIconTransforms[i].GetComponentInChildren<CreatureIcon>();
            if (icon == null)
                continue;

            OnClickIcon(icon.gameObject);
            break;
        }
    }

    public void SetData(_stRankInfo rankInfo, _vMatchCreatureInfo creatureInfos)
    {
        _pvpCharacterData = null;
        _pvpCharacterData = new PvPCharacterData(creatureInfos);

        SetRankInfo((int)rankInfo.kRanking, /*rankInfo.kCharLevel,*/ rankInfo.kCharName);
        CreateCreatureIcon(_pvpCharacterData.creaturesInfo);

        for (int i = 0; i < _creatureIconTransforms.Count; i++)
        {
            CreatureIcon icon = _creatureIconTransforms[i].GetComponentInChildren<CreatureIcon>();
            if (icon == null)
                continue;

            OnClickIcon(icon.gameObject);
            break;
        }
    }

    private void SetRankInfo(ObscuredInt ranking, /*ObscuredUShort charLevel,*/ ObscuredString userName)
    {
        // 3412    {0} 위
        _rank.text = string.Format(StringTableManager.GetData(3412), ranking);
        //_level.text = string.Format("{0}{1}", StringTableManager.GetData(12), charLevel);
        _level.gameObject.SetActive(false);

        _charName = userName;
        _userName.text = userName;
    }

    private void CreateCreatureIcon(PvPCreaturesData creatureInfos)
    {
        ClearCreatureIcon();

        for(int i = 0; i < creatureInfos.Count; i++)
        {
            PvPCreatureInfo info = creatureInfos[i];

            CreatureIcon icon = UIResourceMgr.CreatePrefab<CreatureIcon>(BUNDLELIST.PREFABS_UI_COMMON, _creatureIconTransforms[i], "CreatureIcon");
            int Level = info.Level;
            icon.SetIcon(Level, info.forceCount, info.awake, CDATA_CREATURE_NEWVER.Get(info.enID), enCreatureIcon_Type.PvpDetail);
            icon.CreatureKey = info.key;
            icon.RemoveDragScrollView();
            UIEventListener.Get(icon.gameObject).onClick = OnClickIcon;

            _creatureIconList.Add(icon);
        }
    }

    private void CreateItemIcon(List<PvPItemInfo> items)
    {
        ClearItemIcon();

        for(int i = 0; i < items.Count; i++)
        {
            PvPItemInfo info = items[i];

            CItem citem = new CItem();
            citem.m_ItemID = info.ItemID;
            citem.m_ItemKey = info.ItemKey;

            citem.m_ItemOptions = new _vItemOptions();

            for (int k = 0; k < info.options.Count; k++)
            {
                PvPItemOptionInfo optioninfo = info.options[k];

                CItemOption citemoption = new CItemOption();
                citemoption.m_OptionNo = optioninfo.optionNo;
                citemoption.m_OptionID = optioninfo.optionID;
                citemoption.m_OptionCreator = optioninfo.optionCreator;
                citemoption.m_OptionInt = optioninfo.optionInt;
                citemoption.m_OptionBigint = optioninfo.optionBigint;

                citem.m_ItemOptions.Add(citemoption);
            }

            DATA_ITEM_NEW itemTable = CDATA_ITEM_NEW.Get(info.ItemID);

            Transform parent = null;

            if (itemTable.m_enItemSubType == DATA_ITEM_SUB_TYPE_NEW._enItemStatusSubType.ITEMTYPE_EI_WEAPON)
                parent = _itemIconTransforms[(int)enCreatureItemType.ITEMCREATUETYPE_WEAPON];
            else if (itemTable.m_enItemSubType == DATA_ITEM_SUB_TYPE_NEW._enItemStatusSubType.ITEMTYPE_EI_ARMOR)
                parent = _itemIconTransforms[(int)enCreatureItemType.ITEMCREATUETYPE_ARMOR];
            else if (itemTable.m_enItemSubType == DATA_ITEM_SUB_TYPE_NEW._enItemStatusSubType.ITEMTYPE_EI_ACCESSORY)
                parent = _itemIconTransforms[(int)enCreatureItemType.ITEMCREATUETYPE_ACC1];
            else if (itemTable.m_enItemSubType == DATA_ITEM_SUB_TYPE_NEW._enItemStatusSubType.ITEMTYPE_EI_ACCESSORY_2)
                parent = _itemIconTransforms[(int)enCreatureItemType.ITEMCREATUETYPE_ACC2];

            ItemBaseIcon icon = UIResourceMgr.CreatePrefab<ItemBaseIcon>(BUNDLELIST.PREFABS_UI_COMMON, parent, "ItemBaseIcon");

            UIPanel iconPanel = icon.GetComponent<UIPanel>();
            if (iconPanel != null)
            {
                if (_panel != null)
                    UtilFunc.SetPanelDepth(iconPanel, _panel.depth + 10);
                else
                    UtilFunc.SetPanelDepth(iconPanel, 410);
            }

            TweenScale tweenscale = icon.GetComponent<TweenScale>();
            if (tweenscale != null)
                DestroyImmediate(tweenscale);

            uint ItemForce = info.ItemForce;

            ItemSlot slot = new ItemSlot(citem);
            slot.SetUpgradeLevel((int)ItemForce);

            icon.Init(itemTable, slot);
            icon.SetForceCount((int)ItemForce);
            icon.ActiveGoldBuffIcon(false);

            _itemIconList.Add(icon);
        }
    }

    private void SetCreatureData(CreatureIcon selectIcon, PvPCreatureInfo info)
    {
        DATA_CREATURE_NEWVER creatureTable = CDATA_CREATURE_NEWVER.Get(info.enID);

        int level = info.Level;

        CCreatureAbility creatureStat = BattleRule.CreatureAbilityUI(
            creatureTable,
            BattleRule.AddCreatureStatAbility(level, info.forceCount, creatureTable),
            BattleRule.PvPCreatureItemAbility(info.items));

        CCreatureAbilityUI.SetValues(creatureStat, _statValueTitle);

        CreateItemIcon(info.items);

        _passiveSkill = creatureTable.m_PassiveSkill_0;
        _skill0 = creatureTable.m_Skill_0;
        _activeSkill = creatureTable.m_Skill_1;

        SetSkillInfo();

        foreach(CreatureIcon icon in _creatureIconList)
            icon.SetActiveSelect(false);

        selectIcon.SetActiveSelect(true);
    }

    private void SetSkillInfo()
    {
        _skill0Sprite.sprite2D = null;
        _activeSkillSprite.sprite2D = null;
        _passiveSkillSprite.sprite2D = null;

        DATA_SKILL_NEW pSkillTbl = null;

        // 기본 공격.
        if (_skill0 != Global.InvaildValue)
        {
            pSkillTbl = CDATA_SKILL_NEW.Get(_skill0);
            _skill0Sprite.sprite2D = UIResourceMgr.CreateSprite(BUNDLELIST.TEXTURE_ICON_SKILL, pSkillTbl.m_szIConIndex);
        }

        // 액티브 스킬.
        if (_activeSkill != Global.InvaildValue)
        {
            pSkillTbl = CDATA_SKILL_NEW.Get(_activeSkill);
            _activeSkillSprite.sprite2D = UIResourceMgr.CreateSprite(BUNDLELIST.TEXTURE_ICON_SKILL, pSkillTbl.m_szIConIndex);
        }

        // 패시브 스킬.
        if (_passiveSkill != Global.InvaildValue)
        {
            pSkillTbl = CDATA_SKILL_NEW.Get(_passiveSkill);
            _passiveSkillSprite.sprite2D = UIResourceMgr.CreateSprite(BUNDLELIST.TEXTURE_ICON_SKILL, pSkillTbl.m_szIConIndex);
        }
    }

    //===================================================================================
    //
    // Event
    //
    //===================================================================================
    private void OnClickIcon(GameObject go)
    {
        CreatureIcon selectIcon = go.GetComponent<CreatureIcon>();
        if (selectIcon == null)
            return;

        if (_selectCreatureKey == selectIcon.CreatureKey)
            return;

        _selectCreatureKey = selectIcon.CreatureKey;

        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        for (int i = 0; i < _pvpCharacterData.creaturesInfo.Count; i++)
        {
            if (selectIcon.CreatureKey == _pvpCharacterData.creaturesInfo[i].key)
            {
                SetCreatureData(selectIcon, _pvpCharacterData.creaturesInfo[i]);
                break;
            }

        }
    }

    private void OnPressSkill0(GameObject go, bool aisState)
    {
        PressSkill(aisState, _skill0);
    }

    private void OnPressSkill1(GameObject go, bool aisState)
    {
        PressSkill(aisState, _activeSkill);
    }

    private void OnPressPassive(GameObject go, bool aisState)
    {
        PressSkill(aisState, _passiveSkill);
    }

    private void PressSkill(bool aisState, ObscuredInt skill)
    {
        if (aisState)
        {
            DATA_SKILL_NEW pSkillTbl = CDATA_SKILL_NEW.Get(skill);
            if (pSkillTbl == null)
                return;

            _skillToolTip.SetActive(aisState);

            _skillName.text = StringTableManager.GetData(pSkillTbl.iSkillNameStringID);
            _skillContents.text = UtilFunc.ParseSkillText(pSkillTbl);
        }
        else
        {
            _skillToolTip.SetActive(aisState);
        }
    }

    private void OnReport(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        // 8894    신고
        // 8895    {0}\n\n해당 유저를 부정 사용자로\n신고하시겠습니까?
        SystemPopupWindow.Instance.OpenSystemPopUp(enSystemPopupType.YesNo, StringTableManager.GetData(8894), string.Format(StringTableManager.GetData(8895), _userName.text), RquestReport);
    }

    private void RquestReport(enSystemMessageFlag state)
    {
        if (state != enSystemMessageFlag.YES)
            return;

        _stPvPReportReq stPvPReportReq = new _stPvPReportReq();
        stPvPReportReq.kCharName = _charName;

        CNetManager.Instance.SendPacket(CNetManager.Instance.MatchProxy.PvPReport, stPvPReportReq, typeof(_stPvPReportAck));
    }
}
