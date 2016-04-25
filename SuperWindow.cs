using UnityEngine;
using System;

/// <summary>
/// 说明:
/// 1:继承本类使用;
/// 2:开放属性以pr开头
/// 3:对内容开放;
///     a:但内容添加到mWindowGestrue子物体
///     b:按钮等操作级控件添加到mControl子物体
/// </summary>
public class SuperWindow : MonoBehaviour, IWindow
{
	void Awake() 
	{
		this.InProcessController();
	}
	
	public void InitWindow()
	{
		this.windowAnimation = this.Get_ComponentT<WindowAnimation>();
		this.m_Obj = new GameObject();
		this.m_Obj.Get_ComponentT<UIPanel>().depth = 1;
		this.AddChildRelative_Child(this.m_Obj);
		this.transform.localPosition = Vector3.zero;
		
		//窗口的按钮等控件
		this.m_Control = new GameObject();
		this.m_Control.Get_ComponentT<UIPanel>().depth = 299;
		this.m_Obj.AddChildRelative_Child(this.m_Control);
		this.m_Control.transform.localPosition = Vector3.zero;
		
		//窗口外界部分
		this.m_Shadow = new GameObject();
		this.m_Shadow.Get_ComponentT<UIPanel>().depth = 280;
		this.AddChildRelative_Child(this.m_Shadow);
		this.m_Shadow.transform.localPosition = Vector3.zero;
		
		#region 窗口主体部分
		
		this.m_WindowMain = new GameObject();
		this.m_Obj.AddChildRelative_Child(this.m_WindowMain);
		this.m_WindowMain.Get_ComponentT<UIPanel>().depth = 290;
		
		//窗口内容触控区
		this.m_WindowGestrue = new GameObject();
		this.m_WindowMain.AddChildRelative_Child(this.m_WindowGestrue);
		this.m_WindowGestrue.Get_ComponentT<BoxCollider>().Set_Center_Size(new Vector3(0f, 0f, 0f), new Vector3(520f, 400f, 0f));
		//this.m_WindowGestrue.Get_ComponentT<UIPanel>().depth = 290;
		this.m_WindowGestrue.Get_ComponentT<JXGNoneGesture>();
		
		//窗口框
		this.m_WindowBackground = JXGUIPool.CreateSprite(this.m_WindowGestrue.transform);
		this.m_WindowBackground.Set_Atlas_SpriteName(Atlas.GeneralAtlas, "airmessge_back");
		this.m_WindowBackground.Set_Size(520f, 400f);
		this.m_WindowBackground.type = UIBasicSprite.Type.Sliced;
		this.m_WindowBackground.transform.localPosition = new Vector3(0f, 0f, 0f);
		this.m_WindowBackground.depth = Depth + 5;
		
		#endregion
		
		//标题
		this.m_WindowTitle = JXGUIPool.CreateLabel(this.m_Control.transform);
		this.m_WindowTitle.trueTypeFont = GameSystem.Data.Font.LanTingCuHei;
		this.m_WindowTitle.fontSize = 28;
		this.m_WindowTitle.effectStyle = UILabel.Effect.Shadow;
		this.m_WindowTitle.effectDistance = new Vector2(2f, 2f);
		this.m_WindowTitle.pivot = UIWidget.Pivot.Center;
		this.m_WindowTitle.transform.localPosition = new Vector3(0f, this.m_WindowBackground.height * 0.5f + 20f, 0f);
		this.m_WindowTitle.overflowMethod = UILabel.Overflow.ResizeFreely;
		this.m_WindowTitle.depth = Depth + 9;
		this.m_WindowTitle.text = "窗口标题";
		//关闭按钮
		this.m_CloseButton = ComponentCreator.Create<JXGImageButton>(this.m_Control.transform);
		this.m_CloseButton.Init(Atlas.GeneralAtlas, "window_close", "window_close_onclick");
		this.m_CloseButton.Background.MakePixelPerfect();
		this.m_CloseButton.Background.type = UISprite.Type.Sliced;
		this.m_CloseButton.transform.localPosition = new Vector3(this.m_WindowBackground.width * 0.5f, this.m_WindowBackground.height * 0.5f, 0f);
		this.m_CloseButton.depth = Depth + 10;
		this.m_CloseButton.ClickedAction = (btn) => { this.WillExitClick(); };
		//标题背景
		this.m_WindowTitleBackground = JXGUIPool.CreateSprite(this.m_Control.transform);
		this.m_WindowTitleBackground.Set_Atlas_SpriteName(Atlas.GeneralAtlas, "reward_title_ground");
		this.m_WindowTitleBackground.Set_Size(this.m_WindowTitleBackground.width, this.m_WindowTitle.fontSize + 10);
		this.m_WindowTitleBackground.transform.localPosition = this.m_WindowTitle.transform.localPosition + new Vector3(0f, -2f, 0f);
		this.m_WindowTitleBackground.type = UIBasicSprite.Type.Sliced;
		this.m_WindowTitleBackground.depth = Depth + 8;
		
		//点击外界关闭
		this.BackgroundShadow = JXGUIPool.CreateSprite(this.m_Shadow.transform);
		this.BackgroundShadow.atlas = Atlas.GeneralAtlas;
		this.BackgroundShadow.spriteName = "blank";
		this.BackgroundShadow.type = UISprite.Type.Sliced;
		this.BackgroundShadow.Set_Size(10000, 10000);
		this.BackgroundShadow.color = new Color(0f, 0f, 0f, 0.6f);
		
		JXGTouchUpInside.SetEvent(this.BackgroundShadow, this.WillExitClick);
		this.BackgroundShadow.Get_ComponentT<BoxCollider>().Set_Center_Size(new Vector3(0f, 0f, 0f), new Vector3(10000f, 10000f, 0f));
		
		#if UNITY_EDITOR
		this.m_WindowTitle.name = "m_WindowTitle";
		this.m_WindowTitleBackground.name = "m_WindowTitleBackground";
		this.m_CloseButton.name = "m_CloseButton";
		this.m_WindowBackground.name = "m_WindowBackground";
		this.m_Control.name = "m_Control";
		this.m_WindowGestrue.name = "m_WindowGestrue";
		this.m_Shadow.name = "m_Shadow";
		this.m_WindowMain.name = "MainContent";
		this.m_Obj.name = "mObj";
		#endif
	}
	
