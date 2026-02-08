-- BMS Database Schema for Somee
-- Run this script in Somee's SQL Manager

-- Create Members table (no dependencies)
CREATE TABLE Members (
    MemberId INT IDENTITY(1,1) NOT NULL,
    FullName NVARCHAR(100) NOT NULL,
    Address NVARCHAR(250) NOT NULL,
    PhoneNumber BIGINT NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Status NVARCHAR(20) NOT NULL,
    CONSTRAINT PK__Members__0CF04B181D71BA11 PRIMARY KEY (MemberId)
);

-- Create Users table (no dependencies)
CREATE TABLE Users (
    UserId INT IDENTITY(1,1) NOT NULL,
    Username NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    PasswordHash NVARCHAR(MAX) NOT NULL,
    Role NVARCHAR(20) NOT NULL DEFAULT 'User',
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT PK__Users__1788CC4CC9CB7A04 PRIMARY KEY (UserId),
    CONSTRAINT UQ__Users__536C85E4AB82A892 UNIQUE (Username),
    CONSTRAINT UQ__Users__A9D105348F21BED7 UNIQUE (Email)
);

-- Create Accounts table (depends on Members)
CREATE TABLE Accounts (
    AccountId INT IDENTITY(1,1) NOT NULL,
    MemberId INT NOT NULL,
    AccountType NVARCHAR(50) NOT NULL,
    Balance DECIMAL(18, 2) NOT NULL,
    Status NVARCHAR(20) NOT NULL,
    CreatedAt DATETIME NOT NULL,
    CONSTRAINT PK__Accounts__349DA5A6150E3749 PRIMARY KEY (AccountId),
    CONSTRAINT FK_Accounts_Members FOREIGN KEY (MemberId) REFERENCES Members(MemberId)
);

-- Create Loans table (depends on Members)
CREATE TABLE Loans (
    LoanId INT IDENTITY(1,1) NOT NULL,
    MemberId INT NOT NULL,
    LoanType NVARCHAR(50) NOT NULL,
    PrincipalAmount DECIMAL(18, 2) NOT NULL,
    InterestRate DECIMAL(5, 2) NOT NULL,
    StartDate DATETIME NOT NULL,
    DueDate DATETIME NOT NULL,
    Status NVARCHAR(20) NOT NULL,
    CONSTRAINT PK__Loans__4F5AD4575FD823C3 PRIMARY KEY (LoanId),
    CONSTRAINT FK_Loans_Members FOREIGN KEY (MemberId) REFERENCES Members(MemberId)
);

-- Create MemberShare table (depends on Members)
CREATE TABLE MemberShare (
    ShareId BIGINT IDENTITY(1,1) NOT NULL,
    MemberId INT NOT NULL,
    Amount DECIMAL(18, 2) NOT NULL,
    ContributionDate DATETIME NOT NULL,
    ShareType NVARCHAR(50) NULL,
    CONSTRAINT PK__MemberSh__D32A3FEE4B16AEF5 PRIMARY KEY (ShareId),
    CONSTRAINT FK_MemberShare_Members FOREIGN KEY (MemberId) REFERENCES Members(MemberId)
);

-- Create Transactions table (depends on Accounts)
CREATE TABLE Transactions (
    TransactionId INT IDENTITY(1,1) NOT NULL,
    AccountId INT NOT NULL,
    TransactionType NVARCHAR(50) NOT NULL,
    Amount DECIMAL(18, 2) NOT NULL,
    TransactionDate DATETIME NOT NULL DEFAULT GETDATE(),
    BalanceAfter DECIMAL(18, 2) NOT NULL,
    Notes NVARCHAR(250) NULL,
    CONSTRAINT PK__Transact__55433A6BD0F1E398 PRIMARY KEY (TransactionId),
    CONSTRAINT FK_Transactions_Accounts FOREIGN KEY (AccountId) REFERENCES Accounts(AccountId)
);

-- Note: Admin user will be auto-created on first app launch
-- Default credentials: admin / Admin@123

PRINT 'Database schema created successfully!';
