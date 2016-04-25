using UnityEngine;
using System.Collections;
using GameSystem;
using GameSystem.Data;
using GameSystem.IEvent;
using GameSystem.NetClient;
using GameSystem.View;
using GameSystem.Control;
using System.Collections.Generic;
using System;

public enum WindowAnimationStyle
{
	Default,
	Normal
}
public class WindowAnimation : MonoBehaviour
{
	public void ChangeStyle(SuperWindow window, WindowAnimationStyle style)
	{
		switch (style)
		{
		case WindowAnimationStyle.Default:
			this.IntoAni = window.Get_ComponentT<DefaultWinAniShow>();
			this.OutAni = window.Get_ComponentT<NormalWinAniDis>();
			break;
		case WindowAnimationStyle.Normal:
			this.IntoAni = window.Get_ComponentT<NormalWinAniShow>();
			this.OutAni = window.Get_ComponentT<NormalWinAniDis>();
			break;
		default:
			this.IntoAni = window.Get_ComponentT<DefaultWinAniShow>();
			this.OutAni = window.Get_ComponentT<NormalWinAniDis>();
			break;
		}
	}
	private WinAniShow IntoAni;
	private WinAniDis OutAni;
	
	public void Show(SuperWindow window, Action loadedAction)
	{
		if (this.IntoAni == null)
		{
			this.IntoAni = window.Get_ComponentT<DefaultWinAniShow>();
		}
		this.IntoAni.Show(window, loadedAction);
	}
	public void Disappear(SuperWindow window, Action exitAction)
	{
		if (this.OutAni == null)
		{
			this.OutAni = window.Get_ComponentT<NormalWinAniDis>();
		}
		this.OutAni.Disappear(window, exitAction);
	}
}

public abstract class WinAniShow : MonoBehaviour
{
	public abstract void Show(SuperWindow window, Action loadedAction);
}
public abstract class WinAniDis : MonoBehaviour
{
	public abstract void Disappear(SuperWindow window, Action exitAction);
}

#region NormalWinAniShow
public class NormalWinAniShow : WinAniShow
{
	public override void Show(SuperWindow window,Action loadedAction)
	{
		UIPanel panel = window.AniObj.Get_ComponentT<UIPanel>();
		panel.alpha = 0.01f;
		
		Keyframe[] keys = new Keyframe[]
		{
			new Keyframe(){ time = 0f, value = 0f, inTangent = 1.33f, outTangent = 1.33f, tangentMode = 10 },
			new Keyframe(){ time = 0.8f, value = 1.1f, inTangent = 0f, outTangent = 0f, tangentMode = 0 },
			new Keyframe(){ time = 1f, value = 1f, inTangent = -0.6666669f, outTangent = -0.6666669f, tangentMode = 10 },
		};
		AnimationCurve u3dCurve = new AnimationCurve(keys);
		
		HappyTransformFloatU3DCurve curve = new HappyTransformFloatU3DCurve();
		curve.Init(0f, 1f, 0.25f, u3dCurve);
		Action<float> setValue = delegate(float value)
		{
			this.transform.localScale = new Vector3(value, value, 1f);
			panel.alpha = value;
		};
		curve.AttachObject = this;
		curve.AddSetDelegate(setValue);
		curve.AddCallBack(loadedAction);
		curve.MakeItAlive();
		EventManger.DisableSomeTime(0.25f);
	}
}
#endregion


#region NormalWinAniDis
public class NormalWinAniDis : WinAniDis
{
	public override void Disappear(SuperWindow window, Action exitAction)
	{
		window.Shadow.Get_ComponentT<UIPanel>().alpha = 0f;
		Keyframe[] keys = new Keyframe[]
		{
			new Keyframe(){ time = 0f, value = 1f, inTangent = -0.6666669f, outTangent = -0.6666669f, tangentMode = 10 },
			new Keyframe(){ time = 1f, value = 0f, inTangent = 1.33f, outTangent = 1.33f, tangentMode = 10 },
		};
		AnimationCurve u3dCurve = new AnimationCurve(keys);
		
		HappyTransformFloatU3DCurve curve = new HappyTransformFloatU3DCurve();
		curve.Init(0f, 1f, .2f, u3dCurve);
		Action<float> setValue = delegate(float value)
		{
			if (value != 0)
			{
				window.Shadow.transform.localScale = new Vector3(1 / value, 1 / value, 1f);
			}
			if (value >= 0)
			{
				this.transform.localScale = new Vector3(value, value, 1f);
			}
		};
		curve.AttachObject = this;
		curve.AddSetDelegate(setValue);
		curve.AddCallBack(
			delegate
			{
			exitAction.CheckAndRun();
			this.gameObject.SetActive(false);
		});
		curve.MakeItAlive();
		EventManger.DisableSomeTime(0.4f);
	}
}
#endregion


