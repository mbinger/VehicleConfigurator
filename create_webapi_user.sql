/*use master*/
CREATE LOGIN webapiuser WITH PASSWORD = /*your password here*/

/*create the user as db_owner to be able to set-up the database schema*/

CREATE USER webapiuser FOR LOGIN webapiuser WITH DEFAULT_SCHEMA = dbo
EXEC sp_addrolemember N'db_owner', N'webapiuser'
GRANT CONNECT TO webapiuser

/*todo: now run the service and let hit create the database*/

EXEC sp_droprolemember N'db_owner', N'webapiuser'

/*use VC database*/
CREATE ROLE webapi

/*allow select all*/
GRANT SELECT ON SCHEMA::dbo TO webapi

/*allow modify order and its equipment items*/
GRANT INSERT, UPDATE, DELETE ON dbo.OrderAdditionalEquipmentItems TO webapi
GRANT INSERT, UPDATE, DELETE ON dbo.Orders TO webapi

EXEC sp_addrolemember N'webapi', N'webapiuser'
