﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DAWON_UV_INVENTORY_PROTO.Models
{
    public partial class TbPrctype
    {
        public TbPrctype()
        {
            TbUvToolinfo = new HashSet<TbUvToolinfo>();
        }

        public string PrcCode { get; set; }
        public string PrcName { get; set; }

        public virtual ICollection<TbUvToolinfo> TbUvToolinfo { get; set; }
    }
}