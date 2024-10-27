CREATE OR ALTER PROCEDURE dbo.GetAccounts
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        AccountId,
        FirstName,
        LastName
    FROM
        dbo.Accounts;
END
