/****** Object:  Table [dbo].[TB_Sys_Log]    Script Date: 2017/11/2 15:18:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TB_Sys_Log](
	[Id] [varchar](50) NOT NULL,
	[Operator] [varchar](50) NULL,
	[OperatorIP] [varchar](50) NULL,
	[Message] [varchar](max) NULL,
	[Result] [varchar](20) NULL,
	[Type] [varchar](20) NULL,
	[Module] [varchar](20) NULL,
	[CreateTime] [datetime] NULL,
 CONSTRAINT [PK_TB_Sys_Log] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


