//------------------------------------------------------------
// 脚本名:		LuaManager.cs
// 作者:		    海星
// 描述:		    基于xlua的Lua管理器
//------------------------------------------------------------

using UnityEngine;
using XLua;
using System.IO;

namespace StarFramework.Runtime
{
	public class LuaManager : SingletonAutoMono<LuaManager>
	{
		//Lua解释器 保证唯一
	    private LuaEnv luaEnv;

		//大G表
		public LuaTable Global
		{
			get
			{
				return luaEnv.Global;
			}
		}

		/// <summary>
		/// 初始化
		/// </summary>
		public void Init()
		{
			if (luaEnv != null)
				return;

			//初始化
			luaEnv = new LuaEnv();

			//加载lua脚本 重定向
			//一旦添加过后 解释器会自己从所有添加过的方法里找
			luaEnv.AddLoader(CustomLoader);
			luaEnv.AddLoader(CustomAALoader);
		}

		/// <summary>
		/// 从路径自定义加载
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		private byte[] CustomLoader(ref string fileName)
		{
			string path = Application.dataPath + "/Lua/" + fileName + ".lua";

			if (File.Exists(path))
			{
				return File.ReadAllBytes(path);
			}
			else
			{
				Debug.Log("CustomLoader未找到文件名为" + fileName + "的文件");
			}

			return null;
		}


		private byte[] CustomAALoader(ref string fileAAKey)
		{
			byte[] bytes = null;
			string fileName = fileAAKey;

			LoadManager.Instance.LoadAsset<TextAsset>(fileAAKey,(luaFile)=>{
				if(luaFile != null)
				{
					bytes = luaFile.bytes;
				}
				else
				{
					Debug.LogError("CustomAALoader未找到AAKey为" + fileName + "的文件");
				}
			});
			return bytes;
		}

		public void ExcuteLuaFile(string fileName)
		{
			string str = string.Format("require('{0}')", fileName);
			DoString(str);
		}

		private void DoString(string str)
		{
			if(luaEnv == null)
			{
				Init();
			}
			
			luaEnv.DoString(str);
		}

		public void Tick()
		{
			if (luaEnv == null)
			{
				Debug.LogWarning("不存在运行的Lua解释器，不释放内存");
				return;
			}
			luaEnv.Tick();
		}

		public void Dispose()
		{
			if (luaEnv == null)
			{
				Debug.LogWarning("不存在运行的Lua解释器，不回收实例");
				return;
			}
			luaEnv.Dispose();
			luaEnv = null;
		}
	}
}
