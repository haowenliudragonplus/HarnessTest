using System;
using Framework;
using TMPro;
using UnityEngine.UI;

public class UIWidget_LevelItem : UIWidget_LevelItemBase
{

    public int LevelNumber;

    protected override void OnCreate()
    {
        base.OnCreate();
    }

    protected override void OnInit(object viewData)
    {
        base.OnInit(viewData);
        LevelNumber = (int)viewData;
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        RefreshView();
    }

    // protected override void OnShow()
    // {
    //     base.OnShow();
    //     RefreshView();
    // }

    public void SetData(int levelNumber)
    {
        this.LevelNumber = levelNumber;
        RefreshView();
    }

    private void RefreshView()
    {
        int curLevelIndex = Game.GetMod<ModInGame>().GetLevelIndex(EInGameModeType.Main);
        int curLevel = curLevelIndex + 1;

        if (curLevel > LevelNumber)
        {
            UINode_Progress.gameObject.SetActive(true);
            UIImg_Progress.gameObject.SetActive(true);

            UINode_LevelBgGroup.gameObject.SetActive(false);

            UINode_Finished.gameObject.SetActive(true);
            return;
        }

        var info = Game.GetMod<ModInGame>().GetLevelCfg(LevelNumber - 1, EInGameModeType.Main, false);
        Image imgBg = null;
        if (curLevel < LevelNumber)
        {
            UINode_Progress.gameObject.SetActive(true);
            UIImg_Progress.gameObject.SetActive(false);

            UINode_LevelBgGroup.gameObject.SetActive(true);
            UIImg_LockBg.gameObject.SetActive(info.DifficultyType == (int)EIngameDifficultType.Easy);
            UIImg_NormalBg.gameObject.SetActive(false);
            UIImg_HardBg.gameObject.SetActive(info.DifficultyType == (int)EIngameDifficultType.Hard);
            UIImg_SuperHardBg.gameObject.SetActive(info.DifficultyType == (int)EIngameDifficultType.SuperHard);

            UINode_Finished.gameObject.SetActive(false);

            imgBg = (EIngameDifficultType)info.DifficultyType switch
            {
                EIngameDifficultType.Easy => UIImg_LockBg,
                EIngameDifficultType.Hard => UIImg_HardBg,
                EIngameDifficultType.SuperHard => UIImg_SuperHardBg,
                _ => null
            };
            imgBg.transform.localScale = UnityEngine.Vector3.one * 0.85f;
        }
        else
        {
            UINode_Progress.gameObject.SetActive(true);
            UIImg_Progress.gameObject.SetActive(true);

            UINode_LevelBgGroup.gameObject.SetActive(true);
            UIImg_LockBg.gameObject.SetActive(false);
            UIImg_NormalBg.gameObject.SetActive(info.DifficultyType == (int)EIngameDifficultType.Easy);
            UIImg_HardBg.gameObject.SetActive(info.DifficultyType == (int)EIngameDifficultType.Hard);
            UIImg_SuperHardBg.gameObject.SetActive(info.DifficultyType == (int)EIngameDifficultType.SuperHard);

            UINode_Finished.gameObject.SetActive(false);

            imgBg = (EIngameDifficultType)info.DifficultyType switch
            {
                EIngameDifficultType.Easy => UIImg_NormalBg,
                EIngameDifficultType.Hard => UIImg_HardBg,
                EIngameDifficultType.SuperHard => UIImg_SuperHardBg,
                _ => null
            };

            imgBg.transform.localScale = UnityEngine.Vector3.one;
        }

        if (imgBg != null)
        {
            var txtLevel = imgBg.transform.Find("Txt_Level").GetComponent<TextMeshProUGUI>();
            txtLevel.SetText($"{LevelNumber}");
        }
    }
}
