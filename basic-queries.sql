SELECT * FROM CheckInCheckOut ORDER BY Checkin;

ALTER TABLE CheckInCheckOut ADD Comment NVARCHAR(256);

ALTER TABLE People ADD IsActive BIT;
SELECT * FROM People
UPDATE People SET IsActive = 1

SELECT * FROM CheckInCheckOut WHERE Checkin is not null AND CheckOUt is null and PhoneNumberID = '2076697480';

DELETE FROM CheckInCheckOut;