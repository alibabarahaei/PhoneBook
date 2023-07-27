namespace PhoneBook.Application.Utilities
{
    public static class SD
    {

        #region path






        #region uploader

        public static string UploadImage = "/img/upload/";
        public static string UploadImageServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/upload/");

        #endregion




        #region default images

        public static string DefaultProfileman = "/images/defaults/man.png";
        public static string DefaultProfilewoman = "/images/defaults/woman.png";
        

        #endregion 


        #region user avatar

        public static string UserProfileOrigin = "/userprofile/images/";
        public static string UserProfileOriginServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/userprofile/images/");

        public static string UserProfileThumb = "/userprofile/images/thumb/";
        public static string UserProfileThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/userprofile/images/thumb/");


        #endregion

        #region contact image
        public static string ContactImagesOrigin = "/contact/images/";
        public static string ContactImagesOriginServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/contact/images/");

        public static string ContactImagesThumb = "/contact/images/thumb/";
        public static string ContactImagesThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/contact/images/thumb/");
        #endregion



        #endregion



    }
}
