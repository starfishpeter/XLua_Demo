//------------------------------------------------------------
// 脚本名:		Singleton.cs
// 作者:			海星
// 描述:			线程安全 且不继承Mono
//------------------------------------------------------------

using System;

namespace StarFramework.Runtime
{
    public abstract class Singleton<T> where T : class
    {
        class Nested
        {
            //嵌套类 延迟加载来确保线程的安全
            //延迟加载的意思是：只有第一次访问Instance属性的时候才会创建实例
            //前提 你要的这个单例是无参的公共构造函数
            //这个API有性能开销 但应该还好
            internal static readonly T instance = Activator.CreateInstance(typeof(T), true) as T;
        }

        public static T Instance => Nested.instance;
    }
}
