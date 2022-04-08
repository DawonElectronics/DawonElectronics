﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DAWON_UV_INVENTORY_PROTO.Models
{
    public partial class TbUvToolinfo
    {
        public TbUvToolinfo()
        {
            TbUvWorkorder = new HashSet<TbUvWorkorder>();
        }

        public string ProductId { get; set; }
        public string CustToolno { get; set; }
        public string CustModelname { get; set; }
        public string CreateDate { get; set; }
        public string CustId { get; set; }
        public bool? Sample { get; set; }
        public decimal? WorksizeX { get; set; }
        public decimal? WorksizeY { get; set; }
        public int? ArrayBlk { get; set; }
        public int? Pcs { get; set; }
        public int? Layer { get; set; }
        public string MesPrcCode { get; set; }
        public string MesPrcName { get; set; }
        public string EndCustomer { get; set; }
        public string ProductType { get; set; }
        public string PrcCode { get; set; }
        public string CustRevision { get; set; }
        public string ToolNotes { get; set; }
        public string CustComment { get; set; }
        public decimal? Depth { get; set; }
        public string InsulType { get; set; }
        public string HoleCount { get; set; }
        public string MainHoleSize { get; set; }
        public string MesSeqCode { get; set; }
        public string PrcLayerFrom1 { get; set; }
        public string PrcLayerTo1 { get; set; }
        public string StackType { get; set; }
        public string PrdCategory { get; set; }
        public string LayerStructure { get; set; }
        public string CustomerShipto { get; set; }
        public string PrcLayerFrom2 { get; set; }
        public string PrcLayerTo2 { get; set; }
        public string HoleCount1 { get; set; }
        public string HoleCount2 { get; set; }
        public string HoleCountPth { get; set; }
        public string CustName { get; set; }
        public string PrcName { get; set; }
        public string InsulInfo { get; set; }
        public decimal? InsulThickness { get; set; }
        public decimal? CuThickness { get; set; }
        public int? Pcsperstrip { get; set; }
        public decimal? PcssizeX { get; set; }
        public decimal? PcssizeY { get; set; }
        public int? StriparrayCol { get; set; }
        public int? StriparrayRow { get; set; }
        public int? StriparrayBlk { get; set; }
        public bool? CamFinished { get; set; }
        public string SemCsdata { get; set; }
        public string SemSsdata { get; set; }
        public string YpeDatarev { get; set; }
        public string YpNextResourcelist { get; set; }
        public string YpNextResourceDefault { get; set; }

        public virtual TbCustomer Cust { get; set; }
        public virtual TbPrctype PrcCodeNavigation { get; set; }
        public virtual ICollection<TbUvWorkorder> TbUvWorkorder { get; set; }
    }
}