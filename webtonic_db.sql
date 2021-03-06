CREATE DATABASE [webtonic_db]
GO
USE [webtonic_db]
GO
/****** Object:  Table [dbo].[CourseType]    Script Date: 2021-08-14 06:57:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CourseType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](100) NULL,
	[CourseCode] [varchar](5) NULL,
 CONSTRAINT [PK_CourseType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Students]    Script Date: 2021-08-14 06:57:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Students](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StudentNumber] [int] NULL,
	[FirstName] [varchar](50) NULL,
	[Surname] [varchar](100) NULL,
	[Course] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[Grade] [varchar](1) NULL,
 CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [FK_Students_CourseType] FOREIGN KEY([Course])
REFERENCES [dbo].[CourseType] ([Id])
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_Students_CourseType]
GO
/****** Object:  StoredProcedure [dbo].[sp_deleteStudent]    Script Date: 2021-08-14 06:57:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_deleteStudent] @id int, @uniqueid int output
AS 
BEGIN
  DELETE FROM dbo.Students
  WHERE Id = @id
  SET @uniqueid = -1
  RETURN  @uniqueid
END


GO
/****** Object:  StoredProcedure [dbo].[sp_getAllCourseTypes]    Script Date: 2021-08-14 06:57:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_getAllCourseTypes] 
AS 
BEGIN
  SELECT * FROM dbo.CourseType  
END


GO
/****** Object:  StoredProcedure [dbo].[sp_getCourseTypes]    Script Date: 2021-08-14 06:57:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_getCourseTypes] @Id int, @CourseCode varchar(5), @Description varchar(100)
AS 
BEGIN
  SET NOCOUNT ON;

  DECLARE @sql NVARCHAR(MAX);

  SET @sql = N'  SELECT Id, CourseCode, Description FROM CourseType				 
		         WHERE Id is not null'
       + CASE WHEN @Id > 0 
              THEN N' AND Id = ' + convert(varchar(max), @Id) + '' ELSE N' ' END  
       + CASE WHEN @CourseCode IS NOT NULL 
              THEN N' AND CourseCode = ''' + @CourseCode + '''' ELSE N' ' END  
       + CASE WHEN @Description IS NOT NULL 
              THEN N' AND Description = ''' + @Description + '''' ELSE N' ' END

   EXECUTE sp_executesql @Sql
                    ,N'@Id INT, @CourseCode VARCHAR(5) 
                       ,@Description VARCHAR(100)'
                    ,@Id
                    ,@CourseCode
                    ,@Description
END


GO
/****** Object:  StoredProcedure [dbo].[sp_getStudent]    Script Date: 2021-08-14 06:57:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_getStudent] @StudentNumber int, @CourseCode varchar(5)
AS 
BEGIN
  SET NOCOUNT ON;

  DECLARE @sql NVARCHAR(MAX);

  SET @sql = N'  select dbo.Students.Id, 
						dbo.Students.StudentNumber, 
						dbo.Students.FirstName, 
						dbo.Students.Surname, 
						dbo.Students.Course, 
						dbo.CourseType.Description, 
						dbo.CourseType.CourseCode, 
						dbo.Students.Grade, 
						dbo.Students.CreatedDate, 
						dbo.Students.ModifiedDate
				from dbo.Students
				inner join dbo.CourseType on dbo.Students.Course = dbo.CourseType.Id
				where dbo.Students.StudentNumber = ' + convert(varchar(max), @StudentNumber)
				+ ' and dbo.CourseType.CourseCode = ''' + @CourseCode + ''''

   EXECUTE sp_executesql @Sql
                    ,N'@StudentNumber INT                    
					,@CourseCode VARCHAR(5)'					                    
                    ,@StudentNumber                    
					,@CourseCode
END


GO
/****** Object:  StoredProcedure [dbo].[sp_getStudents]    Script Date: 2021-08-14 06:57:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_getStudents] @StudentNumber int, @FirstName varchar(50), @Surname varchar(100), @CourseId int, @pageIndex int, @pageSize int
AS 
BEGIN
  SET NOCOUNT ON;

  DECLARE @sql NVARCHAR(MAX);
  DECLARE @total NVARCHAR(MAX);

  SET @sql = N'  select dbo.Students.Id, 
						dbo.Students.StudentNumber, 
						dbo.Students.FirstName, 
						dbo.Students.Surname, 
						dbo.Students.Course, 
						dbo.CourseType.Description as CourseDescription, 
						dbo.CourseType.CourseCode, 
						dbo.CourseType.Id as CourseId,
						dbo.Students.Grade, 
						dbo.Students.CreatedDate, 
						dbo.Students.ModifiedDate
				from dbo.Students
				inner join dbo.CourseType on dbo.Students.Course = dbo.CourseType.Id				 
		        where dbo.Students.Id is not null'       
	   + CASE WHEN @CourseId > 0 
              THEN N' AND dbo.Students.Course = ' + convert(varchar(max), @CourseId) + '' ELSE N' ' END  
	   + CASE WHEN @StudentNumber > 0 
              THEN N' AND dbo.Students.StudentNumber = ' + convert(varchar(max), @StudentNumber) + '' ELSE N' ' END  
       + CASE WHEN @FirstName IS NOT NULL 
              THEN N' AND dbo.Students.FirstName = ''' + @FirstName + '''' ELSE N' ' END  
       + CASE WHEN @Surname IS NOT NULL 
              THEN N' AND dbo.Students.Surname = ''' + @Surname + '''' ELSE N' ' END
       + N' ORDER BY dbo.Students.Id OFFSET '+ convert(varchar(max), @pageSize) +'*('+ convert(varchar(max), @pageIndex) +'-1) ROWS FETCH NEXT '+ convert(varchar(max), @pageSize) +' ROWS ONLY '

   EXECUTE sp_executesql @Sql
                    ,N'@StudentNumber INT
                    ,@FirstName VARCHAR(50)
					,@Surname VARCHAR(100)
					,@CourseId INT
					,@pageIndex INT
					,@pageSize INT'					
                    ,@StudentNumber
                    ,@FirstName
					,@Surname
					,@CourseId
					,@pageIndex
					,@pageSize

	SET @total = N'  select count(*) as totalCount		
					 from dbo.Students			 
					 where dbo.Students.Id is not null'       
	   + CASE WHEN @CourseId > 0 
              THEN N' AND dbo.Students.Course = ' + convert(varchar(max), @CourseId) + '' ELSE N' ' END  
	   + CASE WHEN @StudentNumber > 0 
              THEN N' AND dbo.Students.StudentNumber = ' + convert(varchar(max), @StudentNumber) + '' ELSE N' ' END  
       + CASE WHEN @FirstName IS NOT NULL 
              THEN N' AND dbo.Students.FirstName = ''' + @FirstName + '''' ELSE N' ' END  
       + CASE WHEN @Surname IS NOT NULL 
              THEN N' AND dbo.Students.Surname = ''' + @Surname + '''' ELSE N' ' END

	EXECUTE sp_executesql @total
END


GO
/****** Object:  StoredProcedure [dbo].[sp_putStudent]    Script Date: 2021-08-14 06:57:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_putStudent] @Id bigint, @StudentNumber varchar(100), @FirstName varchar(100), @Surname varchar(50), @CourseId varchar(20), @Grade varchar(1), @ModifiedDate datetime, @uniqueid int output
AS 
BEGIN
  UPDATE Students
  SET StudentNumber = @StudentNumber,
  FirstName = @FirstName,
  Surname = @Surname,
  Grade = @Grade,
  Course = @CourseId,
  ModifiedDate = @ModifiedDate
  WHERE Id = @Id
  SET @uniqueid=SCOPE_IDENTITY()
  RETURN  @uniqueid
END


GO
/****** Object:  StoredProcedure [dbo].[sp_setCourseType]    Script Date: 2021-08-14 06:57:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_setCourseType] @CourseCode varchar(5), @Description varchar(100), @uniqueid int output
AS 
BEGIN
  INSERT INTO CourseType (CourseCode, Description)
  VALUES (@CourseCode, @Description)
  SET @uniqueid=SCOPE_IDENTITY()
  RETURN  @uniqueid
END


GO
/****** Object:  StoredProcedure [dbo].[sp_setStudent]    Script Date: 2021-08-14 06:57:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_setStudent] @StudentNumber varchar(100), @FirstName varchar(100), @Surname varchar(50), @CourseId varchar(20), @Grade varchar(1), @CreatedDate datetime, @ModifiedDate datetime, @uniqueid int output
AS 
BEGIN
  INSERT INTO Students (StudentNumber, FirstName, Surname, Course, Grade, CreatedDate, ModifiedDate)
  VALUES (@StudentNumber, @FirstName, @Surname, @CourseId, @Grade, @CreatedDate, @ModifiedDate)
  SET @uniqueid=SCOPE_IDENTITY()
  RETURN  @uniqueid
END


GO
