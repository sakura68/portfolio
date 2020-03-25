using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DGL_DATA_READER;
public class GachaBox : MonoBehaviour
{
    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private Animation _Anim;
    public Animation Anim { get { return _Anim; } }

    [SerializeField] private List<GachaCardItem> _GachaCardItems = new List<GachaCardItem>();

    [SerializeField] private GameObject _ConfirmationCardParent;                                // 11연차 확정카드 부모
    [SerializeField] private GameObject _ConfirmationCard;                                      // 11연차 확정카드

    [SerializeField] private GameObject _OpenEffect;
    public GameObject OpenEffect { get { return _OpenEffect; } }

    [SerializeField] private GameObject _EndEffect;
    public GameObject EndEffect { get { return _EndEffect; } }
    
    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private Camera _3DCamera = null;

    Texture _CardCreatureNormal = null;
    Texture _CardCreatureRoyal = null;
    Texture _CardItemNormal = null;
    Texture _CardItemRoyal = null;

    Texture _CardRaidWeaponNormal = null;
    Texture _CardRaidWeaponRoyal = null;
    Texture _CardRaidWeaponGrade5 = null;
    Texture _CardRaidWeaponGrade6 = null;

    Texture _CardRaidArmorNormal = null;
    Texture _CardRaidArmorRoyal = null;
    Texture _CardRaidArmorGrade5 = null;
    Texture _CardRaidArmorGrade6 = null;

    private int _ShopSummonHighGrade = 4;

    private float _AnimLength = 0;
    public float AnimLength { get { return _AnimLength; } }

    private readonly float _HitDistance = 20000.0f;

    private bool _IsClick = true;
    public bool IsClick { set { _IsClick = value; } }

    private DelegateOnceEventDone OnCardOpenEvent = null;

    private IEnumerator _IEAllCardOpen = null;

    //public float _GuildRaidRewardEffectTime = 0.0f;
    //===================================================================================
    //
    // Default Method
    //
    //===================================================================================
    private void Awake()
    {
    }

    private void OnDestroy()
    {
        if (_IEAllCardOpen != null)
        {
            StopCoroutine(_IEAllCardOpen);
        }

        _IEAllCardOpen = null;
    }

