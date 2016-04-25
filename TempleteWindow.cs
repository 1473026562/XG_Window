using UnityEngine;
using GameSystem.Data;

public class RankPowerWindow:SuperWindow
{
	protected override void NavigationTo(NavigatedMode mode)
	{
		if (mode == NavigatedMode.New)
		{
			this.prSize = new Vector2(400f, 120f);
			this.prHiddenCloseButton = true;
			this.prHiddenBackgroundShadow = true;
			this.prHiddenTitle = true;
			this.prShadowControlSwitch = true;
			this.prGlobalCloseSwitch = true;

			this.mName = JXGUIPool.CreateLabel(this.m_WindowGestrue.transform);
			this.mName.trueTypeFont = GameSystem.Data.Font.LanTingCuHei;
			this.mName.fontSize = 28;
			this.mName.pivot = UIWidget.Pivot.Left;
			this.mName.transform.localPosition = new Vector3(-20f, 0f, 0f);
			this.mName.overflowMethod = UILabel.Overflow.ResizeFreely;
			this.mName.depth = 20;
			

			
		}
	}
	//----------------------------Field---------------------------------------------
	private JXGLabel mName;
	private UIIcon mIcon;
	//---------------------------Method---------------------------------------------
	public void Set(String name)
	{
		if (name != null)
		{
			this.mName.text = name;
		}
	}
}
