USE [master]
GO
/****** Object:  Database [Linja-Developer]    Script Date: 1/12/2018 1:38:44 PM ******/
CREATE DATABASE [Linja-Developer]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Ninja-Developer', FILENAME = N'C:\Program Files (x86)\Plesk\Databases\MSSQL\MSSQL12.MSSQLSERVER2014\MSSQL\DATA\Linja-Developer_b5c71f7bcedc406bb3786e8d8f5aa739.mdf' , SIZE = 6144KB , MAXSIZE = 1843200KB , FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Ninja-Developer_log', FILENAME = N'C:\Program Files (x86)\Plesk\Databases\MSSQL\MSSQL12.MSSQLSERVER2014\MSSQL\DATA\Linja-Developer_40f2b2166b72447b8d1cfedc3eabb341.ldf' , SIZE = 1792KB , MAXSIZE = 1843200KB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Linja-Developer] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Linja-Developer].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Linja-Developer] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Linja-Developer] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Linja-Developer] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Linja-Developer] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Linja-Developer] SET ARITHABORT OFF 
GO
ALTER DATABASE [Linja-Developer] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Linja-Developer] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Linja-Developer] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Linja-Developer] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Linja-Developer] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Linja-Developer] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Linja-Developer] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Linja-Developer] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Linja-Developer] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Linja-Developer] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Linja-Developer] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Linja-Developer] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Linja-Developer] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Linja-Developer] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Linja-Developer] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Linja-Developer] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Linja-Developer] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Linja-Developer] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Linja-Developer] SET  MULTI_USER 
GO
ALTER DATABASE [Linja-Developer] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Linja-Developer] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Linja-Developer] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Linja-Developer] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Linja-Developer] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Linja-Developer]
GO
/****** Object:  User [Linja-Developer]    Script Date: 1/12/2018 1:38:44 PM ******/
CREATE USER [Linja-Developer] FOR LOGIN [Linja-Developer] WITH DEFAULT_SCHEMA=[Linja-Developer]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [Linja-Developer]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [Linja-Developer]
GO
ALTER ROLE [db_datareader] ADD MEMBER [Linja-Developer]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [Linja-Developer]
GO
/****** Object:  Schema [adminanhtc]    Script Date: 1/12/2018 1:38:44 PM ******/
CREATE SCHEMA [adminanhtc]
GO
/****** Object:  Schema [anhtc]    Script Date: 1/12/2018 1:38:44 PM ******/
CREATE SCHEMA [anhtc]
GO
/****** Object:  Schema [Linja-Developer]    Script Date: 1/12/2018 1:38:44 PM ******/
CREATE SCHEMA [Linja-Developer]
GO
/****** Object:  UserDefinedFunction [anhtc].[fn_ConvertToUnsigned]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [anhtc].[fn_ConvertToUnsigned]
(   
    @Signed NVARCHAR(MAX)
)  
RETURNS VARCHAR(MAX)  
AS  
BEGIN           
    DECLARE @Index INT, @LEN INT, @Result VARCHAR(MAX)       
    
    SELECT @Index = 1, @LEN = LEN(@Signed), @Result = ''     
    WHILE (@Index <= @LEN)   
    BEGIN         
        IF EXISTS(SELECT Signed FROM Charset 
        WHERE CHARINDEX(SUBSTRING(@Signed, @Index, 1) COLLATE Latin1_General_CS_AS, Signed) > 0 )    
        BEGIN     
            SET @Result = @Result + (SELECT Unsigned FROM Charset 
									 WHERE CHARINDEX(SUBSTRING(@Signed, @Index, 1) COLLATE Latin1_General_CS_AS, Signed) > 0)       
        END    
        ELSE    
        BEGIN     
            SET @Result = @Result + SUBSTRING(@Signed, @Index, 1)    
        END     
        SET @Index = @Index + 1   
    END   
	
	RETURN @Result  
END

GO
/****** Object:  UserDefinedFunction [anhtc].[fn_Split]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [anhtc].[fn_Split]
(
	 @p_SourceText  nvarchar(max)=''
	,@p_Delimeter nvarchar(100) = ','
)
RETURNS @retTable TABLE 
(
	 Position  int identity(1,1)
	,txt_value nvarchar(2000)
)
AS

BEGIN
	 DECLARE @w_Continue  int
			,@w_StartPos  int
			,@w_Length  int
			,@w_Delimeter_pos int
			,@w_tmp_txt   nvarchar(2000)
			,@w_Delimeter_Len tinyint
			
	 IF LEN(@p_SourceText) = 0
	 BEGIN
		SET  @w_Continue = 0
	 END 
	 ELSE
	 BEGIN
		SET  @w_Continue = 1
		SET @w_StartPos = 1
		SET @p_SourceText = RTRIM( LTRIM( @p_SourceText))
		SET @w_Length   = DATALENGTH( RTRIM( LTRIM( @p_SourceText)))
		SET @w_Delimeter_Len = LEN(@p_Delimeter)
	 END
	  
	 WHILE @w_Continue = 1
	 BEGIN
	  SET @w_Delimeter_pos = CHARINDEX( @p_Delimeter
		  ,(SUBSTRING( @p_SourceText, @w_StartPos
		  ,((@w_Length - @w_StartPos) + @w_Delimeter_Len))))
	 
	  IF @w_Delimeter_pos > 0
	  BEGIN
			SET @w_tmp_txt = LTRIM(RTRIM( SUBSTRING( @p_SourceText, @w_StartPos,(@w_Delimeter_pos - 1)) ))
			SET @w_StartPos = @w_Delimeter_pos + @w_StartPos + (@w_Delimeter_Len- 1)
	   END
	   ELSE
			BEGIN
				SET @w_tmp_txt = LTRIM(RTRIM( SUBSTRING( @p_SourceText, @w_StartPos 
				  ,((@w_Length - @w_StartPos) + @w_Delimeter_Len)) ))
				SELECT @w_Continue = 0
			END
			
			INSERT INTO @retTable VALUES(@w_tmp_txt )
		END
	RETURN
END

GO
/****** Object:  Table [anhtc].[AnhSanPham]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [anhtc].[AnhSanPham](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](150) NULL,
	[SanPhamId] [uniqueidentifier] NULL,
	[HinhAnh] [varbinary](max) NULL,
	[NgayTao] [datetime] NULL,
	[SapXep] [int] NULL,
 CONSTRAINT [PK_AnhSanPham] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [anhtc].[Blog]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [anhtc].[Blog](
	[id] [uniqueidentifier] NOT NULL,
	[DanhMucId] [int] NULL,
	[Name] [nvarchar](150) NULL,
	[NoiDung] [ntext] NULL,
	[TieuDe] [nvarchar](70) NULL,
	[MoTa] [nvarchar](170) NULL,
	[TuKhoa] [nvarchar](170) NULL,
	[SapXep] [int] NULL,
	[NgayTao] [datetime] NULL,
	[NgayDang] [datetime] NULL,
	[HinhAnh] [varbinary](max) NULL,
	[ArticleId] [int] IDENTITY(1,1) NOT NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_Blog] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [anhtc].[BlogTags]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [anhtc].[BlogTags](
	[Id] [uniqueidentifier] NOT NULL,
	[BlogId] [int] NULL,
	[TagId] [int] NULL,
	[SortOrder] [int] NULL,
 CONSTRAINT [PK_BlogTags] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [anhtc].[DanhMuc]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [anhtc].[DanhMuc](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ParentId] [int] NULL,
	[Ma] [nvarchar](50) NULL,
	[Ten] [nvarchar](150) NULL,
	[SapSep] [int] NULL,
	[TinhId] [int] NULL,
 CONSTRAINT [PK_DanhMuc] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [anhtc].[QuanHuyen]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [anhtc].[QuanHuyen](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TinhId] [int] NOT NULL,
	[Name] [nvarchar](150) NULL,
	[SapXep] [int] NULL CONSTRAINT [DF_QuanHuyen_Sapsep]  DEFAULT ((9999)),
 CONSTRAINT [PK_QuanHuyen] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[TinhId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [anhtc].[SanPham]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [anhtc].[SanPham](
	[Id] [uniqueidentifier] NOT NULL,
	[ArticleId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NULL,
	[TieuDe] [nvarchar](170) NULL,
	[MoTa] [nvarchar](170) NULL,
	[TuKhoa] [nvarchar](170) NULL,
	[GiaCu] [int] NULL,
	[GiaMoi] [int] NULL,
	[ChietKhau] [int] NULL,
	[KhuyenMaiId] [int] NULL,
	[NgayBatDau] [datetime] NULL,
	[NgayKetThuc] [datetime] NULL,
	[DanhMucId] [int] NULL,
	[SapSep] [int] NULL,
	[TrangThai] [int] NULL,
	[NoiDung] [ntext] NULL,
	[Active] [bit] NULL,
	[NgayTao] [datetime] NULL,
	[HinhAnh] [varbinary](max) NULL,
	[TuKhoaTimKiem] [nvarchar](170) NULL,
 CONSTRAINT [PK_SanPham] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [anhtc].[Tag]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [anhtc].[Tag](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NULL,
	[Name] [nvarchar](256) NULL,
	[Type] [int] NULL CONSTRAINT [DF_Tag_Type]  DEFAULT ((1)),
	[SortOrder] [int] NULL CONSTRAINT [DF_Tag_SortOrder]  DEFAULT ((999999999)),
	[_TenKhongDau] [varchar](256) NULL,
 CONSTRAINT [PK_Tag] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [anhtc].[Tinh]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [anhtc].[Tinh](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Sapsep] [int] NULL CONSTRAINT [DF_Tinh_Sapsep]  DEFAULT ((9999)),
 CONSTRAINT [PK_Tinh] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspController]    Script Date: 1/12/2018 1:38:44 PM ******/
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
/****** Object:  Table [dbo].[AspNetRole]    Script Date: 1/12/2018 1:38:44 PM ******/
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
/****** Object:  Table [dbo].[AspNetUser]    Script Date: 1/12/2018 1:38:44 PM ******/
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
/****** Object:  Table [dbo].[AspRoleController]    Script Date: 1/12/2018 1:38:44 PM ******/
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
/****** Object:  StoredProcedure [anhtc].[aadmin_BlogTags_CapNhat]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE  PROCEDURE [anhtc].[aadmin_BlogTags_CapNhat] 
	@BlogId int =15,
	@TagIds varchar(500)='14,12',
	@UnTagIds varchar(500)='13,15,16,17,18,19,20,21,22,23,24,25,27',
	@SortOrder  varchar(500)='4,2'
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
	
		exec admin_BlogTags_Delete @BlogId,  @UnTagIds
		exec admin_BlogTags_Insert @BlogId,  @TagIds ,@SortOrder
		
		SELECT 1 
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		SELECT 0
	END CATCH

END

GO
/****** Object:  StoredProcedure [anhtc].[admin_AddAnhSanPham]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 20-12-2017
-- Description:	Thêm mới ảnh sản phẩm
-- =============================================
CREATE PROCEDURE [anhtc].[admin_AddAnhSanPham]
	@Name  nvarchar(150)=''
	,@SanPhamId uniqueidentifier=''
	,@HinhAnh varbinary(MAX)=0x20	
	,@SapXep int=1
AS
BEGIN
	INSERT INTO AnhSanPham
                         (
						 Id
						 , Name
						 , SanPhamId
						 , HinhAnh
						 , NgayTao
						 , SapXep
						 )
		VALUES			 (
							newid()
							,@Name
							,@SanPhamId
							,@HinhAnh
							,getdate()
							,@SapXep
						)
END

GO
/****** Object:  StoredProcedure [anhtc].[admin_AddBlog]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 21-12-2017
-- Description:	sửa bài blog theo Id
-- =============================================
CREATE PROCEDURE [anhtc].[admin_AddBlog]
	@DanhMucId INT
	,@Name NVARCHAR(150)=''
	,@NoiDung NTEXT
	,@TieuDe NVARCHAR(70)=''
	,@MoTa NVARCHAR(170)=''
	,@TuKhoa NVARCHAR(170)=''
	,@SapXep INT
	,@NgayDang DATETIME=''
	,@HinhAnh varbinary(MAX)=0x20	
	,@Active BIT=0
AS 
BEGIN
	INSERT INTO Blog
                         (
						 id
						 , DanhMucId
						 , Name
						 , NoiDung
						 , TieuDe
						 , MoTa
						 , TuKhoa
						 , SapXep
						 , NgayTao
						 , NgayDang
						 , HinhAnh
						 , Active
						 )
		VALUES        (
						NEWID()
						,@DanhMucId
						,@Name
						,@NoiDung
						,@TieuDe
						,@MoTa
						,@TuKhoa
						,@SapXep
						, GETDATE()
						,@NgayDang
						,@HinhAnh
						,@Active
						)
	 

END

GO
/****** Object:  StoredProcedure [anhtc].[admin_addddanhMuc]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 13-12-2017
-- Description:	Thêm mới danh mục
-- =============================================
CREATE PROCEDURE [anhtc].[admin_addddanhMuc]
	@ParentId int
	, @Ma nvarchar(50)=''
	, @Ten nvarchar(150)=''
	, @SapSep int=10
	, @TinhId int  =1
AS
BEGIN
	INSERT INTO DanhMuc
                         (
						 ParentId
						 , Ma
						 , Ten
						 , SapSep
						 , TinhId
						 )
	VALUES				(
						 @ParentId
						 , @Ma
						 , @Ten
						 , @SapSep
						 , @TinhId
						 )
END

GO
/****** Object:  StoredProcedure [anhtc].[admin_AddQuanHuyen]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 13-12-2017
-- Description:	thêm mới quận huyện
-- =============================================
CREATE PROCEDURE [anhtc].[admin_AddQuanHuyen]
	@TinhId int =1
	,@Name nvarchar(150)=''
	,@SapXep int=10
AS
BEGIN
	INSERT INTO QuanHuyen
                (TinhId, Name, SapXep)
	VALUES      (@TinhId,@Name,@SapXep)
END

GO
/****** Object:  StoredProcedure [anhtc].[admin_AddSanPham]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 12-12-2017
-- Description:	Thêm mới sản  phẩm
-- =============================================
CREATE PROCEDURE [anhtc].[admin_AddSanPham]
	 @Name nvarchar(150)='anh'
	, @TieuDe nvarchar(170)='anh'
	, @MoTa nvarchar(170)='anh'
	, @TuKhoa nvarchar(170)='anh'
	, @GiaCu int=100000
	, @GiaMoi int=100000
	, @ChietKhau int=10
	, @KhuyenMaiId int=1
	, @NgayBatDau datetime='11/26/2017'
	, @NgayKetThuc datetime='11/26/2017'
	, @DanhMucId int=1
	, @SapSep int=10
	, @TrangThai int=1
	, @NoiDung ntext=''
	, @Active	bit=1
	, @HinhAnh varbinary(max)=0x20
	, @TuKhoaTimKiem nvarchar(170)='anh1'
AS
BEGIN
--declare @DateFrom datetime,
--		@DateTo   datetime
			
--	set     @DateFrom=CONVERT(datetime, @NgayBatDau, 103)
--	set     @DateTo  =CONVERT(datetime, @NgayKetThuc, 103)
	
		INSERT INTO SanPham
                         (
						 [Id]						
						,[Name]
						,[TieuDe]
						,[MoTa]
						,[TuKhoa]
						,[GiaCu]
						,[GiaMoi]
						,[ChietKhau]
						,[KhuyenMaiId]
						,[NgayBatDau]
						,[NgayKetThuc]
						,[DanhMucId]
						,[SapSep]
						,[TrangThai]
						,[NoiDung]
						,[Active]
						,[NgayTao]
						,[HinhAnh]
						,[TuKhoaTimKiem]
						 )
		VALUES        (
						NewID()						
						, @Name
						, @TieuDe
						, @MoTa
						, @TuKhoa
						, @GiaCu
						, @GiaMoi
						, @ChietKhau
						, @KhuyenMaiId
						, @NgayBatDau
						, @NgayKetThuc
						, @DanhMucId
						, @SapSep
						, @TrangThai
						, @NoiDung
						, @Active
						, getdate()
						, @HinhAnh
						, @TuKhoaTimKiem
						)
				END



GO
/****** Object:  StoredProcedure [anhtc].[admin_BlogTags_Delete]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [anhtc].[admin_BlogTags_Delete]
	@BlogId int=4,
	@UnTagIds varchar(500)='6'
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
		--B1: Tao bang trung gian để lưu id tag
		DECLARE @TagId int
		DECLARE @i INT,
		        @cnt INT = 1
		
		DECLARE @dtUpdate TABLE(id INT, val int)		
		SET @i = 1
		
		INSERT INTO @dtUpdate
		SELECT *
		FROM   [anhtc].[fn_Split](@UnTagIds, ',')
		SELECT @cnt = COUNT(*)
		FROM   @dtUpdate ;
						
		WHILE (@i <= @cnt)
		BEGIN
		    SET @TagId = (
		            SELECT val
		            FROM   @dtUpdate
		            WHERE  id = @i
		        )		    		    		    
		    --B2: Xóa Bản ghi 
			Delete from BlogTags where BlogId = @BlogId and TagId = @TagId
		    SET @i = @i + 1;
		END		
		SELECT 1 
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		SELECT 0
	END CATCH

END

GO
/****** Object:  StoredProcedure [anhtc].[admin_BlogTags_Insert]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [anhtc].[admin_BlogTags_Insert]
	 @BlogId int=15
	,@TagIds varchar(500)='14,12'
	,@SortOrder nvarchar(500)='4,2'
	
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
		--B1: Tao bang trung gian để lưu id tag
		
		DECLARE @i INT,@cnt INT = 1
		
		DECLARE @dtInsert TABLE(id INT, val int)	
		DECLARE @dtSort TABLE(id INT, val int)	
		SET @i = 1
		
		INSERT INTO @dtInsert
		SELECT * FROM   [anhtc].[fn_Split](@TagIds, ',')
		SELECT @cnt = COUNT(*) FROM   @dtInsert ;
		
		INSERT INTO @dtSort
		SELECT * FROM  [anhtc].[fn_Split](@SortOrder, ',')
		
		WHILE (@i <= @cnt)
		BEGIN
			DECLARE @TagId int, @SortIndex Int
		    SET @TagId = ( SELECT val FROM @dtInsert  WHERE  id = @i )	
			SET @SortIndex = ( SELECT val FROM @dtSort WHERE  id = @i )	
			    		    		    
		    --B2: Insert du lieu moi
			if((select count(Id) from BlogTags where BlogId = @BlogId and TagId = @TagId)=0)
			begin
				INSERT INTO BlogTags
				   ([Id]
				   ,[BlogId]
				   ,[TagId]
				   ,[SortOrder]
				   )				
				VALUES
				   (NEWID()
				   ,@BlogId
				   ,@TagId
				   ,@SortIndex
				   )
		    End 

			else
			declare @IdTinTuc uniqueidentifier
			set @IdTinTuc=(select Id from BlogTags where BlogId = @BlogId and TagId =@TagId)
			begin
				UPDATE       BlogTags
				SET          SortOrder = @SortIndex
				WHERE [Id] = @IdTinTuc
			end 

		 --select @TagId  as 'Tag'
		 --select @SortIndex as 'Sắp xếp'

 		    SET @i = @i + 1;
		END		
		SELECT 1 
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		SELECT 0
	END CATCH

END

GO
/****** Object:  StoredProcedure [anhtc].[admin_BlogTags_SelectByBlogId]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [anhtc].[admin_BlogTags_SelectByBlogId]
	@Search nvarchar(250)='',
	@BlogId int=0
AS
BEGIN

	SELECT 
	
	a.Id
	,a.Name
	,case when a.SortOrder = 9999 then 0 else a.SortOrder end as  'SortOrder'
	,a.IsCheck
	FROM (
		Select a.Id
				,a.Name
				,case when b.SortOrder is null OR b.SortOrder = 0 then 9999 else b.SortOrder end as  'SortOrder'
			   ,case when b.BlogId is null then 0 else 1 end as  IsCheck
			from Tag a 
			left join BlogTags b on a.Id =b.TagId and b.BlogId = @BlogId
		where  (a.[Name]  LIKE '%'+ @Search +'%' 
				or @Search ='' 
				or @Search is null
			    ) 
		and a.[Type] = 2 ---Tag giành cho blog.
	    )a

	order by a.IsCheck DESC, a.SortOrder ASC
END

GO
/****** Object:  StoredProcedure [anhtc].[admin_DeleteAnhSanPham]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 20-12-2017
-- Description:	Thêm mới ảnh sản phẩm
-- =============================================
CREATE PROCEDURE [anhtc].[admin_DeleteAnhSanPham]
	@Id uniqueidentifier=''	
AS
BEGIN
	DELETE FROM AnhSanPham
	WHERE        (Id = @Id)
	
END

GO
/****** Object:  StoredProcedure [anhtc].[admin_DeleteBlog]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 21-12-2017
-- Description:	sửa bài blog theo Id
-- =============================================
CREATE PROCEDURE [anhtc].[admin_DeleteBlog]
@id  uniqueidentifier=''
AS 
BEGIN
	DELETE FROM Blog
	WHERE   (id = @id)	 
END

GO
/****** Object:  StoredProcedure [anhtc].[admin_DeleteDanhMuc]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 13-12-2017
-- Description:	Xóa danh mục
-- =============================================
CREATE PROCEDURE [anhtc].[admin_DeleteDanhMuc]
@Id int=1
AS
BEGIN
	DELETE FROM DanhMuc
	WHERE        (Id=@Id)
END

GO
/****** Object:  StoredProcedure [anhtc].[admin_DeleteQuanHuyen]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 13-12-2017
-- Description:	Xóa danh mục
-- =============================================
CREATE PROCEDURE [anhtc].[admin_DeleteQuanHuyen]
@Id int=1
AS
BEGIN
	DELETE FROM QuanHuyen
	WHERE        (Id=@Id)
END

GO
/****** Object:  StoredProcedure [anhtc].[admin_DeleteSanPham]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 13-12-2017
-- Description:	Xóa sản phẩm
-- =============================================
CREATE PROCEDURE [anhtc].[admin_DeleteSanPham]
@Id uniqueidentifier=''
AS
BEGIN
	DELETE FROM SanPham
	WHERE        (Id=@Id)
END

GO
/****** Object:  StoredProcedure [anhtc].[admin_GetAnhSanPhamById]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 20-12-2017
-- Description:	Thêm mới ảnh sản phẩm
-- =============================================
CREATE PROCEDURE [anhtc].[admin_GetAnhSanPhamById]
	@Id uniqueidentifier='03eac0e0-f640-4746-bdbd-ac157f775120'	
AS
BEGIN
	SELECT  Id
			, Name
			, SanPhamId
			, HinhAnh
			, NgayTao
			, SapXep
			,CASE
					WHEN HinhAnh IS NOT NULL  THEN 'Common/ShowPhotoAnhSanPham?Id=' +  CAST(Id AS nvarchar(36))+'&' +CONVERT(varchar(24),GETDATE(),120)
					ELSE NULL
				 END  AS Anh
	FROM AnhSanPham
	WHERE Id=@Id
END

GO
/****** Object:  StoredProcedure [anhtc].[admin_GetAnhSanPhamId]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 20-12-2017
-- Description:	Thêm mới ảnh sản phẩm
-- =============================================
CREATE PROCEDURE [anhtc].[admin_GetAnhSanPhamId]
	@SanPhamId uniqueidentifier='932aa505-03ee-4792-9f7b-3e96d8595bcd'	
AS
BEGIN
	SELECT        Id
				, Name
				, SanPhamId
				, HinhAnh
				, NgayTao
				, SapXep
				,CASE
								WHEN HinhAnh IS NOT NULL  THEN 'Common/ShowPhotoAnhSanPham?Id=' +  CAST(Id AS nvarchar(36))+'&' +CONVERT(varchar(24),GETDATE(),120)
								ELSE NULL
							 END  AS Anh
	FROM            AnhSanPham

	WHERE SanPhamId=@SanPhamId

	ORDER BY SapXep ASC
END

GO
/****** Object:  StoredProcedure [anhtc].[admin_GetBlogByAll]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 21-12-2017
-- Description:	xem bài blog
-- =============================================
CREATE PROCEDURE [anhtc].[admin_GetBlogByAll]
AS
BEGIN
	SELECT  id
			, DanhMucId
			, Name
			, NoiDung
			, TieuDe
			, MoTa
			, TuKhoa
			, SapXep
			, ISNULL (CONVERT(VARCHAR(50), NgayTao,103),'') AS 'NgayTao'
			, ISNULL (CONVERT(VARCHAR(50), NgayDang,103),'') AS 'NgayDang'
			, HinhAnh
			, ArticleId
			, Active
			,CASE
					WHEN HinhAnh IS NOT NULL  THEN 'Common/ShowPhotoBlogById?Id=' +  CAST(Id AS nvarchar(36))+'&' +CONVERT(varchar(24),GETDATE(),120)
					ELSE NULL
				 END  AS Anh
	FROM    Blog
	ORDER BY NgayTao DESC, SapXep ASC
END

GO
/****** Object:  StoredProcedure [anhtc].[admin_GetBlogById]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 21-12-2017
-- Description:	sửa bài blog theo Id
-- =============================================
CREATE PROCEDURE [anhtc].[admin_GetBlogById]
@id uniqueidentifier='3f17e927-2206-4537-aa85-fe01949a5799'
AS
BEGIN
	SELECT  id
			, DanhMucId
			, Name
			, NoiDung
			, TieuDe
			, MoTa
			, TuKhoa
			, SapXep
			, ISNULL (CONVERT(VARCHAR(50), NgayTao,103),'') AS 'NgayTao'
			, ISNULL (CONVERT(VARCHAR(50), NgayDang,103),'') AS 'NgayDang'
			, HinhAnh
			, ArticleId
			, Active
			,CASE
					WHEN HinhAnh IS NOT NULL  THEN 'Common/ShowPhotoBlogById?Id=' +  CAST(Id AS nvarchar(36))+'&' +CONVERT(varchar(24),GETDATE(),120)
					ELSE NULL
				 END  AS Anh
	FROM    Blog
	WHERE id=@id
END

GO
/****** Object:  StoredProcedure [anhtc].[admin_GetdanhMucByAll]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 13-12-2017
-- Description:	lấy danh sách danh mục theo tỉnh  và  danh mục cha
-- =============================================
CREATE PROCEDURE [anhtc].[admin_GetdanhMucByAll]
	@TinhId int =1
	,@Id int=0
AS
BEGIN
	IF(@TinhId=0)
		BEGIN 
			SELECT        Id
						, ParentId
						, Ma
						, Ten						
						, SapSep
						, TinhId
			FROM            DanhMuc
			WHERE (Id=@TinhId) OR (ParentId=@Id)
			ORDER BY SapSep ASC
		END
	ELSE
		BEGIN
			SELECT        Id
						, ParentId
						, Ma
						, Ten
						, SapSep
						, TinhId
			FROM DanhMuc
			WHERE (Id=@Id OR ParentId=@Id) and TinhId = @TinhId
			ORDER BY SapSep ASC
		END
END

GO
/****** Object:  StoredProcedure [anhtc].[admin_GetdanhMucById]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 13-12-2017
-- Description:	lấy danh mục để sửa
-- =============================================
CREATE PROCEDURE [anhtc].[admin_GetdanhMucById]
	@Id int=1
AS
BEGIN
	SELECT        Id, ParentId, Ma, Ten, SapSep, TinhId
	FROM            DanhMuc
	WHERE Id=@Id
END

GO
/****** Object:  StoredProcedure [anhtc].[admin_GetdanhMucBySelect]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 14-12-2017
-- Description:	Lấy toàn  bộ danh mục
-- =============================================
CREATE PROCEDURE [anhtc].[admin_GetdanhMucBySelect]
	@TinhId int=1,
	@ParentId INT =2
AS
BEGIN
	IF(@TinhId=0)
	BEGIN
		SELECT        Id, ParentId, Ma, Ten, SapSep, TinhId
		FROM            DanhMuc
		WHERE ParentId=@ParentId
		ORDER BY SapSep ASC
	END
	ELSE
		BEGIN 
			SELECT   Id, ParentId, Ma, Ten, SapSep, TinhId
			FROM            DanhMuc
			WHERE TinhId=@TinhId AND (ParentId=@ParentId)
			
			ORDER BY SapSep ASC
		END
END

GO
/****** Object:  StoredProcedure [anhtc].[admin_GetQuanHuyenByAll]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 13-12-2017
-- Description:	lấy danh sách quận huyện theo tỉnh
-- =============================================
CREATE PROCEDURE [anhtc].[admin_GetQuanHuyenByAll]
	@TinhId int=2
AS
BEGIN
	IF (@TinhId=0)
	BEGIN
		SELECT        a.Id, a.TinhId, a.Name,a.SapXep,b.Name aS 'TenTinh'
		FROM        QuanHuyen a INNER JOIN Tinh b ON  a.TinhId =b.Id
		ORDER BY a.SapXep ASC
	END
	ELSE
	BEGIN

		SELECT        a.Id, a.TinhId, a.Name,a.SapXep,b.Name aS 'TenTinh'
		FROM        QuanHuyen a INNER JOIN Tinh b ON  a.TinhId =b.Id
		WHERE a.TinhId=@TinhId
		ORDER BY a.SapXep ASC
	END
END

GO
/****** Object:  StoredProcedure [anhtc].[admin_GetQuanHuyenById]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 13-12-2017
-- Description:	thêm mới quận huyện
-- =============================================
CREATE PROCEDURE [anhtc].[admin_GetQuanHuyenById]
	@Id int=1
	
AS
BEGIN
	SELECT        Id, TinhId, Name, SapXep
	FROM            QuanHuyen
	WHERE Id=@Id
END

GO
/****** Object:  StoredProcedure [anhtc].[admin_GetSanPhamByAll]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 13-12-2017
-- Description:	Lấy danh sách sản phẩm
-- =============================================
CREATE PROCEDURE [anhtc].[admin_GetSanPhamByAll]
	
AS
BEGIN
	SELECT  a.Id
			, a.ArticleId
			, a.Name
			, a.GiaMoi
			, a.ChietKhau
			, a.SapSep
			, a.TrangThai
			, a.Active
			, a.HinhAnh
			, b.Ten
			,ISNULL (CONVERT(VARCHAR(50), a.NgayBatDau,103),'') AS 'NgayBatDau'
			,ISNULL (CONVERT(varchar(50), a.NgayKetThuc, 103), '') AS 'NgayKetThuc'
			,ISNULL (CONVERT(varchar(50), a.NgayTao, 103), '') AS 'NgayTao'	
			

	FROM    SanPham a INNER JOIN DanhMuc b on a.DanhMucId=b.Id
	ORDER BY a.NgayTao DESC ,a.SapSep ASC
END

GO
/****** Object:  StoredProcedure [anhtc].[admin_GetTinhByAll]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 13-12-2017
-- Description:	Lấy danh sách các tỉnh  thành phố
-- =============================================
CREATE PROCEDURE [anhtc].[admin_GetTinhByAll]
AS
BEGIN
	SELECT        Id, Name,Sapsep
	FROM            Tinh	
	ORDER BY sapsep ASC 
END

GO
/****** Object:  StoredProcedure [anhtc].[admin_GetUpdateSanPhamById]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 12-12-2017
-- Description:	Thêm mới sản  phẩm
-- =============================================
CREATE PROCEDURE [anhtc].[admin_GetUpdateSanPhamById]
	@Id uniqueidentifier='1E975C95-C6FD-4BCF-A5EA-5CF2D5C08B79'
AS
BEGIN
		SELECT  Id
				, ArticleId
				, Name
				, TieuDe
				, MoTa
				, TuKhoa
				, GiaCu
				, GiaMoi
				, ChietKhau
				, KhuyenMaiId
				,convert(varchar(18),NgayBatDau,103)+' '+convert(varchar(18),NgayBatDau,108) as 'NgayBatDau'
				,convert(varchar(18),NgayKetThuc,103)+' '+convert(varchar(18),NgayKetThuc,108) as'NgayKetThuc'			
				, DanhMucId
				, SapSep
				, TrangThai
				, NoiDung
				, Active
				, HinhAnh
				,ISNULL(CONVERT(nvarchar(50), NgayTao,103),'') AS 'NgayTao'
				,CASE
					WHEN HinhAnh IS NOT NULL  THEN 'Common/ShowPhotoSanPhamById?Id=' +  CAST(Id AS nvarchar(36))+'&' +CONVERT(varchar(24),GETDATE(),120)
					ELSE NULL
				 END  AS Anh
				,TuKhoaTimKiem
		FROM	SanPham

		WHERE Id=@Id
END



GO
/****** Object:  StoredProcedure [anhtc].[admin_Login]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ANHTC
-- Create date: 11-12-2017
-- Description:	LOGIN
-- =============================================
CREATE PROCEDURE [anhtc].[admin_Login]
	@UserName nvarchar(150)='anhtc'
	,@PasswordHash nvarchar(550)='123456'
AS
BEGIN
	SELECT        Id
				, Email
				, PasswordHash
				, Phone
				, UserName
				, Avatar
				, CreateDate
				, Active
				, RoleId
				, Hoten
	FROM     dbo.AspNetUser
	WHERE UserName=@UserName AND PasswordHash=@PasswordHash

END

GO
/****** Object:  StoredProcedure [anhtc].[admin_Tag_Delete]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE  PROCEDURE [anhtc].[admin_Tag_Delete]
	@Id int
AS
BEGIN
	Delete Tag 
	where id = @Id
		
END

GO
/****** Object:  StoredProcedure [anhtc].[admin_Tag_Insert]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE  PROCEDURE [anhtc].[admin_Tag_Insert]
	@Name nvarchar(256),
	@SortOrder int,
	@Loai int
AS
BEGIN
	if(@SortOrder =0) 
		set @SortOrder = 999999999
	INSERT INTO Tag
           ([Name],
		    [SortOrder],
			[Type]
		   )
     VALUES
           (@Name, @SortOrder,@Loai)
		
END

GO
/****** Object:  StoredProcedure [anhtc].[admin_Tag_select]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE  PROCEDURE  [anhtc].[admin_Tag_select] 
	@Search nvarchar(250)='',
	@Loai int=0
AS
BEGIN
	SELECT  * FROM Tag
	WHERE 
	(
		Name LIKE '%'+ @Search +'%' 
		or 
		@Search ='' or @Search is null
	)
	 AND (@Loai =0 or [Type] = @Loai)
	ORDER BY SortOrder	
END


GO
/****** Object:  StoredProcedure [anhtc].[admin_Tag_select_ById]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE  PROCEDURE [anhtc].[admin_Tag_select_ById] 
	@Id int=1
AS
BEGIN
	select * from Tag
	
	where id=@Id
			
END

GO
/****** Object:  StoredProcedure [anhtc].[admin_Tag_Update]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE  PROCEDURE [anhtc].[admin_Tag_Update]
	@Id int=1,
	@Name nvarchar(256)=N'Sản Phẩm',
	@SortOrder int=1,
	@Loai int=1
AS
BEGIN
	if(@SortOrder =0) 
		set @SortOrder = 999999999
	UPDATE Tag
	   SET [Name] = @Name,
		   [SortOrder] = @SortOrder,
		   [Type] = @Loai
	 WHERE id=@Id

END

GO
/****** Object:  StoredProcedure [anhtc].[admin_UpdateAnhSanPham]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 20-12-2017
-- Description:	Thêm mới ảnh sản phẩm
-- =============================================
CREATE PROCEDURE [anhtc].[admin_UpdateAnhSanPham]
	@Id uniqueidentifier=''
	,@Name  nvarchar(150)=''
	,@SanPhamId uniqueidentifier=''
	,@HinhAnh varbinary(MAX)=0x20	
	,@SapXep int=1
	,@IsChangeImage bit=0
AS
BEGIN
	UPDATE AnhSanPham
	SET		Name = @Name
			, SanPhamId = @SanPhamId	
			, NgayTao = getdate()
			, SapXep = @SapXep

	WHERE Id=@Id

	-- Thao tác chỉnh sửa ảnh người dùng
	IF(@HinhAnh != 0x20)
	BEGIN
	UPDATE AnhSanPham
		SET  HinhAnh = @HinhAnh
		WHERE Id=@Id
	END
	ELSE			
	BEGIN
		IF(@IsChangeImage=1) -- co thao tac chinh sua anh 
		BEGIN
		UPDATE AnhSanPham
			SET HinhAnh=null
			WHERE Id=@Id
		END
	END
END

GO
/****** Object:  StoredProcedure [anhtc].[admin_UpdateBlog]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 21-12-2017
-- Description:	sửa bài blog theo Id
-- =============================================
CREATE PROCEDURE [anhtc].[admin_UpdateBlog]
@id uniqueidentifier=''
,@DanhMucId INT
,@Name NVARCHAR(150)=''
,@NoiDung NTEXT
,@TieuDe NVARCHAR(70)=''
,@MoTa NVARCHAR(170)=''
,@TuKhoa NVARCHAR(170)=''
,@SapXep INT
,@NgayDang DATETIME=''
,@HinhAnh varbinary(MAX)=0x20	
,@Active BIT=0
,@IsChangeImage bit=0

AS 
BEGIN
	UPDATE  Blog
	SET     DanhMucId = @DanhMucId
			, Name = @Name
			, NoiDung = @NoiDung
			, TieuDe = @TieuDe
			, MoTa = @MoTa
			, TuKhoa = @TuKhoa
			, SapXep = @SapXep
			, NgayTao = GETDATE()
			, NgayDang = @NgayDang			
			, Active = @Active
	 WHERE  id =@id
	 -- Thao tác chỉnh sửa ảnh người dùng
	IF(@HinhAnh != 0x20)
	BEGIN
	UPDATE Blog
		SET  HinhAnh = @HinhAnh
		WHERE id=@id
	END
	ELSE			
	BEGIN
		IF(@IsChangeImage=1) -- co thao tac chinh sua anh 
		BEGIN
		UPDATE Blog
			SET HinhAnh=null
			WHERE id=@id
		END
	END
END

GO
/****** Object:  StoredProcedure [anhtc].[admin_UpdatedanhMuc]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 13-12-2017
-- Description:	Thêm mới danh mục
-- =============================================
CREATE PROCEDURE [anhtc].[admin_UpdatedanhMuc]
	@Id int=1
	, @ParentId int
	, @Ma nvarchar(50)=''
	, @Ten nvarchar(150)=''
	, @SapSep int=10
	, @TinhId int  =1
AS
BEGIN
	UPDATE  DanhMuc
	SET     ParentId = @ParentId
			, Ma = @Ma
			, Ten = @Ten
			, SapSep = @SapSep
			, TinhId = @TinhId
	WHERE Id=@Id
END

GO
/****** Object:  StoredProcedure [anhtc].[admin_UpdateQuanHuyen]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 13-12-2017
-- Description:	thêm mới quận huyện
-- =============================================
CREATE PROCEDURE [anhtc].[admin_UpdateQuanHuyen]
	@Id int=1
	,@TinhId int =1
	,@Name nvarchar(150)=''
	,@SapXep int=10
AS
BEGIN
	UPDATE       QuanHuyen
	SET                TinhId = @TinhId, Name = @Name, SapXep = @SapXep
	WHERE Id=@Id
END

GO
/****** Object:  StoredProcedure [anhtc].[admin_UpdateSanPham]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		anhtc
-- Create date: 12-12-2017
-- Description:	Thêm mới sản  phẩm
-- =============================================
CREATE PROCEDURE [anhtc].[admin_UpdateSanPham]
	@Id uniqueidentifier='1E975C95-C6FD-4BCF-A5EA-5CF2D5C08B79'
	,@Name nvarchar(150)=N'Sản phẩm'
	, @TieuDe nvarchar(150)=''
	, @MoTa nvarchar(170)=''
	, @TuKhoa nvarchar(170)=''
	, @GiaCu int=200000
	, @GiaMoi int=100000
	, @ChietKhau int=10
	, @KhuyenMaiId int=1
	, @NgayBatDau datetime=''
	, @NgayKetThuc datetime=''
	, @DanhMucId int=2
	, @SapSep int=10
	, @TrangThai int=2
	, @NoiDung ntext=''
	, @Active	bit=1
	, @HinhAnh varbinary(max)=0x20
	, @IsChangeImage bit=0
	, @TuKhoaTimKiem nvarchar(170)=''
AS
BEGIN
		UPDATE  SanPham

		SET  Name = @Name
			, TieuDe = @TieuDe
			, MoTa = @MoTa
			, TuKhoa = @TuKhoa
			, GiaCu = @GiaCu
			, GiaMoi = @GiaMoi
			, ChietKhau = @ChietKhau
			, KhuyenMaiId = @KhuyenMaiId
			, NgayBatDau = @NgayBatDau
			, NgayKetThuc = @NgayKetThuc
			, DanhMucId = @DanhMucId
			, SapSep = @SapSep
			, TrangThai = @TrangThai
			, NoiDung = @NoiDung
			, Active = @Active
			, NgayTao = GETDATE()
			, TuKhoaTimKiem = @TuKhoaTimKiem

			WHERE Id=@Id

	-- Thao tác chỉnh sửa ảnh người dùng
	IF(@HinhAnh != 0x20)
	BEGIN
	UPDATE SanPham
		SET  HinhAnh = @HinhAnh
		WHERE Id=@Id
	END
	ELSE			
	BEGIN
		IF(@IsChangeImage=1) -- co thao tac chinh sua anh 
		BEGIN
		UPDATE SanPham
			SET HinhAnh=null
			WHERE Id=@Id
		END
	END
END



GO
/****** Object:  StoredProcedure [dbo].[admin_AddController]    Script Date: 1/12/2018 1:38:44 PM ******/
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
/****** Object:  StoredProcedure [dbo].[admin_CheckControllerandAction]    Script Date: 1/12/2018 1:38:44 PM ******/
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
/****** Object:  StoredProcedure [dbo].[admin_checkRoleByUserId]    Script Date: 1/12/2018 1:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
-- Author:		anhtc
-- Create date: 11-12-2017
-- Description:	kiểm tra quyền trước khi đăng nhập
-- =============================================
CREATE PROCEDURE [dbo].[admin_checkRoleByUserId]
	@userId uniqueidentifier='01056748-95FD-40D0-A89B-CA0B58B98379'
AS
BEGIN
	SELECT         B.RoleName
	FROM            AspNetUser A INNER JOIN
                    AspNetRole B ON A.RoleId = B.Id
					WHERE A.Id=@userId
END

GO
/****** Object:  StoredProcedure [dbo].[admin_CreateRoleId]    Script Date: 1/12/2018 1:38:44 PM ******/
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
/****** Object:  StoredProcedure [dbo].[admin_DeletePhanquyenUserById]    Script Date: 1/12/2018 1:38:44 PM ******/
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
/****** Object:  StoredProcedure [dbo].[admin_DeleteRoleById]    Script Date: 1/12/2018 1:38:44 PM ******/
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
/****** Object:  StoredProcedure [dbo].[admin_DeleteUserById]    Script Date: 1/12/2018 1:38:44 PM ******/
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
/****** Object:  StoredProcedure [dbo].[admin_GetAllActionByController]    Script Date: 1/12/2018 1:38:44 PM ******/
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
/****** Object:  StoredProcedure [dbo].[admin_GetAllControllerByRole]    Script Date: 1/12/2018 1:38:44 PM ******/
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
/****** Object:  StoredProcedure [dbo].[admin_GetAllRoles]    Script Date: 1/12/2018 1:38:44 PM ******/
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
/****** Object:  StoredProcedure [dbo].[admin_GetAllRolesController]    Script Date: 1/12/2018 1:38:44 PM ******/
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
/****** Object:  StoredProcedure [dbo].[admin_GetAllUser]    Script Date: 1/12/2018 1:38:44 PM ******/
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
/****** Object:  StoredProcedure [dbo].[admin_GetMotaQuyenByAction]    Script Date: 1/12/2018 1:38:44 PM ******/
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
/****** Object:  StoredProcedure [dbo].[admin_GetQuyenByAll]    Script Date: 1/12/2018 1:38:45 PM ******/
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
/****** Object:  StoredProcedure [dbo].[admin_GetRoleById]    Script Date: 1/12/2018 1:38:45 PM ******/
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
/****** Object:  StoredProcedure [dbo].[admin_GetUpdateUserById]    Script Date: 1/12/2018 1:38:45 PM ******/
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
					WHEN A.Avatar IS NOT NULL  THEN 'Roles/ShowPhotoById?Id=' +  CAST(A.Id AS nvarchar(36))+'&' +CONVERT(varchar(24),GETDATE(),120)
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
/****** Object:  StoredProcedure [dbo].[admin_InsertUser]    Script Date: 1/12/2018 1:38:45 PM ******/
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
						, @Hoten
						)
