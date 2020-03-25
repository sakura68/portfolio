using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DGL_DATA_READER;

/// <summary>
/// AchieveComplete 뎁스 9990 생성되고 사라지기 때문에
/// 확인용으로 팝업보다도 위에 띄워버린다.
/// </summary>
public class AchieveComplete : MonoBehaviour
{
    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private UILabel _TitleLabel;

    [SerializeField] private UILabel _AchieveContentLabel;
    [SerializeField] private UI2DSprite _AchieveEmblem;
    [SerializeField] private UI2DSprite _AchieveRewardIcon;

    [SerializeField] private List<UISprite> _BGSprites;
    [SerializeField] private UISprite _DecoBottomSprite;

    [SerializeField] private TweenController _TweenController;


    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private const int _Gap = -32;

    private float _ElapsedTime = 0.2f;
    private float _Time = 0.0f;

    private bool _IsMainScene = true;

    //===================================================================================
    //
    // Default Method
    //
    //===================================================================================
    private void Awake()
    {
        // 7461	업적 완료
        _TitleLabel.text = StringTableManager.GetData(7461);
    }

    private void Update()
    {
        if (_IsMainScene == true)
            return;

        _Time += Time.deltaTime;

        if(_Time > _ElapsedTime)        // 배틀씬 일때만 0.2초마다 로비로 돌아왔는지 확인.
        {
            _Time = 0.0f;

            if(UIControlManager.instance != null)       // 로비로 왔으면 실행.
            {
                UtilTransform.AttachTransForm(UIControlManager.instance.m_pAnchorTrans, transform, SetTransformType.OriginValue);
                transform.localPosition = new Vector3(0.0f, 148.0f, 0.0f);
                transform.localScale = Vector3.one;

                StartTween(true);
                _IsMainScene = true;
            }
        }
    }

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    public void Init(_stAcv_AlramAck stResult, EnumGameScene sceneType)
    {
        transform.localPosition = new Vector3(0.0f, 148.0f, 0.0f);
        transform.localScale = Vector3.one;

        if (stResult.vAcvIDs == null || stResult.vAcvIDs.Count < 1)
        {
#if DEBUG_LOG
            Debug.Log("<color=red>_stAcv_AlramAck 받은 업적 데이터가 없다.</color>");
#endif
            return;
        }

        if (CDATA_ACV_MAIN.GetCount() < 1)
            CDATA_ACV_MAIN.Load();

        if(sceneType == EnumGameScene.MainMenuScene)
        {
            StartTween(true);
            _IsMainScene = true;
        }
        else
        {
            StartTween(false);
            _IsMainScene = false;
        }

        Vector3 OriginLabelPos = _AchieveContentLabel.transform.localPosition;
        Vector3 OriginDecoPos = _DecoBottomSprite.transform.localPosition;
        int OriginBgHeight = _BGSprites[0].height;

        int RecvAchieveCount = stResult.vAcvIDs.Count;
        for (int i = 0; i < RecvAchieveCount; ++i)
        {
            DATA_ACV_MAIN AcvData = CDATA_ACV_MAIN.Get(stResult.vAcvIDs[i]);
            if (AcvData == null)
                continue;

            if (i == 0)
            {
                _AchieveContentLabel.text = StringTableManager.GetData(AcvData.iMissionTitle);
            }
            else
            {
                UILabel CloneLabel = UIResourceMgr.InstantiateGameObject(_AchieveContentLabel, _AchieveContentLabel.transform.parent, SetTransformType.OriginValue);
                if(CloneLabel != null)
                {
                    CloneLabel.text = StringTableManager.GetData(AcvData.iMissionTitle);

                    CloneLabel.transform.localScale = Vector3.one;
                    CloneLabel.transform.localPosition = new Vector3(OriginLabelPos.x, OriginLabelPos.y + (i * _Gap), OriginLabelPos.z);

                    _DecoBottomSprite.transform.localPosition = new Vector3(OriginDecoPos.x, OriginDecoPos.y + (i * _Gap), OriginDecoPos.z);

                    for (int k = 0; k < _BGSprites.Count; ++k)
                    {
                        UISprite bgSprite = _BGSprites[k];
                        if (bgSprite == null)
                            continue;

                        bgSprite.height = OriginBgHeight + (i * (_Gap * -1));
                    }
                }
            }
        }
    }

    private void StartTween(bool IsStart)
    {
        _TweenController.gameObject.SetActive(IsStart);

        if (IsStart == true)
        {
            _TweenController.Initialize();
            _TweenController.StartTweenAll();
            _TweenController.AddEvent(OnDestroyUI);
            _TweenController.EndEventAction(false);
        }
    }

    //===================================================================================
    //
    // Event
    //
    //===================================================================================
    private void OnDestroyUI()
    {
        Destroy(gameObject);
    }
}
