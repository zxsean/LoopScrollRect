# LoopScrollRect  
基于https://github.com/qiankanglai/LoopScrollRect  
简化了LoopScrollPrefabSource的操作,同时增加了2个回调接口,方便日常使用.  
m_provideDataCallBack,等同于原版的item初始化流程,不过不需要单独在item上挂载脚本.  
m_clickItemCallBack,点击回调.  

## 更新

1.0.1 2020-8-28  
[+]添加一个item入场动画,默认开启.  
[*]合并主干版本代码.  
[+]添加Refill使用offset参数后,如果offset为最后一页的index时,没法一页填充满.  
[+]添加一个修正offset值的方法,会自动修正index的值,避免出现item被吞了的情况(原版有警告).  
[+]添加一个接口,用于默认实现go回收事件.  