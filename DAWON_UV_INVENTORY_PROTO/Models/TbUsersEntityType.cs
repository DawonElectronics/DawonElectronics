﻿// <auto-generated />
using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata;

#pragma warning disable 219, 612, 618
#nullable enable

namespace DAWON_UV_INVENTORY_PROTO.Models
{
    internal partial class TbUsersEntityType
    {
        public static RuntimeEntityType Create(RuntimeModel model, RuntimeEntityType? baseEntityType = null)
        {
            var runtimeEntityType = model.AddEntityType(
                "DAWON_UV_INVENTORY_PROTO.Models.TbUsers",
                typeof(TbUsers),
                baseEntityType);

            var userId = runtimeEntityType.AddProperty(
                "UserId",
                typeof(string),
                propertyInfo: typeof(TbUsers).GetProperty("UserId", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(TbUsers).GetField("<UserId>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                afterSaveBehavior: PropertySaveBehavior.Throw,
                maxLength: 20);
            userId.AddAnnotation("Relational:ColumnName", "user_id");
            userId.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var isRetired = runtimeEntityType.AddProperty(
                "IsRetired",
                typeof(bool?),
                propertyInfo: typeof(TbUsers).GetProperty("IsRetired", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(TbUsers).GetField("<IsRetired>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true,
                valueGenerated: ValueGenerated.OnAdd);
            isRetired.AddAnnotation("Relational:ColumnName", "isRetired");
            isRetired.AddAnnotation("Relational:DefaultValueSql", "((0))");
            isRetired.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var userGroup = runtimeEntityType.AddProperty(
                "UserGroup",
                typeof(string),
                propertyInfo: typeof(TbUsers).GetProperty("UserGroup", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(TbUsers).GetField("<UserGroup>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true,
                maxLength: 20);
            userGroup.AddAnnotation("Relational:ColumnName", "user_group");
            userGroup.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var userName = runtimeEntityType.AddProperty(
                "UserName",
                typeof(string),
                propertyInfo: typeof(TbUsers).GetProperty("UserName", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(TbUsers).GetField("<UserName>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true,
                maxLength: 20);
            userName.AddAnnotation("Relational:ColumnName", "user_name");
            userName.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var userRole = runtimeEntityType.AddProperty(
                "UserRole",
                typeof(string),
                propertyInfo: typeof(TbUsers).GetProperty("UserRole", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(TbUsers).GetField("<UserRole>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true,
                maxLength: 20);
            userRole.AddAnnotation("Relational:ColumnName", "user_role");
            userRole.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var key = runtimeEntityType.AddKey(
                new[] { userId });
            runtimeEntityType.SetPrimaryKey(key);

            var uNIQUE_user_id = runtimeEntityType.AddIndex(
                new[] { userId },
                name: "UNIQUE_user_id",
                unique: true);

            return runtimeEntityType;
        }

        public static void CreateAnnotations(RuntimeEntityType runtimeEntityType)
        {
            runtimeEntityType.AddAnnotation("Relational:FunctionName", null);
            runtimeEntityType.AddAnnotation("Relational:Schema", null);
            runtimeEntityType.AddAnnotation("Relational:SqlQuery", null);
            runtimeEntityType.AddAnnotation("Relational:TableName", "tb_users");
            runtimeEntityType.AddAnnotation("Relational:ViewName", null);
            runtimeEntityType.AddAnnotation("Relational:ViewSchema", null);

            Customize(runtimeEntityType);
        }

        static partial void Customize(RuntimeEntityType runtimeEntityType);
    }
}