using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectorYPE.Models
{
    public class YPToolInfo
    {
        public string CustToolno { get; set; }
        public string CustModelname { get; set; }
        public string CreateDate { get; set; }
        public string PrdCategory { get; set; }
        public string ProductType { get; set; }
        public bool? Sample { get; set; }
        public string? WorksizeX { get; set; }
        public string? WorksizeY { get; set; }
        public string? ArrayBlk { get; set; }
        public string? Pcs { get; set; }
        public string? Layer { get; set; }
        public string MesPrcCode { get; set; }
        public string MesPrcName { get; set; }
        public string EndCustomer { get; set; }
        public string MesRevision { get; set; }
        public string ToolNotes { get; set; }
        public string DataRevision { get; set; }
        public string CustComment { get; set; }
        public double? Depth { get; set; }
        public string InsulType { get; set; }
        public string MainHoleSize { get; set; }
        public string MesSeqCode { get; set; }
        public string PrcLayerFrom1 { get; set; }
        public string PrcLayerTo1 { get; set; }
        public string PrcLayerFrom2 { get; set; }
        public string PrcLayerTo2 { get; set; }
        public string CustName { get; set; }
        public string InsulInfo { get; set; }
        public string? InsulThickness { get; set; }
        public string? CuThickness { get; set; }

    }
}
