USE [Ninja-Developer]
GO
/****** Object:  Table [dbo].[AspController]    Script Date: 12/8/2017 5:33:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspController](
	[Controller] [nvarchar](150) NOT NULL,
	[Action] [nvarchar](150) NOT NULL,
	[Area] [nvarchar](50) NULL,
	[Description] [nvarchar](150) NULL,
	[IsDelete] [bit] NULL CONSTRAINT [DF_AspController_IsDelete]  DEFAULT ((1)),
 CONSTRAINT [PK_AspController] PRIMARY KEY CLUSTERED 
(
	[Controller] ASC,
	[Action] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetRole]    Script Date: 12/8/2017 5:33:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRole](
	[Id] [uniqueidentifier] NOT NULL,
	[RoleName] [nvarchar](150) NULL,
 CONSTRAINT [PK_AspNetRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUser]    Script Date: 12/8/2017 5:33:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AspNetUser](
	[Id] [uniqueidentifier] NOT NULL,
	[Email] [nvarchar](350) NULL,
	[PasswordHash] [nvarchar](550) NULL,
	[Phone] [nchar](13) NULL,
	[UserName] [nvarchar](150) NULL,
	[Avatar] [varbinary](max) NULL,
	[CreateDate] [datetime] NULL,
	[Active] [bit] NULL,
	[RoleId] [uniqueidentifier] NULL,
	[Hoten] [nvarchar](150) NULL,
 CONSTRAINT [PK_AspNetUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AspRoleController]    Script Date: 12/8/2017 5:33:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspRoleController](
	[RoleId] [uniqueidentifier] NOT NULL,
	[Controller] [nvarchar](150) NOT NULL,
	[Action] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](150) NULL,
 CONSTRAINT [PK_AspRoleController] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[Controller] ASC,
	[Action] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  StoredProcedure [dbo].[admin_AddController]    Script Date: 12/8/2017 5:33:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 04-12-2017
-- Description:	Thêm mới controller and action
-- =============================================
CREATE PROCEDURE [dbo].[admin_AddController]
	@Controller nvarchar(150)=''
	,@Action nvarchar(150)=''
	,@Area nvarchar(50)=''
	,@Description nvarchar(150)=''
	,@IsDelete bit=0
AS
BEGIN
	INSERT INTO AspController
                (
				[Controller]
				, [Action]
				, [Area]
				, [Description]
				, [IsDelete]
				)
VALUES         (
				@Controller
				, @Action
				, @Area
				, @Description
				, @IsDelete
				)
END

GO
/****** Object:  StoredProcedure [dbo].[admin_CheckControllerandAction]    Script Date: 12/8/2017 5:33:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 04-12-2017
-- Description:	check xem controller and action đã  tồn tại chưa
-- =============================================
CREATE PROCEDURE [dbo].[admin_CheckControllerandAction] 
	@Controller nvarchar(150)='Homes'
	,@Action nvarchar(150)='Index'
AS
BEGIN
	IF(EXISTS(SELECT A.Controller, A.Action FROM AspController A WHERE A.Controller=@Controller AND A.Action=@Action))
		SELECT 1
		
	ELSE
		SELECT 0
		
END

GO
/****** Object:  StoredProcedure [dbo].[admin_CreateRoleId]    Script Date: 12/8/2017 5:33:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 04-12-2017
-- Description:	tạo quyền admin
-- =============================================
CREATE PROCEDURE [dbo].[admin_CreateRoleId] 
	@RoleName nvarchar(150)='user'
AS
BEGIN
	INSERT INTO AspNetRole
                         (
						  Id
						 , RoleName
						 )
	VALUES        (NEWID(), @RoleName)
END

GO
/****** Object:  StoredProcedure [dbo].[admin_DeletePhanquyenUserById]    Script Date: 12/8/2017 5:33:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[admin_DeletePhanquyenUserById]
	@RoleId uniqueidentifier=''
	,@Controller nvarchar(250)=''
	,@Action nvarchar(150)=''
	,@Description nvarchar(350)=''	
AS
BEGIN
		DELETE FROM AspRoleController
		WHERE [RoleId]=@RoleId AND [Controller] =@Controller AND [Action] =@Action
END
GO
/****** Object:  StoredProcedure [dbo].[admin_DeleteRoleById]    Script Date: 12/8/2017 5:33:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 05-12-2017
-- Description:	get quyền
-- =============================================
CREATE PROCEDURE [dbo].[admin_DeleteRoleById]	
@Id uniqueidentifier='48C3CC2F-AF84-4751-A519-138206D2D4BB'
AS
BEGIN
	DELETE FROM AspNetRole
	WHERE Id=@Id
END

GO
/****** Object:  StoredProcedure [dbo].[admin_DeleteUserById]    Script Date: 12/8/2017 5:33:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 07-12-2017
-- Description: Thêm mới  người dùng
-- =============================================
CREATE PROCEDURE [dbo].[admin_DeleteUserById]
	@Id uniqueidentifier=''	
AS
BEGIN
	DELETE FROM AspNetUser
	WHERE Id= @Id
END

GO
/****** Object:  StoredProcedure [dbo].[admin_GetAllActionByController]    Script Date: 12/8/2017 5:33:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ANHTC
-- Create date: 06-12-2017
-- Description:	Lấy all Controller
-- =============================================
CREATE PROCEDURE [dbo].[admin_GetAllActionByController]
	@userId  uniqueidentifier='6521D551-3021-4648-AB96-E42EDDD22A18'
	,@Controller NVARCHAR(150)='ImageBrowser'
AS
BEGIN	
	
	SELECT * 
	FROM
	(
	SELECT   T1.[Controller]
			,T1.[Action]
			,T1.[Description] 
			,0 as 'isBranch'
			FROM AspController T1 LEFT JOIN AspRoleController T2 ON T1.Controller= T2.Controller 
			WHERE T1.Action NOT IN (SELECT Action FROM AspRoleController WHERE RoleId=@userId and Controller=@Controller)			 		
	UNION 
		SELECT   T2.[Controller]
			,T2.[Action]
			,T2.[Description] 
			,1 as 'isBranch'
		FROM AspController T1 INNER JOIN AspRoleController T2 ON T1.Controller= T2.Controller 		
		WHERE T2.RoleId=@userId AND T2.Controller=@Controller
	)A
	WHERE A.[Controller]=@Controller 
	ORDER BY A.isBranch DESC
END

GO
/****** Object:  StoredProcedure [dbo].[admin_GetAllControllerByRole]    Script Date: 12/8/2017 5:33:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ANHTC
-- Create date: 06-12-2017
-- Description:	Lấy all Controller
-- =============================================
CREATE PROCEDURE [dbo].[admin_GetAllControllerByRole]	
AS
BEGIN
	SELECT  DISTINCT [Controller] 
	FROM AspController
END

GO
/****** Object:  StoredProcedure [dbo].[admin_GetAllRoles]    Script Date: 12/8/2017 5:33:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 05-12-2017
-- Description:	get quyền
-- =============================================
CREATE PROCEDURE [dbo].[admin_GetAllRoles]	
AS
BEGIN
	SELECT  Id, RoleName
	FROM    AspNetRole
END

GO
/****** Object:  StoredProcedure [dbo].[admin_GetAllRolesController]    Script Date: 12/8/2017 5:33:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[admin_GetAllRolesController]	
AS
BEGIN
	SELECT  Controller
			, Action
			, Area
			, Description
			, IsDelete
	FROM AspController
END

GO
/****** Object:  StoredProcedure [dbo].[admin_GetAllUser]    Script Date: 12/8/2017 5:33:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 05-12-2017
-- Description:	lấy toàn  bộ người dùng
-- =============================================
CREATE PROCEDURE [dbo].[admin_GetAllUser]
	
AS
BEGIN
	SELECT        a.Id
				, a.Email
				, a.PasswordHash
				, a.Phone
				, a.UserName
				, a.Avatar
				, a.CreateDate
				, a.Active
				, b.RoleName
				, a.Hoten
	FROM    AspNetUser a INNER JOIN
            AspNetRole b ON a.RoleId = b.Id
END

GO
/****** Object:  StoredProcedure [dbo].[admin_GetMotaQuyenByAction]    Script Date: 12/8/2017 5:33:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 07-12-2017
-- Description:	Lấy quyền vừa cập nhật để sửa mô tả
-- =============================================
CREATE PROCEDURE [dbo].[admin_GetMotaQuyenByAction]
	@Controller nvarchar(150)='Homes'	
	,@Action nvarchar(150)='Index'	
AS
BEGIN
	SELECT  Description
			, Area
			, Action
			, Controller
			, IsDelete

	FROM    AspController

	WHERE  Controller=@Controller and [Action]=@Action
				
END

GO
/****** Object:  StoredProcedure [dbo].[admin_GetQuyenByAll]    Script Date: 12/8/2017 5:33:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 08-12-2017
-- Description:	Lấy quyền để phân loại quản trị cho người dùng
-- =============================================
CREATE PROCEDURE [dbo].[admin_GetQuyenByAll]
	
AS
BEGIN
	SELECT        Id, RoleName
	FROM            AspNetRole
END

GO
/****** Object:  StoredProcedure [dbo].[admin_GetRoleById]    Script Date: 12/8/2017 5:33:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 05-12-2017
-- Description:	get quyền
-- =============================================
CREATE PROCEDURE [dbo].[admin_GetRoleById]	
@Id uniqueidentifier='48C3CC2F-AF84-4751-A519-138206D2D4BB'
AS
BEGIN
	SELECT Id,RoleName
	FROM       AspNetRole
	
	WHERE Id=@Id
END

GO
/****** Object:  StoredProcedure [dbo].[admin_GetUpdateUserById]    Script Date: 12/8/2017 5:33:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 07-12-2017
-- Description: Thêm mới  người dùng
-- =============================================
CREATE PROCEDURE [dbo].[admin_GetUpdateUserById]
	@Id uniqueidentifier='01056748-95FD-40D0-A89B-CA0B58B98379'	
AS
BEGIN
	SELECT      A.Id
				, A.Email
				, A.PasswordHash
				, A.Phone
				, A.UserName
				, A.Avatar
				,CASE
					WHEN A.Avatar IS NOT NULL  THEN 'ShowPhotoById?Id=' +  CAST(A.Id AS nvarchar(36))+'&' +CONVERT(varchar(24),GETDATE(),120)
					ELSE NULL
				 END  AS Anh
				, A.CreateDate
				, A.Active
				, A.RoleId
				, A.Hoten
				, B.RoleName
	FROM	AspNetUser A INNER JOIN AspNetRole B ON A.RoleId=B.Id
	WHERE A.Id= @Id
END

GO
/****** Object:  StoredProcedure [dbo].[admin_InsertUser]    Script Date: 12/8/2017 5:33:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 07-12-2017
-- Description: Thêm mới  người dùng
-- =============================================
CREATE PROCEDURE [dbo].[admin_InsertUser]
	@Email nvarchar(350)=''
	,@PasswordHash nvarchar(550)=''
	,@Phone nchar(13)=''
	,@UserName nvarchar(150)=''
	,@Avatar varbinary(MAX)=null
	,@Active bit=1
	,@RoleId uniqueidentifier=''
	,@Hoten nvarchar(150)=''
AS
BEGIN
	INSERT INTO AspNetUser
                         (
						 Id
						 , Email
						 , PasswordHash
						 , Phone
						 , UserName
						 , Avatar
						 , CreateDate
						 , Active
						 , RoleId
						 , Hoten
						 )
		VALUES			(
						NEWID()
						, @Email
						, @PasswordHash
						, @Phone
						, @UserName
						, @Avatar
						, GETDATE()
						, @Active
						, @RoleId
						,@Hoten
						)
END

GO
/****** Object:  StoredProcedure [dbo].[admin_UpdateMotaQuyen]    Script Date: 12/8/2017 5:33:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 07-12-2017
-- Description:	Sửa mô tả cho các quyền
-- =============================================
CREATE PROCEDURE [dbo].[admin_UpdateMotaQuyen]
	@Controller nvarchar(150)=''
	,@Action nvarchar(150)=''
	,@Description nvarchar(150)=''	
	,@Area nvarchar(150)=''	
AS
BEGIN
	UPDATE  AspController

	SET       [Description] = @Description
			, [Area] = @Area

	WHERE Controller= @Controller AND [Action]=@Action
				
END

GO
/****** Object:  StoredProcedure [dbo].[admin_UpdatePhanquyenUserById]    Script Date: 12/8/2017 5:33:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 06-12-2017
-- Description:	Update quyền cho người dùng
-- =============================================
CREATE PROCEDURE [dbo].[admin_UpdatePhanquyenUserById]
	@RoleId uniqueidentifier=''
	,@Controller nvarchar(250)=''
	,@Action nvarchar(150)=''
	,@Description nvarchar(350)=''
	,@remoquyen nvarchar(150)=''
AS
BEGIN
	IF(EXISTS(SELECT [RoleId] FROM AspRoleController WHERE [RoleId]=@RoleId AND [Controller]=@Controller AND [Action]=@Action))	
		BEGIN
			UPDATE  AspRoleController
			SET   [Description] = @Description
			WHERE [RoleId]=@RoleId AND [Controller] =@Controller AND [Action] =@Action
		END--UPDATE QUYỀN
	ELSE
		BEGIN
			INSERT INTO AspRoleController
								([RoleId]
								, [Controller]
								, [Action]
								, [Description])

					VALUES  (@RoleId
							,@Controller
							,@Action
							,@Description)
		END--THÊM MỚI QUYỀN
END

GO
/****** Object:  StoredProcedure [dbo].[admin_UpdateRoles]    Script Date: 12/8/2017 5:33:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 05-12-2017
-- Description:	get quyền
-- =============================================
CREATE PROCEDURE [dbo].[admin_UpdateRoles]	
@Id uniqueidentifier=''
,@RoleName nvarchar(150)=''
AS
BEGIN
	UPDATE       AspNetRole
	SET          RoleName =@RoleName
	WHERE Id=@Id
END

GO
/****** Object:  StoredProcedure [dbo].[admin_UpdateUser]    Script Date: 12/8/2017 5:33:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 07-12-2017
-- Description: Thêm mới  người dùng
-- =============================================
CREATE PROCEDURE [dbo].[admin_UpdateUser]
	@Id uniqueidentifier=''
	,@Email nvarchar(350)=''
	,@PasswordHash nvarchar(550)=''
	,@Phone nchar(13)=''
	,@UserName nvarchar(150)=''
	,@Avatar varbinary(MAX)=null
	,@Active bit=1
	,@RoleId uniqueidentifier=''
	,@Hoten nvarchar(150)=''
AS
BEGIN
	UPDATE  AspNetUser

	SET      Email = @Email
			, PasswordHash = @PasswordHash
			, Phone = @Phone
			, UserName = @UserName
			, Avatar = @Avatar
			, CreateDate = GETDATE()
			, Active = @Active
			, RoleId = @RoleId
			, Hoten = @Hoten
			WHERE Id= @Id
END

GO
