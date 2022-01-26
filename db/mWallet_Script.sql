USE [master];  
GO  
CREATE DATABASE [mWallet]
ON   
( NAME = mWallet,  
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\saledat.mdf',  
    SIZE = 8192KB,  
    MAXSIZE = UNLIMITED,  
    FILEGROWTH = 65536KB )  
LOG ON  
( NAME = mWallet_log,  
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\salelog.ldf',  
    SIZE = 8192KB,  
    MAXSIZE = 2048GB,  
    FILEGROWTH = 65536KB );  
GO

CREATE TABLE [dbo].[Expenses](
	[Date] [datetime] NULL,
	[Description] [varchar](100) NOT NULL,
	[Amount] [decimal](10, 2) NULL
) ON [PRIMARY]

CREATE TABLE [dbo].[Expenses_AT](
	[Date] [datetime] NULL,
	[Description] [varchar](100) NOT NULL,
	[Amount] [decimal](10, 2) NULL,
	[Type] [char](1) NOT NULL,
	[Action] [varchar](50) NULL
) ON [PRIMARY]

CREATE TABLE [dbo].[Income](
	[Date] [datetime] NULL,
	[Description] [varchar](100) NULL,
	[Amount] [decimal](10, 2) NULL,
	[Type] [char](1) NULL
) ON [PRIMARY]

CREATE TABLE [dbo].[Income_AT](
	[Date] [datetime] NULL,
	[Description] [varchar](100) NOT NULL,
	[Amount] [decimal](10, 2) NULL,
	[Type] [char](1) NOT NULL,
	[Action] [varchar](50) NULL
) ON [PRIMARY]

CREATE TABLE [dbo].[Users](
	[UUID] [varchar](100) NOT NULL,
	[Username] [varchar](100) NOT NULL,
	[Password] [varchar](100) NOT NULL
) ON [PRIMARY]

CREATE TABLE [dbo].[Wallet](
	[Current_Balance] [decimal](10, 2) NOT NULL,
	[Bank_Balance] [decimal](10, 2) NOT NULL
) ON [PRIMARY]

CREATE TABLE [dbo].[Wallet_History](
	[Date] [datetime] NULL,
	[Current_Balance] [decimal](10, 2) NOT NULL,
	[Bank_Balance] [decimal](10, 2) NOT NULL
) ON [PRIMARY]

INSERT INTO Wallet VALUES (0,0)
