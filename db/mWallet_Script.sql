USE [master]
GO
/****** Object:  Database [mWallet]    Script Date: 24/01/2022 8:03:18 PM ******/
CREATE DATABASE [mWallet]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'mWallet', FILENAME = N'C:\db\mWallet.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'mWallet_log', FILENAME = N'C:\db\mWallet_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [mWallet] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [mWallet].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [mWallet] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [mWallet] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [mWallet] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [mWallet] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [mWallet] SET ARITHABORT OFF 
GO
ALTER DATABASE [mWallet] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [mWallet] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [mWallet] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [mWallet] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [mWallet] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [mWallet] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [mWallet] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [mWallet] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [mWallet] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [mWallet] SET  DISABLE_BROKER 
GO
ALTER DATABASE [mWallet] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [mWallet] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [mWallet] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [mWallet] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [mWallet] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [mWallet] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [mWallet] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [mWallet] SET RECOVERY FULL 
GO
ALTER DATABASE [mWallet] SET  MULTI_USER 
GO
ALTER DATABASE [mWallet] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [mWallet] SET DB_CHAINING OFF 
GO
ALTER DATABASE [mWallet] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [mWallet] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [mWallet] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'mWallet', N'ON'
GO
ALTER DATABASE [mWallet] SET QUERY_STORE = OFF
GO
USE [mWallet]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [mWallet]
GO
/****** Object:  Table [dbo].[Expenses]    Script Date: 24/01/2022 8:03:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Expenses](
	[Date] [date] NOT NULL,
	[Description] [varchar](100) NOT NULL,
	[Amount] [decimal](10, 2) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Income]    Script Date: 24/01/2022 8:03:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Income](
	[Date] [date] NULL,
	[Description] [varchar](100) NULL,
	[Amount] [decimal](10, 2) NULL,
	[Type] [char](1) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 24/01/2022 8:03:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UUID] [varchar](100) NOT NULL,
	[Username] [varchar](100) NOT NULL,
	[Password] [varchar](100) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Wallet]    Script Date: 24/01/2022 8:03:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Wallet](
	[Current_Balance] [decimal](10, 2) NOT NULL,
	[Bank_Balance] [decimal](10, 2) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Wallet_History]    Script Date: 24/01/2022 8:03:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Wallet_History](
	[Date] [date] NOT NULL,
	[Current_Balance] [decimal](10, 2) NOT NULL,
	[Bank_Balance] [decimal](10, 2) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[InsertExpenses]    Script Date: 24/01/2022 8:03:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertExpenses]
	@date NVARCHAR(100),
	@desc NVARCHAR(100),
	@amount DECIMAL(10,2),
	@type CHAR
AS
DECLARE
	@currentAmt DECIMAL(10,2);

	SET NOCOUNT ON;
	INSERT INTO Expenses VALUES (@date, @desc, @amount);

	IF (@type = 'C') 
		BEGIN
			SELECT @currentAmt = Current_Balance FROM Wallet;
			UPDATE Wallet SET Current_Balance = @currentAmt - @amount;
		END
	ELSE
		BEGIN
			SELECT @currentAmt = Bank_Balance FROM Wallet;
			UPDATE Wallet SET Bank_Balance = @currentAmt - @amount;
		END



GO
/****** Object:  StoredProcedure [dbo].[InsertIncome]    Script Date: 24/01/2022 8:03:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--EXEC InsertExpenses @date = '01-21-2022', @desc = 'test', @amount = 0.05, @type = 'C';

CREATE PROCEDURE [dbo].[InsertIncome]
	@date NVARCHAR(100),
	@desc NVARCHAR(100),
	@amount DECIMAL(10,2),
	@type CHAR
AS
DECLARE
	@currentAmt DECIMAL(10,2);

	SET NOCOUNT ON;
	INSERT INTO Income VALUES (@date, @desc, @amount, @type);

	IF (@type = 'C') 
		BEGIN
			SELECT @currentAmt = Current_Balance FROM Wallet;
			UPDATE Wallet SET Current_Balance = @currentAmt + @amount;
		END
	ELSE
		BEGIN
			SELECT @currentAmt = Bank_Balance FROM Wallet;
			UPDATE Wallet SET Bank_Balance = @currentAmt + @amount;
		END
GO
USE [master]
GO
ALTER DATABASE [mWallet] SET  READ_WRITE 
GO
