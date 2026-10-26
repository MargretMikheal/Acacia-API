-- ==============================================================================
-- Acacia Perfumery & Fragrance Formulation Platform
-- Production Domain Data Seeder Script (Idempotent)
-- Target Host: MonsterASP Hosted MS SQL Server (db_Acacia)
-- Generated: 2026-10-21
-- ==============================================================================

SET NOCOUNT ON;
BEGIN TRANSACTION;

-- 1. Ensure ProductTypes exist
IF NOT EXISTS (SELECT 1 FROM [ProductTypes] WHERE [Id] = 1)
BEGIN
    SET IDENTITY_INSERT [ProductTypes] ON;
    INSERT INTO [ProductTypes] ([Id], [NameAr], [NameEn], [CreationDate])
    VALUES
    (1, N'عطر', N'Perfume', GETUTCDATE()),
    (2, N'مخمريه', N'Mukhammaria', GETUTCDATE()),
    (3, N'بودي سبلاش', N'BodySplash', GETUTCDATE()),
    (4, N'شموع', N'Candle', GETUTCDATE()),
    (5, N'لوشن الجسم', N'BodyLotion', GETUTCDATE()),
    (6, N'مباخر', N'Incense', GETUTCDATE());
    SET IDENTITY_INSERT [ProductTypes] OFF;
END;

-- 2. Ensure ProductSizes exist
IF NOT EXISTS (SELECT 1 FROM [ProductSizes])
BEGIN
    INSERT INTO [ProductSizes] ([ProductTypeId], [SizeValue], [CreationDate])
    VALUES
    (1, 30.00, GETUTCDATE()),
    (1, 50.00, GETUTCDATE()),
    (1, 100.00, GETUTCDATE()),
    (2, 25.00, GETUTCDATE()),
    (2, 50.00, GETUTCDATE()),
    (3, 150.00, GETUTCDATE()),
    (3, 250.00, GETUTCDATE()),
    (4, 180.00, GETUTCDATE()),
    (4, 300.00, GETUTCDATE()),
    (5, 200.00, GETUTCDATE()),
    (5, 400.00, GETUTCDATE()),
    (6, 50.00, GETUTCDATE()),
    (6, 100.00, GETUTCDATE());
END;

-- 3. Ensure Companies exist
IF NOT EXISTS (SELECT 1 FROM [Companies])
BEGIN
    INSERT INTO [Companies] ([Name], [ImageUrl], [ImagePublicId], [CreationDate])
    VALUES
    (N'Givaudan Fine Fragrances', N'https://acacia-storage.s3.us-east-005.backblazeb2.com/companies/c1_givaudan_fine_fragrances.webp', N'companies/c1_givaudan_fine_fragrances.webp', GETUTCDATE()),
    (N'Firmenich Heritage Oils', N'https://acacia-storage.s3.us-east-005.backblazeb2.com/companies/c2_firmenich_heritage_oils.webp', N'companies/c2_firmenich_heritage_oils.webp', GETUTCDATE()),
    (N'IFF - International Flavors & Fragrances', N'https://acacia-storage.s3.us-east-005.backblazeb2.com/companies/c3_iff_flavors_fragrances.webp', N'companies/c3_iff_flavors_fragrances.webp', GETUTCDATE()),
    (N'Mane Grasse Artisans', N'https://acacia-storage.s3.us-east-005.backblazeb2.com/companies/c4_mane_grasse_artisans.webp', N'companies/c4_mane_grasse_artisans.webp', GETUTCDATE()),
    (N'Symrise Oriental Division', N'https://acacia-storage.s3.us-east-005.backblazeb2.com/companies/c5_symrise_oriental.webp', N'companies/c5_symrise_oriental.webp', GETUTCDATE()),
    (N'Al Haramain Oud Distillers', N'https://acacia-storage.s3.us-east-005.backblazeb2.com/companies/c6_al_haramain_distillers.webp', N'companies/c6_al_haramain_distillers.webp', GETUTCDATE()),
    (N'Ajmal Heritage Fragrances', N'https://acacia-storage.s3.us-east-005.backblazeb2.com/companies/c7_ajmal_heritage.webp', N'companies/c7_ajmal_heritage.webp', GETUTCDATE());
