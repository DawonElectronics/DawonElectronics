﻿// <auto-generated />
using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata;

#pragma warning disable 219, 612, 618
#nullable enable

namespace DAWON_UV_INVENTORY_PROTO.Models
{
    internal partial class TbUvWorkorderEntityType
    {
        public static RuntimeEntityType Create(RuntimeModel model, RuntimeEntityType? baseEntityType = null)
        {
            var runtimeEntityType = model.AddEntityType(
                "DAWON_UV_INVENTORY_PROTO.Models.TbUvWorkorder",
                typeof(TbUvWorkorder),
                baseEntityType);

            var id = runtimeEntityType.AddProperty(
                "Id",
                typeof(long),
                propertyInfo: typeof(TbUvWorkorder).GetProperty("Id", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(TbUvWorkorder).GetField("<Id>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                valueGenerated: ValueGenerated.OnAdd,
                afterSaveBehavior: PropertySaveBehavior.Throw);
            id.AddAnnotation("Relational:ColumnName", "id");
            id.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            var createTime = runtimeEntityType.AddProperty(
                "CreateTime",
                typeof(DateTime?),
                propertyInfo: typeof(TbUvWorkorder).GetProperty("CreateTime", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(TbUvWorkorder).GetField("<CreateTime>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true);
            createTime.AddAnnotation("Relational:ColumnName", "create_time");
            createTime.AddAnnotation("Relational:ColumnType", "datetime");
            createTime.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var custId = runtimeEntityType.AddProperty(
                "CustId",
                typeof(string),
                propertyInfo: typeof(TbUvWorkorder).GetProperty("CustId", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(TbUvWorkorder).GetField("<CustId>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true,
                maxLength: 10);
            custId.AddAnnotation("Relational:ColumnName", "cust_id");
            custId.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var formatBg = runtimeEntityType.AddProperty(
                "FormatBg",
                typeof(string),
                propertyInfo: typeof(TbUvWorkorder).GetProperty("FormatBg", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(TbUvWorkorder).GetField("<FormatBg>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true,
                maxLength: 20,
                unicode: false);
            formatBg.AddAnnotation("Relational:ColumnName", "Format_bg");
            formatBg.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var formatBold = runtimeEntityType.AddProperty(
                "FormatBold",
                typeof(bool?),
                propertyInfo: typeof(TbUvWorkorder).GetProperty("FormatBold", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(TbUvWorkorder).GetField("<FormatBold>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true);
            formatBold.AddAnnotation("Relational:ColumnName", "Format_bold");
            formatBold.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var formatFg = runtimeEntityType.AddProperty(
                "FormatFg",
                typeof(string),
                propertyInfo: typeof(TbUvWorkorder).GetProperty("FormatFg", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(TbUvWorkorder).GetField("<FormatFg>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true,
                maxLength: 20,
                unicode: false);
            formatFg.AddAnnotation("Relational:ColumnName", "Format_fg");
            formatFg.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var isDone = runtimeEntityType.AddProperty(
                "IsDone",
                typeof(bool?),
                propertyInfo: typeof(TbUvWorkorder).GetProperty("IsDone", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(TbUvWorkorder).GetField("<IsDone>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true,
                valueGenerated: ValueGenerated.OnAdd);
            isDone.AddAnnotation("Relational:ColumnName", "isDone");
            isDone.AddAnnotation("Relational:DefaultValueSql", "((0))");
            isDone.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var isPrinted = runtimeEntityType.AddProperty(
                "IsPrinted",
                typeof(bool?),
                propertyInfo: typeof(TbUvWorkorder).GetProperty("IsPrinted", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(TbUvWorkorder).GetField("<IsPrinted>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true,
                valueGenerated: ValueGenerated.OnAdd);
            isPrinted.AddAnnotation("Relational:ColumnName", "isPrinted");
            isPrinted.AddAnnotation("Relational:DefaultValueSql", "((0))");
            isPrinted.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var lotHistory = runtimeEntityType.AddProperty(
                "LotHistory",
                typeof(string),
                propertyInfo: typeof(TbUvWorkorder).GetProperty("LotHistory", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(TbUvWorkorder).GetField("<LotHistory>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true);
            lotHistory.AddAnnotation("Relational:ColumnType", "ntext");
            lotHistory.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var lotNotes = runtimeEntityType.AddProperty(
                "LotNotes",
                typeof(string),
                propertyInfo: typeof(TbUvWorkorder).GetProperty("LotNotes", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(TbUvWorkorder).GetField("<LotNotes>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true);
            lotNotes.AddAnnotation("Relational:ColumnName", "lot_notes");
            lotNotes.AddAnnotation("Relational:ColumnType", "ntext");
            lotNotes.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var lotType = runtimeEntityType.AddProperty(
                "LotType",
                typeof(string),
                propertyInfo: typeof(TbUvWorkorder).GetProperty("LotType", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(TbUvWorkorder).GetField("<LotType>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true,
                maxLength: 20);
            lotType.AddAnnotation("Relational:ColumnName", "lot_type");
            lotType.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var lotid = runtimeEntityType.AddProperty(
                "Lotid",
                typeof(string),
                propertyInfo: typeof(TbUvWorkorder).GetProperty("Lotid", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(TbUvWorkorder).GetField("<Lotid>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true,
                maxLength: 100);
            lotid.AddAnnotation("Relational:ColumnName", "lotid");
            lotid.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var machineCs = runtimeEntityType.AddProperty(
                "MachineCs",
                typeof(string),
                propertyInfo: typeof(TbUvWorkorder).GetProperty("MachineCs", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(TbUvWorkorder).GetField("<MachineCs>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true,
                maxLength: 300);
            machineCs.AddAnnotation("Relational:ColumnName", "machine_cs");
            machineCs.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var machineSs = runtimeEntityType.AddProperty(
                "MachineSs",
                typeof(string),
                propertyInfo: typeof(TbUvWorkorder).GetProperty("MachineSs", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(TbUvWorkorder).GetField("<MachineSs>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true,
                maxLength: 300);
            machineSs.AddAnnotation("Relational:ColumnName", "machine_ss");
            machineSs.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var pnlqty = runtimeEntityType.AddProperty(
                "Pnlqty",
                typeof(short?),
                propertyInfo: typeof(TbUvWorkorder).GetProperty("Pnlqty", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(TbUvWorkorder).GetField("<Pnlqty>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true);
            pnlqty.AddAnnotation("Relational:ColumnName", "pnlqty");
            pnlqty.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var productId = runtimeEntityType.AddProperty(
                "ProductId",
                typeof(string),
                propertyInfo: typeof(TbUvWorkorder).GetProperty("ProductId", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(TbUvWorkorder).GetField("<ProductId>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true,
                maxLength: 100);
            productId.AddAnnotation("Relational:ColumnName", "product_id");
            productId.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var sampleOrder = runtimeEntityType.AddProperty(
                "SampleOrder",
                typeof(bool?),
                propertyInfo: typeof(TbUvWorkorder).GetProperty("SampleOrder", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(TbUvWorkorder).GetField("<SampleOrder>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true);
            sampleOrder.AddAnnotation("Relational:ColumnName", "sample_order");
            sampleOrder.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var trackinTime = runtimeEntityType.AddProperty(
                "TrackinTime",
                typeof(DateTime?),
                propertyInfo: typeof(TbUvWorkorder).GetProperty("TrackinTime", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(TbUvWorkorder).GetField("<TrackinTime>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true);
            trackinTime.AddAnnotation("Relational:ColumnName", "trackin_time");
            trackinTime.AddAnnotation("Relational:ColumnType", "datetime");
            trackinTime.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var trackinUserId = runtimeEntityType.AddProperty(
                "TrackinUserId",
                typeof(string),
                propertyInfo: typeof(TbUvWorkorder).GetProperty("TrackinUserId", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(TbUvWorkorder).GetField("<TrackinUserId>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true,
                maxLength: 20);
            trackinUserId.AddAnnotation("Relational:ColumnName", "trackin_user_id");
            trackinUserId.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var trackoutTime = runtimeEntityType.AddProperty(
                "TrackoutTime",
                typeof(DateTime?),
                propertyInfo: typeof(TbUvWorkorder).GetProperty("TrackoutTime", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(TbUvWorkorder).GetField("<TrackoutTime>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true);
            trackoutTime.AddAnnotation("Relational:ColumnName", "trackout_time");
            trackoutTime.AddAnnotation("Relational:ColumnType", "datetime");
            trackoutTime.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var trackoutUserId = runtimeEntityType.AddProperty(
                "TrackoutUserId",
                typeof(string),
                propertyInfo: typeof(TbUvWorkorder).GetProperty("TrackoutUserId", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(TbUvWorkorder).GetField("<TrackoutUserId>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true,
                maxLength: 20);
            trackoutUserId.AddAnnotation("Relational:ColumnName", "trackout_user_id");
            trackoutUserId.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var txid = runtimeEntityType.AddProperty(
                "Txid",
                typeof(Guid),
                propertyInfo: typeof(TbUvWorkorder).GetProperty("Txid", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(TbUvWorkorder).GetField("<Txid>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));
            txid.AddAnnotation("Relational:ColumnName", "txid");
            txid.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var waitTrackout = runtimeEntityType.AddProperty(
                "WaitTrackout",
                typeof(bool?),
                propertyInfo: typeof(TbUvWorkorder).GetProperty("WaitTrackout", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(TbUvWorkorder).GetField("<WaitTrackout>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true,
                valueGenerated: ValueGenerated.OnAdd);
            waitTrackout.AddAnnotation("Relational:DefaultValueSql", "((0))");
            waitTrackout.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var key = runtimeEntityType.AddKey(
                new[] { id });
            runtimeEntityType.SetPrimaryKey(key);

            var index = runtimeEntityType.AddIndex(
                new[] { custId });

            var index0 = runtimeEntityType.AddIndex(
                new[] { productId });

            var index1 = runtimeEntityType.AddIndex(
                new[] { trackinUserId });

            var index2 = runtimeEntityType.AddIndex(
                new[] { trackoutUserId });

            var iX_tb_uv_workorder = runtimeEntityType.AddIndex(
                new[] { trackoutTime, custId, isDone },
                name: "IX_tb_uv_workorder");

            return runtimeEntityType;
        }

        public static RuntimeForeignKey CreateForeignKey1(RuntimeEntityType declaringEntityType, RuntimeEntityType principalEntityType)
        {
            var runtimeForeignKey = declaringEntityType.AddForeignKey(new[] { declaringEntityType.FindProperty("CustId")! },
                principalEntityType.FindKey(new[] { principalEntityType.FindProperty("CustId")! })!,
                principalEntityType);

            var cust = declaringEntityType.AddNavigation("Cust",
                runtimeForeignKey,
                onDependent: true,
                typeof(TbCustomer),
                propertyInfo: typeof(TbUvWorkorder).GetProperty("Cust", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(TbUvWorkorder).GetField("<Cust>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));

            var tbUvWorkorder = principalEntityType.AddNavigation("TbUvWorkorder",
                runtimeForeignKey,
                onDependent: false,
                typeof(ICollection<TbUvWorkorder>),
                propertyInfo: typeof(TbCustomer).GetProperty("TbUvWorkorder", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(TbCustomer).GetField("<TbUvWorkorder>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));

            runtimeForeignKey.AddAnnotation("Relational:Name", "tb_workorder_FK_custid");
            return runtimeForeignKey;
        }

        public static RuntimeForeignKey CreateForeignKey2(RuntimeEntityType declaringEntityType, RuntimeEntityType principalEntityType)
        {
            var runtimeForeignKey = declaringEntityType.AddForeignKey(new[] { declaringEntityType.FindProperty("ProductId")! },
                principalEntityType.FindKey(new[] { principalEntityType.FindProperty("ProductId")! })!,
                principalEntityType);

            var product = declaringEntityType.AddNavigation("Product",
                runtimeForeignKey,
                onDependent: true,
                typeof(TbUvToolinfo),
                propertyInfo: typeof(TbUvWorkorder).GetProperty("Product", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(TbUvWorkorder).GetField("<Product>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));

            var tbUvWorkorder = principalEntityType.AddNavigation("TbUvWorkorder",
                runtimeForeignKey,
                onDependent: false,
                typeof(ICollection<TbUvWorkorder>),
                propertyInfo: typeof(TbUvToolinfo).GetProperty("TbUvWorkorder", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(TbUvToolinfo).GetField("<TbUvWorkorder>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));

            runtimeForeignKey.AddAnnotation("Relational:Name", "tb_workorder_FK_toolinfo");
            return runtimeForeignKey;
        }

        public static RuntimeForeignKey CreateForeignKey3(RuntimeEntityType declaringEntityType, RuntimeEntityType principalEntityType)
        {
            var runtimeForeignKey = declaringEntityType.AddForeignKey(new[] { declaringEntityType.FindProperty("TrackinUserId")! },
                principalEntityType.FindKey(new[] { principalEntityType.FindProperty("UserId")! })!,
                principalEntityType);

            var trackinUser = declaringEntityType.AddNavigation("TrackinUser",
                runtimeForeignKey,
                onDependent: true,
                typeof(TbUsers),
                propertyInfo: typeof(TbUvWorkorder).GetProperty("TrackinUser", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(TbUvWorkorder).GetField("<TrackinUser>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));

            var tbUvWorkorderTrackinUser = principalEntityType.AddNavigation("TbUvWorkorderTrackinUser",
                runtimeForeignKey,
                onDependent: false,
                typeof(ICollection<TbUvWorkorder>),
                propertyInfo: typeof(TbUsers).GetProperty("TbUvWorkorderTrackinUser", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(TbUsers).GetField("<TbUvWorkorderTrackinUser>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));

            runtimeForeignKey.AddAnnotation("Relational:Name", "tb_workorder_FK_trackinuserid");
            return runtimeForeignKey;
        }

        public static RuntimeForeignKey CreateForeignKey4(RuntimeEntityType declaringEntityType, RuntimeEntityType principalEntityType)
        {
            var runtimeForeignKey = declaringEntityType.AddForeignKey(new[] { declaringEntityType.FindProperty("TrackoutUserId")! },
                principalEntityType.FindKey(new[] { principalEntityType.FindProperty("UserId")! })!,
                principalEntityType);

            var trackoutUser = declaringEntityType.AddNavigation("TrackoutUser",
                runtimeForeignKey,
                onDependent: true,
                typeof(TbUsers),
                propertyInfo: typeof(TbUvWorkorder).GetProperty("TrackoutUser", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(TbUvWorkorder).GetField("<TrackoutUser>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));

            var tbUvWorkorderTrackoutUser = principalEntityType.AddNavigation("TbUvWorkorderTrackoutUser",
                runtimeForeignKey,
                onDependent: false,
                typeof(ICollection<TbUvWorkorder>),
                propertyInfo: typeof(TbUsers).GetProperty("TbUvWorkorderTrackoutUser", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(TbUsers).GetField("<TbUvWorkorderTrackoutUser>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));

            runtimeForeignKey.AddAnnotation("Relational:Name", "tb_workorder_FK_trackoutuserid");
            return runtimeForeignKey;
        }

        public static void CreateAnnotations(RuntimeEntityType runtimeEntityType)
        {
            runtimeEntityType.AddAnnotation("Relational:FunctionName", null);
            runtimeEntityType.AddAnnotation("Relational:Schema", null);
            runtimeEntityType.AddAnnotation("Relational:SqlQuery", null);
            runtimeEntityType.AddAnnotation("Relational:TableName", "tb_uv_workorder");
            runtimeEntityType.AddAnnotation("Relational:ViewName", null);
            runtimeEntityType.AddAnnotation("Relational:ViewSchema", null);

            Customize(runtimeEntityType);
        }

        static partial void Customize(RuntimeEntityType runtimeEntityType);
    }
}
