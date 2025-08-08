# storePOS
C# - ASP .net core - sql server management

use these queries for tables in database :
CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    [Password] NVARCHAR(100) NOT NULL,
    [Role] NVARCHAR(20) NOT NULL CHECK ([Role] IN (N'مدیر', N'صندوقدار'))
);


INSERT INTO Users (Username, Password, Role)
VALUES (N'admin', N'1234', N'مدیر');



CREATE TABLE Invoices (
    Id INT PRIMARY KEY IDENTITY(1,1),
    CashierUsername NVARCHAR(50) NOT NULL,
    InvoiceDate DATETIME NOT NULL,
    TotalAmount DECIMAL(18, 2) NOT NULL
);

CREATE TABLE InvoiceItems (
    Id INT PRIMARY KEY IDENTITY(1,1),
    InvoiceId INT NOT NULL FOREIGN KEY REFERENCES Invoices(Id),
    ProductCode NVARCHAR(50),
    ProductName NVARCHAR(100),
    Quantity INT,
    UnitPrice DECIMAL(18, 2),
    TotalPrice AS (Quantity * UnitPrice) PERSISTED
);

CREATE TABLE Products (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Barcode NVARCHAR(50) UNIQUE,
    Category NVARCHAR(100),
    Name NVARCHAR(100),
    Price DECIMAL(18,2)
);



CREATE TABLE PurchaseInvoices (
    Id INT IDENTITY PRIMARY KEY,
    SupplierName NVARCHAR(100),
    PurchaseDate DATETIME,
    RegisteredBy NVARCHAR(100)
);


CREATE TABLE PurchaseItems (
    Id INT IDENTITY PRIMARY KEY,
    InvoiceId INT FOREIGN KEY REFERENCES PurchaseInvoices(Id),
    ProductName NVARCHAR(100),
    Quantity INT,
    UnitPrice DECIMAL(18, 2),
    TotalPrice AS (Quantity * UnitPrice) PERSISTED
);


CREATE TABLE Customers (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),
    PhoneNumber NVARCHAR(20),
    Address NVARCHAR(300),
    Balance DECIMAL(18,2) DEFAULT 0
)

CREATE TABLE Cheques (
    Id INT PRIMARY KEY IDENTITY,
    CustomerId INT FOREIGN KEY REFERENCES Customers(Id),
    Amount DECIMAL(18,2),
    DueDate DATE,
    Type NVARCHAR(20) 
)
