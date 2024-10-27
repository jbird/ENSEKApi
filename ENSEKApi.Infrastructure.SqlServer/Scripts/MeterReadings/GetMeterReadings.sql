CREATE OR ALTER PROCEDURE dbo.GetMeterReadings
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        AccountId,
        MeterReadingDateTime,
        MeterReadValue
    FROM
        dbo.MeterReadings;
END
