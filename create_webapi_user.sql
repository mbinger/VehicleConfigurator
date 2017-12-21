/*use master*/
CREATE LOGIN webapiuser WITH PASSWORD = /*your password here*/

/*use VC database*/
CREATE ROLE webapi
GRANT SELECT ON dbo.AdditionalEquipmentItems TO webapi
GRANT SELECT ON dbo.Cars TO webapi
GRANT SELECT ON dbo.Colors TO webapi
GRANT SELECT ON dbo.ColorTypeRefs TO webapi
GRANT SELECT ON dbo.Engines TO webapi
GRANT SELECT ON dbo.FuelTypeRefs TO webapi

GRANT INSERT, SELECT, UPDATE, DELETE ON dbo.OrderAdditionalEquipmentItems TO webapi
GRANT INSERT, SELECT, UPDATE, DELETE ON dbo.Orders TO webapi

GRANT SELECT ON dbo.OrderStatusRefs TO webapi
GRANT SELECT ON dbo.Rims TO webapi
GRANT SELECT ON dbo.RimTypeRefs TO webapi

CREATE USER webapiuser FOR LOGIN webapiuser WITH DEFAULT_SCHEMA = dbo
EXEC sp_addrolemember N'webapi', N'webapiuser'