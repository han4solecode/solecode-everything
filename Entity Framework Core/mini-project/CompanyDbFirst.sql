CREATE TABLE Employees (
	empNo SERIAL NOT NULL PRIMARY KEY,
	fName VARCHAR(255) NOT NULL,
	lName VARCHAR(255) NOT NULL,
	address VARCHAR(255) NOT NULL,
	DOB DATE NOT NULL,
	sex VARCHAR(255) NOT NULL,
	position VARCHAR(255) NOT NULL,
	deptNo INT
	-- CONSTRAINT fk_employees_departments_deptNo
	-- 	FOREIGN KEY (deptNo)
	-- 	REFERENCES Departments(deptNo)
	-- 	ON DELETE SET NULL
);

CREATE TABLE Departments (
	deptNo SERIAL NOT NULL PRIMARY KEY,
	deptName VARCHAR(255) NOT NULL,
	mgrEmpNo INT UNIQUE
	-- CONSTRAINT fk_departments_employees_mgrEmpNo
	-- 	FOREIGN KEY (mgrEmpNo)
	-- 	REFERENCES Employees(empNo)
	-- 	ON DELETE SET NULL
);

CREATE TABLE Projects (
	projNo SERIAL NOT NULL PRIMARY KEY,
	projName VARCHAR(255) NOT NULL,
	deptNo INT
	-- CONSTRAINT fk_projects_departments_deptNo
	-- 	FOREIGN KEY (deptNo)
	-- 	REFERENCES Departments(deptNo)
	-- 	ON DELETE SET NULL
);

CREATE TABLE WorksOns (
	empNo INT,
	projNo INT,
	dateWorked date DEFAULT CURRENT_DATE,
	hoursWorked INT DEFAULT 0,
	PRIMARY KEY (empNo, projNo)
	-- CONSTRAINT fk_worksons_employee_empNo
	-- 	FOREIGN KEY (empNo)
	-- 	REFERENCES Employees(empNo)
	-- 	ON DELETE SET NULL,
	-- CONSTRAINT fk_worksons_projects_projNo
	--  FOREIGN KEY (projNo)
	-- 	REFERENCES Projects(projNo)
	-- 	ON DELETE SET NULL
);

-- Add FKs
-- Employees table
ALTER TABLE Employees
	ADD CONSTRAINT fk_employees_departments_deptNo
	FOREIGN KEY (deptNo)
	REFERENCES Departments(deptNo)
	ON DELETE SET NULL;

-- Departments table
ALTER TABLE Departments
	ADD CONSTRAINT fk_departments_employees_mgrEmpNo
	FOREIGN KEY (mgrEmpNo)
	REFERENCES Employees(empNo)
	ON DELETE SET NULL;

-- Projects table
ALTER TABLE Projects
	ADD CONSTRAINT fk_projects_departments_deptNo
	FOREIGN KEY (deptNo)
	REFERENCES Departments(deptNo)
	ON DELETE SET NULL;

-- WorksOns table
ALTER TABLE WorksOns
	ADD CONSTRAINT fk_worksons_employee_empNo
	FOREIGN KEY (empNo)
	REFERENCES Employees(empNo)
	ON DELETE SET NULL;

ALTER TABLE WorksOns
	ADD CONSTRAINT fk_worksons_projects_projNo
	FOREIGN KEY (projNo)
	REFERENCES Projects(projNo)
	ON DELETE SET NULL;

