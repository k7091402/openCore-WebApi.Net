use master
go

if EXISTS (select * from sys.databases where name = N'CoreAPI')
drop database [CoreAPI]
go

create database [CoreAPI]
go

--------------------------------------------------
USE [CoreAPI]
GO

/* GET */
create schema [get]
go

create procedure [get].[test] (@params nvarchar(max))
as
begin
	
	set nocount on;

	declare @request_id		int
	      , @message		nvarchar(4000)

	select 
		@request_id = json_value(@params, '$.request_id'),
		@message = json_value(@params, '$.message')

	select (select 
			N'get' as verb,
			@request_id as request_id,
			@message as [message]
			for json PATH, WITHOUT_ARRAY_WRAPPER) as result
end
go

/* POST */
create schema [pos]
go

create procedure [pos].[test] (@params nvarchar(max))
as
begin	
	
	set nocount on;

	declare @request_id		int
	      , @message		nvarchar(4000)

	select 
		@request_id = json_value(@params, '$.request_id'),
		@message = json_value(@params, '$.message')

	select (select 
			N'post' as verb,
			@request_id as request_id,
			@message as [message]
			for json path, WITHOUT_ARRAY_WRAPPER) as result

end
go

/* PUT */
create schema [put]
go

create procedure [put].[test] (@params nvarchar(max))
as
begin
	
	set nocount on;

	declare @request_id		int
	      , @message		nvarchar(4000)

	select 
		@request_id = json_value(@params, '$.request_id'),
		@message = json_value(@params, '$.message')

	select (select 
		N'put' as verb,
		@request_id as request_id,
		@message as [message]
	for json path, WITHOUT_ARRAY_WRAPPER) as result

end
go

/* DELETE */
create schema [del]
go

create procedure [del].[test] (@params nvarchar(max))
as
begin
	set nocount on;

	declare @request_id		int
	      , @message		nvarchar(4000)

	select 
		@request_id = json_value(@params, '$.request_id'),
		@message = json_value(@params, '$.message')

	select(select 
			N'delete' as verb,
			@request_id as request_id,
			@message as [message]
			for json path, WITHOUT_ARRAY_WRAPPER) as result

end
go

--exec get.test '{"request_id":0, "message":"It Works"}'
