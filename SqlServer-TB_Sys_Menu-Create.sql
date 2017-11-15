/****** Object:  Table [dbo].[TB_Sys_Menu]    Script Date: 2017/10/31 19:11:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TB_Sys_Menu](
	[MenuId] [varchar](50) NOT NULL,
	[MenuType] [varchar](50) NULL,
	[MenuName] [varchar](150) NULL,
	[MenuLink] [varchar](150) NULL,
	[MenuIcon] [varchar](50) NULL,
	[MenuIconPath] [varchar](150) NULL,
	[MenuIsLeaf] [int] NULL,
	[MenuIsValid] [int] NULL,
	[MenuIsHidden] [int] NULL,
	[MenuNo] [int] NULL,
	[MenuRemark] [varchar](150) NULL,
	[MenuParentId] [varchar](50) NULL,
	[MenuOperation] [varchar](50) NULL,
	[MenuCreateBy] [varchar](50) NULL,
	[MenuCreateTime] [datetime] NULL,
	[MenuUpdateBy] [varchar](50) NULL,
	[MenuUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_TB_Sys_Menu] PRIMARY KEY CLUSTERED 
(
	[MenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


