USE [alternate]
GO
/****** Object:  StoredProcedure [pla].[sc_login_key_new]    Script Date: 24.12.2013 12:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [pla].[sc_login_key_new]
	@value1 VARCHAR(16),
	@value2 VARCHAR(16)
with execute as owner
AS
BEGIN
	DECLARE @coma AS INT = 27;
	DECLARE @res AS char(60) = SUBSTRING(REPLACE(NEWID(), '-', ''), 1, 29)+ CAST(@coma AS CHAR(2)) + SUBSTRING(REPLACE(NEWID(), '-', ''), 1, 29);
	DECLARE @link AS INT = 0;

	SELECT @link = dp.link FROM adm.dpr_param AS dp WHERE s_name = 'login_key';	
	IF @link = 0 
		INSERT INTO adm.dpr_param (s_name, s_value) VALUES ('login_key', adm.Encript_AES(@res, @value1, @value2));
	ELSE
	BEGIN
		-- получаем данные по текущему пользователю
		DECLARE @old_value AS CHAR(60), @u AS VARCHAR(60), @p AS VARCHAR(60);
		SELECT @old_value = adm.Decript_AES(dp.s_value, @value1, @value2) FROM adm.dpr_param dp WHERE dp.link = @link;
		SET @u = SUBSTRING(@old_value, 1, @coma);
		SET @p = SUBSTRING(@old_value, @coma+1, 60);
		
		IF EXISTS (SELECT * FROM sys.syslogins WHERE name = @u)
		begin 
			exec sp_droplogin @u
			EXEC sp_addlogin @u, @p, db_name(), @@LANGUAGE
		end			
			
		PRINT @old_value
		PRINT @u 
		PRINT @p
				
		UPDATE adm.dpr_param SET s_value = adm.Encript_AES(@res, @value1, @value2) WHERE link = @link;
	END

/*
	declare @res as nvarchar(max)
	exec pla.sc_login_key_new 'alternativeloyal', 'loyalalternative'
	select @res = s_value from adm.dpr_param
	select adm.decript_aes (@res, 'alternativeloyal', 'loyalalternative')
*/
END

