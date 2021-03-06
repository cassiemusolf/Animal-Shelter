USE [Animal_Shelter]
GO
/****** Object:  Table [dbo].[animal]    Script Date: 2/21/2017 4:20:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[animal](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[breed] [varchar](255) NULL,
	[admittance_date] [datetime] NULL,
	[gender] [varchar](255) NULL,
	[type_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[type]    Script Date: 2/21/2017 4:20:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[type](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
