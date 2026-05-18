using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using DG.Tweening;
using DragonPlus.Haptics;
using Framework;
using UnityEngine;
using Color = UnityEngine.Color;
using Object = UnityEngine.Object;

/// <summary>
/// 箭头实体
/// </summary>
public class ArrowEntity
{
    public const float ArrowLineWidth = 0.16f;//箭头线的宽度
    public const float PointSize = 0.16f;//点的大小（图片分辨率）
    private const float MoveForwardSpeed = 30;//前进速度
    private const float MoveBackwardSpeed = 40;//后退速度
    private const float ExtraColliderWidth = 0.63f;//额外的碰撞器宽度
    private static Color IdleColor_UnCollider;
    private static Color IdleColor_Collider;
    private static Color MoveableColor;
    private static Color HintColor_UnCollider;
    private static Color HintColor_Collider;
    private static Color HintMoveableColor;
    private static Color HintNoMoveableColor;
    

    public GameObject Holder { get; private set; } //实体
    public ArrowData ArrowData { get; private set; }// 数据
    public InGameModeBase Mode { get; private set; }

    public GameObject Go_Arrow => go_Arrow;
    private GameObject go_Arrow;//箭头
    private Vector2 headPos;//头位置
    private Vector2 tailPos;//尾位置
    private LineRenderer com_LineRenderer;//线渲染器组件
    public List<Collider2D> bodyColliderList = new List<Collider2D>();//所有碰撞器

    private HashSet<int> playedPointAniIndexList = new HashSet<int>();//已经播放过动画的点索引
    private List<Vector3> drawPointV3List = new List<Vector3>();//绘制的点列表

    private Sequence sequence_ColorAni;
    private Sequence sequence_BeHintAni;
    private CoroutineHandler coroutineHandler_LineAni;
    public bool IsEnterAniOver { get; private set; }

    public ArrowEntity(GameObject holder, List<int> turnPointIndexList, string colorHex)
    {
        Holder = holder;
        ArrowData = new ArrowData(this, turnPointIndexList, colorHex);
        IsEnterAniOver = false;

        InitData();

        Init();
    }

    private void InitData()
    {
        int v = 2;
        if (v == 1)
        {
            IdleColor_UnCollider = ColorUtils.Hex2Color("#000000");//静止的颜色（没有碰撞过）
            IdleColor_Collider = ColorUtils.Hex2Color("#ff3b4f");//静止的颜色（碰撞过）
            MoveableColor = ColorUtils.Hex2Color("#3b80ff");//可移动的颜色
            HintColor_UnCollider = ColorUtils.Hex2Color("#a5e0f6");//提示的颜色（没有碰撞过）
            HintColor_Collider = ColorUtils.Hex2Color("#ee95a8");//提示的颜色（碰撞过）

        }
        else
        {
            IdleColor_UnCollider = ColorUtils.Hex2Color(ArrowData.colorHex);//静止的颜色（没有碰撞过）
            IdleColor_Collider = ColorUtils.Hex2Color("#ffffff");//静止的颜色（碰撞过）
            MoveableColor = ColorUtils.Hex2Color("#18ff00");//可移动的颜色
            HintColor_UnCollider = ColorUtils.Hex2Color("#434a67");//提示的颜色（没有碰撞过）
            HintColor_Collider = ColorUtils.Hex2Color("#434a67");//提示的颜色（碰撞过）
        }
        HintMoveableColor = ColorUtils.Hex2Color("#FFFFFF");
        HintNoMoveableColor = ColorUtils.Hex2Color("#f32424");
        
    }

