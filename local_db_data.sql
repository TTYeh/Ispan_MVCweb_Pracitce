USE [dbDemo]
GO
/****** Object:  User [sa5]    Script Date: 2023/3/13 下午 05:00:28 ******/
CREATE USER [sa5] FOR LOGIN [sa5] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [sa5]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 2023/3/13 下午 05:00:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[fId] [int] IDENTITY(1,1) NOT NULL,
	[fName] [nvarchar](50) NULL,
	[fPhone] [nvarchar](50) NULL,
	[fEmail] [nvarchar](50) NULL,
	[fAddress] [nvarchar](50) NULL,
	[fPassword] [nvarchar](50) NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[fId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 2023/3/13 下午 05:00:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[fId] [int] IDENTITY(1,1) NOT NULL,
	[fName] [nvarchar](50) NULL,
	[fQty] [int] NULL,
	[fCost] [money] NULL,
	[fPrice] [money] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[fId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tshopingCart]    Script Date: 2023/3/13 下午 05:00:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tshopingCart](
	[fId] [int] IDENTITY(1,1) NOT NULL,
	[fDate] [nvarchar](50) NULL,
	[fCustomerId] [int] NULL,
	[fProductId] [int] NULL,
	[fCount] [int] NULL,
	[fPrice] [money] NULL,
 CONSTRAINT [PK_tshopingCart] PRIMARY KEY CLUSTERED 
(
	[fId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
