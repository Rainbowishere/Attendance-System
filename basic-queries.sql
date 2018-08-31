SELECT * FROM CheckInCheckOut ORDER BY Checkin DESC;

ALTER TABLE CheckInCheckOut ADD Comment NVARCHAR(256);

ALTER TABLE People ADD IsActive BIT;
SELECT * FROM People
UPDATE People SET IsActive = 1

SELECT * FROM CheckInCheckOut WHERE Checkin is not null AND CheckOUt is null and PhoneNumberID = '2076697480';

DELETE FROM CheckInCheckOut;

SELECT * FROM People;

SELECT dbo.CheckinCheckout.PhoneNumberID, dbo.People.FullName, dbo.People.Source, dbo.People.EmployeeID, dbo.CheckinCheckout.Checkin, dbo.CheckinCheckout.Checkout, dbo.CheckinCheckout.Purpose, dbo.CheckinCheckout.Device, dbo.CheckinCheckout.Comment
FROM dbo.CheckinCheckout INNER JOIN dbo.People ON dbo.CheckinCheckout.PhoneNumberID = dbo.People.PhoneNumberID ORDER BY Checkin DESC;