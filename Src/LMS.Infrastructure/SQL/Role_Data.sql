INSERT INTO Role("RoleName", "Description", "IsEnabled", "CreatedOn", "CreatedBy") 
VALUES ('Admin', 'Adminstrator having all the permissions', 1, '2024-05-20','Admin')

INSERT INTO Role("RoleName", "Description", "IsEnabled", "CreatedOn", "CreatedBy") 
VALUES('User', 'User having restricted access permissions', 1, '2024-05-20','Admin')

Select * from Role
	