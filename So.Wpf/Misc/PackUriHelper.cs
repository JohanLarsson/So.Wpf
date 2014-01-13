//namespace So.Wpf.Misc
//{
//    using System;
//    using System.Reflection;
//    /// <summary>
//    /// http://blogs.msdn.com/b/greg_schechter/archive/2008/05/12/introduction-to-writing-effects.aspx
//    /// </summary>
//    internal static class PackUriHelper
//    {
//        /// <summary>
//        /// Helper method for generating a "pack://" URI for a given relative file based on the
//        /// assembly that this class is in.
//        /// </summary>
//        public static Uri MakePackUri(string relativeFile)
//        {
//            string uriString = "pack://application:,,,/" + AssemblyShortName + ";component/" + relativeFile;
//            return new Uri(uriString);
//        }

//        private static string AssemblyShortName
//        {
//            get
//            {
//                if (_assemblyShortName == null)
//                {
//                    Assembly a = typeof(PackUriHelper).Assembly;

//                    // Pull out the short name.
//                    _assemblyShortName = a.ToString().Split(',')[0];
//                }

//                return _assemblyShortName;
//            }
//        }

//        private static string _assemblyShortName;
//    }
//}
