using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Application.Models
{
    public class StorePathModel
    {

        #region uploader

        public static string UploadImage { get; set; }
        public static string UploadImageServer { get; set; }

        #endregion

        #region default images

        public static string DefaultProfileman { get; set; }
        public static string DefaultProfilewoman  { get; set; }


    #endregion


    #region user avatar

        public static string UserProfileOrigin { get; set; }
        public static string UserProfileOriginServer { get; set; }

        public static string UserProfileThumb { get; set; }
        public static string UserProfileThumbServer { get; set; }
        #endregion

        #region contact image
        public static string ContactImagesOrigin { get; set; }
        public static string ContactImagesOriginServer { get; set; }

        public static string ContactImagesThumb { get; set; }
        public static string ContactImagesThumbServer { get; set; }
        #endregion



    }
}
