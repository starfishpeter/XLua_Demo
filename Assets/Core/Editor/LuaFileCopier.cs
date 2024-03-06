//------------------------------------------------------------
// 脚本名:		LuaFileCopier.cs
// 作者:		海星
// 描述:		#Lua文件快速复制成txt
//------------------------------------------------------------

using UnityEditor;
using System.IO;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;

namespace StarFramework.Editor
{
	public class LuaFileCopier
	{
		private static string luaFolderPath = "Assets/Lua";
		private static string destinationFolder = "Assets/Lua/LuaAssets";

			[MenuItem("Tools/Copy Lua Files")]
			public static void CopyLuaFiles()
			{
				string[] luaFiles = Directory.GetFiles(luaFolderPath, "*.lua");
				int fileCounts = 0;

				foreach (string filePath in luaFiles)
				{
					string fileName = Path.GetFileNameWithoutExtension(filePath);
					string destinationPath = Path.Combine(destinationFolder, fileName + ".txt");

					File.Copy(filePath, destinationPath, true);
					fileCounts++;
				}

				AssetDatabase.Refresh();

				EditorUtility.DisplayDialog("Lua File Copy", "成功拷贝了" + fileCounts + "份文件", "知道了");
			}
	}
}