    private void Init()
    {
        Mode = (Game.GetMod<ModFsm>().CurState as FsmState_InGame).Mode;

        // 绘制箭头
        go_Arrow = Game.GetMod<ModAsset>().GetGameObject(InGameConst.Prefab_Arrow).GetInstance();
        go_Arrow.transform.SetParent(Holder.transform, false);
        go_Arrow.transform.localPosition = ArrowData.segmentDataList[0].startPos;
        go_Arrow.SetLayer(InGameConst.LayerName_InGame_Element, true);
        SpriteRendererUtils.SetSoringLayer(go_Arrow, CommonConst.SortingLayer_Layer1, true);
        go_Arrow.transform.localRotation = Quaternion.Euler(0, 0, InGameUtils.GetRotationAngle(ArrowData.segmentDataList[0].dir));
        var collder_Arrow = go_Arrow.transform.GetComponentInChildren<Collider2D>();
        go_Arrow.GetComponentInChildren<SpriteRenderer>().color = IdleColor_UnCollider;
        InGameRayUtils.Add(collder_Arrow, this);
        // 绘制线
        if (ArrowData.segmentDataList.Count < 1)
        {
            CLog.Error("线段数量必须大于0！！！！！");
        }
        for (int i = 0; i < ArrowData.segmentDataList.Count; i++)
        {
            var drawPoint = ArrowData.segmentDataList[i].startPos;
            drawPointV3List.Add(drawPoint);
        }
        headPos = ArrowData.segmentDataList[0].startPos;
        tailPos = ArrowData.segmentDataList[^1].endPos;
        drawPointV3List.Add(ArrowData.segmentDataList[^1].endPos);
        com_LineRenderer = LineRendererUtils.CreateLineRenderer(Holder.transform, ArrowLineWidth, IdleColor_UnCollider,
          drawPointV3List, CommonConst.SortingLayer_Layer1, InGameConst.LayerName_InGame_Element,
          cornerVetices: 5, endCapVertices: 5);
        com_LineRenderer.transform.SetParent(Holder.transform, false);
        // 绘制线的碰撞器
        float colliderWidth = ArrowLineWidth + ExtraColliderWidth;
        for (int i = 0; i < com_LineRenderer.positionCount - 1; i++)
        {
            Vector3 startPos = com_LineRenderer.GetPosition(i);
            Vector3 endPos = com_LineRenderer.GetPosition(i + 1);

            // 计算线段的向量
            Vector2 direction = endPos - startPos;
            float length = direction.magnitude;

            // 除最后一条线段，每段线段都延伸到终点
            if (i < com_LineRenderer.positionCount - 2)
            {
                length += colliderWidth / 2;
                endPos += (Vector3)(direction.normalized * colliderWidth / 2);
            }

            // 判断是水平还是垂直线段
            bool isHorizontal = Mathf.Abs(direction.x) > Mathf.Abs(direction.y);

            var boxCollider = com_LineRenderer.gameObject.AddComponent<BoxCollider2D>();
            boxCollider.isTrigger = true;
            boxCollider.offset = (startPos + endPos) / 2;

            if (isHorizontal)
            {
                // 水平线段：宽度=长度，高度=线宽
                boxCollider.size = new Vector2(length, colliderWidth);
            }
            else
            {
                // 垂直线段：宽度=线宽，高度=长度
                boxCollider.size = new Vector2(colliderWidth, length);
            }
            bodyColliderList.Add(boxCollider);
            InGameRayUtils.Add(boxCollider, this);
        }
    }

    /// <summary>
    /// 被选择
    /// </summary>
    public void OnSelected()
    {
        Mode.Data.ReduceStep();

        if (Mode.Data.HasCannotCompleteCarInMove.Get())
        {
            CLog.Info("有不能完成的箭头正在移动");
            return;
        }

        if (!ArrowData.CheckCanCompleteMove())
        {
            CLog.Info("不可以完整移动");

            GameUtils.Virbrate(HapticTypes.Medium);

            Mode.Data.HasCannotCompleteCarInMove.Set(ArrowData.segmentDataList[0].startPointIndex, true);
        }
        else
        {
            CLog.Info("可以完整移动");

            GameUtils.Virbrate(HapticTypes.Light);

            // 刷新提示道具状态
            if (Mode.Data.InHint)
            {
                Mode.Data.InHint = !Mode.Data.InHint;
                Mode.RefreshHintState(Mode.Data.InHint);
            }
            var view = Game.GetMod<ModUI>().FindView(UIViewName.UIView_InGame_Main) as UIView_InGame_Main;
            view?.RefreshView_Hint();
            //
            Mode.Data.ResetUnSelectValidArrowTime();
            //
            Game.GetMod<ModAudio>().PlaySound(AudioName.sfx_move);
            Mode.Data.TryFinishArrowGuide();
        }

        RefreshAllBodyCollider(false);

        if (!Mode.Data.HasCannotCompleteCarInMove.Get(ArrowData.segmentDataList[0].startPointIndex))
        {
            PlayChangeColorAni(IdleColor_UnCollider, MoveableColor);
        }

        ArrowData.State = EArrowState.MoveForward;
    }