	#region 成员区
	/////////////////////////////////成员区///////////////////////////////////////////////////////////////////////////////////////////////// 
	private GameObject m_Obj;                           //除Shadow外的父物体
	protected GameObject m_Control;                     //按钮等操作控件父
	protected GameObject m_WindowGestrue;               //窗口触控拦截区
	protected GameObject m_Shadow;                      //外界部分
	private JXGLabel m_WindowTitle;                   //窗口标题
	private JXGSprite m_WindowTitleBackground;        //窗口标题背景
	protected JXGImageButton m_CloseButton;           //关闭
	private GameObject m_WindowMain;                    //窗口主体
	private JXGSprite m_WindowBackground;             //窗口主背景
	protected JXGSprite BackgroundShadow;
	private WindowAnimation windowAnimation;            //动画
	#endregion
	
	////////////////////////////////属性区/////////////////////////////////////////////////////////////////////////////////////////////////////////
	#region 属性区
	/// <summary>
	/// 窗口背景图尺寸
	/// </summary>
	public Vector2 prWindowBack_Size                    //窗口背景图尺寸
	{
		set
		{
			if (this.m_WindowBackground != null)
			{
				this.m_WindowBackground.Set_Size(value.x, value.y);
			}
		}
	}
	/// <summary>
	/// 背景图
	/// </summary>
	public JXGSprite prWindowBackground
	{
		get { return m_WindowBackground; }
	}
	/// <summary>
	/// 关闭按钮自动布局
	/// </summary>
	public bool prCloseButtonAutoLayout { get; set; }
	/// <summary>
	/// 尺寸
	/// </summary>
	public Vector2 prSize
	{
		set
		{
			if (this.m_WindowBackground != null)
			{
				this.m_WindowBackground.Set_Size(value.x, value.y);
			}
			if (!this.prglobalCloseSwitch && this.m_WindowGestrue != null)
			{
				this.m_WindowGestrue.Get_ComponentT<BoxCollider>().size = new Vector3(value.x, value.y, 0f);
			}
			if (this.prCloseButtonAutoLayout)
			{
				this.m_CloseButton.transform.localPosition = new Vector3(value.x * 0.5f, value.y * 0.5f, 0f);
			}
		}
		get 
		{
			if (this.m_WindowBackground != null)
			{
				return this.m_WindowBackground.localSize;
			}
			return Vector2.zero;
		}
	}
	/// <summary>
	/// 关闭按钮开关
	/// </summary>
	public bool prHiddenCloseButton                     //关闭按钮_开关;
	{
		set { if (this.m_CloseButton != null)this.m_CloseButton.gameObject.SetActive(!value); }
	}
	/// <summary>
	/// 标题开关
	/// </summary>
	public bool prHiddenTitle                           //标题_开关;
	{
		set
		{
			if (this.m_WindowTitle != null) this.m_WindowTitle.SetActive(!value);
			if (this.m_WindowTitleBackground != null) this.m_WindowTitleBackground.SetActive(!value);
		}
	}
	/// <summary>
	/// 窗口外框开关
	/// </summary>
	public bool prHiddenWindowBack                      //窗口框_开关
	{
		set { if (this.m_WindowBackground != null) this.m_WindowBackground.alpha = value ? 0 : 1; }
	}
	/// <summary>
	/// 标题字号
	/// </summary>
	public int prTitle_FontSize                         //标题_字号
	{
		get { if (this.m_WindowTitle != null) return this.m_WindowTitle.fontSize; return 0; }
		set
		{
			if (this.m_WindowTitle != null)
			{
				this.m_WindowTitle.fontSize = value;
			}
		}
	}
	/// <summary>
	/// 标题尺寸
	/// </summary>
	public Vector2 prTitleBack_Size                     //标题_尺寸
	{
		get { if (this.m_WindowTitleBackground != null) return this.m_WindowTitleBackground.localSize; return Vector2.zero; }
		set { if (this.m_WindowTitleBackground != null) this.m_WindowTitleBackground.Set_Size(value.x, value.y); }
	}
	/// <summary>
	/// 标题位置
	/// </summary>
	public Vector3 prTitle_Pos                          //标题_位置;
	{
		get { if (m_WindowTitle != null) return m_WindowTitle.transform.localPosition; else return Vector3.zero; }
		set { if (m_WindowTitle != null) m_WindowTitle.transform.localPosition = value; }
	}
	/// <summary>
	/// 标题背景位置
	/// </summary>
	public Vector3 prTitleBack_Pos                      //标题背景_位置;
	{
		get { if (m_WindowTitleBackground != null) return m_WindowTitleBackground.transform.localPosition; else return Vector3.zero; }
		set { if (m_WindowTitleBackground != null) m_WindowTitleBackground.transform.localPosition = value; }
	}
	/// <summary>
	/// 标题背景宽度
	/// </summary>
	public int prTitleBack_Weight                       //标题背景_宽度
	{
		get { if (m_WindowTitleBackground != null) return m_WindowTitleBackground.width; else return 0; }
		set { if (m_WindowTitleBackground != null) m_WindowTitleBackground.width = value; }
	}
	/// <summary>
	/// 标题背景高度
	/// </summary>
	public int prTitleBack_Height                       //标题背景_高度
	{
		get { if (m_WindowTitleBackground != null) return m_WindowTitleBackground.height; else return 0; }
		set { if (m_WindowTitleBackground != null) m_WindowTitleBackground.height = value; }
	}
	/// <summary>
	/// 标题背景开关
	/// </summary>
	public bool prHiddenTitleBack                       //标题背景_开关
	{
		set { if (m_WindowTitleBackground != null) m_WindowTitleBackground.SetActive(!value); }
	}
	/// <summary>
	/// 标题
	/// </summary>
	public JXGLabel prTitle                           //标题
	{
		get { if (this.m_WindowTitle == null) this.m_WindowTitle = JXGUIPool.CreateLabel(this.m_Control.transform); return this.m_WindowTitle; }
	}
	public string prTitle_Content                       //标题_Content
	{
		get { if (m_WindowTitle != null) return m_WindowTitle.text; else return ""; }
		set { if (m_WindowTitle != null) m_WindowTitle.text = value; }
	}
	public Vector3 prWindowBack_Pos                     //窗口框_位置;
	{
		get { if (m_WindowBackground != null) return m_WindowBackground.transform.localPosition; else return Vector3.zero; }
		set { if (m_WindowBackground != null) m_WindowBackground.transform.localPosition = value; }
	}
	public Vector3 prWindow_Pos                         //窗口位置
	{
		get { if (m_WindowGestrue != null) return m_WindowGestrue.transform.localPosition; else return Vector3.zero; }
		set { if (m_WindowGestrue != null) m_WindowGestrue.transform.localPosition = value; }
	}
	/// <summary>
	/// 全局关闭开关
	/// </summary>
	public bool prGlobalCloseSwitch                     //全局关闭开关
	{
		set { if (m_WindowGestrue != null) m_WindowGestrue.Get_ComponentT<BoxCollider>().size = value ? Vector3.zero : new Vector3(this.m_WindowBackground.width, this.m_WindowBackground.height, 0f); this.prglobalCloseSwitch = value; }
	}
	private bool prglobalCloseSwitch = false;
	/// <summary>
	/// 关闭按钮位置
	/// </summary>
	public Vector3 prCloseButton_Pos                    //关闭按钮_位置;
	{
		get { if (m_CloseButton != null) return m_CloseButton.transform.localPosition; else return Vector3.zero; }
		set { if (m_CloseButton != null) m_CloseButton.transform.localPosition = value; }
	}
	private int depth = 1;
	public int Depth                                    //层级设置;
	{
		get { return depth; }
		set 
		{
			depth = value;
			if(this.m_Shadow!=null)  this.m_Shadow.Get_ComponentT<UIPanel>().depth = value ;
			if (this.m_WindowMain != null) this.m_WindowMain.Get_ComponentT<UIPanel>().depth = value + 10;
			if (this.m_Control != null) this.m_Control.Get_ComponentT<UIPanel>().depth = value + 19;
		}
	}
	public bool prHiddenBackgroundShadow                //外框阴影窗口_开关
	{
		set { this.BackgroundShadow.alpha = value ? 0.01f : 1f; }
	}
	public bool prShadowControlSwitch                   //外框关闭窗口_开关
	{
		set
		{
			if (value)
			{
				JXGTouchUpInside.SetEvent(this.BackgroundShadow, WillExitClick);
			}
			else
			{
				JXGTouchUpInside.SetEvent(this.BackgroundShadow, null);
			}
		}
	}
	protected bool prAnimationSwitch = true;            //窗口动画开关
	private Action delegateExitAction;//退出回调;
	private Action delegateLoadedAction;//进入回调;
	/// <summary>
	/// 截入动画完毕时调用;
	/// </summary>
	protected Action DelegateLoadedAction
	{
		set { delegateLoadedAction = value; }
	}
	/// <summary>
	/// 退出动画结束后调用;
	/// </summary>
	protected Action DelegateExitAction
	{
		set { delegateExitAction = value; }
	}
	#endregion
	////////////////////////////////////////////////////Method////////////////////////////////////////////////////////
	/// <summary>
	/// 载入流程;
	/// </summary>
	public void InProcessController()
	{
		this.ChangeNaviModeToNextState();
		this.InitWindow();
		//New->InitOver
		this.ChangeNaviModeToNextState();
		this.WindowShow(delegate
		                {
			//InitOver->RunTime
			this.ChangeNaviModeToNextState();
			this.delegateLoadedAction.CheckAndRun();
		});
	}
	/// <summary>
	/// 退出流程;
	/// </summary>
	public void OutProcessController()
	{
		//RunTime->WillExit
		this.ChangeNaviModeToNextState();
		this.WindowDisappear(delegate
		                     {
			//WillExit->Exit
			this.ChangeNaviModeToNextState();
			this.delegateExitAction.CheckAndRun();
			this.WindowEnd();
		}); 
	}
	/// <summary>
	/// 载入动画开始;
	/// </summary>
	/// <param name="loadedAction"></param>
	public void WindowShow(Action loadedAction)
	{
		if (prAnimationSwitch)
		{
			if (this.windowAnimation != null)
			{
				this.windowAnimation.Show(this, loadedAction);
				return;
			}
		}
		loadedAction.CheckAndRun();
	}
	/// <summary>
	/// 退出动画开始;
	/// </summary>
	/// <param name="exitAction"></param>
	public void WindowDisappear(Action exitAction)
	{
		if (prAnimationSwitch)
		{
			if (this.windowAnimation != null)
			{
				this.windowAnimation.Disappear(this, exitAction);
				return;
			}
		}
		exitAction.CheckAndRun();
	}
	/// <summary>
	/// 状态控制的统一切换方法;
	/// </summary>
	private void ChangeNaviModeToNextState()
	{
		this.NavigatedTo();
		this.NavigationTo(this.NaviMode);
	}
	/// <summary>
	/// 统一出口,所有关闭窗口必须从此处调用;
	/// </summary>
	protected void WillExitClick()
	{
		this.OutProcessController();
	}
	protected void WillExitClick(Action endAction)
	{
		this.delegateExitAction = endAction;
		this.OutProcessController();
	}
	/// <summary>
	/// 销毁(关闭最后一步)
	/// </summary>
	private void WindowEnd()
	{
		//YouCanCollectToCachePool;
		Destroy(this.gameObject);
	}
	private NavigatedMode naviMode = NavigatedMode.Empty;
	public NavigatedMode NaviMode
	{
		get { return naviMode; }
	}
	protected virtual void NavigationTo(NavigatedMode mode) { }
	private void NavigatedTo()
	{
		//任何时刻都有可能调用WillExit;所以这里用Switch;
		//Start;载入前
		//New;  初始化完毕
		//On;   载入动画完毕
		//Exit; 将要退出->执行动画
		//End;  退出完毕
		switch (this.naviMode)
		{
		case NavigatedMode.Empty:
			this.naviMode = NavigatedMode.Start;
			EventManger.DisableSomeTime(3f);
			break;
		case NavigatedMode.Start:
			this.naviMode = NavigatedMode.New;
			break;
		case NavigatedMode.New:
			this.naviMode = NavigatedMode.On;
			EventManger.ResetDisableTime(0f);
			break;
		case NavigatedMode.On:
			this.naviMode = NavigatedMode.Exit;
			break;
		case NavigatedMode.Exit:
			this.naviMode = NavigatedMode.End;
			break;
		case NavigatedMode.End:
			this.naviMode = NavigatedMode.Empty;
			break;
		}
	}
	
	public GameObject AniObj
	{
		get { return this.m_Obj; }
	}
	
	public GameObject Shadow
	{
		get { return this.m_Shadow; }
	}
}