    private void Update()
    {
        if (_IsClick == false)
            return;

        if (_3DCamera == null)
            return;

        if(Input.GetMouseButtonUp(0))
        {
            Ray ray = _3DCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, _HitDistance) == true)
            {
                GachaCardItem item = hit.transform.GetComponent<GachaCardItem>();
                if(item != null)
                {
                    item.Action();
                }
            }
        }
    }

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    /// <summary>
    /// 길드레이드 보상 테스트용
    /// </summary>
    public void Init()
    {
        // 길드레이드 보상에서는 카드이미지를 쓰지 않기 때문에 끈다.
        {
            for (int i = 0; i < _GachaCardItems.Count; ++i)
            {
                GachaCardItem CardItem = _GachaCardItems[i];
                if (CardItem == null)
                    continue;

                CardItem.gameObject.SetActive(false);
            }

            Transform[] Children = gameObject.GetComponentsInChildren<Transform>(true);
            foreach (Transform child in Children)
            {
                if (child.name.IndexOf("GachaCard_N") != -1)
                {
                    child.gameObject.SetActive(false);
                }

                //if (child.name == "pillar")
                //{
                //    ParticleSystem particle = child.GetComponent<ParticleSystem>();
                //    _GuildRaidRewardEffectTime = particle.main.startDelayMultiplier + particle.main.duration;
                //}
            }
        }

        _Anim.Stop();
        _OpenEffect.SetActive(false);
        _EndEffect.SetActive(false);
    }

    /// <summary>
    /// 랜덤박스 초기화
    /// </summary>
    /// <param name="recvData">서버에서 받은 데이터</param>
    public void Init(_stShopBuyAck recvData)
    {
        if (_Anim != null)
        {
            _AnimLength = _Anim.clip.length;
        }

        _3DCamera = UIControlManager.instance.Camera3D;

        CreateCardMaterials(recvData);
        
        if (CDATA_FIXED_CONSTANTS.GetCount() < 1)
            CDATA_FIXED_CONSTANTS.Load();

        DATA_FIXED_CONSTANTS dataconstant = CDATA_FIXED_CONSTANTS.Get(DATA_FIXED_CONSTANTS._enConstant.Shop_Summon_High_Grade);
        if (dataconstant != null)
        {
            _ShopSummonHighGrade = (int)dataconstant.Value;
        }

        for (int i = 0; i < _GachaCardItems.Count; ++i)
        {
            GachaCardItem CardItem = _GachaCardItems[i];
            if (CardItem == null)
                continue;

            CardItem.Init();
        }
    }

    /// <summary>
    /// 카드 뒷면 텍스쳐와 필요한 메테리얼 미리 로드
    /// </summary>
    /// <param name="recvData"></param>
    private void CreateCardMaterials(_stShopBuyAck recvData)
    {
        string CardNormalTexture = "CardCreature_N";
        string CardRoyalTexture = "CardCreature_S";
        string ItemNormalTexture = "CardItem_N";
        string ItemRoyalTexture = "CardItem_S";

        //if (recvData.cShopBuyGood.kPayType == DATA_ITEM_SUB_TYPE_NEW._enItemStatusSubType.ITEMTYPE_MI_DIA)
        //{
        //    CardNormalMaterial = "CardCreature_N";
        //    CardRoyalMaterial = "CardCreature_S";
        //    ItemNormalMaterial = "CardItem_N";
        //    ItemRoyalMaterial = "CardItem_S";
        //}
        //else 
        if (recvData.cShopBuyGood.kPayType == DATA_ITEM_SUB_TYPE_NEW._enItemStatusSubType.ITEMTYPE_MI_MEDAL)
        {
            //CardNormalTexture = "CardCreature_N";
            //CardRoyalTexture = "CardCreature_S";
            ItemNormalTexture = "CardPvp_N";
            ItemRoyalTexture = "CardPvp_S";
        }
        else if (recvData.cShopBuyGood.kPayType == DATA_ITEM_SUB_TYPE_NEW._enItemStatusSubType.ITEMTYPE_MI_SHARD)
        {
#if ASSET_BUNDLE
            if(_CardRaidWeaponNormal == null) _CardRaidWeaponNormal = ResourceBundle.intance.ResourceObject(BUNDLELIST.PREFABS_ETC_GACHACARD_MATERIALS.ToString(), "CardRaidWeapon_N", typeof(Texture)) as Texture;
            if(_CardRaidWeaponRoyal == null) _CardRaidWeaponRoyal = ResourceBundle.intance.ResourceObject(BUNDLELIST.PREFABS_ETC_GACHACARD_MATERIALS.ToString(), "CardRaidWeapon_S", typeof(Texture)) as Texture;
            if(_CardRaidWeaponGrade5 == null) _CardRaidWeaponGrade5 = ResourceBundle.intance.ResourceObject(BUNDLELIST.PREFABS_ETC_GACHACARD_MATERIALS.ToString(), "CardRaidWeapon_5", typeof(Texture)) as Texture;
            if(_CardRaidWeaponGrade6 == null) _CardRaidWeaponGrade6 = ResourceBundle.intance.ResourceObject(BUNDLELIST.PREFABS_ETC_GACHACARD_MATERIALS.ToString(), "CardRaidWeapon_6", typeof(Texture)) as Texture;
            
            if(_CardRaidArmorNormal == null) _CardRaidArmorNormal = ResourceBundle.intance.ResourceObject(BUNDLELIST.PREFABS_ETC_GACHACARD_MATERIALS.ToString(), "CardRaidArmor_N", typeof(Texture)) as Texture;
            if(_CardRaidArmorRoyal == null) _CardRaidArmorRoyal = ResourceBundle.intance.ResourceObject(BUNDLELIST.PREFABS_ETC_GACHACARD_MATERIALS.ToString(), "CardRaidArmor_S", typeof(Texture)) as Texture;
            if(_CardRaidArmorGrade5 == null) _CardRaidArmorGrade5 = ResourceBundle.intance.ResourceObject(BUNDLELIST.PREFABS_ETC_GACHACARD_MATERIALS.ToString(), "CardRaidArmor_5", typeof(Texture)) as Texture;
            if(_CardRaidArmorGrade6 == null) _CardRaidArmorGrade6 = ResourceBundle.intance.ResourceObject(BUNDLELIST.PREFABS_ETC_GACHACARD_MATERIALS.ToString(), "CardRaidArmor_6", typeof(Texture)) as Texture;
#else
            if (_CardRaidWeaponNormal == null) _CardRaidWeaponNormal = Resources.Load("Prefabs/Etc/GachaCard/Materials/CardRaidWeapon_N") as Texture;
            if (_CardRaidWeaponRoyal == null) _CardRaidWeaponRoyal = Resources.Load("Prefabs/Etc/GachaCard/Materials/CardRaidWeapon_S") as Texture;
            if (_CardRaidWeaponGrade5 == null) _CardRaidWeaponGrade5 = Resources.Load("Prefabs/Etc/GachaCard/Materials/CardRaidWeapon_5") as Texture;
            if (_CardRaidWeaponGrade6 == null) _CardRaidWeaponGrade6 = Resources.Load("Prefabs/Etc/GachaCard/Materials/CardRaidWeapon_6") as Texture;

            if (_CardRaidArmorNormal == null) _CardRaidArmorNormal = Resources.Load("Prefabs/Etc/GachaCard/Materials/CardRaidArmor_N") as Texture;
            if (_CardRaidArmorRoyal == null) _CardRaidArmorRoyal = Resources.Load("Prefabs/Etc/GachaCard/Materials/CardRaidArmor_S") as Texture;
            if (_CardRaidArmorGrade5 == null) _CardRaidArmorGrade5 = Resources.Load("Prefabs/Etc/GachaCard/Materials/CardRaidArmor_5") as Texture;
            if (_CardRaidArmorGrade6 == null) _CardRaidArmorGrade6 = Resources.Load("Prefabs/Etc/GachaCard/Materials/CardRaidArmor_6") as Texture;
#endif
        }

        _CardCreatureNormal = LoadCardTexture(CardNormalTexture);
        _CardCreatureRoyal = LoadCardTexture(CardRoyalTexture);
        _CardItemNormal = LoadCardTexture(ItemNormalTexture);
        _CardItemRoyal = LoadCardTexture(ItemRoyalTexture);
    }

    private Texture LoadCardTexture(string textureName)
    {
        Texture texture;
#if ASSET_BUNDLE
        texture = ResourceBundle.intance.ResourceObject(BUNDLELIST.PREFABS_ETC_GACHACARD_MATERIALS.ToString(), textureName, typeof(Texture)) as Texture;
#else
        texture = Resources.Load(string.Format("Prefabs/Etc/GachaCard/Materials/{0}", textureName)) as Texture;
        if (texture == null)
        {
            object originTexture = Resources.Load(string.Format("Prefabs/Etc/GachaCard/Materials/{0}", textureName));
            if (originTexture != null)
            {
                if (originTexture is Material)
                {
                    texture = (originTexture as Material).mainTexture;
                }
            }
        }
#endif

        return texture;
    }

    public void CreateCard(_stShopBuyAck recvData, DelegateOnceEventDone cardOpenEvt, DelegateOnceEventDoneWithInt event3D)
    {
        int iCount = 0;
        if (recvData.vAddCreatures != null && recvData.vAddCreatures.Count > 0)           // 크리쳐
        {
            iCount = recvData.vAddCreatures.Count;

            if (iCount == 1)     // 단차
            {
                OneCreatureGacha(recvData, cardOpenEvt, event3D);
            }
            else if (iCount == 11) // 11연차
            {
                ContinueCreatureGacha(iCount, recvData, cardOpenEvt, event3D);
            }
            else
            {
#if DEBUG_LOG
                Debug.LogError("CreateCard - 약속되지 않은 값이 들어왔다.");
#endif
            }
        }
        else if(recvData.vAddItems != null && recvData.vAddItems.Count > 0)           // 아이템
        {
            iCount = recvData.vAddItems.Count;

            if (iCount == 1)     // 단차
            {
                OneItemGacha(recvData, cardOpenEvt, event3D);
            }
            else if (iCount == 11) // 11연차
            {
                ContinueItemGacha(iCount, recvData, cardOpenEvt, event3D);
            }
            else
            {
#if DEBUG_LOG
                Debug.LogError("CreateCard - 약속되지 않은 값이 들어왔다.");
#endif
            }
        }
    }

    private void OneCreatureGacha(_stShopBuyAck recvData, DelegateOnceEventDone cardOpenEvt, DelegateOnceEventDoneWithInt event3D)
    {
        for (int i = 0; i < _GachaCardItems.Count; ++i)
        {
            GachaCardItem CardItem = _GachaCardItems[i];
            if (CardItem == null)
                continue;

            CardItem.SetActive(false);
        }

        CCreatureDetail CreatureDetail = recvData.vAddCreatures[0];
        if (CreatureDetail == null)
            return;

        DATA_CREATURE_NEWVER CreatureTableData = CDATA_CREATURE_NEWVER.Get(CreatureDetail.kCreatureID);
        if (CreatureTableData == null)
            return;

        GachaCardItem ConfirmationCard = _ConfirmationCardParent.GetComponent<GachaCardItem>();
        if (ConfirmationCard == null)
            return;

        ConfirmationCard.SetActive(true);

        DATA_ITEM_SUB_TYPE_NEW._enItemStatusSubType PayType = recvData.cShopBuyGood.kPayType;

        if (_ShopSummonHighGrade <= (int)CreatureTableData.m_enGrade)       // royal
        {
            ConfirmationCard.CreateCard(PayType, GachaCardItem.enCardGrade.Royal, _CardCreatureRoyal, CreatureDetail, CreatureTableData, cardOpenEvt, event3D);
        }
        else
        {
            ConfirmationCard.CreateCard(PayType, GachaCardItem.enCardGrade.Normal, _CardCreatureNormal, CreatureDetail, CreatureTableData, cardOpenEvt, null);
        }
    }

    private void ContinueCreatureGacha(int iCount, _stShopBuyAck recvData, DelegateOnceEventDone cardOpenEvt, DelegateOnceEventDoneWithInt event3D)
    {
        GachaCardItem ConfirmationCard = null;
        DATA_CREATURE_NEWVER CreatureTableData = null;
        DATA_ITEM_SUB_TYPE_NEW._enItemStatusSubType PayType = recvData.cShopBuyGood.kPayType;

        for (int i = 0; i < iCount; ++i)
        {
            CCreatureDetail CreatureDetail = recvData.vAddCreatures[i];
            if (CreatureDetail == null)
                continue;

            CreatureTableData = CDATA_CREATURE_NEWVER.Get(CreatureDetail.kCreatureID);
            if (CreatureTableData == null)
                continue;

            GachaCardItem CardItem = _GachaCardItems[i];
            if (CardItem == null)
                continue;

            if (_ConfirmationCardParent == CardItem.gameObject)
            {
                ConfirmationCard = CardItem;
                continue;
            }

            if (_ShopSummonHighGrade <= (int)CreatureTableData.m_enGrade)       // royal
            {
                CardItem.CreateCard(PayType, GachaCardItem.enCardGrade.Royal, _CardCreatureRoyal, CreatureDetail, CreatureTableData, cardOpenEvt, event3D);
            }
            else                                                                // normal
            {
                CardItem.CreateCard(PayType, GachaCardItem.enCardGrade.Normal, _CardCreatureNormal, CreatureDetail, CreatureTableData, cardOpenEvt, null);
            }
        }

        // 마지막에 들어오는 11연차 확정카드 셋팅.
        {
            CCreatureDetail CreatureDetail = recvData.vAddCreatures[recvData.vAddCreatures.Count - 1];
            CreatureTableData = CDATA_CREATURE_NEWVER.Get(CreatureDetail.kCreatureID);
            if (CreatureTableData != null)
            {
                if (_ShopSummonHighGrade <= (int)CreatureTableData.m_enGrade)       // royal
                {
                    ConfirmationCard.CreateCard(PayType, GachaCardItem.enCardGrade.Royal, _CardCreatureRoyal, CreatureDetail, CreatureTableData, cardOpenEvt, event3D);
                }
                else                                                                // normal
                {
                    ConfirmationCard.CreateCard(PayType, GachaCardItem.enCardGrade.Normal, _CardCreatureNormal, CreatureDetail, CreatureTableData, cardOpenEvt, null);
                }
            }
        }
    }

    private void OneItemGacha(_stShopBuyAck recvData, DelegateOnceEventDone cardOpenEvt, DelegateOnceEventDoneWithInt event3D)
    {
        for (int i = 0; i < _GachaCardItems.Count; ++i)
        {
            GachaCardItem CardItem = _GachaCardItems[i];
            if (CardItem == null)
                continue;

            CardItem.SetActive(false);
        }

        CItem item = recvData.vAddItems[0];
        if (item == null)
            return;

        DATA_ITEM_NEW ItemTableData = CDATA_ITEM_NEW.Get(item.m_ItemID);
        if (ItemTableData == null)
            return;

        GachaCardItem ConfirmationCard = _ConfirmationCardParent.GetComponent<GachaCardItem>();
        if (ConfirmationCard == null)
            return;

        ConfirmationCard.SetActive(true);

        DATA_ITEM_SUB_TYPE_NEW._enItemStatusSubType PayType = recvData.cShopBuyGood.kPayType;

        if (_ShopSummonHighGrade <= ItemTableData.m_iGrade)
        {
            if (PayType == DATA_ITEM_SUB_TYPE_NEW._enItemStatusSubType.ITEMTYPE_MI_SHARD)
            {
                if (recvData.cShopBuyGood.kGoodsID == DATA_SHOP_NEW_DEFINE_GOODS_NAME_ENUM._enShopGoodsName.SHOP_RAID_WEAPON_0306)      // 무기면?
                {
                    ConfirmationCard.CreateCard(PayType, GachaCardItem.enCardGrade.Royal, _CardRaidWeaponRoyal, item, ItemTableData, cardOpenEvt);
                }
                else if (recvData.cShopBuyGood.kGoodsID == DATA_SHOP_NEW_DEFINE_GOODS_NAME_ENUM._enShopGoodsName.SHOP_RAID_ARMOR_0306)     // 방어구면?
                {
                    ConfirmationCard.CreateCard(PayType, GachaCardItem.enCardGrade.Royal, _CardRaidArmorRoyal, item, ItemTableData, cardOpenEvt);
                }
                else if (recvData.cShopBuyGood.kGoodsID == DATA_SHOP_NEW_DEFINE_GOODS_NAME_ENUM._enShopGoodsName.SHOP_RAID_WEAPON_05)       // 5성 무기면?
                {
                    ConfirmationCard.CreateCard(PayType, GachaCardItem.enCardGrade.Royal, _CardRaidWeaponGrade5, item, ItemTableData, cardOpenEvt);
                }
                else if (recvData.cShopBuyGood.kGoodsID == DATA_SHOP_NEW_DEFINE_GOODS_NAME_ENUM._enShopGoodsName.SHOP_RAID_WEAPON_06)       // 6성 무기면?
                {
                    ConfirmationCard.CreateCard(PayType, GachaCardItem.enCardGrade.Royal, _CardRaidWeaponGrade6, item, ItemTableData, cardOpenEvt);
                }
                else if (recvData.cShopBuyGood.kGoodsID == DATA_SHOP_NEW_DEFINE_GOODS_NAME_ENUM._enShopGoodsName.SHOP_RAID_ARMOR_05)       // 5성 방어구면?
                {
                    ConfirmationCard.CreateCard(PayType, GachaCardItem.enCardGrade.Royal, _CardRaidArmorGrade5, item, ItemTableData, cardOpenEvt);
                }
                else if (recvData.cShopBuyGood.kGoodsID == DATA_SHOP_NEW_DEFINE_GOODS_NAME_ENUM._enShopGoodsName.SHOP_RAID_ARMOR_06)       // 6성 방어구면?
                {
                    ConfirmationCard.CreateCard(PayType, GachaCardItem.enCardGrade.Royal, _CardRaidArmorGrade6, item, ItemTableData, cardOpenEvt);
                }
            }
            else
            {
                ConfirmationCard.CreateCard(PayType, GachaCardItem.enCardGrade.Royal, _CardItemRoyal, item, ItemTableData, cardOpenEvt);
            }
        }
        else
        {
            if (PayType == DATA_ITEM_SUB_TYPE_NEW._enItemStatusSubType.ITEMTYPE_MI_SHARD)        // 레이드 아이템이면?
            {
                if (recvData.cShopBuyGood.kGoodsID == DATA_SHOP_NEW_DEFINE_GOODS_NAME_ENUM._enShopGoodsName.SHOP_RAID_WEAPON_0306)      // 무기면?
                {
                    ConfirmationCard.CreateCard(PayType, GachaCardItem.enCardGrade.Normal, _CardRaidWeaponNormal, item, ItemTableData, cardOpenEvt);
                }
                else if (recvData.cShopBuyGood.kGoodsID == DATA_SHOP_NEW_DEFINE_GOODS_NAME_ENUM._enShopGoodsName.SHOP_RAID_ARMOR_0306)     // 방어구면?
                {
                    ConfirmationCard.CreateCard(PayType, GachaCardItem.enCardGrade.Normal, _CardRaidArmorNormal, item, ItemTableData, cardOpenEvt);
                }
            }
            else
            {
                ConfirmationCard.CreateCard(PayType, GachaCardItem.enCardGrade.Normal, _CardItemNormal, item, ItemTableData, cardOpenEvt);
            }
        }
    }

    private void ContinueItemGacha(int iCount, _stShopBuyAck recvData, DelegateOnceEventDone cardOpenEvt, DelegateOnceEventDoneWithInt event3D)
    {
        GachaCardItem RoyalItemCard = null;
        DATA_ITEM_SUB_TYPE_NEW._enItemStatusSubType PayType = recvData.cShopBuyGood.kPayType;

        for (int i = 0; i < iCount; ++i)
        {
            CItem item = recvData.vAddItems[i];
            if (item == null)
                continue;

            DATA_ITEM_NEW ItemTableData = CDATA_ITEM_NEW.Get(item.m_ItemID);
            if (ItemTableData == null)
                continue;

            GachaCardItem CardItem = _GachaCardItems[i];
            if (CardItem == null)
                continue;

            if (_ShopSummonHighGrade <= ItemTableData.m_iGrade)       // royal
            {
                if(RoyalItemCard == null)
                {
                    RoyalItemCard = CardItem;
                }

                CardItem.CreateCard(PayType, GachaCardItem.enCardGrade.Royal, _CardItemRoyal, item, ItemTableData, cardOpenEvt);
            }
            else                                                                // normal
            {
                CardItem.CreateCard(PayType, GachaCardItem.enCardGrade.Normal, _CardItemNormal, item, ItemTableData, cardOpenEvt);
            }
        }

        if(RoyalItemCard != null)
        {
            GachaCardItem CenterCard = _ConfirmationCardParent.GetComponent<GachaCardItem>();
            if (CenterCard == null)
                return;

            GachaCardItem.enCardGrade CenterCardGrade = CenterCard.CardGrade;
            CItem CenterCardItem = CenterCard.Item;
            DATA_ITEM_NEW CenterCardItemTableData = CenterCard.ItemTableData;
            Texture CenterCardTexture = CenterCard.CardTexture;

            GachaCardItem.enCardGrade RoyalCardGrade = RoyalItemCard.CardGrade;
            CItem RoyalCardItem = RoyalItemCard.Item;
            DATA_ITEM_NEW RoyalCardItemTableData = RoyalItemCard.ItemTableData;
            Texture RoyalCardTexture = RoyalItemCard.CardTexture;

            CenterCard.ChangeItemCard(PayType, RoyalCardGrade, RoyalCardTexture, RoyalCardItem, RoyalCardItemTableData);
            RoyalItemCard.ChangeItemCard(PayType, CenterCardGrade, CenterCardTexture, CenterCardItem, CenterCardItemTableData);
        }
    }

    public void ActiveCardAction()
    {
        for (int i = 0; i < _GachaCardItems.Count; ++i)
        {
            GachaCardItem CardItem = _GachaCardItems[i];
            if (CardItem == null)
                continue;

            CardItem.SetAnimation();
            CardItem.SetEffect();
            CardItem.EnableCollider(true);
        }
    }

    public void AllCardOpen()
    {
        for(int i = 0; i < _GachaCardItems.Count; ++i)
        {
            GachaCardItem carditem = _GachaCardItems[i];
            if (carditem == null)
                continue;

            carditem.onOpen3DEvent = null;
        }

        if(_IEAllCardOpen == null)
        {
            _IEAllCardOpen = IeAllCardOpen();
        }

        StartCoroutine(_IEAllCardOpen);
    }

    private IEnumerator IeAllCardOpen()
    {
        for (int i = 0; i < _GachaCardItems.Count; ++i)
        {
            GachaCardItem CardItem = _GachaCardItems[i];
            if (CardItem == null)
                continue;

            if (CardItem.IsFinish == false)
            {
                //CardItem.onOpen3DEvent = null;
                CardItem.Action();
            }

            yield return new WaitForSeconds(0.2f);
        }

        //yield return new WaitForSeconds(1.0f);
    }
}
