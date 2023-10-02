CREATE DATABASE PetClinic;
USE PetClinic;

CREATE TABLE Owner (

    Owner_Id int IDENTITY(1,1) PRIMARY KEY,
    Owner_Username varchar(20) UNIQUE NOT NULL,
    Owner_Password varchar(30) NOT NULL,
	Owner_Name varchar(30) NOT NULL,
	Owner_NIC varchar(20),
    Owner_TP int NOT NULL,
	Owner_Address varchar(50) NOT NULL,
	Owner_Email varchar(50),
	Owner_DOB date
);
INSERT INTO Owner VALUES
('lakshan','Abc12345','Lakshan','200133304085',0776542312,'Colombo','chamikahewage@gmail.com','2001-11-28'),
('sachin101','Abc12345','Sachin','321654987',776546545,'Galle','sachin@gmail.com','1988-11-22'),
('isuru02','Efg12345','Isuru','555555555',776321456,'Horana','isuru@gmail.com','1998-5-5');

CREATE TABLE Pet (
    Pet_Id int IDENTITY(1,1) PRIMARY KEY,
    Pet_Type varchar(20) NOT NULL,
    Pet_Breed varchar(30),
	Pet_Name varchar(30) NOT NULL,
	Pet_DOB date,
	Pet_Gender varchar(10),
    Pet_Bloodtype varchar(15),
	Owner_Id int FOREIGN KEY REFERENCES Owner(Owner_Id)
	);

CREATE TABLE Vet (
    Vet_Id int IDENTITY(1,1) PRIMARY KEY,
	Vet_Specialization varchar(30) NOT NULL,
    Vet_Name varchar(30) NOT NULL,
    Vet_Gender varchar(10),
	Vet_Tp int,
	Vet_Email varchar(30),
	Vet_Availability date NOT NULL,
	Vet_Clinic varchar(30) NOT NULL,
	
	);
CREATE TABLE Health (
    Health_Id int IDENTITY(1,1) PRIMARY KEY,
	Health_Category varchar(30) NOT NULL,
    Treatement_Name varchar(30) NOT NULL,
    Treatement_Dosage varchar(30),
	Last_Visit date,
	Next_Visit date,
	Pet_Id int FOREIGN KEY REFERENCES Pet(Pet_Id),
	Vet_Id int FOREIGN KEY REFERENCES Vet(Vet_Id)
	);


INSERT INTO Vet VALUES
('Vaccine','John Sena','Male',776456745,'johnc@gmail.com','2022-12-22','National Pet Clinic'),
('Vaccine','Angelina Jooliye','Female',776546545,'angie@gmail.com','2022-11-22','Dehiwala Pet Clinic'),
('Surgery','Jason Sithum','Male',776321456,'json@gmail.com','2022-11-6','National Pet Clinic'),
('Nutrition','Pita Parker','Male',776321456,'pita@gmail.com','2022-10-20','Horana Pet Clinic');

/*
INSERT INTO Health VALUES
('Vaccine','Rabies','10ml','2020-2-2','2022-12-28','11','2'),
('Immunization','Malaria','15ml','2012-3-3','2022-11-22','12','3'),
('Surgery','ChickenPox','6ml','2010-4-4','2022-12-5','13','1'),
('Nutrition','Worms','50ml','2022-5-5','2022-11-21','13','2');

DROP TABLE Health;
DROP TABLE Vet;
SELECT * FROM Health;

SELECT Owner_Name, Pet_Name,Health_Id,Health_Category,Treatement_Name,Treatement_Dosage,Last_Visit,Next_Visit FROM Health,Pet,Owner 
WHERE Health.Pet_Id=Pet.Pet_Id 
AND Pet.Owner_Id=Owner.Owner_Id
AND Owner_Name='Sachin'


SELECT * FROM Pet;
SELECT * FROM Vet;
DELETE from Vet

SELECT Pet.Pet_Name, Pet.Pet_Type,
Health.Health_Category,
Health.Treatement_Name, 
Health.Treatement_Dosage,
Health.Last_Visit, 
Health.Next_Visit
FROM Pet, Health
WHERE Pet.Pet_Id = Health.Pet_Id 
AND Pet.Pet_Type='Cat';

SELECT Pet.Pet_Type, Pet.Pet_Name,
Health.Health_Category,
Health.Treatement_Name,
Health.Treatement_Dosage,
Health.Last_Visit,
Health.Next_Visit 
FROM Pet, Health 
WHERE Pet.Pet_Id = Health.Pet_Id 
AND Pet.Owner_Id='2'
                    
SELECT * FROM Pet WHERE Pet.Owner_Id='5'

UPDATE Pet SET  Pet_Type= '" + cmb_type.SelectedItem + "', 
Pet_Breed = '" + txt_breed.Text + "', 
Pet_Name = '" + txt_name.Text + "',
Pet_DOB=  '" + dob_picker.Value + "',
Pet_Gender= '" + gender + "',
Pet_Bloodtype= '" + txt_bloodtype.Text + "'
WHERE Pet_Id =  cmb_id.SelectedItem ;


CREATE TABLE Admin (

    Admin_Id int IDENTITY(1,1) PRIMARY KEY,
    Admin_Username varchar(20) UNIQUE NOT NULL,
    Admin_Password varchar(30) NOT NULL,
	);

	SELECT * FROM Admin

	SELECT Pet_Id,Owner_Username,Owner_Password, Owner_Name,Owner_NIC,Owner_TP,Owner_Address,Owner_Email,Owner_DOB 
	 FROM Owner,Pet WHERE Pet.Owner_Id = Owner.Owner_Id AND Pet.Pet_Id=1;
	 
	 SELECT Pet_Id,Owner_Id,Owner_Username,Owner_Password, Owner_Name,Owner_NIC,Owner_TP,Owner_Address,Owner_Email,Owner_DOB 
	FROM Owner
	LEFT JOIN Pet
	ON Owner.Owner_Id = Pet.Owner_Id
	AND Pet.Pet_Id=1;
	

	Pet_Id,Owner_Username,Owner_Password, Owner_Name,Owner_NIC,Owner_TP,Owner_Address,Owner_Email,Owner_DOB 
	
	Owner_Id,
	AND Pet.Pet_Id=1;

SELECT Owner_Id,Pet_Type,Pet_Id,Pet_Breed,Pet_Name,Pet_DOB,Pet_Gender,Pet_Bloodtype FROM Pet WHERE Owner_Id=5
SELECT * FROM Pet WHERE Pet_Id =1
SELECT Owner_Id FROM Owner

UPDATE Owner SET Owner_Password =s'fs' Owner_Name = '" + txt_name.Text + "', Owner_NIC = '" + txt_nic.Text + "'Owner_TP = '" + txt_tp.Text + "'Owner_Address = '" + txt_address.Text + "'Owner_Email = '" + txt_email.Text + "'Owner_DOB=  '" + dob_picker.Value + "'WHERE Owner_Id = '" + Convert.ToInt32(this.cmb_id.GetItemText(this.cmb_id.SelectedItem)) + "'", con);
        
SELECT Next_Visit from HEALTH
WHERE 
    group by Next_Visit

SELECT COUNT(*) FROM Health,Pet WHERE Pet.Pet_Id=Health.Pet_Id AND Pet.Owner_Id=5

*/