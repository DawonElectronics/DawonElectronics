// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

#pragma warning disable 219, 612, 618
#nullable enable

namespace DAWON_UV_INVENTORY_PROTO.Models
{
    public partial class Db_Uv_InventoryContextModel
    {
        partial void Initialize()
        {
            var tbBomYpeMaterial = TbBomYpeMaterialEntityType.Create(this);
            var tbCustomer = TbCustomerEntityType.Create(this);
            var tbMachine = TbMachineEntityType.Create(this);
            var tbPrctype = TbPrctypeEntityType.Create(this);
            var tbUsers = TbUsersEntityType.Create(this);
            var tbUvToolinfo = TbUvToolinfoEntityType.Create(this);
            var tbUvWorkorder = TbUvWorkorderEntityType.Create(this);
            var viewUvWorkorder = ViewUvWorkorderEntityType.Create(this);
            var viewUvWorkorder2 = ViewUvWorkorder2EntityType.Create(this);
            var viewUvWorkorderDone = ViewUvWorkorderDoneEntityType.Create(this);

            TbUvToolinfoEntityType.CreateForeignKey1(tbUvToolinfo, tbCustomer);
            TbUvToolinfoEntityType.CreateForeignKey2(tbUvToolinfo, tbPrctype);
            TbUvWorkorderEntityType.CreateForeignKey1(tbUvWorkorder, tbCustomer);
            TbUvWorkorderEntityType.CreateForeignKey2(tbUvWorkorder, tbUvToolinfo);
            TbUvWorkorderEntityType.CreateForeignKey3(tbUvWorkorder, tbUsers);
            TbUvWorkorderEntityType.CreateForeignKey4(tbUvWorkorder, tbUsers);

            TbBomYpeMaterialEntityType.CreateAnnotations(tbBomYpeMaterial);
            TbCustomerEntityType.CreateAnnotations(tbCustomer);
            TbMachineEntityType.CreateAnnotations(tbMachine);
            TbPrctypeEntityType.CreateAnnotations(tbPrctype);
            TbUsersEntityType.CreateAnnotations(tbUsers);
            TbUvToolinfoEntityType.CreateAnnotations(tbUvToolinfo);
            TbUvWorkorderEntityType.CreateAnnotations(tbUvWorkorder);
            ViewUvWorkorderEntityType.CreateAnnotations(viewUvWorkorder);
            ViewUvWorkorder2EntityType.CreateAnnotations(viewUvWorkorder2);
            ViewUvWorkorderDoneEntityType.CreateAnnotations(viewUvWorkorderDone);

            AddAnnotation("ProductVersion", "6.0.3");
            AddAnnotation("Relational:MaxIdentifierLength", 128);
            AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
        }
    }
}
