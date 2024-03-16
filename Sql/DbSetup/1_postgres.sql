
CREATE DATABASE "VolunteerTracker"
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'English_United States.1252'
    LC_CTYPE = 'English_United States.1252'
    LOCALE_PROVIDER = 'libc'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;

GRANT ALL ON DATABASE "VolunteerTracker" TO postgres;


CREATE TABLE IF NOT EXISTS public."__EFMigrationsHistory"
(
    "MigrationId" character varying(150) COLLATE pg_catalog."default" NOT NULL,
    "ProductVersion" character varying(32) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

ALTER TABLE IF EXISTS public."__EFMigrationsHistory"
    OWNER to postgres;
	
CREATE ROLE efmigrations WITH
	LOGIN
	NOSUPERUSER
	NOCREATEDB
	NOCREATEROLE
	INHERIT
	NOREPLICATION
	CONNECTION LIMIT -1
	PASSWORD --insert password'';
		
CREATE ROLE aspnet_volunteertracker WITH
	LOGIN
	NOSUPERUSER
	NOCREATEDB
	NOCREATEROLE
	INHERIT
	NOREPLICATION
	CONNECTION LIMIT -1
	PASSWORD --insert password'';
	
	CREATE ROLE data_access WITH
	LOGIN
	NOSUPERUSER
	NOCREATEDB
	NOCREATEROLE
	INHERIT
	NOREPLICATION
	CONNECTION LIMIT -1
	PASSWORD --insert password'';

GRANT pg_read_all_data, pg_write_all_data TO data_access;
	

GRANT DELETE, INSERT, SELECT, UPDATE ON TABLE public."__EFMigrationsHistory" TO efmigrations;

GRANT CREATE, CONNECT ON DATABASE "VolunteerTracker" TO efmigrations;
GRANT ALL PRIVILEGES ON SCHEMA public TO efmigrations;

GRANT DELETE, INSERT, SELECT, UPDATE ON TABLE public."__EFMigrationsHistory" TO "aspnet_volunteertracker";

GRANT CREATE, CONNECT ON DATABASE "aspnet-VolunteerTracker" TO "aspnet_volunteertracker";
GRANT ALL PRIVILEGES ON SCHEMA public TO "aspnet_volunteertracker";