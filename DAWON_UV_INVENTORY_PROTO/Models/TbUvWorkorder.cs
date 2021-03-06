// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DAWON_UV_INVENTORY_PROTO.Models
{
    public partial class TbUvWorkorder
    {
        public DateTime? CreateTime { get; set; }
        public DateTime? TrackinTime { get; set; }
        public DateTime? TrackoutTime { get; set; }
        public Guid Txid { get; set; }
        public string TrackinUserId { get; set; }
        public string TrackoutUserId { get; set; }
        public string Lotid { get; set; }
        public short? Pnlqty { get; set; }
        public bool? SampleOrder { get; set; }
        public string ProductId { get; set; }
        public string LotNotes { get; set; }
        public string MachineCs { get; set; }
        public string MachineSs { get; set; }
        public bool? IsDone { get; set; }
        public bool? IsPrinted { get; set; }
        public string CustId { get; set; }
        public long Id { get; set; }
        public string LotType { get; set; }
        public bool? WaitTrackout { get; set; }
        public string FormatBg { get; set; }
        public string FormatFg { get; set; }
        public bool? FormatBold { get; set; }
        public string LotHistory { get; set; }
        public string YpShortlot { get; set; }
        public string RtrLot { get; set; }
        public string SampleDep { get; set; }
        public string NftOrderserial { get; set; }
        public string RtrLoss { get; set; }
        public string YpNextResource { get; set; }
        public short? Rtrpnlqty { get; set; }

        public virtual TbCustomer Cust { get; set; }
        public virtual TbUvToolinfo Product { get; set; }
        public virtual TbUsers TrackinUser { get; set; }
        public virtual TbUsers TrackoutUser { get; set; }
    }
}