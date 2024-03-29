USE [alternate]
GO
/****** Object:  StoredProcedure [web].[processing_web]    Script Date: 13.12.2013 21:09:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [web].[processing_web]( 
	@activ_dir TINYINT, -- направление активации 1 - активация на телефонный номер
	@pref VARCHAR(64), -- префикс платежной системы
	@pin VARCHAR(12), -- pin-код
	@phone_num VARCHAR(10), -- номер телефона, на который нужно активировать карту
	@phone_ab VARCHAR(10), -- номер телефона, с кторого пришла СМС
	@txt VARCHAR(MAX), -- текст сообщения
	@op VARCHAR(512), -- оператор связи
	@cn CHAR(2), -- двухбуквенный код страны
	@sn VARCHAR(4), -- краткий номер смс
	@link_processign INT -- связанный с процессингом идентификатор
)
WITH EXECUTE AS OWNER
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @error_id INT, @error_text VARCHAR(512);

	BEGIN TRY
	
		-- Проверка переданных значений
		 EXEC adm.check_pin_phone @pin, @phone_num;
		 
		---- Получаем карточку
		DECLARE @card_id BIGINT = -1;
		SELECT @card_id = pc.link FROM adm.pr_cards AS pc WHERE pc.s_pin = HASHBYTES('SHA2_512', @pref+@pin);
		IF @card_id = -1
		BEGIN
			set @error_id = adm.get_self_error('ECardNotFound'); 
			SET @error_text = CONCAT('Карта соответствующая pin-коду "', @pin, '" - не найдена.');
			THROW @error_id, @error_text, 1;
		END
		
		-- Попытка перевода д/с
			INSERT INTO adm.pr_journal(f_card, f_oper_types, d_date, field_1, field_2) VALUES(@card_id,  3, GETDATE(), 'test', 'test');
		-- Отказ в активации
			INSERT INTO adm.pr_journal(f_card, f_oper_types, d_date, field_1, field_2) VALUES(@card_id,  4, GETDATE(), 'Не готов процессинг', ''); 
		
		INSERT INTO web.pr_processing_res(id, res, res_out) VALUES (@link_processign, 0, 'Не готов процессинг');
		
	END TRY
	BEGIN CATCH
		INSERT INTO web.pr_processing_res(id, res, res_out) VALUES (@link_processign, 0, adm.error_msg());
	END CATCH

END