END;

-- 4. Ensure PriceLists exist
IF NOT EXISTS (SELECT 1 FROM [PriceLists])
BEGIN
    INSERT INTO [PriceLists] ([Name], [Description], [CreationDate])
    VALUES
    (N'Commercial Standard Tier', N'Base pricing matrix for standard retail consumer formulation sizes.', GETUTCDATE()),
    (N'Private Reserve Luxury Tier', N'High-concentration extract pricing with rare vintage harvests and premium margins.', GETUTCDATE());

    DECLARE @StdListId INT = (SELECT TOP 1 [Id] FROM [PriceLists] WHERE [Name] LIKE '%Standard%');
    DECLARE @ResListId INT = (SELECT TOP 1 [Id] FROM [PriceLists] WHERE [Name] LIKE '%Reserve%');

    INSERT INTO [PriceListItems] ([PriceListId], [ProductTypeId], [ProductSizeId], [Price], [CreationDate])
    SELECT
        @StdListId,
        ps.[ProductTypeId],
        ps.[Id],
        CASE ps.[ProductTypeId]
            WHEN 1 THEN ps.[SizeValue] * 0.85
            WHEN 2 THEN ps.[SizeValue] * 1.20
            WHEN 3 THEN ps.[SizeValue] * 0.25
            WHEN 4 THEN ps.[SizeValue] * 0.30
            WHEN 5 THEN ps.[SizeValue] * 0.20
            WHEN 6 THEN ps.[SizeValue] * 0.90
            ELSE 35.00
        END,
        GETUTCDATE()
    FROM [ProductSizes] ps;

    INSERT INTO [PriceListItems] ([PriceListId], [ProductTypeId], [ProductSizeId], [Price], [CreationDate])
    SELECT
        @ResListId,
        ps.[ProductTypeId],
        ps.[Id],
        ROUND(
            CASE ps.[ProductTypeId]
                WHEN 1 THEN ps.[SizeValue] * 0.85
                WHEN 2 THEN ps.[SizeValue] * 1.20
                WHEN 3 THEN ps.[SizeValue] * 0.25
                WHEN 4 THEN ps.[SizeValue] * 0.30
                WHEN 5 THEN ps.[SizeValue] * 0.20
                WHEN 6 THEN ps.[SizeValue] * 0.90
                ELSE 35.00
            END * 1.65, 2),
        GETUTCDATE()
    FROM [ProductSizes] ps;
END;

