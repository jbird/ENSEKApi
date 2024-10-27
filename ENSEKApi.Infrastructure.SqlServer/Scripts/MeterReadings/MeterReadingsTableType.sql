IF NOT EXISTS(SELECT 1 FROM sys.types WHERE name = 'dbo.MeterReadingTableType' AND is_table_type = 1 AND SCHEMA_ID('dbo') = schema_id)
BEGIN
    CREATE TYPE dbo.MeterReadingTableType AS TABLE
    (
        AccountId INT,
        ReadingDateTime DATETIMEOFFSET,
        ReadValue INT
    );
END
