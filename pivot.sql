--YC1
CREATE DATABASE BTXOAYBANG
--DROP DATABASE BTXOAYBANG
USE BTXOAYBANG;

--DROP TABLE BANGDIEM
CREATE TABLE BANGDIEM
(
	MASV char(7),
	MON nvarchar(20),
	DIEM float,
);

INSERT INTO BANGDIEM VALUES ('1712801', N'Toán', 8.5);
INSERT INTO BANGDIEM VALUES ('1712802', N'Lý', 8.5);
INSERT INTO BANGDIEM VALUES ('1712802', N'Lý', 9);
INSERT INTO BANGDIEM VALUES ('1712803', N'Lý', 8.5);
INSERT INTO BANGDIEM VALUES ('1712803', N'Hóa', 7);


SELECT * FROM BANGDIEM;

--YC2
--SELECT *
--FROM
--(SELECT MASV, MON, DIEM FROM BANGDIEM) BANGNGUON
--PIVOT
--(
--	MAX(DIEM)
--	FOR MON IN ([Toán], [Lý], [Hóa])
--) BANGCHUYEN;

DECLARE @subject AS NVARCHAR(MAX),
		@query AS NVARCHAR(MAX)
 
--Lay toan bo mon hoc trong column mon hoc
select @subject = STUFF((SELECT ',' + QUOTENAME(MON) 
                    from BANGDIEM
                    group by MON
            FOR XML PATH(''), TYPE
            ).value('.', 'NVARCHAR(MAX)') 
        ,1,1,'')

--Lay diem cao nhat tai tung mon hoc cua moi sinh vien
set @query = 'SELECT MASV,' + @subject + ' from 
             (
                select MASV, MON, DIEM
                from BANGDIEM
            ) src
            pivot 
            (
                max(DIEM)
                for MON in (' + @subject + ')
            ) p '

execute(@query);

INSERT INTO BANGDIEM VALUES ('1712803', N'Văn', 7);




