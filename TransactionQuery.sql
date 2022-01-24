CREATE DATABASE TransactionDB;

CREATE TABLE Transactions (
	[TransactionId] [nvarchar](50) PRIMARY KEY NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[CurrencyCode] [nvarchar](3) NOT NULL,
	[TransactionDate] [datetime2](7) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
)

CREATE TABLE TransactionLog(
	[id] [nvarchar](450) PRIMARY KEY NOT NULL,
	[filename] [nvarchar](max) NOT NULL,
	[process] [nvarchar](max) NOT NULL,
	[time] [datetime2](7) NOT NULL,
)