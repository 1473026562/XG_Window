# XG_Window
ngui window for u3d;
介绍:NGUI上使用的一个窗口类;



功能说明:可以在superWindow基础上设计出各种各样的窗口,
1,可以实现点击内部关闭的窗口,
2,可以实现延时自动关闭的窗口
3,可以实现点击外部关闭的窗口,
4,可以实现点击叉号关闭的窗口

结构说明:代码结构简单易懂,面向接口编程,易维护和扩张功能;体现在:
1,动画被抽象出来WindowAnimations;可以灵活的更改和括涨,
2,窗口被抽出接口IWindow

生命周期:使用了建造者模式和模板方法模式混合来管理窗口生命周期;
1,NavigationMode.None;
2,NavigationMode.Start;
3,NavigationMode.New;
4,NavigationMode.On;
5,NavigationMode.Exit;
6,NavigationMode.End;

详情可下载代码看;
