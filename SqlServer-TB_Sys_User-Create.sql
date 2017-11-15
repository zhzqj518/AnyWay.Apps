/****** Object:  Table [dbo].[TB_Sys_User]    Script Date: 2017/10/31 19:17:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TB_Sys_User](
	[UserID] [varchar](50) NOT NULL,
	[UserName] [varchar](50) NULL,
	[UserLoginName] [varchar](50) NULL,
	[UserSSOName] [varchar](50) NULL,
	[UserOrgID] [varchar](50) NULL,
	[UserPwd] [varchar](50) NULL,
	[UserSex] [int] NULL,
	[UserMail] [varchar](150) NULL,
	[UserMobile] [varchar](50) NULL,
	[UserTel] [varchar](50) NULL,
	[UserType] [varchar](20) NULL,
	[UserTicketID] [varchar](50) NULL,
	[UserIsEmailVerificate] [int] NULL,
	[UserEmailVerificationCode] [varchar](50) NULL,
	[UserIsSMSVerificate] [int] NULL,
	[UserSMSVerificationCode] [varchar](50) NULL,
	[UserRemark] [varchar](500) NULL,
	[UserIsValid] [int] NULL,
	[UserCreateBy] [varchar](50) NULL,
	[UserCreateTime] [datetime] NULL,
	[UserUpdateBy] [varchar](50) NULL,
	[UserUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_TB_Sys_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