END

GO
/****** Object:  StoredProcedure [dbo].[admin_UpdateMotaQuyen]    Script Date: 1/12/2018 1:38:45 PM ******/
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
/****** Object:  StoredProcedure [dbo].[admin_UpdatePhanquyenUserById]    Script Date: 1/12/2018 1:38:45 PM ******/
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
/****** Object:  StoredProcedure [dbo].[admin_UpdateRoles]    Script Date: 1/12/2018 1:38:45 PM ******/
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
/****** Object:  StoredProcedure [dbo].[admin_UpdateUser]    Script Date: 1/12/2018 1:38:45 PM ******/
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
	,@IsChangeImage bit=0
AS
BEGIN
	UPDATE  AspNetUser

	SET      Email = @Email
			, PasswordHash = @PasswordHash
			, Phone = @Phone
			, UserName = @UserName
			
			, CreateDate = GETDATE()
			, Active = @Active
			, RoleId = @RoleId
			, Hoten = @Hoten
			WHERE Id= @Id


	-- Thao tác chỉnh sửa ảnh người dùng
	IF(@Avatar != 0x20)
	BEGIN
	UPDATE AspNetUser
		SET  Avatar = @Avatar
		WHERE Id=@Id
	END
	ELSE			
	BEGIN
		IF(@IsChangeImage=1) -- co thao tac chinh sua anh 
		BEGIN
		UPDATE AspNetUser
			SET Avatar=null
			WHERE Id=@Id
		END
	END
