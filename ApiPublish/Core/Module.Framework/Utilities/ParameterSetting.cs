using System;
using Module.Framework.UltimateClient;

namespace Module.Framework.Utilities
{
    public static class ParameterSetting
    {
        public static string PathFileUpload
        {
            get
            {
                return "Portals\\_default\\Uploads";//ReadValueSetting("PathFileUpload");
            }
        }

        public static int DefaulePageIndex
        {
            get
            {
                return 1;
            }
        }

        public static int PageSize
        {
            get
            {
                return 10;
            }
        }
        public static int MaxPageSize
        {
            get
            {
                return 1000;
            }
        }
        public static int MinPageSize
        {
            get { return 1; }
        }
    }
}