    public void OnUpdate(float deltaTime)
    {
        if (ArrowData.State == EArrowState.Idle)
            return;

        if (ArrowData.State == EArrowState.MoveForward)
        {
            MoveForward(deltaTime);
        }
        else if (ArrowData.State == EArrowState.MoveBackward)
        {
            MoveBackward(deltaTime);
        }
    }

    #region 向前移动

    private void MoveForward(float deltaTime)
    {
        drawPointV3List.Clear();
        var moveDist = deltaTime * MoveForwardSpeed;
        // 计算头位置
        headPos += ArrowData.moveDir * moveDist;
        go_Arrow.transform.localPosition = headPos;
        drawPointV3List.Add(headPos);
        // 计算尾位置
        float distSum = (headPos - ArrowData.segmentDataList[0].startPos).magnitude;
        for (int i = 0; i < ArrowData.segmentDataList.Count; i++)
        {
            if (distSum >= ArrowData.totalDist)
                break;
            float leftDist = ArrowData.totalDist - distSum;
            if (leftDist >= ArrowData.segmentDataList[i].Dist)
            {
                distSum += ArrowData.segmentDataList[i].Dist;
                drawPointV3List.Add(ArrowData.segmentDataList[i].endPos);
            }
            else
            {
                distSum += leftDist;
                drawPointV3List.Add(ArrowData.segmentDataList[i].startPos - ArrowData.segmentDataList[i].dir * leftDist);
            }
        }
        if (drawPointV3List.Count == 1)//如果只有一个，表示尾部已经超过最初起点，相当于表现上是一条直线了
        {
            float totalDist = ArrowData.totalDist;
            drawPointV3List.Add(headPos - ArrowData.moveDir * totalDist);
        }
        // 播放点动画
        if (!Mode.Data.HasCannotCompleteCarInMove.Get(ArrowData.segmentDataList[0].startPointIndex))
        {
            CheckAndPlayPointAni();
        }
        // 设置线的显示
        LineRendererUtils.SetLineRenderer(com_LineRenderer, drawPointV3List);

        // 判断头部和尾部是否都超出屏幕边界
        if (!GameUtils.IsInSight2D(drawPointV3List[0], Mode.ElementCamera)
            && !GameUtils.IsInSight2D(drawPointV3List[^1], Mode.ElementCamera)
            && (headPos - ArrowData.segmentDataList[0].startPos).magnitude >= ArrowData.totalDist)
        {
            OnDestroy();

            // 检测游戏是否胜利
            Mode.CheckGameWin();
        }
    }

    /// <summary>
    /// 播放点动画
    /// </summary>
    private void CheckAndPlayPointAni()
    {
        float passedDist = (headPos - ArrowData.segmentDataList[0].startPos).magnitude;
        List<int> pointIndexList = new List<int>();
        float leftDistTemp = passedDist;
        for (int i = ArrowData.segmentDataList.Count - 1; i >= 0; i--)
        {
            var segmentData = ArrowData.segmentDataList[i];
            if (leftDistTemp > segmentData.Dist)
            {
                int pointIndex = segmentData.endPointIndex;
                if (!playedPointAniIndexList.Contains(pointIndex))
                {
                    pointIndexList.Add(pointIndex);
                    playedPointAniIndexList.Add(pointIndex);
                }
                if (i == 0)
                {
                    if (!playedPointAniIndexList.Contains(segmentData.startPointIndex))
                    {
                        pointIndexList.Add(segmentData.startPointIndex);
                        playedPointAniIndexList.Add(segmentData.startPointIndex);
                    }
                }
                leftDistTemp -= segmentData.Dist;
            }
            else
            {
                break;
            }

            // 播放点动画
            foreach (var pointIndex in pointIndexList)
            {
                var pointEntity = Mode.Data.GetPointEntity(pointIndex);
                pointEntity?.PlayZoomAni();
            }
        }
    }


    #endregion 向前移动

    #region 向后移动

    /// <summary>
    /// 碰撞到其他
    /// </summary>
    public void ColliderToOther()
    {
        ArrowData.State = EArrowState.MoveBackward;
        if (!ArrowData.HaveCollided)
        {
            Game.GetMod<ModAudio>().PlaySound(AudioName.sfx_no_move);
            var view = Game.GetMod<ModUI>().FindView(UIViewName.UIView_InGame_Main) as UIView_InGame_Main;
            view?.PlayColliderOtherEffect();
            Mode.CameraInput.Shake(0.25f, 0.1f, 20, 90f);
            PlayChangeColorAni(IdleColor_UnCollider, IdleColor_Collider);
            Mode.Data.ReduceHp();
            ArrowData.HaveCollided = true;
        }
    }

