SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MeterReadings](
	[AccountId] [INT] NOT NULL,
	[MeterReadingDateTime] [DATETIME] NOT NULL,
	[MeterReadValue] [INT] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MeterReadings]  WITH CHECK ADD  CONSTRAINT [FK_MeterReadings_Accounts] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Accounts] ([AccountId])
GO

ALTER TABLE [dbo].[MeterReadings] CHECK CONSTRAINT [FK_MeterReadings_Accounts]
GO

