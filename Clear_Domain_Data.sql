-- ==============================================================================
-- Acacia Perfumery Platform - Domain Data Reset Script
-- Description: Safely purges all domain catalog rows (SKUs, oils, packaging,
--              ingredients, price lists, and companies) for clean manual data entry.
-- Note: Leaves ASP.NET Core Identity tables ([AspNetUsers], [AspNetRoles], etc.)
--       completely intact so administrator accounts and logins remain functional.
-- ==============================================================================

BEGIN TRANSACTION;

BEGIN TRY
    -- 1. Delete dependent leaf records (Foreign Keys)
    DELETE FROM [FinalProducts];
    DELETE FROM [OilIngredients];
    DELETE FROM [OilSeasonScores];
    DELETE FROM [OilOccasionScores];

    -- 2. Delete core catalog records
    DELETE FROM [Oils];
    DELETE FROM [BottleDesigns];
    DELETE FROM [Ingredients];
    DELETE FROM [PriceListItems];
    DELETE FROM [PriceLists];
    DELETE FROM [Companies];
    DELETE FROM [ProductSizes];
    DELETE FROM [ProductTypes];

    -- 3. Reset auto-increment identity seed counters
    DBCC CHECKIDENT ('FinalProducts', RESEED, 0);
    DBCC CHECKIDENT ('Oils', RESEED, 0);
    DBCC CHECKIDENT ('BottleDesigns', RESEED, 0);
    DBCC CHECKIDENT ('Ingredients', RESEED, 0);
    DBCC CHECKIDENT ('PriceListItems', RESEED, 0);
    DBCC CHECKIDENT ('PriceLists', RESEED, 0);
    DBCC CHECKIDENT ('Companies', RESEED, 0);
    DBCC CHECKIDENT ('ProductSizes', RESEED, 0);
    DBCC CHECKIDENT ('ProductTypes', RESEED, 0);

    COMMIT TRANSACTION;
    PRINT 'Acacia domain data successfully purged. Tables are now clean and ready for manual data entry.';
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
    RAISERROR (@ErrorMessage, 16, 1);
END CATCH;