    private void MoveBackward(float deltaTime)
    {
        drawPointV3List.Clear();

        float distSum_Temp = (headPos - ArrowData.segmentDataList[0].startPos).magnitude;
        var moveDist = deltaTime * MoveBackwardSpeed;
        bool isReachStartPoint = false;
        if (moveDist >= distSum_Temp)
        {
            moveDist = distSum_Temp;
            isReachStartPoint = true;
        }

        // 计算头位置
        headPos -= ArrowData.moveDir * moveDist;
        go_Arrow.transform.localPosition = headPos;
        drawPointV3List.Add(headPos);
        // 计算尾位置
        float distSum = (headPos - ArrowData.segmentDataList[0].startPos).magnitude;
        for (int i = 0; i < ArrowData.segmentDataList.Count; i++)
        {
            if (distSum >= ArrowData.totalDist)
                break;
            float leftDist = ArrowData.totalDist - distSum;
            if (leftDist >= ArrowData.segmentDataList[i].Dist)
            {
                distSum += ArrowData.segmentDataList[i].Dist;
                drawPointV3List.Add(ArrowData.segmentDataList[i].endPos);
            }
            else
            {
                distSum += leftDist;
                drawPointV3List.Add(ArrowData.segmentDataList[i].startPos - ArrowData.segmentDataList[i].dir * leftDist);
            }
        }
        // 设置线的显示
        LineRendererUtils.SetLineRenderer(com_LineRenderer, drawPointV3List);

        if (isReachStartPoint)
        {
            RefreshAllBodyCollider(true);
            Mode.Data.HasCannotCompleteCarInMove.Remove(ArrowData.segmentDataList[0].startPointIndex);
            ArrowData.State = EArrowState.Idle;
        }
    }

    /// <summary>
    /// 被碰撞
    /// </summary>
    public void BeCollider()
    {
        if (ArrowData.BeHint)
        {
            PlayChangeColorAni2(MoveableColor, IdleColor_Collider);
            return;
        }
        else if (!ArrowData.HaveCollided)
        {
            PlayChangeColorAni2(IdleColor_UnCollider, IdleColor_Collider);
        }
    }

    #endregion 向后移动

    /// <summary>
    /// 颜色A变到颜色B
    /// </summary>
    private void PlayChangeColorAni(Color startColor, Color endColor)
    {
        sequence_ColorAni.Kill(true);
        go_Arrow.GetComponentInChildren<SpriteRenderer>().color = startColor;
        LineRendererUtils.SetColor(com_LineRenderer, startColor);
        Color2 color2_1 = new Color2(com_LineRenderer.startColor, com_LineRenderer.startColor);
        Color2 color2_2 = new Color2(endColor, endColor);
        float time = 0.6f;
        sequence_ColorAni = DOTween.Sequence();
        sequence_ColorAni.Append(com_LineRenderer.DOColor(color2_1, color2_2, time));
        sequence_ColorAni.Join(go_Arrow.GetComponentInChildren<SpriteRenderer>().DOColor(endColor, time));
        sequence_ColorAni.SetUpdate(true);
    }

    /// <summary>
    /// 颜色A变到颜色B再变回颜色A
    /// </summary>
    private void PlayChangeColorAni2(Color startColor, Color endColor)
    {
        sequence_ColorAni.Kill(true);
        go_Arrow.GetComponentInChildren<SpriteRenderer>().color = startColor;
        LineRendererUtils.SetColor(com_LineRenderer, startColor);
        Color2 color2_1 = new Color2(com_LineRenderer.startColor, com_LineRenderer.startColor);
        Color2 color2_2 = new Color2(endColor, endColor);
        float time = 0.15f;
        sequence_ColorAni = DOTween.Sequence();
        sequence_ColorAni.Append(com_LineRenderer.DOColor(color2_1, color2_2, time));
        sequence_ColorAni.Join(go_Arrow.GetComponentInChildren<SpriteRenderer>().DOColor(endColor, time));
        sequence_ColorAni.Append(com_LineRenderer.DOColor(color2_2, color2_1, time));
        sequence_ColorAni.Join(go_Arrow.GetComponentInChildren<SpriteRenderer>().DOColor(startColor, time));
        sequence_ColorAni.SetUpdate(true);
    }

