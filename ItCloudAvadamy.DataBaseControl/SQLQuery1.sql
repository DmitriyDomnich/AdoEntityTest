CREATE DATABASE Library
USE Library

CREATE TABLE Books(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Name NVARCHAR(100),
	Author NVARCHAR(100),
	Publisher NVARCHAR(100),
	Year INT,
	UserID INT FOREIGN KEY (UserID) REFERENCES Users(Id)
);

CREATE TABLE Users(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Name NVARCHAR(100),
	Age INT NOT NULL,
);

INSERT INTO Books (Name, Author, Publisher, Year, UserID)
	VALUES ('Zhenya', 'Domnich', 'Nike', 1993, 1);
INSERT INTO Books (Name, Author, Publisher, Year, UserID)
	VALUES ('Kolya', 'Domnich', 'Bab', 3001, 1);
INSERT INTO Books (Name, Author, Publisher, Year, UserID)
	VALUES ('Ivan', 'Statii', 'Aga', 3008, 3);
INSERT INTO Books (Name, Author, Publisher, Year, UserID)
	VALUES ('Tolok', 'Piqe', 'Bab', 2000, 1);
INSERT INTO Books (Name, Author, Publisher, Year, UserID)
	VALUES ('Tolok', 'Piqe', 'Bab', 2000, NULL);

INSERT INTO Users(Name, Age) 
VALUES ('Tom', 18), ('Jack',98), ('Mike',1), ('Shvars', 71);



SELECT * FROM Books b
JOIN Users u ON b.UserID=u.Id
WHERE u.Id=1;



