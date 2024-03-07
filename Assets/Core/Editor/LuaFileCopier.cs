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
			//主要是为了防止文件名更改导致冗余的情况发生
			ClearDestinationFolder();

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

		//清空目标路径
		private static void ClearDestinationFolder()
		{
			if (Directory.Exists(destinationFolder))
			{
				string[] files = Directory.GetFiles(destinationFolder);
				string[] directories = Directory.GetDirectories(destinationFolder);

				foreach (string file in files)
				{
					File.Delete(file);
				}

				foreach (string directory in directories)
				{
					Directory.Delete(directory, true);
				}
			}
			else
			{
				Directory.CreateDirectory(destinationFolder);
			}
		}
	}
}