    public LineRenderer RefreshMoveHintLine()
    {
        
        if (Mode.Data.HasCannotCompleteCarInMove.Get())
        {
            CLog.Info("有不能完成的箭头正在移动");
            return null;
        }
        List<Vector3> pointList = new List<Vector3>();
        pointList.Add(ArrowData.segmentDataList[0].startPos);
        pointList.Add(ArrowData.segmentDataList[0].startPos + ArrowData.moveDir * 50);
        return LineRendererUtils.CreateLineRenderer(Holder.transform, ArrowLineWidth,
            ArrowData.CheckCanCompleteMove() ?  HintMoveableColor : HintNoMoveableColor, pointList,
            CommonConst.SortingLayer_Layer1, InGameConst.LayerName_InGame_Element,
            -9);
    }
    
    private void RefreshAllBodyCollider(bool b)
    {
        foreach (var collider in bodyColliderList)
        {
            collider.enabled = b;
        }
    }

    private LineRenderer hintLineRenderer;
    public void RefreshHintLine(bool b)
    {
        if (this == null || ArrowData == null || Holder == null
            || ArrowData.segmentDataList == null || ArrowData.segmentDataList.Count == 0)
            return;
        if (ArrowData.State != EArrowState.Idle)
            return;

        if (b)
        {
            List<Vector3> pointList = new List<Vector3>();
            pointList.Add(ArrowData.segmentDataList[0].startPos);
            pointList.Add(ArrowData.segmentDataList[0].startPos + ArrowData.moveDir * 50);
            hintLineRenderer = LineRendererUtils.CreateLineRenderer(Holder.transform, ArrowEntity.ArrowLineWidth,
                ArrowData.HaveCollided ? HintColor_Collider : HintColor_UnCollider, pointList,
                CommonConst.SortingLayer_Layer1, InGameConst.LayerName_InGame_Element,
                sortingOrder: ArrowData.HaveCollided ? -1 : -11);
        }
        else
        {
            if (hintLineRenderer != null)
            {
                Object.Destroy(hintLineRenderer.gameObject);
                hintLineRenderer = null;
            }
        }
    }

    /// <summary>
    /// 被提示
    /// </summary>
    public void BeHint()
    {
        ArrowData.BeHint = true;
        var cameraInput = Mode.CameraInput;
        cameraInput.Focus(ArrowData.segmentDataList[0].startPos, () =>
        {
            // 提示动画
            // 1.复制一份箭头物体
            GameObject signGo = Object.Instantiate(Holder);
            signGo.transform.SetParent(Holder.transform, false);
            foreach (var col in signGo.GetComponentsInChildren<Collider2D>())
            {
                col.enabled = false;
            }
            signGo.GetComponent<TriggerComponent>().enabled = false;
            Object.Destroy(signGo.GetComponent<Rigidbody2D>());
            // 2.设置显示大小
            var sr1 = signGo.transform.Find("Arrow(Clone)/Root/Arrow").GetComponent<SpriteRenderer>();
            var sr2 = signGo.transform.Find("LineRenderer").GetComponent<LineRenderer>();
            float ratio = 1.5f;
            sr1.transform.localScale *= ratio;
            LineRendererUtils.SetWidth(sr2, ArrowLineWidth * ratio);
            // 3.alpha：0 - 1 -0
            float time = 0.33f;
            sequence_BeHintAni = DOTween.Sequence();
            var color_Start = sr2.startColor;
            var color_End = ColorUtils.SetColorA(color_Start, 0);
            var color2_1 = new Color2(color_Start, color_Start);
            var color2_2 = new Color2(color_End, color_End);
            sequence_BeHintAni.Append(sr1.DOFade(0, time));
            sequence_BeHintAni.Join(sr2.DOColor(color2_1, color2_2, time));
            sequence_BeHintAni.Append(sr1.DOFade(1, time));
            sequence_BeHintAni.Join(sr2.DOColor(color2_2, color2_1, time));
            sequence_BeHintAni.Append(sr1.DOFade(0, time));
            sequence_BeHintAni.Join(sr2.DOColor(color2_1, color2_2, time));
            sequence_BeHintAni.SetUpdate(true);
            sequence_BeHintAni.OnComplete(() =>
            {
                Object.Destroy(signGo);
            });
        });
        LineRendererUtils.SetColor(com_LineRenderer, MoveableColor);
        go_Arrow.GetComponentInChildren<SpriteRenderer>().color = MoveableColor;
    }



