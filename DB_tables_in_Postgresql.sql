CREATE TABLE Countries (
    	Id SERIAL PRIMARY KEY,
	Name TEXT UNIQUE
	);
	
CREATE TABLE User_Roles (
    	Id SERIAL PRIMARY KEY,
	Role_Name TEXT UNIQUE
	);

CREATE TABLE Users (
    	Id BIGSERIAL PRIMARY KEY,
	Username TEXT UNIQUE,
	Password  TEXT,
	Email  BIGINT UNIQUE,
	User_Role INT references User_Roles(Id)
	);
	
CREATE TABLE Airline_Companies(
	Id BIGSERIAL PRIMARY KEY,
	Name TEXT UNIQUE ,
	CountryId INT references Countries(Id),
	User_Id BIGINT UNIQUE references User_Roles(Id)
	);

CREATE TABLE Administrators (
    	Id SERIAL PRIMARY KEY,
	First_Name TEXT,
	Last_Name TEXT,
	Level int,
	User_id BIGINT UNIQUE references User_Roles(Id)
	);
	
CREATE TABLE Customers(
    	Id BIGSERIAL PRIMARY KEY,
	First_Name TEXT,
	Last_Name TEXT,
	Address TEXT,
	Phone_No TEXT UNIQUE,
	Credit_Card_No TEXT UNIQUE,
	User_Id BIGINT UNIQUE references User_Roles(Id)
	);
	
CREATE TABLE Flights(
    	Id BIGSERIAL PRIMARY KEY,
	Airline_Company_Id BIGINT references Airline_Companies(Id),
	Origin_Country_Id INT references Countries(Id),
	Destination_Country_Id INT references Countries(Id),
	Departure_Time TIMESTAMPTZ,
	Landing_Time TIMESTAMPTZ,
	Tickets_Remaining INT
	);
	
CREATE TABLE Tickets(
    	Id BIGSERIAL PRIMARY KEY,
	Id_Flight BIGINT UNIQUE references Flights(Id),
	Id_Customer BIGINT UNIQUE references Customers(Id)
	)
