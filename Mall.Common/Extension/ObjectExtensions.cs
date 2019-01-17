using System;
using System.Collections.Generic;
using System.Text;

namespace Mall.Common.Extension
{
    public static class ObjectExtensions
    {
        public static bool IsNotNull(this Object value)
        {
            if (value != null && value.ToString().Trim() != string.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static bool IsNull(this Object value)
        {
            if (value == null || value.ToString() == string.Empty)
            {
                return true;
            }
            return false;
        }
    }
}