-- 5. Ensure Raw Ingredients exist
IF NOT EXISTS (SELECT 1 FROM [Ingredients])
BEGIN
    INSERT INTO [Ingredients] ([NameAr], [NameEn], [ImageUrl], [ImagePublicId], [CreationDate])
    VALUES
    (N'عود كمبودي عتيق', N'Vintage Cambodian Oud', N'https://acacia-storage.s3.us-east-005.backblazeb2.com/ingredients/ing_vintage_cambodian_oud.webp', N'ingredients/ing_vintage_cambodian_oud.webp', GETUTCDATE()),
    (N'ورد طائفي قطفة أولى', N'First Harvest Taif Rose', N'https://acacia-storage.s3.us-east-005.backblazeb2.com/ingredients/ing_first_harvest_taif_rose.webp', N'ingredients/ing_first_harvest_taif_rose.webp', GETUTCDATE()),
    (N'عنبر خام حوتي فاخر', N'White Oceanic Ambergris', N'https://acacia-storage.s3.us-east-005.backblazeb2.com/ingredients/ing_white_oceanic_ambergris.webp', N'ingredients/ing_white_oceanic_ambergris.webp', GETUTCDATE()),
    (N'مسك الغزال الأبيض النقي', N'Pure White Silk Musk', N'https://acacia-storage.s3.us-east-005.backblazeb2.com/ingredients/ing_pure_white_silk_musk.webp', N'ingredients/ing_pure_white_silk_musk.webp', GETUTCDATE()),
    (N'برغموت كالابريا الإيطالي', N'Calabrian Bergamot', N'https://acacia-storage.s3.us-east-005.backblazeb2.com/ingredients/ing_calabrian_bergamot.webp', N'ingredients/ing_calabrian_bergamot.webp', GETUTCDATE()),
    (N'صندل ميسور الهندي', N'Mysore Sandalwood', N'https://acacia-storage.s3.us-east-005.backblazeb2.com/ingredients/ing_mysore_sandalwood.webp', N'ingredients/ing_mysore_sandalwood.webp', GETUTCDATE()),
    (N'حبهان هيل أخضر', N'Green Cardamom Pods', N'https://acacia-storage.s3.us-east-005.backblazeb2.com/ingredients/ing_green_cardamom_pods.webp', N'ingredients/ing_green_cardamom_pods.webp', GETUTCDATE()),
    (N'فانيليا بوربون مدغشقر', N'Madagascar Bourbon Vanilla', N'https://acacia-storage.s3.us-east-005.backblazeb2.com/ingredients/ing_madagascar_bourbon_vanilla.webp', N'ingredients/ing_madagascar_bourbon_vanilla.webp', GETUTCDATE()),
    (N'لبان حوجري عماني فاخر', N'Royal Omani Hojari Frankincense', N'https://acacia-storage.s3.us-east-005.backblazeb2.com/ingredients/ing_royal_omani_hojari_frankincense.webp', N'ingredients/ing_royal_omani_hojari_frankincense.webp', GETUTCDATE()),
    (N'باتشولي نقي معتق', N'Aged Indonesian Patchouli', N'https://acacia-storage.s3.us-east-005.backblazeb2.com/ingredients/ing_aged_indonesian_patchouli.webp', N'ingredients/ing_aged_indonesian_patchouli.webp', GETUTCDATE()),
    (N'زعفران كشميري أحمر', N'Kashmiri Red Saffron', N'https://acacia-storage.s3.us-east-005.backblazeb2.com/ingredients/ing_kashmiri_red_saffron.webp', N'ingredients/ing_kashmiri_red_saffron.webp', GETUTCDATE()),
    (N'أمبروكسان نقي', N'Pure Ambroxan Molecule', N'https://acacia-storage.s3.us-east-005.backblazeb2.com/ingredients/ing_pure_ambroxan_molecule.webp', N'ingredients/ing_pure_ambroxan_molecule.webp', GETUTCDATE());
END;

-- 6. Ensure BottleDesigns exist
IF NOT EXISTS (SELECT 1 FROM [BottleDesigns])
BEGIN
    INSERT INTO [BottleDesigns] ([Name], [ProductTypeId], [Price], [ImageUrl], [ImagePublicId], [CreationDate])
    VALUES
    (N'Royal Amber Heavy Crystal Flacon 50ml', 1, 18.50, N'https://acacia-storage.s3.us-east-005.backblazeb2.com/bottles/btl_royal_amber_crystal_flacon_50ml.webp', N'bottles/btl_royal_amber_crystal_flacon_50ml.webp', GETUTCDATE()),
    (N'Obsidian Noir Matte Flacon 100ml', 1, 24.00, N'https://acacia-storage.s3.us-east-005.backblazeb2.com/bottles/btl_obsidian_noir_matte_flacon_100ml.webp', N'bottles/btl_obsidian_noir_matte_flacon_100ml.webp', GETUTCDATE()),
    (N'Golden Filigree Oriental Vessel 50ml', 1, 32.00, N'https://acacia-storage.s3.us-east-005.backblazeb2.com/bottles/btl_golden_filigree_oriental_vessel_50ml.webp', N'bottles/btl_golden_filigree_oriental_vessel_50ml.webp', GETUTCDATE()),
    (N'Handcrafted Ceramic Mukhammaria Jar 25g', 2, 12.00, N'https://acacia-storage.s3.us-east-005.backblazeb2.com/bottles/btl_ceramic_mukhammaria_jar_25g.webp', N'bottles/btl_ceramic_mukhammaria_jar_25g.webp', GETUTCDATE()),
    (N'Frosted Amber Glass Jar with Wooden Lid 180g', 4, 14.50, N'https://acacia-storage.s3.us-east-005.backblazeb2.com/bottles/btl_frosted_amber_candle_jar_180g.webp', N'bottles/btl_frosted_amber_candle_jar_180g.webp', GETUTCDATE()),
    (N'Fine Mist Aluminum Atomizer 250ml', 3, 9.50, N'https://acacia-storage.s3.us-east-005.backblazeb2.com/bottles/btl_fine_mist_aluminum_atomizer_250ml.webp', N'bottles/btl_fine_mist_aluminum_atomizer_250ml.webp', GETUTCDATE()),
    (N'Traditional Engraved Brass Mabkhara & Casket 50g', 6, 28.00, N'https://acacia-storage.s3.us-east-005.backblazeb2.com/bottles/btl_engraved_brass_mabkhara_50g.webp', N'bottles/btl_engraved_brass_mabkhara_50g.webp', GETUTCDATE());