#region DefaultWinAniShow
public class DefaultWinAniShow : WinAniShow
{
	public override void Show(SuperWindow window,Action loadedAction)
	{
		UIPanel panel = window.AniObj.Get_ComponentT<UIPanel>();
		panel.alpha = 0.01f;
		
		Keyframe[] keys = new Keyframe[]
		{
			new Keyframe(){ time = 0f, value = 0f, inTangent = 1.33f, outTangent = 1.33f, tangentMode = 10 },
			new Keyframe(){ time = 0.8f, value = 1.1f, inTangent = 0f, outTangent = 0f, tangentMode = 0 },
			new Keyframe(){ time = 1f, value = 1f, inTangent = -0.6666669f, outTangent = -0.6666669f, tangentMode = 10 },
		};
		AnimationCurve u3dCurve = new AnimationCurve(keys);
		
		HappyTransformFloatU3DCurve curve = new HappyTransformFloatU3DCurve();
		curve.Init(0f, 1f, 0.25f, u3dCurve);
		Action<float> setValue = delegate(float value)
		{
			this.transform.localScale = new Vector3(value, value, 1f);
			panel.alpha = value;
		};
		curve.AttachObject = this;
		curve.AddSetDelegate(setValue);
		curve.AddCallBack(loadedAction);
		curve.MakeItAlive();
		EventManger.DisableSomeTime(0.25f);
	}
}
#endregion



#region Bak
//public void Show(SuperWindow window)
//{
//    GameSystem.Control.Animation.NextFrameCall(() =>
//    {
//        window.AniObj.Get_ComponentT<UIPanel>().alpha = 1f;
//        Keyframe[] keys = new Keyframe[]
//    {
//        new Keyframe(){ time = 0f, value = 0f, inTangent = 1.33f, outTangent = 1.33f, tangentMode = 10 },
//        new Keyframe(){ time = 1f, value = 1f, inTangent = -0.6666669f, outTangent = -0.6666669f, tangentMode = 10 },
//    };
//        AnimationCurve u3dCurve = new AnimationCurve(keys);

//        HappyTransformFloatU3DCurve curve = new HappyTransformFloatU3DCurve();
//        curve.Init(0f, 1f, 0.2f, u3dCurve);
//        Action<float> setValue = delegate(float value)
//        {
//            if (value != 0)
//            {
//                window.Shadow.transform.localScale = new Vector3(1 / value, 1 / value, 1f);
//            }
//            this.transform.localScale = new Vector3(value, value, 1f);
//        };
//        curve.AttachObject = this;
//        curve.AddSetDelegate(setValue);
//        curve.MakeItAlive();
//        EventManger.DisableSomeTime(0.6f);
//    });
//}


//public void Disappear(SuperWindow window, Action exitAction)
//{
//    window.Shadow.Get_ComponentT<UIPanel>().alpha = 0f;
//    Keyframe[] keys = new Keyframe[]
//    {
//        new Keyframe(){ time = 0f, value = 1f, inTangent = -0.6666669f, outTangent = -0.6666669f, tangentMode = 10 },
//        new Keyframe(){ time = 1f, value = 0f, inTangent = 1.33f, outTangent = 1.33f, tangentMode = 10 },
//    };
//    AnimationCurve u3dCurve = new AnimationCurve(keys);

//    HappyTransformFloatU3DCurve curve = new HappyTransformFloatU3DCurve();
//    curve.Init(0f, 1f, .2f, u3dCurve);
//    Action<float> setValue = delegate(float value)
//    {
//        if (value != 0)
//        {
//            window.Shadow.transform.localScale = new Vector3(1 / value, 1 / value, 1f);
//        }
//        this.transform.localScale = new Vector3(value, value, 1f);
//    };
//    curve.AttachObject = this;
//    curve.AddSetDelegate(setValue);
//    curve.AddCallBack(
//        delegate
//        {
//            exitAction.CheckAndRun();
//            this.gameObject.SetActive(false);
//        });
//    curve.MakeItAlive();
//    EventManger.DisableSomeTime(0.6f);
//} 
#endregion