﻿using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace MVC5Course.Models
{
    internal class 產品名稱必須至少包含兩個空白Attribute : DataTypeAttribute
    {
        public 產品名稱必須至少包含兩個空白Attribute() : base(DataType.Text)
        {

        }

        public override bool IsValid(object value)
        {
            string str = (string)value;

            return str.Count(p => p==' ') == 2;
        }
    }
}