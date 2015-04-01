-- Delete data tables so they can be created without conflicts
drop table if exists systemusers cascade;

-- Create new table with constraints
create table systemusers 
(
	Id serial primary key,
	username text not null check(length(username)>0),
	hashedpass bytea not null check(length(hashedpass)>0)
);

-- Add appropriate comments to tables and columns

comment on table systemusers is 'System Users and Passwords';
comment on column systemusers.Id is 'The user Id.';
comment on column systemusers.username is 'the username of system users';
comment on column systemusers .hashedpass is 'the password of system users';
