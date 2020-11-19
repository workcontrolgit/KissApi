-- 1.Create database KissApiDB
CREATE DATABASE KissApiDB
GO
USE KissApiDB
GO
-- 2. Create table Products 
CREATE TABLE [dbo].[Products](
	[Id] [uniqueidentifier] ROWGUIDCOL NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Barcode] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Rate] [decimal](18, 2) NOT NULL,
	[AddedOn] [datetime] NOT NULL,
	[ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO