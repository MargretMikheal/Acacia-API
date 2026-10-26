IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250817095518_init'
)
BEGIN
    CREATE TABLE [Companies] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(200) NOT NULL,
        [ImageUrl] nvarchar(500) NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Companies] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250817095518_init'
)
BEGIN
    CREATE TABLE [Ingredients] (
        [Id] int NOT NULL IDENTITY,
        [NameAr] nvarchar(100) NOT NULL,
        [NameEn] nvarchar(100) NOT NULL,
        [ImageUrl] nvarchar(500) NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Ingredients] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250817095518_init'
)
BEGIN
    CREATE TABLE [PriceLists] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(200) NOT NULL,
        [Description] nvarchar(1000) NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_PriceLists] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250817095518_init'
)
BEGIN
    CREATE TABLE [ProductTypes] (
        [Id] int NOT NULL IDENTITY,
        [NameAr] nvarchar(100) NOT NULL,
        [NameEn] nvarchar(100) NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_ProductTypes] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250817095518_init'
)
BEGIN
    CREATE TABLE [Oils] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(200) NOT NULL,
        [CompanyId] int NOT NULL,
        [Category] int NOT NULL,
        [Style] int NOT NULL,
        [DescriptionAr] nvarchar(1000) NOT NULL,
        [DescriptionEn] nvarchar(1000) NOT NULL,
        [PriceListId] int NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Oils] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Oils_Companies_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Companies] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Oils_PriceLists_PriceListId] FOREIGN KEY ([PriceListId]) REFERENCES [PriceLists] ([Id]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250817095518_init'
)
BEGIN
    CREATE TABLE [BottleDesigns] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [ProductTypeId] int NOT NULL,
        [Price] decimal(18,2) NOT NULL,
        [ImageUrl] nvarchar(max) NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_BottleDesigns] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_BottleDesigns_ProductTypes_ProductTypeId] FOREIGN KEY ([ProductTypeId]) REFERENCES [ProductTypes] ([Id]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250817095518_init'
)
BEGIN
    CREATE TABLE [ProductSizes] (
        [Id] int NOT NULL IDENTITY,
        [ProductTypeId] int NOT NULL,
        [SizeValue] decimal(18,2) NOT NULL,
        [SizeUnit] nvarchar(50) NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_ProductSizes] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProductSizes_ProductTypes_ProductTypeId] FOREIGN KEY ([ProductTypeId]) REFERENCES [ProductTypes] ([Id]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250817095518_init'
)
BEGIN
    CREATE TABLE [OilIngredients] (
        [OilId] int NOT NULL,
        [IngredientId] int NOT NULL,
        [Percentage] decimal(5,2) NOT NULL,
        CONSTRAINT [PK_OilIngredients] PRIMARY KEY ([OilId], [IngredientId]),
        CONSTRAINT [FK_OilIngredients_Ingredients_IngredientId] FOREIGN KEY ([IngredientId]) REFERENCES [Ingredients] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_OilIngredients_Oils_OilId] FOREIGN KEY ([OilId]) REFERENCES [Oils] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250817095518_init'
)
BEGIN
    CREATE TABLE [OilOccasionScores] (
        [Id] int NOT NULL IDENTITY,
        [OilId] int NOT NULL,
        [Formal] decimal(4,2) NULL,
        [Casual] decimal(4,2) NULL,
        [Night] decimal(4,2) NULL,
        [Sport] decimal(4,2) NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_OilOccasionScores] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_OilOccasionScores_Oils_OilId] FOREIGN KEY ([OilId]) REFERENCES [Oils] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250817095518_init'
)
BEGIN
    CREATE TABLE [OilSeasonScores] (
        [Id] int NOT NULL IDENTITY,
        [OilId] int NOT NULL,
        [Summer] decimal(4,2) NULL,
        [Winter] decimal(4,2) NULL,
        [Fall] decimal(4,2) NULL,
        [Spring] decimal(4,2) NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_OilSeasonScores] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_OilSeasonScores_Oils_OilId] FOREIGN KEY ([OilId]) REFERENCES [Oils] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250817095518_init'
)
BEGIN
    CREATE TABLE [FinalProducts] (
        [Id] int NOT NULL IDENTITY,
        [OilId] int NOT NULL,
        [ProductTypeId] int NOT NULL,
        [ProductSizeId] int NOT NULL,
        [BottleDesignId] int NOT NULL,
        [BasePrice] decimal(18,2) NOT NULL,
        [TotalPrice] decimal(18,2) NOT NULL,
        [FinalProductType] nvarchar(13) NOT NULL,
        [HasGlitter] bit NULL,
        [WaxType] nvarchar(max) NULL,
        [BurnHours] int NULL,
        [IncenseMaterial] nvarchar(max) NULL,
        [IsOilBased] bit NULL,
        [AlcoholPercentage] decimal(18,2) NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_FinalProducts] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_FinalProducts_BottleDesigns_BottleDesignId] FOREIGN KEY ([BottleDesignId]) REFERENCES [BottleDesigns] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_FinalProducts_Oils_OilId] FOREIGN KEY ([OilId]) REFERENCES [Oils] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_FinalProducts_ProductSizes_ProductSizeId] FOREIGN KEY ([ProductSizeId]) REFERENCES [ProductSizes] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_FinalProducts_ProductTypes_ProductTypeId] FOREIGN KEY ([ProductTypeId]) REFERENCES [ProductTypes] ([Id]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250817095518_init'
)
BEGIN
    CREATE TABLE [PriceListItems] (
        [Id] int NOT NULL IDENTITY,
        [PriceListId] int NOT NULL,
        [ProductTypeId] int NOT NULL,
        [ProductSizeId] int NOT NULL,
        [Price] decimal(18,2) NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_PriceListItems] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PriceListItems_PriceLists_PriceListId] FOREIGN KEY ([PriceListId]) REFERENCES [PriceLists] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_PriceListItems_ProductSizes_ProductSizeId] FOREIGN KEY ([ProductSizeId]) REFERENCES [ProductSizes] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_PriceListItems_ProductTypes_ProductTypeId] FOREIGN KEY ([ProductTypeId]) REFERENCES [ProductTypes] ([Id]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250817095518_init'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreationDate', N'NameAr', N'NameEn') AND [object_id] = OBJECT_ID(N'[ProductTypes]'))
        SET IDENTITY_INSERT [ProductTypes] ON;
    EXEC(N'INSERT INTO [ProductTypes] ([Id], [CreationDate], [NameAr], [NameEn])
    VALUES (1, ''2024-01-01T00:00:00.0000000'', N''عطر'', N''Perfume''),
    (2, ''2024-01-01T00:00:00.0000000'', N''مخمريه'', N''Mukhammaria''),
    (3, ''2024-01-01T00:00:00.0000000'', N''بودي سبلاش'', N''BodySplash''),
    (4, ''2024-01-01T00:00:00.0000000'', N''شموع'', N''Candle''),
    (5, ''2024-01-01T00:00:00.0000000'', N''لوشن الجسم'', N''BodyLotion''),
    (6, ''2024-01-01T00:00:00.0000000'', N''مباخر'', N''Incense'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreationDate', N'NameAr', N'NameEn') AND [object_id] = OBJECT_ID(N'[ProductTypes]'))
        SET IDENTITY_INSERT [ProductTypes] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250817095518_init'
)
BEGIN
    CREATE INDEX [IX_BottleDesigns_ProductTypeId] ON [BottleDesigns] ([ProductTypeId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250817095518_init'
)
BEGIN
    CREATE INDEX [IX_FinalProducts_BottleDesignId] ON [FinalProducts] ([BottleDesignId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250817095518_init'
)
BEGIN
    CREATE INDEX [IX_FinalProducts_OilId] ON [FinalProducts] ([OilId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250817095518_init'
)
BEGIN
    CREATE INDEX [IX_FinalProducts_ProductSizeId] ON [FinalProducts] ([ProductSizeId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250817095518_init'
)
BEGIN
    CREATE INDEX [IX_FinalProducts_ProductTypeId] ON [FinalProducts] ([ProductTypeId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250817095518_init'
)
BEGIN
    CREATE INDEX [IX_OilIngredients_IngredientId] ON [OilIngredients] ([IngredientId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250817095518_init'
)
BEGIN
    CREATE UNIQUE INDEX [IX_OilOccasionScores_OilId] ON [OilOccasionScores] ([OilId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250817095518_init'
)
BEGIN
    CREATE INDEX [IX_Oils_CompanyId] ON [Oils] ([CompanyId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250817095518_init'
)
BEGIN
    CREATE INDEX [IX_Oils_PriceListId] ON [Oils] ([PriceListId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250817095518_init'
)
BEGIN
    CREATE UNIQUE INDEX [IX_OilSeasonScores_OilId] ON [OilSeasonScores] ([OilId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250817095518_init'
)
BEGIN
    CREATE INDEX [IX_PriceListItems_PriceListId] ON [PriceListItems] ([PriceListId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250817095518_init'
)
BEGIN
    CREATE INDEX [IX_PriceListItems_ProductSizeId] ON [PriceListItems] ([ProductSizeId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250817095518_init'
)
BEGIN
    CREATE INDEX [IX_PriceListItems_ProductTypeId] ON [PriceListItems] ([ProductTypeId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250817095518_init'
)
BEGIN
    CREATE INDEX [IX_ProductSizes_ProductTypeId] ON [ProductSizes] ([ProductTypeId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250817095518_init'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250817095518_init', N'9.0.7');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250817223646_DeleteSizeUnit'
)
BEGIN
    DECLARE @var sysname;
    SELECT @var = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductSizes]') AND [c].[name] = N'SizeUnit');
    IF @var IS NOT NULL EXEC(N'ALTER TABLE [ProductSizes] DROP CONSTRAINT [' + @var + '];');
    ALTER TABLE [ProductSizes] DROP COLUMN [SizeUnit];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250817223646_DeleteSizeUnit'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250817223646_DeleteSizeUnit', N'9.0.7');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250831184105_Add_ImagePublicId_To_BottleDesign_Table'
)
BEGIN
    ALTER TABLE [BottleDesigns] ADD [ImagePublicId] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250831184105_Add_ImagePublicId_To_BottleDesign_Table'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250831184105_Add_ImagePublicId_To_BottleDesign_Table', N'9.0.7');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250903143148_AddImagePublicId'
)
BEGIN
    ALTER TABLE [Ingredients] ADD [ImagePublicId] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250903143148_AddImagePublicId'
)
BEGIN
    ALTER TABLE [Companies] ADD [ImagePublicId] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250903143148_AddImagePublicId'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250903143148_AddImagePublicId', N'9.0.7');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250906154553_MakeImageNullable'
)
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Ingredients]') AND [c].[name] = N'ImageUrl');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Ingredients] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Ingredients] ALTER COLUMN [ImageUrl] nvarchar(500) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250906154553_MakeImageNullable'
)
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Ingredients]') AND [c].[name] = N'ImagePublicId');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Ingredients] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Ingredients] ALTER COLUMN [ImagePublicId] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250906154553_MakeImageNullable'
)
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Companies]') AND [c].[name] = N'ImageUrl');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Companies] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Companies] ALTER COLUMN [ImageUrl] nvarchar(500) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250906154553_MakeImageNullable'
)
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Companies]') AND [c].[name] = N'ImagePublicId');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Companies] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Companies] ALTER COLUMN [ImagePublicId] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250906154553_MakeImageNullable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250906154553_MakeImageNullable', N'9.0.7');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261021102229_AddFinalProductsTable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261021102229_AddFinalProductsTable', N'9.0.7');
END;

COMMIT;
GO

