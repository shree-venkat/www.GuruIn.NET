SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

DELETE FROM [dbo].[BadWords] 
DELETE FROM [dbo].[Comments] 
DELETE FROM [dbo].[Tags] 
DELETE FROM [dbo].[Posts] 
DELETE FROM [dbo].[Categories] 
GO

DROP PROCEDURE [dbo].[CreateBlogPost]
GO

DROP FUNCTION [dbo].[fnSplit]
GO
