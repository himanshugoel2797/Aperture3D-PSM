using System;
using System.IO;
using System.Reflection;

namespace Aperture3D
{
		public static class VFS
	{
		private static string baseDir;
		public static Type BaseType;

		static VFS ()
		{
			BaseType = typeof(VFS);
			VFS.baseDir = (Environment.OSVersion.VersionString.Contains ("Mobile") ? "" : Environment.CurrentDirectory);
		}

		public static Stream OpenFile (string path)
		{
			return VFS.OpenFile (path, FileMode.OpenOrCreate);
		}

		public static Stream OpenFile (string path, FileMode mode)
		{
			return VFS.OpenFile (path, mode, FileAccess.Read);
		}

		public static Stream OpenFile (string path, FileMode mode, FileAccess access)
		{
			return VFS.OpenFile (path, mode, access, FileShare.ReadWrite);
		}

		public static Stream OpenFile (string path, FileMode mode, FileAccess access, FileShare share)
		{
			if (VFS.isVFS1 (path)) {
				return VFS.OpenVFS1File (path);
			}
			string path2 = VFS.GenerateRealPath (path);
			Stream result = null;
			try {
				result = File.Open (path2, mode, access, share);
			} catch (DirectoryNotFoundException) {
				//Directory.CreateDirectory (Path.GetDirectoryName (path2));
				result = File.Open (path, mode, access, share);
			}
			return result;
		}

		public static Stream OpenVFS1File (string filename)
		{
			filename = VFS.GenerateRealPath (filename);
			Console.WriteLine ("GetEmbeddedResource(" + filename + ")");
			Assembly assembly = Assembly.GetAssembly (VFS.BaseType);
			string text = filename;
			if (assembly.GetManifestResourceInfo (text) == null) {
				string[] manifestResourceNames = assembly.GetManifestResourceNames ();
				for (int i = 0; i < manifestResourceNames.Length; i++) {
					Console.WriteLine ("all_embedded_names[i]=" + manifestResourceNames [i]);
				}
				Console.WriteLine ("embedded filename=" + assembly);
				throw new FileNotFoundException ("File not found.", filename);
			}
			return assembly.GetManifestResourceStream (text);
		}

		private static bool isVFS1 (string path)
		{
			return path.Contains ("vfs1:");
		}

		public static void DeleteFile (string path)
		{
			File.Delete (VFS.GenerateRealPath (path));
		}

		public static string GenerateRealPath (string path)
		{
			Console.Write ("GenerateRealPath(\"" + path + "\")");
			if (!VFS.isVFS1 (path)) {
				path = path.Replace ("vfs0:", VFS.baseDir);
			} else {
				Assembly.GetAssembly (VFS.BaseType);
				path = path.Replace ("vfs1:", "");
				path = path.Replace ("/", ".");
				path = VFS.BaseType.Namespace + path;
			}
			Console.WriteLine (" = \"" + path + "\"");
			return path;
		}
		
		public static byte[] GetFileBytes (string path)
		{
			using (Stream tmp = OpenFile(path)) {
				byte[] toRet = new byte[tmp.Length];
				tmp.Read (toRet, 0, (int)tmp.Length);
				return toRet;
			}
		}
	}
}