END;

-- 7. Ensure Oils and Scores exist
IF NOT EXISTS (SELECT 1 FROM [Oils])
BEGIN
    DECLARE @CmpHaramain INT = (SELECT TOP 1 [Id] FROM [Companies] WHERE [Name] LIKE '%Haramain%');
    DECLARE @CmpAjmal INT = (SELECT TOP 1 [Id] FROM [Companies] WHERE [Name] LIKE '%Ajmal%');
    DECLARE @CmpGivaudan INT = (SELECT TOP 1 [Id] FROM [Companies] WHERE [Name] LIKE '%Givaudan%');
    DECLARE @CmpFirmenich INT = (SELECT TOP 1 [Id] FROM [Companies] WHERE [Name] LIKE '%Firmenich%');

    DECLARE @PlStd INT = (SELECT TOP 1 [Id] FROM [PriceLists] WHERE [Name] LIKE '%Standard%');
    DECLARE @PlRes INT = (SELECT TOP 1 [Id] FROM [PriceLists] WHERE [Name] LIKE '%Reserve%');

    -- Oil 1
    INSERT INTO [Oils] ([Name], [CompanyId], [Category], [Style], [DescriptionAr], [DescriptionEn], [PriceListId], [CreationDate])
    VALUES (N'Royal Oud Al-Malaki', @CmpHaramain, 3, 1,
            N'تركيبة شرقية ملكية تعتمد على العود الكمبودي المعتق والعنبر البحري مع لمسات خشب الصندل الدافئة.',
            N'A majestic royal oriental accord blending aged Cambodian agarwood, marine ambergris, and warm Mysore sandalwood.',
            @PlRes, GETUTCDATE());
    DECLARE @Oil1Id INT = SCOPE_IDENTITY();

    INSERT INTO [OilSeasonScores] ([OilId], [Summer], [Winter], [Fall], [Spring], [CreationDate])
    VALUES (@Oil1Id, 0.20, 1.00, 0.85, 0.40, GETUTCDATE());

    INSERT INTO [OilOccasionScores] ([OilId], [Formal], [Casual], [Night], [Sport], [CreationDate])
    VALUES (@Oil1Id, 1.00, 0.30, 0.95, 0.05, GETUTCDATE());

    -- Oil 2
    INSERT INTO [Oils] ([Name], [CompanyId], [Category], [Style], [DescriptionAr], [DescriptionEn], [PriceListId], [CreationDate])
    VALUES (N'Desert Rose & Amber', @CmpAjmal, 2, 1,
            N'تناغم فاخر يجمع بين الورد الطائفي النقي والعنبر الحوتي والزعفران مع قاعدة مسكية مخملية.',
            N'An opulent oriental floral elixir of precious Taif rose petals, warm glowing amber, and Kashmiri saffron threads.',
            @PlStd, GETUTCDATE());
    DECLARE @Oil2Id INT = SCOPE_IDENTITY();

    INSERT INTO [OilSeasonScores] ([OilId], [Summer], [Winter], [Fall], [Spring], [CreationDate])
    VALUES (@Oil2Id, 0.50, 0.80, 0.75, 0.90, GETUTCDATE());

    INSERT INTO [OilOccasionScores] ([OilId], [Formal], [Casual], [Night], [Sport], [CreationDate])
    VALUES (@Oil2Id, 0.85, 0.65, 0.90, 0.20, GETUTCDATE());

    -- Oil 3
    INSERT INTO [Oils] ([Name], [CompanyId], [Category], [Style], [DescriptionAr], [DescriptionEn], [PriceListId], [CreationDate])
    VALUES (N'Citrus Bergamot & Vetiver', @CmpGivaudan, 1, 2,
            N'نفحات حمضية منعشة ومشرقة من برغموت كالابريا ونجيل الهند الصيفي مع لمسات من الهيل الأخضر.',
            N'A sparkling Mediterranean burst of sun-drenched Calabrian bergamot, crisp vetiver, and green crushed cardamom.',
            @PlStd, GETUTCDATE());
    DECLARE @Oil3Id INT = SCOPE_IDENTITY();

    INSERT INTO [OilSeasonScores] ([OilId], [Summer], [Winter], [Fall], [Spring], [CreationDate])
    VALUES (@Oil3Id, 1.00, 0.15, 0.35, 0.90, GETUTCDATE());

    INSERT INTO [OilOccasionScores] ([OilId], [Formal], [Casual], [Night], [Sport], [CreationDate])
    VALUES (@Oil3Id, 0.70, 0.95, 0.45, 0.90, GETUTCDATE());

    -- Oil 4
    INSERT INTO [Oils] ([Name], [CompanyId], [Category], [Style], [DescriptionAr], [DescriptionEn], [PriceListId], [CreationDate])
    VALUES (N'Imperial Cardamom & Sandalwood', @CmpFirmenich, 3, 1,
            N'تركيبة دافئة راقية من الهيل العطري وفانيليا بوربون مع خشب الصندل الميسوري النقي.',
            N'An aristocratic spiced woody composition showcasing green aromatic cardamom, Madagascar vanilla, and creamy Mysore sandalwood.',
            @PlRes, GETUTCDATE());
    DECLARE @Oil4Id INT = SCOPE_IDENTITY();

    INSERT INTO [OilSeasonScores] ([OilId], [Summer], [Winter], [Fall], [Spring], [CreationDate])
    VALUES (@Oil4Id, 0.30, 0.95, 0.90, 0.60, GETUTCDATE());

    INSERT INTO [OilOccasionScores] ([OilId], [Formal], [Casual], [Night], [Sport], [CreationDate])
    VALUES (@Oil4Id, 0.95, 0.60, 0.85, 0.10, GETUTCDATE());

    -- Oil 5
    INSERT INTO [Oils] ([Name], [CompanyId], [Category], [Style], [DescriptionAr], [DescriptionEn], [PriceListId], [CreationDate])
    VALUES (N'Velvet Musk & White Floral', @CmpGivaudan, 2, 2,
            N'عبير ناعم رقيق يجمع بين المسك الأبيض النقي والزهور الحريرية وقاعدة خفيفة من الفانيليا.',
            N'A cloud of pure white silk musk enveloping gentle dewy petals and subtle creamy Madagascar vanilla.',
            @PlStd, GETUTCDATE());
    DECLARE @Oil5Id INT = SCOPE_IDENTITY();

    INSERT INTO [OilSeasonScores] ([OilId], [Summer], [Winter], [Fall], [Spring], [CreationDate])
    VALUES (@Oil5Id, 0.85, 0.50, 0.60, 1.00, GETUTCDATE());

    INSERT INTO [OilOccasionScores] ([OilId], [Formal], [Casual], [Night], [Sport], [CreationDate])
    VALUES (@Oil5Id, 0.75, 0.95, 0.70, 0.50, GETUTCDATE());

    -- Oil 6
    INSERT INTO [Oils] ([Name], [CompanyId], [Category], [Style], [DescriptionAr], [DescriptionEn], [PriceListId], [CreationDate])
    VALUES (N'Smoky Frankincense & Leather', @CmpFirmenich, 1, 1,
            N'عطر شرقي أسطوري من اللبان الحوجري العماني والجلود الفاخرة وخشب الصندل والعود.',
            N'A deeply evocative resinous masterwork featuring Omani green frankincense smoke, burnished leather, and dark woods.',
            @PlRes, GETUTCDATE());
    DECLARE @Oil6Id INT = SCOPE_IDENTITY();

    INSERT INTO [OilSeasonScores] ([OilId], [Summer], [Winter], [Fall], [Spring], [CreationDate])
    VALUES (@Oil6Id, 0.10, 1.00, 0.90, 0.30, GETUTCDATE());

    INSERT INTO [OilOccasionScores] ([OilId], [Formal], [Casual], [Night], [Sport], [CreationDate])
    VALUES (@Oil6Id, 0.95, 0.35, 1.00, 0.00, GETUTCDATE());

    -- Formulation Links (OilIngredients)
    DECLARE @IngOud INT = (SELECT TOP 1 [Id] FROM [Ingredients] WHERE [NameEn] LIKE '%Oud%');
    DECLARE @IngAmber INT = (SELECT TOP 1 [Id] FROM [Ingredients] WHERE [NameEn] LIKE '%Ambergris%');
    DECLARE @IngSandal INT = (SELECT TOP 1 [Id] FROM [Ingredients] WHERE [NameEn] LIKE '%Sandalwood%');
    DECLARE @IngRose INT = (SELECT TOP 1 [Id] FROM [Ingredients] WHERE [NameEn] LIKE '%Rose%');
    DECLARE @IngSaffron INT = (SELECT TOP 1 [Id] FROM [Ingredients] WHERE [NameEn] LIKE '%Saffron%');
    DECLARE @IngBergamot INT = (SELECT TOP 1 [Id] FROM [Ingredients] WHERE [NameEn] LIKE '%Bergamot%');
    DECLARE @IngCardamom INT = (SELECT TOP 1 [Id] FROM [Ingredients] WHERE [NameEn] LIKE '%Cardamom%');
    DECLARE @IngMusk INT = (SELECT TOP 1 [Id] FROM [Ingredients] WHERE [NameEn] LIKE '%Musk%');
    DECLARE @IngVanilla INT = (SELECT TOP 1 [Id] FROM [Ingredients] WHERE [NameEn] LIKE '%Vanilla%');
    DECLARE @IngFrankincense INT = (SELECT TOP 1 [Id] FROM [Ingredients] WHERE [NameEn] LIKE '%Frankincense%');

    IF (@IngOud IS NOT NULL AND @IngAmber IS NOT NULL AND @IngSandal IS NOT NULL)
    BEGIN
        INSERT INTO [OilIngredients] ([OilId], [IngredientId], [Percentage])
        VALUES
        (@Oil1Id, @IngOud, 45.00),
        (@Oil1Id, @IngAmber, 30.00),
        (@Oil1Id, @IngSandal, 25.00);
    END;

    IF (@IngRose IS NOT NULL AND @IngAmber IS NOT NULL AND @IngSaffron IS NOT NULL)
    BEGIN
        INSERT INTO [OilIngredients] ([OilId], [IngredientId], [Percentage])
        VALUES
        (@Oil2Id, @IngRose, 50.00),
        (@Oil2Id, @IngAmber, 30.00),
        (@Oil2Id, @IngSaffron, 20.00);
    END;
