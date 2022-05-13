using Core.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class DM_DungChungViewModel: EntityBase
    {
        private string Ma;
        private string Ten;
        private bool Used;
        private string Parent;

        public string Ma1
        {
            get
            {
                return Ma;
            }

            set
            {
                Ma = value;
            }
        }

        public string Ten1
        {
            get
            {
                return Ten;
            }

            set
            {
                Ten = value;
            }
        }

        public bool Used1
        {
            get
            {
                return Used;
            }

            set
            {
                Used = value;
            }
        }

        public string Parent1
        {
            get
            {
                return Parent;
            }

            set
            {
                Parent = value;
            }
        }
    }
    public class CBO_DungChungViewModel
    {
        private string parentID;
        private string Ma;
        private string Ten;

        public string ParentID
        {
            get
            {
                return parentID;
            }

            set
            {
                parentID = value;
            }
        }
        public string Ma1
        {
            get
            {
                return Ma;
            }

            set
            {
                Ma = value;
            }
        }

        public string Ten1
        {
            get
            {
                return Ten;
            }

            set
            {
                Ten = value;
            }
        }
    }

    public class CBO_DungChungParam
    {
        private string tableName;
        private string parentID1;
        private string parentID2;

        public string TableName
        {
            get
            {
                return tableName;
            }

            set
            {
                tableName = value;
            }
        }

        public string ParentID1
        {
            get
            {
                return parentID1;
            }

            set
            {
                parentID1 = value;
            }
        }

        public string ParentID2
        {
            get
            {
                return parentID2;
            }

            set
            {
                parentID2 = value;
            }
        }
    }
    public class MT_MauInDataBindingModel
    {
        public string StoreProcedure { get; set; }
        public string Parameters { get; set; }
    }
    public class E_ParametersDataModel
    {
        public string TypePrint { get; set; }
        public string Path { get; set; }
        public string Procdures { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public string StartColumns { get; set; }
        public string EndColumns { get; set; }
        public int TotalCoumns { get; set; }
        public int RowStart { get; set; }
    }
    public class CM_Author
    {
        public string Key { get; set; }
        public string KeyDisplay { get; set; }
        public string Values { get; set; }
        
    }
    public class CM_Autocomplete_Map
    {
        public string Name { get; set; }
    }
    public class CM_Autocomplete_Param
    {
        public string TuKhoa { get; set; }
        public string Key { get; set; }
    }
}
