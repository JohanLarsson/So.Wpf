namespace So.Wpf.Samples.Browser
{
    using System.IO;
    public class DummyFile
    {
        public DummyFile(FileInfo fileInfo)
        {
            FileInfo = fileInfo;
        }

        public FileInfo FileInfo { get; set; }

        public string Name
        {
            get
            {
                return FileInfo.Name;
            }
        }

        public string Type
        {
            get
            {
                return "File";
            }
        }
    }
}