END;

-- 8. Ensure FinalProducts (Multi-Category SKUs) exist
IF NOT EXISTS (SELECT 1 FROM [FinalProducts])
BEGIN
    DECLARE @O1 INT = (SELECT TOP 1 [Id] FROM [Oils] WHERE [Name] = N'Royal Oud Al-Malaki');
    DECLARE @O2 INT = (SELECT TOP 1 [Id] FROM [Oils] WHERE [Name] = N'Desert Rose & Amber');
    DECLARE @O3 INT = (SELECT TOP 1 [Id] FROM [Oils] WHERE [Name] = N'Citrus Bergamot & Vetiver');
    DECLARE @O6 INT = (SELECT TOP 1 [Id] FROM [Oils] WHERE [Name] = N'Smoky Frankincense & Leather');

    DECLARE @SzPerf50 INT = (SELECT TOP 1 [Id] FROM [ProductSizes] WHERE [ProductTypeId] = 1 AND [SizeValue] = 50.00);
    DECLARE @SzPerf100 INT = (SELECT TOP 1 [Id] FROM [ProductSizes] WHERE [ProductTypeId] = 1 AND [SizeValue] = 100.00);
    DECLARE @SzMukhammaria25 INT = (SELECT TOP 1 [Id] FROM [ProductSizes] WHERE [ProductTypeId] = 2 AND [SizeValue] = 25.00);
    DECLARE @SzSplash250 INT = (SELECT TOP 1 [Id] FROM [ProductSizes] WHERE [ProductTypeId] = 3 AND [SizeValue] = 250.00);
    DECLARE @SzCandle180 INT = (SELECT TOP 1 [Id] FROM [ProductSizes] WHERE [ProductTypeId] = 4 AND [SizeValue] = 180.00);
    DECLARE @SzIncense50 INT = (SELECT TOP 1 [Id] FROM [ProductSizes] WHERE [ProductTypeId] = 6 AND [SizeValue] = 50.00);

    DECLARE @BtlRoyalAmber INT = (SELECT TOP 1 [Id] FROM [BottleDesigns] WHERE [Name] LIKE '%Royal Amber%');
    DECLARE @BtlObsidianNoir INT = (SELECT TOP 1 [Id] FROM [BottleDesigns] WHERE [Name] LIKE '%Obsidian Noir%');
    DECLARE @BtlFiligree INT = (SELECT TOP 1 [Id] FROM [BottleDesigns] WHERE [Name] LIKE '%Golden Filigree%');
    DECLARE @BtlMukhammariaJar INT = (SELECT TOP 1 [Id] FROM [BottleDesigns] WHERE [Name] LIKE '%Mukhammaria Jar%');
    DECLARE @BtlCandleJar INT = (SELECT TOP 1 [Id] FROM [BottleDesigns] WHERE [Name] LIKE '%Amber Glass Jar%');
    DECLARE @BtlSplashAtomizer INT = (SELECT TOP 1 [Id] FROM [BottleDesigns] WHERE [Name] LIKE '%Mist Aluminum%');
    DECLARE @BtlIncenseCasket INT = (SELECT TOP 1 [Id] FROM [BottleDesigns] WHERE [Name] LIKE '%Mabkhara%');

    INSERT INTO [FinalProducts]
    ([FinalProductType], [OilId], [ProductTypeId], [ProductSizeId], [BottleDesignId], [BasePrice], [TotalPrice], [AlcoholPercentage], [WaxType], [BurnHours], [HasGlitter], [IsOilBased], [IncenseMaterial], [CreationDate])
    VALUES
    -- Perfumes
    (N'Perfume', @O1, 1, @SzPerf50, @BtlRoyalAmber, 65.00, 125.00, 25.00, NULL, NULL, NULL, NULL, NULL, GETUTCDATE()),
    (N'Perfume', @O1, 1, @SzPerf100, @BtlObsidianNoir, 95.00, 185.00, 20.00, NULL, NULL, NULL, NULL, NULL, GETUTCDATE()),
    (N'Perfume', @O2, 1, @SzPerf50, @BtlFiligree, 55.00, 110.00, 22.00, NULL, NULL, NULL, NULL, NULL, GETUTCDATE()),
    (N'Perfume', @O3, 1, @SzPerf100, @BtlObsidianNoir, 45.00, 95.00, 18.00, NULL, NULL, NULL, NULL, NULL, GETUTCDATE()),
    -- Candle
    (N'Candle', @O1, 4, @SzCandle180, @BtlCandleJar, 30.00, 65.00, NULL, N'Natural Soy & Coconut Wax Blend', 45, NULL, NULL, NULL, GETUTCDATE()),
    -- Mukhammaria
    (N'Mukhammaria', @O2, 2, @SzMukhammaria25, @BtlMukhammariaJar, 22.00, 48.00, NULL, NULL, NULL, NULL, 1, NULL, GETUTCDATE()),
    -- Body Splash
    (N'BodySplash', @O3, 3, @SzSplash250, @BtlSplashAtomizer, 18.00, 38.00, NULL, NULL, NULL, 0, NULL, NULL, GETUTCDATE()),
    -- Incense
    (N'Incense', @O6, 6, @SzIncense50, @BtlIncenseCasket, 40.00, 85.00, NULL, NULL, NULL, NULL, NULL, N'Selected Agarwood Chips & Omani Hojari Resin', GETUTCDATE());
END;

COMMIT TRANSACTION;
PRINT 'Acacia domain data seeded successfully.';