    public void DisplayAnimateLine()
    {
        coroutineHandler_LineAni = Game.GetMod<ModCoroutine>().StartCoroutine(PlayCoroAnimateLine(com_LineRenderer));
    }

    public IEnumerator PlayCoroAnimateLine(LineRenderer line)
    {
        if (line == null)
        {
            CLog.Error("LineRenderer不能为null");
            IsEnterAniOver = true;
            yield return null;
        }

        go_Arrow.transform.localScale = Vector3.zero;

        // 获取原始点
        Vector3[] originalPoints = new Vector3[line.positionCount];
        line.GetPositions(originalPoints);
        // CLog.Info($"原始点数量: {originalPoints.Length}");
        var originalNumCornerVertices = line.numCornerVertices;
        var originalNumCapVertices = line.numCapVertices;
        line.numCornerVertices = 12;
        line.numCapVertices = 10;

        // 插值生成平滑的点 每段插值不超过5个点
        Vector3[] smoothPoints = LineRendererUtils.GenerateSmoothPoints(originalPoints, LineRendererUtils.PointsPerSegment);
        // CLog.Info($"平滑后点数量: {smoothPoints.Length}");

        // 先隐藏线条 - 只显示最后一个点
        line.positionCount = 1;
        line.SetPosition(0, smoothPoints[smoothPoints.Length - 1]);

        int totalPoints = smoothPoints.Length;
        int visiblePoints = Mathf.CeilToInt(0.2f * totalPoints);
        float timer = 2f;

        while (visiblePoints < smoothPoints.Length)
        {
            if (!line)
                yield break;

            timer += Time.deltaTime * LineRendererUtils.LineAnimSpeed;
            visiblePoints = Mathf.CeilToInt(timer);
            // 限制显示范围
            visiblePoints = Mathf.Clamp(visiblePoints, 2, totalPoints);
            line.positionCount = visiblePoints;
            // 从后往前填充点（尾部到头部动画）
            for (int i = 0; i < visiblePoints; i++)
            {
                int sourceIndex = totalPoints - visiblePoints + i;
                line.SetPosition(i, smoothPoints[sourceIndex]);
            }
            yield return new WaitForEndOfFrame();
        }

        if (line)
        {
            // 动画完成后恢复原始点
            line.numCornerVertices = originalNumCornerVertices;
            line.numCapVertices = originalNumCapVertices;
            line.positionCount = originalPoints.Length;
            for (int i = 0; i < originalPoints.Length; i++)
            {
                line.SetPosition(i, originalPoints[i]);
            }

            if (go_Arrow != null)
                go_Arrow.transform.localScale = Vector3.one;
        }

        IsEnterAniOver = true;
    }

    public void OnDestroy()
    {
        Mode.Data.HasCannotCompleteCarInMove.Remove(ArrowData.segmentDataList[0].startPointIndex);
        sequence_ColorAni?.Kill(true);
        Game.GetMod<ModCoroutine>().StopCoroutine(coroutineHandler_LineAni);
        sequence_BeHintAni?.Kill(true);
        go_Arrow.transform.DOKill();
        Holder.transform.DOKill();
        Object.DestroyImmediate(Holder);
        ArrowData.OnDestroy();
    }
}

/// <summary>
/// 箭头数据
/// </summary>
public class ArrowData
{
    public ArrowEntity Entity { get; private set; } //实体

    public float totalDist;//总距离
    public List<SegmentData> segmentDataList = new();//每一段的数据（每个点之间）
    public string colorHex;

    public Vector2 moveDir => segmentDataList[0].dir;//箭头的移动方向
    public EArrowState State;//状态

    public bool HaveCollided;//碰撞过
    public bool BeHint;//被提示

    public ArrowData(ArrowEntity entity, List<int> turnPointIndexList, string colorHex)
    {
        Entity = entity;
        this.colorHex = colorHex;

        InitData(turnPointIndexList);
    }

