﻿using System;
using System.Collections.Generic;
using Aqua.InputEvent;
using Core.Unit;
using UnityEngine;


public class ActionInterrupt
{

    private string mInterruptName;
    /// <summary>
    /// 中断名称
    /// </summary>
    public string InterruptName { get { return mInterruptName; } set { mInterruptName = value; } }


    private string mActionID = "N0000";


    /// <summary>
    /// 动作编号
    /// </summary>
    public string ActionID { get { return mActionID; } set { mActionID = value; } }


    private ActionInterruptConnectMode mConnectMode;
    /// <summary>
    /// 中断过度连接方式
    /// </summary>
    public ActionInterruptConnectMode ConnectMode { get { return mConnectMode; } set { mConnectMode = value; } }


    private int mDetectionStartTime;
    /// <summary>
    /// 中断检测起始时间()
    /// </summary>
    public int DetectionStartTime
    {
        get { return mDetectionStartTime; }
        set { mDetectionStartTime = value; }
    }


    private int mDetectionEndTime;
    /// <summary>
    /// 中断检测结束时间()
    /// </summary>
    public int DetectionEndTime
    {
        get { return mDetectionEndTime; }
        set { mDetectionEndTime = value; }
    }


    private int _ConnectTime;
    /// <summary>
    /// 连接时间
    /// </summary>
    public int ConnectTime
    {
        get { return _ConnectTime; }
        set { _ConnectTime = value; }
    }


    private ConditionType mConditionType;
    /// <summary>
    /// 条件出发类型
    /// </summary>
    public ConditionType ConditionType
    {
        get { return mConditionType; }
        set { mConditionType = value; }
    }


    private List<IInterruptCondition> mConditions = new List<IInterruptCondition>();
    /// <summary>
    /// 条件列表
    /// </summary>
    public List<IInterruptCondition> Conditions
    {
        get { return mConditions; }
        set { mConditions = value; }
    }




    //public List<ActionInterrupt> ActionInterrupts { get { return mActionInterrupts; } }


    //private int mConnectTime = 100;
    ///// <summary>
    ///// 过度连接时间
    ///// </summary>
    //public int ConnectTime { get { return mConnectTime; } set { mConnectTime = value; }}


    //private bool mEnabledButton;
    ///// <summary>
    ///// 是否启用按钮条件
    ///// </summary>
    //public bool EnabledButton { get { return mEnabledButton; } set { mEnabledButton = value; } }


    //private InterruptButtonData[] mButtonCondition;
    ///// <summary>
    ///// 按钮条件
    ///// </summary>
    //public InterruptButtonData[] ButtonCondition { get { return mButtonCondition; } set { } }



}



/// <summary>
/// 中断过度连接方式
/// </summary>

public enum ActionInterruptConnectMode
{
    Immediately,
    WaitFinish,
}


public enum ConditionType
{
    Or,
    And,
}


//public enum CompareType
//{
//    EQUALS,
//    SMALLER,
//    LARGER,
//    NOT_EQUALS,
//}



public interface IInterruptCondition
{
    bool Check(UnitBase unitBase);
}



public class InputCondition : IInterruptCondition
{

    private readonly GameInputType _inputCode;
    private readonly InputState _inputState;


    public InputCondition(InputState state, GameInputType code)
    {
        _inputState = state;
        _inputCode = code;
    }


    public bool Check(UnitBase unitBase)
    {
        if (InputManager.Instance.IsControllable(unitBase ) == false)
        {
            return false;
        }

        var inputSource = InputManager.Instance.GetKeycodeState(_inputCode);
        if (inputSource != null)
        {
            switch (_inputState)
            {
                case InputState.KeyDown:
                    return inputSource.IsDown;
                case InputState.KeyPressing:
                    return inputSource.IsPress;
                case InputState.KeyDoubleClick:
                    break;
                case InputState.KeyRelease:
                    return inputSource.IsUp;
                default:
                    return false;
            }
        }

#if UNITY_EDITOR
        Debug.Log(string.Format("输入类型 [{0}] 不存在!", _inputCode));
#endif
        return false;
    }
}


//public class BoolCondition : IInterruptCondition
//{
//    private string _Key;
//    private bool _ExpectValue;

//    public BoolCondition(string key ,bool expectValue)
//    {
//        _Key = key;
//        _ExpectValue = expectValue;
//    }

//    public bool Check(Dictionary<string, int> intValues)
//    {
//        if (intValues.ContainsKey(_Key))
//        {
//            bool value = intValues[_Key] != 0;
//            return _ExpectValue == value;
//        }

//        return false;
//    }
//}


//public class IntCondition : IInterruptCondition
//{
//    private string _Key;
//    private int _ExpectValue;
//    private CompareType _CompareType;

//    public bool Check(Dictionary<string, int> intValues)
//    {
//        if (intValues.ContainsKey(_Key))
//        {
//            int value = intValues[_Key];
//            switch (_CompareType)
//            {
//                case CompareType.EQUALS:
//                    return _ExpectValue == value;
//                case CompareType.LARGER:
//                    return value > _ExpectValue;
//                case CompareType.NOT_EQUALS:
//                    return value != _ExpectValue;
//                case CompareType.SMALLER:
//                    return value < _ExpectValue;
//            }
//        }

//        return false;
//    }
//}


//}

