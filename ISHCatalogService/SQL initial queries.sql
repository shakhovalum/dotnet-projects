CREATE DATABASE ISHCatalog;
GO


CREATE TABLE Category (
    CategoryID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    Image NVARCHAR(MAX) NULL,
    ParentCategoryID INT NULL,
    FOREIGN KEY (ParentCategoryID) REFERENCES Category(CategoryID)
);
GO

CREATE TABLE Item (
    ItemID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    Image NVARCHAR(MAX) NULL,
    CategoryID INT NOT NULL,
    Price DECIMAL(19, 4) NOT NULL,
    Amount INT NOT NULL CHECK (Amount > 0),
    FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID)
);
GO

