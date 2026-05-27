PRAGMA foreign_keys = ON;

CREATE TABLE Patients (
    Pesel     TEXT NOT NULL,
    FirstName TEXT NOT NULL,
    LastName  TEXT NOT NULL,
    Age       INTEGER NOT NULL,
    Sex       INTEGER NOT NULL,
    CONSTRAINT Patients_pk PRIMARY KEY (Pesel)
);

CREATE TABLE Wards (
    Id          INTEGER NOT NULL,
    Name        TEXT NOT NULL,
    Description TEXT NOT NULL,
    CONSTRAINT Wards_pk PRIMARY KEY (Id AUTOINCREMENT)
);

CREATE TABLE BedTypes (
    Id          INTEGER NOT NULL,
    Name        TEXT NOT NULL,
    Description TEXT NOT NULL,
    CONSTRAINT BedTypes_pk PRIMARY KEY (Id AUTOINCREMENT)
);

CREATE TABLE Rooms (
    Id     TEXT NOT NULL,
    WardId INTEGER NOT NULL,
    HasTv  INTEGER NOT NULL,
    CONSTRAINT Rooms_pk PRIMARY KEY (Id),
    CONSTRAINT Room_Ward FOREIGN KEY (WardId) REFERENCES Wards (Id)
);

CREATE TABLE Beds (
    Id        INTEGER NOT NULL,
    RoomId    TEXT NOT NULL,
    BedTypeId INTEGER NOT NULL,
    CONSTRAINT Beds_pk PRIMARY KEY (Id),
    CONSTRAINT Beds_BedTypes FOREIGN KEY (BedTypeId) REFERENCES BedTypes (Id),
    CONSTRAINT Beds_Rooms FOREIGN KEY (RoomId) REFERENCES Rooms (Id)
);

CREATE TABLE Admissions (
    Id            INTEGER NOT NULL,
    AdmissionDate datetime NOT NULL,
    DischargeDate datetime NULL,
    PatientPesel  TEXT NOT NULL,
    WardId        INTEGER NOT NULL,
    CONSTRAINT Admissions_pk PRIMARY KEY (Id AUTOINCREMENT),
    CONSTRAINT Admissions_Patients FOREIGN KEY (PatientPesel) REFERENCES Patients (Pesel),
    CONSTRAINT Admissions_Wards FOREIGN KEY (WardId) REFERENCES Wards (Id)
);

CREATE TABLE BedAssignments (
    Id           INTEGER NOT NULL,
    PatientPesel TEXT NOT NULL,
    BedId        INTEGER NOT NULL,
    "From"       datetime NOT NULL,
    "To"         datetime NULL,
    CONSTRAINT BedAssignments_pk PRIMARY KEY (Id AUTOINCREMENT),
    CONSTRAINT BedAssignments_Beds FOREIGN KEY (BedId) REFERENCES Beds (Id),
    CONSTRAINT BedAssignments_Patients FOREIGN KEY (PatientPesel) REFERENCES Patients (Pesel)
);

-- ============================================
-- DML - przykładowe dane
-- ============================================

INSERT INTO Wards (Name, Description) VALUES
('Kardiologia', 'Oddział chorób serca i układu krążenia'),
('Chirurgia',   'Oddział chirurgii ogólnej'),
('Ortopedia',   'Oddział leczenia urazów i schorzeń kości'),
('Pediatria',   'Oddział dziecięcy'),
('Neurologia',  'Oddział chorób układu nerwowego');

INSERT INTO BedTypes (Name, Description) VALUES
('Standard',           'Łóżko standardowe'),
('Intensywna terapia', 'Łóżko OIOM'),
('Rehabilitacyjne',    'Łóżko rehabilitacyjne'),
('Dziecięce',          'Łóżko pediatryczne'),
('Elektryczne',        'Łóżko sterowane elektrycznie');

INSERT INTO Patients (Pesel, FirstName, LastName, Age, Sex) VALUES
('90010112345', 'Jan',     'Kowalski',    35, 1),
('85050567890', 'Anna',    'Nowak',       40, 0),
('72031245678', 'Piotr',   'Wiśniewski',  53, 1),
('04122098765', 'Zuzanna', 'Kaczmarek',   20, 0),
('68111122233', 'Marek',   'Lewandowski', 57, 1);

INSERT INTO Rooms (Id, WardId, HasTv) VALUES
('A101', 1, 1),
('B201', 2, 1),
('C301', 3, 0),
('D401', 4, 1),
('E501', 5, 0);

INSERT INTO Beds (Id, RoomId, BedTypeId) VALUES
(1, 'A101', 1),
(2, 'B201', 2),
(3, 'C301', 3),
(4, 'D401', 4),
(5, 'E501', 5);

INSERT INTO Admissions (AdmissionDate, DischargeDate, PatientPesel, WardId) VALUES
('2026-05-01 10:00:00', '2026-05-05 14:00:00', '90010112345', 1),
('2026-05-03 09:30:00', NULL,                  '85050567890', 2),
('2026-05-06 12:15:00', '2026-05-10 11:00:00', '72031245678', 3),
('2026-05-08 08:45:00', NULL,                  '04122098765', 4),
('2026-05-09 16:20:00', NULL,                  '68111122233', 5);

INSERT INTO BedAssignments (PatientPesel, BedId, "From", "To") VALUES
('90010112345', 1, '2026-05-01 10:30:00', '2026-05-05 13:00:00'),
('85050567890', 2, '2026-05-03 10:00:00', NULL),
('72031245678', 3, '2026-05-06 12:30:00', '2026-05-10 10:30:00'),
('04122098765', 4, '2026-05-08 09:00:00', NULL),
('68111122233', 5, '2026-05-09 17:00:00', NULL);
