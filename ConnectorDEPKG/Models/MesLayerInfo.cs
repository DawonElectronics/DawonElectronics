using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectorDEPKG.Models
{
    public class MesLayerInfo
    {
        public string InsulThickness { get; set; }
        public string CopperThickness { get; set; }
        public MaterialInfo MaterialInfo { get; set; }
    }

    public class MaterialInfo
    {
        public string BomNumber { get; set; }
        public string FromLayer { get; set; }
        public string ToLayer { get; set; }
        public string MaterialType { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialName { get; set; }
        public string MaterialMaker { get; set; }
        public string MaterialSpec1 { get; set; }
        public string MaterialSpec2 { get; set; }
        public string MaterialSize { get; set; }
        public string MaterialThickness { get; set; }
        public string CopperThickness1 { get; set; }
        public string CopperThickness2 { get; set; }
    }
}