    private void InitData(List<int> turnPointIndexList)
    {
        var mode = (Game.GetMod<ModFsm>().CurState as FsmState_InGame).Mode;

        HaveCollided = false;
        State = EArrowState.Idle;
        // 初始化每一段的数据
        for (int i = 0; i < turnPointIndexList.Count - 1; i++)
        {
            int pointIndex_Start = turnPointIndexList[i];
            int pointIndex_End = turnPointIndexList[i + 1];
            var v1 = InGameUtils.GetUnitVector2(pointIndex_Start);
            var v2 = InGameUtils.GetUnitVector2(pointIndex_End);
            var dir = (v1 - v2).normalized;
            bool isHorizontal = dir.x != 0;
            bool isPositiveDir = isHorizontal
                ? dir.x > 0
                : dir.y > 0;
            int pointCount = isHorizontal
                ? Mathf.Abs(pointIndex_End - pointIndex_Start)
                : Mathf.Abs(pointIndex_End - pointIndex_Start) / mode.Data.LevelData.wide;

            for (int j = 0; j < pointCount; j++)
            {
                SegmentData segmentData = new SegmentData();
                segmentData.startPointIndex = pointIndex_Start +
                    (isPositiveDir ? -1 : 1) * (isHorizontal ? j : j * mode.Data.LevelData.wide);
                segmentData.endPointIndex = pointIndex_Start +
                    (isPositiveDir ? -1 : 1) * (isHorizontal ? j + 1 : (j + 1) * mode.Data.LevelData.wide);
                segmentData.startPos = InGameUtils.GetRealVector2(segmentData.startPointIndex);
                if (i == turnPointIndexList.Count - 2
                    && j == pointCount - 1)
                {
                    segmentData.endPos = InGameUtils.GetRealVector2(segmentData.endPointIndex) - dir * ArrowEntity.PointSize / 2;//最后一个点不能绘制在点中心，需要绘制到线段的终点
                }
                else
                {
                    segmentData.endPos = InGameUtils.GetRealVector2(segmentData.endPointIndex);
                }
                Vector2 dir_Temp = new Vector2((int)dir.x, (int)dir.y);
                if (dir_Temp.x != 1
                    && dir_Temp.x != -1
                    && dir_Temp.y != 1
                    && dir_Temp.y != -1)
                {
                    CLog.Error($"初始化方向失败，检查逻辑！！！！！" +
                       $"箭头的头pointIndex：{turnPointIndexList[0]}，方向获取失败的pointIndex：{turnPointIndexList[i]}");
                }
                else
                {
                    segmentData.dir = dir_Temp;
                }
                segmentDataList.Add(segmentData);
                totalDist += segmentData.Dist;

                mode.Data.visiblePointIndexList.Add(segmentData.startPointIndex);
                mode.Data.visiblePointIndexList.Add(segmentData.endPointIndex);
            }
        }
        for (int i = 0; i < segmentDataList.Count; i++)
        {
            if (i == segmentDataList.Count - 1)
            {
                segmentDataList[i].nextSegmentData = null;
            }
            else
            {
                segmentDataList[i].nextSegmentData = segmentDataList[i + 1];
            }
        }
    }

    /// <summary>
    /// 检测是否能完整的移动
    /// </summary>
    public bool CheckCanCompleteMove()
    {
        Ray2D ray = new Ray2D(segmentDataList[0].startPos, segmentDataList[0].dir);
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction,
            11111, LayerMask.GetMask(InGameConst.LayerName_InGame_Element));
        if (hits.Length == 0)
            return true;
        bool ret = true;
        foreach (var item in hits)
        {
            if (item.collider == null)
                continue;
            var entity = InGameRayUtils.GetEntity(item.collider);
            if (entity == null)
                continue;
            if (entity == Entity)
                continue;
            if (entity.ArrowData.State != EArrowState.Idle)
                continue;
            ret = false;
            break;
        }
        return ret;
    }

    public void OnDestroy()
    {
        State = EArrowState.Idle;
        var mode = (Game.GetMod<ModFsm>().CurState as FsmState_InGame).Mode;
        mode.Data.RemoveArrowEntity(Entity);
        Entity = null;
        segmentDataList.Clear();
    }
}

/// <summary>
/// 每一段的数据
/// </summary>
public class SegmentData
{
    public int startPointIndex;
    public int endPointIndex;
    public Vector2 startPos;
    public Vector2 endPos;
    public Vector2 dir;
    public SegmentData nextSegmentData;

    public float Dist => Vector2.Distance(endPos, startPos);//距离
}

/// <summary>
/// 箭头状态
/// </summary>
public enum EArrowState
{
    Idle = 1,
    MoveForward,
    MoveBackward,
}

