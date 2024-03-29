USE [alternate]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER trigger [web].[ai_pr_processing]
on [web].[pr_processing]
after INSERT
as
	declare @f_link bigint;
	declare @activ_dir TINYINT;
	declare @pref varchar(64);
	declare @pin varchar(12);
	declare @phone_num varchar(10);
	declare @phone_ab varchar(10);
	declare @txt VARCHAR(MAX);
	declare @op varchar(32);
	declare @cn char(2);
	declare @sn CHAR(4);
	
	select  @f_link = link, @activ_dir = activ_dir, @pref = pref, @pin = pin, @phone_num = phone_num, @phone_ab = phone_ab,
		@txt = txt, @op = op, @cn = cn, @sn = sn from inserted;
	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;
			
	EXEC web.processing_web @activ_dir, @pref, @pin, @phone_num, @phone_ab, @txt, @op, @cn, @sn, @f_link;
	