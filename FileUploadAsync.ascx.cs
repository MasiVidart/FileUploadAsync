using System;
using System.Web.Script.Serialization;

namespace WebApplication1
{
    public partial class FileUploadAsync : System.Web.UI.UserControl
    {
        /// <summary>
        /// Attach an event handler to this event so the user control can call the handler as a callback when processing the file finishes.
        /// </summary>
        public event EventHandler FileProcessed;

        protected void btnUploadFile_Click(object sender, EventArgs e)
        {
            ProcessFile();

            if (FileProcessed != null)
            {
                FileProcessed(sender, e);
            }
        }

        /// <summary>
        /// Process the data received and stores it as a FileUploadAsyncFile in PostedFile property.
        /// </summary>
        public void ProcessFile()
        {
            JavaScriptSerializer js = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };
            PostedFile = js.Deserialize<FileUploadAsyncFile>(hfFile.Value);

            hfFile.Value = string.Empty;
        }

        public FileUploadAsyncFile PostedFile { get; private set; }

        public string Accept { get; set; }

        public class FileUploadAsyncFile
        {
            /// <summary>
            /// File name. Extension included.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// File contents in base64 string format.
            /// </summary>
            public string Data { get; set; }

            /// <summary>
            /// Takes the extension from the file name property.
            /// </summary>
            public string Extension { get { return Name.Substring(Name.LastIndexOf('.')); } }

            public byte[] GetBytes() { return Convert.FromBase64String(Data); }
        }
    }
}
