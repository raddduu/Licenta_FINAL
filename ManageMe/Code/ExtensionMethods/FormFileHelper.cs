namespace ManageMe.Web.Code.ExtensionMethods
{
    public static class FormFileHelper
    {
        public static IFormFile? ToIFormFile(this byte[] byteArray, string fileName)
        {
            try
            {
                using (var memoryStream = new MemoryStream(byteArray))
                {
                    var formFile = new FormFile(memoryStream, 0, byteArray.Length, null, fileName)
                    {
                        Headers = new HeaderDictionary(),
                        ContentType = "image/jpeg"
                    };

                    return formFile;
                }
            }
            catch
            {
                return null;
            }
        }

        public static byte[]? ToByteArray(this IFormFile? formFile)
        {
            if (formFile == null)
            {
                return null;
            }
            using (var memoryStream = new MemoryStream())
            {
                formFile.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
