USE [master]
GO

/****** Object:  Database [Hotel]    Script Date: 24/12/2017 11:50:08 ******/
CREATE DATABASE [Hotel]
GO

USE [Hotel]
GO

/****** Object:  Table [dbo].[Message]    Script Date: 24/12/2017 11:51:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Message](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Message] [varchar](max) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Learned] [bit] NOT NULL,
 CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Message] ADD  CONSTRAINT [DF_Message_Date]  DEFAULT (getdate()) FOR [Date]
GO

ALTER TABLE [dbo].[Message] ADD  CONSTRAINT [DF_Message_Learned]  DEFAULT ((0)) FOR [Learned]
GO
