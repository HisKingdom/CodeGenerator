# CodeGenerator
通过反射根据模板生成相应的代码

最好用的官方 舍不得掏钱买 角落前辈的 也不知道是啥原因不开源 用起来有问题 他也没时间改
t4的core 各种坑  vs扩展  dte  什么的 看了几天官方文档  妈蛋 没点头绪
想了想生成器的原理
怒了 大爷的  C# 字符串拼接  哥还不会么 


首先在MetaTableInfo这个类里面把SourceAssembly这个常量更改成你的项目下的程序集的绝对路径
然后CodeGeneratorHelper这个类里面的三个常量 你看着改啦  注释都有啊
