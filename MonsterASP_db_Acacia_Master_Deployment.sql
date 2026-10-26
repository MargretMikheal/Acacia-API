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
    WHERE [MigrationId] = N'20250811103741_Initial_Identity_Migration'
)
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250811103741_Initial_Identity_Migration'
)
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [FirstName] nvarchar(max) NOT NULL,
        [LastName] nvarchar(max) NOT NULL,
        [Address] nvarchar(max) NULL,
        [Country] nvarchar(max) NULL,
        [CreatedAt] datetime2 NOT NULL,
        [LastLoginDate] datetime2 NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250811103741_Initial_Identity_Migration'
)
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250811103741_Initial_Identity_Migration'
)
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250811103741_Initial_Identity_Migration'
)
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250811103741_Initial_Identity_Migration'
)
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250811103741_Initial_Identity_Migration'
)
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250811103741_Initial_Identity_Migration'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
        SET IDENTITY_INSERT [AspNetRoles] ON;
    EXEC(N'INSERT INTO [AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName])
    VALUES (N''8e445865-a24d-4543-a6c6-9443d048cdb9'', NULL, N''User'', N''USER''),
    (N''cbc43a8e-f7bb-4445-baaf-1add431ffbbf'', NULL, N''Administrator'', N''ADMINISTRATOR'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
        SET IDENTITY_INSERT [AspNetRoles] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250811103741_Initial_Identity_Migration'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'Address', N'ConcurrencyStamp', N'Country', N'CreatedAt', N'Email', N'EmailConfirmed', N'FirstName', N'LastLoginDate', N'LastName', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
        SET IDENTITY_INSERT [AspNetUsers] ON;
    EXEC(N'INSERT INTO [AspNetUsers] ([Id], [AccessFailedCount], [Address], [ConcurrencyStamp], [Country], [CreatedAt], [Email], [EmailConfirmed], [FirstName], [LastLoginDate], [LastName], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName])
    VALUES (N''7e445865-a24d-4543-b6c6-9443d048cdb9'', 0, N''123 Nasser Street, Caito City, GC 12345'', N''c27d4b01-88f5-485b-8b56-f3324ca956a8'', N''Egypt'', ''2025-08-11T10:37:40.5740948Z'', N''admin@acacia.com'', CAST(1 AS bit), N''System'', NULL, N''Admin'', CAST(0 AS bit), NULL, N''ADMIN@ACACIA.COM'', N''ADMIN@ACACIA.COM'', N''AQAAAAIAAYagAAAAEIueYJFXZvDVbR1Hej5lFWNIDKQyeFbnvV8RiS6ab9sH7zzdAno4Js1iyGCzR0G0tQ=='', NULL, CAST(0 AS bit), N''c88c1084-e82c-4611-a217-397b403608a5'', CAST(0 AS bit), N''admin@acacia''),
    (N''9e224968-33e4-4652-b7b7-8574d048cdb9'', 0, N''123 Nasser Street, Caito City, GC 12345'', N''771a185c-c780-4e03-b371-cf0f228cf1b7'', N''Egypt'', ''2025-08-11T10:37:40.5297226Z'', N''user@acacia.com'', CAST(1 AS bit), N''System'', NULL, N''User'', CAST(0 AS bit), NULL, N''USER@ACACIA.COM'', N''USER@GAMEIT.COM'', N''AQAAAAIAAYagAAAAEKvz8u7N6xolDPcqrpQsG/SaP4nAkAbR+R8INA4JryHexuhHHg90C8ljq9I/jv+vgw=='', NULL, CAST(0 AS bit), N''4b5cc570-b801-4de3-91c9-5d843f57bd3e'', CAST(0 AS bit), N''user@acacia'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'Address', N'ConcurrencyStamp', N'Country', N'CreatedAt', N'Email', N'EmailConfirmed', N'FirstName', N'LastLoginDate', N'LastName', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
        SET IDENTITY_INSERT [AspNetUsers] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250811103741_Initial_Identity_Migration'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserRoles]'))
        SET IDENTITY_INSERT [AspNetUserRoles] ON;
    EXEC(N'INSERT INTO [AspNetUserRoles] ([RoleId], [UserId])
    VALUES (N''cbc43a8e-f7bb-4445-baaf-1add431ffbbf'', N''7e445865-a24d-4543-b6c6-9443d048cdb9''),
    (N''8e445865-a24d-4543-a6c6-9443d048cdb9'', N''9e224968-33e4-4652-b7b7-8574d048cdb9'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserRoles]'))
        SET IDENTITY_INSERT [AspNetUserRoles] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250811103741_Initial_Identity_Migration'
)
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250811103741_Initial_Identity_Migration'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250811103741_Initial_Identity_Migration'
)
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250811103741_Initial_Identity_Migration'
)
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250811103741_Initial_Identity_Migration'
)
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250811103741_Initial_Identity_Migration'
)
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250811103741_Initial_Identity_Migration'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250811103741_Initial_Identity_Migration'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250811103741_Initial_Identity_Migration', N'9.0.7');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250814081006_Update_Identity_ApplicationUser_Table'
)
BEGIN
    EXEC(N'DELETE FROM [AspNetUserRoles]
    WHERE [RoleId] = N''cbc43a8e-f7bb-4445-baaf-1add431ffbbf'' AND [UserId] = N''7e445865-a24d-4543-b6c6-9443d048cdb9'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250814081006_Update_Identity_ApplicationUser_Table'
)
BEGIN
    EXEC(N'DELETE FROM [AspNetUserRoles]
    WHERE [RoleId] = N''8e445865-a24d-4543-a6c6-9443d048cdb9'' AND [UserId] = N''9e224968-33e4-4652-b7b7-8574d048cdb9'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250814081006_Update_Identity_ApplicationUser_Table'
)
BEGIN
    EXEC(N'DELETE FROM [AspNetUsers]
    WHERE [Id] = N''7e445865-a24d-4543-b6c6-9443d048cdb9'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250814081006_Update_Identity_ApplicationUser_Table'
)
BEGIN
    EXEC(N'DELETE FROM [AspNetUsers]
    WHERE [Id] = N''9e224968-33e4-4652-b7b7-8574d048cdb9'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250814081006_Update_Identity_ApplicationUser_Table'
)
BEGIN
    DECLARE @var sysname;
    SELECT @var = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'CreatedAt');
    IF @var IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var + '];');
    ALTER TABLE [AspNetUsers] ADD DEFAULT (GETUTCDATE()) FOR [CreatedAt];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250814081006_Update_Identity_ApplicationUser_Table'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250814081006_Update_Identity_ApplicationUser_Table', N'9.0.7');
END;

COMMIT;
GO

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
    VALUES (1, ''2024-01-01T00:00:00.0000000'', N''???'', N''Perfume''),
    (2, ''2024-01-01T00:00:00.0000000'', N''??????'', N''Mukhammaria''),
    (3, ''2024-01-01T00:00:00.0000000'', N''???? ?????'', N''BodySplash''),
    (4, ''2024-01-01T00:00:00.0000000'', N''????'', N''Candle''),
    (5, ''2024-01-01T00:00:00.0000000'', N''???? ?????'', N''BodyLotion''),
    (6, ''2024-01-01T00:00:00.0000000'', N''?????'', N''Incense'')');
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

