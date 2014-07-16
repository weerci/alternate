
 
INSERT INTO web.pr_processing(ACTIV_DIR, PREF, PIN, PHONE_NUM, PHONE_AB, TXT, OP, TID, CN, SN)
VALUES
(
	1,
	'yandex',
	'692389776419',
	'9094778733',
	'9094778733',
	'test1',
	'',
	'',
	'',
	''
)
GO 


/* DECLARE @res INT, @res_1 INT
select @res = IDENT_CURRENT('web.pr_processing'), @res_1 = IDENT_CURRENT('web.pr_processing') 

SELECT @res, @res_1  
SELECT * FROM web.pr_processing_res ppr WHERE ppr.id = @res */

/* 
TRUNCATE TABLE web.pr_processing
TRUNCATE TABLE web.pr_processing_res
DELETE FROM web.pr_processing
DELETE FROM web.pr_processing_res
*/

SELECT TOP 4 * FROM web.pr_processing pp ORDER BY pp.link desc
SELECT TOP 4 * FROM web.pr_processing_res ppr ORDER BY ppr.id DESC
SELECT TOP 4 * FROM adm.pr_journal pj ORDER BY link DESC
SELECT web.check_processing_id()
