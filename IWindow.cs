
using UnityEngine;
using System;
public enum NavigatedMode
{
	Empty = 0,      //无实例
	Start = 1,        //刚创建父物体及各变量
	New = 2,   //父类Init完毕
	On = 3,    //进入运行时:注意子类的所有方法的调用都是在RunTime后才会调用的;包括Init()Set();
	Exit = 4,   //准备退出
	End = 5        //已退出
}
//目前只作为Window父类的流程控制接口;可以控制3种窗口的流程;
//1:构造方法;状态改为Start并进入Init
//2:Init完毕:状态改为New
//3:执行Show动画完毕:状态改为On
//4:按下关闭:状态转为Exit
//5:关闭动画结束:状态转为End
public interface IWindow
{
	void OutProcessController();
	void InProcessController();
	void InitWindow();
	void WindowShow(Action action);
	void WindowDisappear(Action action);
	GameObject AniObj { get; }
	GameObject Shadow { get; }
}