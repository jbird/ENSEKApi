CREATE OR ALTER PROCEDURE dbo.PopulateMeterReadings
    @MeterReadings MeterReadingTableType READONLY
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO
        dbo.MeterReadings
        (
            AccountId,
            MeterReadingDateTime,
            MeterReadValue
        )
    SELECT
        AccountId, ReadingDateTime, ReadValue
    FROM
        @MeterReadings;
END;
