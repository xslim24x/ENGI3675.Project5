--Use SQL Shell for the following script:

--Initialize starting conditions by deleting database
drop database if exists "Assignment5";

--Initilize User
drop role if exists "Assignment5";
create role "Assignment5" login;
comment on role "Assignment5" is 'Resctricted ISS app pool user';

--Creating Database and granting permissions
create database "Assignment5";
comment on database "Assignment5" is 'Database for Assigment3';

grant connect on database "Assignment5" to "Assignment5";
