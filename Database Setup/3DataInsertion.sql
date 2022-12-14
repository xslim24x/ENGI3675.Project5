

--create stored procedure for adding users
DROP FUNCTION if EXISTS addUser(text, text);

CREATE OR REPLACE FUNCTION 
	addUser(uname text, plaintext text)
RETURNS void AS $$
    BEGIN
	INSERT INTO systemusers(username,hashedpass) 
	VALUES ($1,decode(md5($2),'hex'));
    END;
    $$ LANGUAGE plpgsql;


--create a stored procedure for authenticating
DROP FUNCTION if EXISTS Authenticate(text, bytea);

CREATE OR REPLACE FUNCTION 
	Authenticate(uname text, hash bytea )
RETURNS bool AS $$
    DECLARE
	isgood boolean;
    BEGIN
	SELECT count(*) > 0 into isgood from systemusers
	WHERE (username=$1) and 
	(hashedpass=$2);
	return isgood;
    END;
    $$ LANGUAGE plpgsql;

-- insert data into table
TRUNCATE systemusers RESTART IDENTITY CASCADE;
Select addUser('newbie', '_M\314;Z\247e\326\035\203''\336\270\202\317\231');