END

GO
/****** Object:  StoredProcedure [dbo].[PasgoManager_BlogTags_CapNhat]    Script Date: 1/12/2018 1:38:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE  PROCEDURE [dbo].[PasgoManager_BlogTags_CapNhat] 
	@BlogId int =15,
	@TagIds varchar(500)='14,12',
	@UnTagIds varchar(500)='13,15,16,17,18,19,20,21,22,23,24,25,27',
	@SortOrder  varchar(500)='4,2'
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
	
		exec PasgoManager_BlogTags_Delete @BlogId,  @UnTagIds
		exec PasgoManager_BlogTags_Insert @BlogId,  @TagIds ,@SortOrder
		
		SELECT 1 
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		SELECT 0
	END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[PasgoManager_BlogTags_Delete]    Script Date: 1/12/2018 1:38:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[PasgoManager_BlogTags_Delete] 
	@BlogId int=4,
	@UnTagIds varchar(500)='6'
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
		--B1: Tao bang trung gian để lưu id tag
		DECLARE @TagId int
		DECLARE @i INT,
		        @cnt INT = 1
		
		DECLARE @dtUpdate TABLE(id INT, val int)		
		SET @i = 1
		
		INSERT INTO @dtUpdate
		SELECT *
		FROM   dbo.fn_Split(@UnTagIds, ',')
		SELECT @cnt = COUNT(*)
		FROM   @dtUpdate ;
						
		WHILE (@i <= @cnt)
		BEGIN
		    SET @TagId = (
		            SELECT val
		            FROM   @dtUpdate
		            WHERE  id = @i
		        )		    		    		    
		    --B2: Xóa Bản ghi 
			Delete from BlogTags where BlogId = @BlogId and TagId = @TagId
		    SET @i = @i + 1;
		END		
		SELECT 1 
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		SELECT 0
	END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[PasgoManager_BlogTags_Insert]    Script Date: 1/12/2018 1:38:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[PasgoManager_BlogTags_Insert] 
	 @BlogId int=15
	,@TagIds varchar(500)='14,12'
	,@SortOrder nvarchar(500)='4,2'
	
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
		--B1: Tao bang trung gian để lưu id tag
		
		DECLARE @i INT,@cnt INT = 1
		
		DECLARE @dtInsert TABLE(id INT, val int)	
		DECLARE @dtSort TABLE(id INT, val int)	
		SET @i = 1
		
		INSERT INTO @dtInsert
		SELECT * FROM   dbo.fn_Split(@TagIds, ',')
		SELECT @cnt = COUNT(*) FROM   @dtInsert ;
		
		INSERT INTO @dtSort
		SELECT * FROM   dbo.fn_Split(@SortOrder, ',')
		
		WHILE (@i <= @cnt)
		BEGIN
			DECLARE @TagId int, @SortIndex Int
		    SET @TagId = ( SELECT val FROM @dtInsert  WHERE  id = @i )	
			SET @SortIndex = ( SELECT val FROM @dtSort WHERE  id = @i )	
			    		    		    
		    --B2: Insert du lieu moi
			if((select count(Id) from BlogTags where BlogId = @BlogId and TagId = @TagId)=0)
			begin
				INSERT INTO [dbo].[BlogTags]
				   ([Id]
				   ,[BlogId]
				   ,[TagId]
				   ,[SortOrder]
				   )				
				VALUES
				   (NEWID()
				   ,@BlogId
				   ,@TagId
				   ,@SortIndex
				   )
		    End 

			else
			declare @IdTinTuc uniqueidentifier
			set @IdTinTuc=(select Id from BlogTags where BlogId = @BlogId and TagId =@TagId)
			begin
				UPDATE       BlogTags
				SET          SortOrder = @SortIndex
				WHERE [Id] = @IdTinTuc
			end 

		 --select @TagId  as 'Tag'
		 --select @SortIndex as 'Sắp xếp'

 		    SET @i = @i + 1;
		END		
		SELECT 1 
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		SELECT 0
	END CATCH

END

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1,CHO SẢN PHẨM, 2 CHO BLOG' , @level0type=N'SCHEMA',@level0name=N'anhtc', @level1type=N'TABLE',@level1name=N'Tag', @level2type=N'COLUMN',@level2name=N'Type'
GO
USE [master]
GO
ALTER DATABASE [Linja-Developer] SET  READ_WRITE 
GO
